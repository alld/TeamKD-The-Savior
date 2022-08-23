using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAI : MonoBehaviour
{
    //회피 패턴, 도주 패턴, 자리 재선정
    //피격시 판단, 공격대상 재설정 (싸우고있는 대상, 아군이 피격됨)

    #region 캐시처리

    private CapsuleCollider unit_collider;
    private UnitStateData unit;
    private UnitMelee unitMelee;
    private CharacterController unitControl;
    private Ray ray;
    private RaycastHit hit;
    private Animator animator;
    private string ani_Attack = "Attack";
    private string ani_Walk = "Walk";
    private string ani_Skill = "Skill";
    private string ani_Ultimate = "Ultimate";
    private string ani_Death = "Death";
    private string ani_Stun = "Stun";
    #endregion


    #region AI 계산값
    public Transform targetPoint;
    /// <summary>
    /// 주요 타겟 
    /// </summary>
    [SerializeField]
    public UnitStateData targetObj;

    private float targetDistance;
    /// <summary>
    /// 공격한 대상이 스케쥴이 공격이면서, 공격대상이 자신일경우 
    /// 해당 대상에서 스케쥴이 변동될때 컴포넌트로 접근하여 대상을 제외시킴.
    /// </summary>
    private List<int> attackers = new List<int>(); // 공격하고 있는 유닛들 
    /// <summary>
    /// 적 한정으로 개별적으로 매겨지는 동적 수치 (주기적으로 감소 피격시 증가(증가폭 전력기준))
    /// </summary>
    private List<int> threatUnit; //상대 위협수치
    /// <summary>
    /// 트리거 범위에 포함된 범위 근접을 제외하면 원거리 사거리와 동일
    /// </summary>
    private List<int> perceptionAllyUnit = new List<int>(); // 인식 대상 유닛
    private List<int> perceptionEnemyUnit = new List<int>(); // 인식 적 유닛
    private List<UnitStateData> AllyUnit;
    private List<UnitStateData> EnemyUnit;

    Vector3 Move_Yzero = new Vector3();
    Vector3 Movetemp = new Vector3();

    #endregion

    #region AI 성향 변수

    #endregion

    //enum UnitState { COMMON, ATTACK, ATTACK_MOVE, MOVE, SKILL, DIE, SPEIAL_SKILL }
    //UnitState unitState;
    private WaitForSeconds delay_001 = new WaitForSeconds(0.01f);
    private WaitForSeconds delay_03 = new WaitForSeconds(0.3f);
    private WaitForSeconds delay_05 = new WaitForSeconds(0.5f);
    private WaitForSeconds delay_10 = new WaitForSeconds(1.0f);
    public delegate AIPattern dele_AI();
    public dele_AI dele_attacked;
    /// <summary>
    /// 기능 : 인공지능 유닛을 구분하기위한 넘버, 해당 컴포넌트들을 이 값을 기준으로 기준넘버가 변경됨
    /// <br></br>방법 : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    public int unitNumber;
    public int partyNumber;
    public bool isplayer;

    public List<AIPattern> aiSchedule = new List<AIPattern>();
    /// <summary>
    /// <br>스케쥴에 입력되는 행동 패턴</br>
    /// <br><paramref name="Attacking"/> :: 공격하기</br>
    /// <br><paramref name="Avoidng"/> :: 회피하기</br>
    /// <br><paramref name="RuuningAway"/> :: 도망가기</br>
    /// <br><paramref name="Follow"/> :: 따라가기</br>
    /// <br><paramref name="Push"/> :: 밀치기</br>
    /// <br><paramref name="Stern"/> :: 스턴</br>
    /// <br><paramref name="Skill"/> :: 일반 스킬</br>
    /// <br><paramref name="SpecialSkill"/> :: 궁극기</br>
    /// <br><paramref name="Moving"/> :: 이동하기</br>
    /// <br><paramref name="Provocation"/> :: 도발</br>
    /// <br><paramref name="Death"/> :: 죽음</br>
    /// <br><paramref name="Stand"/> :: 대기</br>
    /// </summary>
    public enum AIPattern { Stand, Attacking, Avoiding, RuuningAway, Follow, Push, Stern, Skill, SpecialSkill, Moving, Provocation, Death, Pass }
    public AIPattern aiPattern;
    public enum UnitState { Attack, Move, AttackMove, Skill, SpeialSkill, Die, Stand }
    public UnitState unitState;
    private bool isOnScheduler;
    private bool isOnGoing;
    private bool isGaze;
    private bool isRemove;
    private bool isMoving;
    private bool onAttackAvailable = false;
    private bool onSkillAvailable = false;
    private bool onSpecialSkillAvailable = false;

    // 상점에서도 같은 오브젝트를 사용하기때문에, Start사용시 예외처리 필수
    public void Start()
    {
        if (GameManager.instance.dungeonOS != null)
        {
            unitMelee = GetComponent<UnitMelee>();
            unitControl = GetComponent<CharacterController>();
            unit_collider = gameObject.AddComponent<CapsuleCollider>();
            unit = GetComponent<UnitStateData>();
            animator = GetComponentInChildren<Animator>();
            targetPoint = Instantiate(Resources.Load<GameObject>("Unit/MovePoint")).transform;
            targetPoint.position = transform.position;
            if (unit.playerUnit)
            {
                AllyUnit = DungeonOS.instance.partyUnit;
                EnemyUnit = DungeonOS.instance.monsterGroup;
            }
            else
            {
                AllyUnit = DungeonOS.instance.monsterGroup;
                EnemyUnit = DungeonOS.instance.partyUnit;
            }
            OnStartAI();
        }
    }

    public void AIDeleSetting()
    {
        DungeonOS.instance.dele_RoundStart += OnStartAI;
        DungeonOS.instance.dele_RoundEnd += OnEndAI;
    }

    private void AIDeleDestroy()
    {
        DungeonOS.instance.dele_RoundStart -= OnStartAI;
        DungeonOS.instance.dele_RoundEnd -= OnEndAI;
        dele_attacked = null;
    }


    private void OnDestroy()
    {
        AIDeleDestroy();
    }


    private void ResetAISetting()
    {
        unit_collider.radius = unit.Add_priRange;
        onAttackAvailable = true;
        onSkillAvailable = true;
        onSpecialSkillAvailable = true;
        aiSchedule.Clear();
    }


    public void OnStartAI()
    {
        StartCoroutine(delay_StartAI());
    }

    IEnumerator delay_StartAI()
    {
        yield return delay_10;
        AIDeleSetting();
        ResetAISetting();
        AutoScheduler(3, 0);
    }

    public void OnEndAI()
    {
        AIDeleDestroy();
        isOnScheduler = false;
        ResetAISetting();
    }

    #region 스케쥴러

    /// <summary>
    /// <br><paramref name="PatternNumber"/> :: </br>
    /// <br> 스케쥴 명령을 내림</br>
    /// </summary>
    /// <param name="PatternNumber">
    /// <br> - 0 : 스케쥴러 진행</br>
    /// <br> - 1 : 행동 추가</br>
    /// <br> - 2 : 끼어 들기</br>
    /// <br> - 3 : 행동 탐색</br>
    /// <br> - 4 : 현재 행동 제거</br></param>
    /// <returns></returns>
    public bool AutoScheduler(int PatternNumber, AIPattern Pattern)
    {
        switch (PatternNumber)
        {
            case 0: // 스케쥴리 진행
                isRemove = false;
                if (aiSchedule.Count == 0)
                {
                    return AutoScheduler(3, 0);
                }
                else if (!isOnScheduler)
                {
                    switch (aiSchedule[0])
                    {
                        case AIPattern.Stand:
                            StartCoroutine(State_Stand());
                            break;
                        case AIPattern.Attacking:
                            StartCoroutine(State_Attacking());
                            break;
                        case AIPattern.Avoiding:
                            StartCoroutine(State_Avoiding());
                            break;
                        case AIPattern.RuuningAway:
                            StartCoroutine(State_RuuningAway());
                            break;
                        case AIPattern.Follow:
                            StartCoroutine(State_Follow(5.0f, Vector3.zero));
                            break;
                        case AIPattern.Push:
                            break;
                        case AIPattern.Stern:
                            StartCoroutine(State_Stern(5.0f));
                            break;
                        case AIPattern.Skill:
                            break;
                        case AIPattern.SpecialSkill:
                            break;
                        case AIPattern.Moving:
                            StartCoroutine(State_Moving(Vector3.zero));
                            break;
                        case AIPattern.Provocation:
                            break;
                        case AIPattern.Death:
                            StartCoroutine(State_Death());
                            break;
                        default:
                            break;
                    }
                    return true;
                }
                else return false;

            case 1: // 행동 추가
                aiSchedule.Add(Pattern);
                if (!isOnScheduler) AutoScheduler(0, AIPattern.Pass);
                return true;

            case 2: // 끼어 들기
                if (aiSchedule.Count == 0) return AutoScheduler(1, Pattern);
                aiSchedule.Insert(1, Pattern);

                if (isOnGoing) Queueing();
                else AutoScheduler(4, 0);
                return true;

            case 3: // 행동 탐색
                aiSchedule.Add(ThinkOverPattern());
                return AutoScheduler(0, AIPattern.Pass);

            case 4: // 행동 제거
                isRemove = true;
                if (!isOnScheduler)
                {
                    aiSchedule.RemoveAt(0);
                    return AutoScheduler(0, AIPattern.Pass);
                }
                break;
        }
        return false;
    }
    /// <summary>
    /// 스킬 딜레이 상태에서 스케쥴 진행시, 딜레이 끝날때까지 대기
    /// </summary>
    /// <returns></returns>
    IEnumerator Queueing()
    {
        while (true)
        {
            if (!isOnGoing)
            {
                AutoScheduler(4, 0);
                break;
            }
            yield return delay_05;
        }
    }
    /// <summary>
    /// 각 행동에 종료 조건이 적용후, 행동이 끝날때까지 대기시킴
    /// </summary>
    /// <returns></returns>
    IEnumerator QueueingRemove()
    {
        while (true)
        {
            if (!isOnScheduler)
            {
                AutoScheduler(4, 0);
                break;
            }
            yield return delay_05;
        }
    }

    /// <summary>
    /// 0.5초 스킬 딜레이 
    /// </summary>
    /// <returns></returns>
    IEnumerator IsOnGoing()
    {
        isOnGoing = true;
        yield return delay_05;
        isOnGoing = false;
    }

    /// <summary>
    /// AI 패턴 자동 탐색 및 추가
    /// </summary>
    /// <returns></returns>
    public AIPattern ThinkOverPattern()
    {
        if (EnemyUnit.Count == 0)
        {
            return AIPattern.Stand;
        }
        else
        {
            if (perceptionEnemyUnit.Count > 0)
            {
                return AIPattern.Attacking;
            }
            else return AIPattern.Attacking;
        }


        /* 주변 공격 가능 인원수
         * 
         * 즉시 공격 가능인원
         * 
         * 자신을 공격하고 있는 체크
         * 
         * 인식범위의 아군이 공격받는지 체크
         * 
         * 공격 시기  - 스킬 쿨다운 되있다면 스킬 / 없다면 일반스킬 
         * 
         * 체력이 너무 낮으면 도주 판정 (확률) 기준 수치 / 적이 너무많을 경우
         * 
         * 적의 공격을 너무 많이 받거나, 원거리 성향일수록 회피, 주변 근거리 아군이있을경우(거리벌리기)
         * 
         * 일반적 적 탐지해서, 사거리 안에있을때 적을 공격함 
         * 
         * 우선 공격 대상(우선도 / 거리 비례 / 가중치 / 자신 피격중)
         * 
         * 공격 변경시기 검토 (주기적 공격 우선도 검색) // 스케쥴러 매번 검색 검토
         * 
         * 적이 없을 경우 대기 판정 
         * 
         * 주변 아군이 다굴 당할시 보호 (피격시 주변 아군의 델리게이트 호출)
         * 
         * 추후 잔여 체력 / 데미지 / 공격속도 비교 평균 전력 산출 전력 비교
         * 
         * 목표가 정해지면 해당 기준에 맞쳐서 이동이 정해짐
         * 
         * 가능한 이동방향으로 이동하며 중간에 가로막는 적을 우선공격함(레이캐스트)
         * 
         */



        //return AIPattern.Death;
    }
    #endregion

    #region 패턴

    IEnumerator State_Stand() // 대기
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        animator.SetBool(ani_Attack, false);
        animator.SetBool(ani_Walk, false);
        animator.SetBool(ani_Skill, false);
        Action_Stand();
        //애니메이션 작동
        while (!isRemove)
        {
            // 추가 조건으로 빠져나올것 :: 스턴 상태 종료, 특수상태 종료 같은상황
            // 적이 다가옴 
            yield return delay_05;
        }

        isOnScheduler = false;
        AutoScheduler(0, AIPattern.Pass);
    }

    IEnumerator State_Attacking() // 공격
    {
        
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());

        while (isOnScheduler)
        {
            AttackTargetSearch();
            if (targetObj == null)
            {
                MovePointSearch();
                Action_AttackMove();
            }
            else break;

            yield return delay_03;
        }
        isMoving = false;

        //애니메이션 작동
        while (!isRemove)
        {
            if (onAttackAvailable)
            {
                animator.SetTrigger(ani_Attack);
                Action_Attack();
            }
            if (!targetObj.isLive) { isRemove = true; }
            yield return delay_05;
        }
        //animator.SetBool(ani_Attack, true);
        isMoving = false;
        isOnScheduler = false;
        aiSchedule.RemoveAt(0);
        AutoScheduler(0, AIPattern.Pass);
    }

    IEnumerator State_Avoiding() // 회피
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());

        while (isOnScheduler)
        {
            AvoidPointSearch();
            if (Vector3.Distance(targetPoint.position, transform.position) < 1)
            {
                Action_Move();
            }
            else break;

            yield return delay_10;
        }
        isMoving = false;
        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(1, AIPattern.Attacking);
    }

    IEnumerator State_RuuningAway() // 도주 
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());

        while (isOnScheduler)
        {
            AvoidPointSearch();
            if (Vector3.Distance(targetPoint.position, transform.position) < 0.1)
            {
                Action_Move();
            }
            else break;

            yield return delay_10;
        }
        isMoving = false;
        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(1, AIPattern.Avoiding);
    }

    IEnumerator State_Follow(float time, Vector3 Target) // 따라가기 
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        time *= 2;
        while (isOnScheduler)
        {
            FollowTargetSearch(Target);
            if (--time < 0) break;
            if (Vector3.Distance(targetPoint.position, transform.position) < 1)
            {
                Action_Move();
            }
            else break;

            yield return delay_05;
        }
        isMoving = false;
        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(1, AIPattern.Avoiding);
    }

    IEnumerator State_Moving(Vector3 Target) // 이동하기 
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        while (isOnScheduler)
        {
            if (Vector3.zero == Target) MovePointSearch();
            else targetPoint.position = Target;
            if (Vector3.Distance(targetPoint.position, transform.position) < 1)
            {
                Action_Move();
            }
            else break;

            yield return delay_05;
        }
        isMoving = false;
        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(1, AIPattern.Avoiding);
    }

    IEnumerator State_Stern(float time) // 스턴
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        Action_Stand();
        time *= 2;

        while (isOnScheduler)
        {
            if (--time < 0) break;
            yield return delay_05;
        }
        //애니메이션 작동
        while (!isRemove)
        {
            // 추가 조건으로 빠져나올것 :: 스턴 상태 종료, 특수상태 종료 같은상황
            // 적이 다가옴 
            yield return delay_05;
        }

        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(0, 0);
    }

    IEnumerator State_Death() // 사망
    {
        Action_Die();
        //애니메이션 작동
        yield return delay_03;
    }

    IEnumerator State_test() // 임시용
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        Action_Stand();

        while (isOnScheduler)
        {
            if (onSpecialSkillAvailable && onSkillAvailable & onAttackAvailable) break;
            yield return delay_05;
        }
        //애니메이션 작동
        while (!isRemove)
        {
            // 추가 조건으로 빠져나올것 :: 스턴 상태 종료, 특수상태 종료 같은상황
            // 적이 다가옴 
            yield return delay_05;
        }

        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(0, 0);
    }

    #endregion

    #region 대상 탐색
    /* 검토해야할 부분
     * 
     * 유닛마다 타겟우선도를 별도로 부여할지를 검토
     * 거리랑, 잔여체력, 위협도, 공격유무, 기존 타겟팅, 타겟 변경조건, 
     * 여러 형태로 설정할것. 적 사망시 델리게이트로 접근 
     * 
     * 
     */
    private void AttackTargetSearch()
    {
        int temp = 0;
        targetObj = null;
        foreach (var item in EnemyUnit)
        {
            if (!item.isLive) continue;
            targetDistance = Vector3.Distance(transform.position, item.transform.position);
            if (targetDistance <= unit.attackRange)
            {
                if (item.priorities > temp)
                {
                    temp = item.priorities;
                    targetObj = item;
                }
            }
        }
    }

    private void MovePointSearch()
    {
        float tempDistance = 99999;
        Vector3 tempPoint = Vector3.zero;
        foreach (var item in EnemyUnit)
        {
            if (!item.isLive) continue;
            targetDistance = Vector3.Distance(transform.position, item.transform.position);
            if (targetDistance < tempDistance)
            {
                tempDistance = targetDistance;
                tempPoint = item.transform.position;
            }
        }
        targetPoint.transform.position = tempPoint;
    }

    private void FollowTargetSearch(Vector3 followTargetPoint)
    {
        if (followTargetPoint == Vector3.zero)
        {
            float tempDistance = 99999;
            Vector3 tempPoint = Vector3.zero;
            foreach (var item in EnemyUnit)
            {
                targetDistance = Vector3.Distance(transform.position, item.transform.position);
                if (targetDistance < tempDistance)
                {
                    tempDistance = targetDistance;
                    tempPoint = item.transform.position;
                }
            }
            targetPoint.transform.position = tempPoint;
        }
        else targetPoint.transform.position = followTargetPoint;
    }

    /// <summary>
    /// 적이 가장 없는 지역을 향해서 이동시킴 
    /// </summary>
    private void AvoidPointSearch()
    {
        Vector3 tempPoint = Vector3.zero;
        targetObj = null;
        foreach (var item in EnemyUnit)
        {
            tempPoint += item.transform.position;
        }
        tempPoint = tempPoint / EnemyUnit.Count;
        tempPoint = transform.position - tempPoint;
        targetPoint.transform.position = tempPoint;
    }

    #region 인식 범위 탐색 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UNIT"))
        {
            if ((other.GetComponent<UnitStateData>().playerUnit == unit.playerUnit) && other.GetComponent<UnitStateData>().isLive)
            {
                perceptionAllyUnit.Add(other.GetComponent<UnitStateData>().partyNumber);
                if (other.gameObject != this.gameObject)
                {
                    dele_attacked += other.GetComponent<UnitAI>().ThinkOverPattern;
                }
            }
            else perceptionEnemyUnit.Add(other.GetComponent<UnitStateData>().partyNumber);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UNIT"))
        {
            if ((other.GetComponent<UnitStateData>().playerUnit == unit.playerUnit) && other.GetComponent<UnitStateData>().isLive)
            {
                perceptionAllyUnit.Remove(other.GetComponent<UnitStateData>().partyNumber);
                if (other.gameObject != this.gameObject)
                {
                    dele_attacked -= other.GetComponent<UnitAI>().ThinkOverPattern;
                }
            }
            else perceptionEnemyUnit.Remove(other.GetComponent<UnitStateData>().partyNumber);
        }
    }

    #endregion


    #endregion

    #region 상태 설정
    /// <summary>
    /// 유닛이 공격하기위해 이동하는 함수
    /// </summary>
    public void Action_AttackMove()
    {
        unitState = UnitState.AttackMove;
        if (!isMoving) StartCoroutine(Moving());
    }

    /// <summary>
    /// 유닛이 가만히 있을때 사용되는 함수
    /// </summary>
    public void Action_Stand()
    {
        unitState = UnitState.Stand;
    }


    /// <summary>
    /// 유닛이 죽었을때 호출하는 함수
    /// </summary>
    public void Action_Die()
    {
        unitState = UnitState.Die;
        animator.SetBool(ani_Death, true);
        unit.isLive = false;
        if (isplayer) DungeonOS.instance.DieCount_Ally--;
        else DungeonOS.instance.DieCount_Enemy--;
        DungeonOS.instance.OnStateCheck();
    }
    // 할것 :: 사망시 해당 AI 접근하여 인식 그룹에서 제외

    /// <summary>
    /// 유닛이 자동으로 일반스킬을 쓰는 함수
    /// </summary>
    public bool Action_Skil()
    {
        unitState = UnitState.Skill;
        //UnitSkill.instance
        return true;
    }

    /// <summary>
    /// 유닛이 궁극기을 쓰는 함수
    /// </summary>
    public bool Action_SpeialSkil()
    {
        unitState = UnitState.SpeialSkill;
        //UnitSkill.instance
        return true;
    }

    /// <summary>
    /// 유닛을 공격하게 하는 함수
    /// </summary>
    public bool Action_Attack()
    {
        unitState = UnitState.Attack;
        unitMelee.OnAttack();
        onAttackAvailable = false;
        if (!isGaze) StartCoroutine(TargetGaze());
        StartCoroutine(CooldownCheck(unit.attackSpeed, 0));
        return true;
    }

    /// <summary>
    /// 유닛을 자동으로 움직이게 하는 함수
    /// </summary>
    public void Action_Move()
    {
        unitState = UnitState.Move;
        if (!isMoving) StartCoroutine(Moving());
    }
    /// <summary>
    /// 공격 속도(재공격), 스킬 쿨다운을 체크하는 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator CooldownCheck(float cooldown, byte switchNumber)
    {
        switch (switchNumber)
        {
            case 0: // 일반공격
                while (cooldown-- > 0)
                {
                    yield return delay_03;
                }
                onAttackAvailable = true;
                break;
            case 1: // 스킬
                while (cooldown-- > 0)
                {
                    yield return delay_10;
                }
                onSkillAvailable = true;
                break;
            case 2: // 궁극기
                while (cooldown-- > 0)
                {
                    yield return delay_10;
                }
                onSpecialSkillAvailable = true;
                break;
        }
    }
    #endregion

    #region 행동

    IEnumerator Moving()
    {
        isMoving = true;
        while (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPoint.position) >= 1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPoint.position), 20);
                Movetemp = (targetPoint.position - transform.position).normalized  * unit.moveSpeed;
                //Move_Yzero = Movetemp - (Vector3.up * Movetemp.y);
                animator.SetBool(ani_Walk, true);
                unitControl.SimpleMove(Movetemp);
                yield return delay_001;

                // 아군 빗겨나기 레이캐스트 사용 
            }
            else
            {
                animator.SetBool(ani_Walk, false);
                isMoving = false;
            }
        }
    }

    IEnumerator TargetGaze()
    {
        isGaze = true;
        transform.LookAt(targetPoint);
        yield return delay_03;
        isGaze = false;
    }
    #endregion
}
