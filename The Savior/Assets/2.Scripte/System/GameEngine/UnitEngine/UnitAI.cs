using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    //회피 패턴, 도주 패턴, 자리 재선정
    //피격시 판단, 공격대상 재설정 (싸우고있는 대상, 아군이 피격됨)

    #region AI 계산값
    public Transform targetPoint;
    private UnitTable targetObj;
    private UnitMelee unitMelee;

    private float targetDistance;
    private UnitTable unit;
    private CharacterController unitControl;
    #endregion

    #region AI 성향 변수
    
    #endregion

    //enum UnitState { COMMON, ATTACK, ATTACK_MOVE, MOVE, SKILL, DIE, SPEIAL_SKILL }
    //UnitState unitState;
    private WaitForSeconds delay_001 = new WaitForSeconds(0.01f);
    private WaitForSeconds delay_05 = new WaitForSeconds(0.5f);
    private WaitForSeconds delay_10 = new WaitForSeconds(1.0f);
    /// <summary>
    /// 기능 : 인공지능 유닛을 구분하기위한 넘버, 해당 컴포넌트들을 이 값을 기준으로 기준넘버가 변경됨
    /// <br></br>방법 : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    public int unitNumber;
    public int partyNumber;

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
    public enum AIPattern { Stand, Attacking, Avoiding, RuuningAway, Follow, Push, Stern, Skill, SpecialSkill, Moving, Provocation, Death }
    public AIPattern aiPattern;
    public enum UnitState { Attack, Move, AttackMove, Skill, SpeialSkill, Die, Stand }
    public UnitState unitState;
    private bool isOnScheduler;
    private bool isOnGoing;
    private bool isRemove;
    private bool isMoving;
    private bool onAttackAvailable = false;
    private bool onSkillAvailable = false;
    private bool onSpecialSkillAvailable = false;

    // 상점에서도 같은 오브젝트를 사용하기때문에, Start사용시 예외처리 필수
    private void Start()
    {
        unitMelee = GetComponent<UnitMelee>();
        unitControl.GetComponent<CharacterController>();
    }

    private void ResetAISetting()
    {
        
        aiSchedule.Clear();
    }


    public void OnStartAI()
    {
        ResetAISetting();
        unit = DungeonOS.instance.partyUnit[partyNumber];
    }

    public void OnEndAI()
    {
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
    /// <br> - 4 : 현재 행동 제거</br>
    /// <returns></returns>
    private bool AutoScheduler(int PatternNumber, AIPattern Pattern)
    {
        switch (PatternNumber)
        {
            case 0: // 스케쥴리 진행
                if (aiSchedule.Count == 0)
                {
                    return AutoScheduler(3, 0);
                }
                else if (!isOnScheduler)
                {
                    isOnGoing = true;
                    switch (Pattern)
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
                            break;
                        case AIPattern.Follow:
                            break;
                        case AIPattern.Push:
                            break;
                        case AIPattern.Stern:
                            break;
                        case AIPattern.Skill:
                            break;
                        case AIPattern.SpecialSkill:
                            break;
                        case AIPattern.Moving:
                            break;
                        case AIPattern.Provocation:
                            break;
                        case AIPattern.Death:
                            break;
                        default:
                            break;
                    }
                    return true;
                }
                else return false;

            case 1: // 행동 추가
                aiSchedule.Add(Pattern);
                if (!isOnScheduler) AutoScheduler(0, 0);
                return true;

            case 2: // 끼어 들기
                if (aiSchedule.Count == 0) return AutoScheduler(1, Pattern);
                aiSchedule.Insert(1, Pattern);

                if (isOnGoing) Queueing();
                else AutoScheduler(4, 0);
                return true;

            case 3: // 행동 탐색
                aiSchedule.Add(ThinkOverPattern());
                return AutoScheduler(0, 0);

            case 4: // 행동 제거
                isRemove = true;
                if (!isOnScheduler)
                {
                    aiSchedule.RemoveAt(0);
                    return AutoScheduler(0, 0);
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
    private AIPattern ThinkOverPattern()
    {
        return AIPattern.Death;
    }
    #endregion

    #region 패턴

    IEnumerator State_Stand()
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        Action_Stand();
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

    IEnumerator State_Attacking()
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

            yield return delay_10;
        }
        Action_Attack();
        //애니메이션 작동
        while (!isRemove)
        {
            // 추가 조건으로 빠져나올것 :: 스턴 상태 종료, 특수상태 종료 같은상황
            // 적이 다가옴 
            yield return delay_05;
        }
        isMoving = false;
        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(0, 0);
    }

    IEnumerator State_Avoiding()
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
        foreach (var item in DungeonOS.instance.monsterGroup)
        {
            targetDistance = Vector3.Distance(transform.position, item.transform.position);
            if (targetDistance <= unit.attackRange)
            {
                if(item.priorities > temp)
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
        targetObj = null;
        foreach (var item in DungeonOS.instance.monsterGroup)
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

    /// <summary>
    /// 적이 가장 없는 지역을 향해서 이동시킴 
    /// </summary>
    private void AvoidPointSearch()
    {
        Vector3 tempPoint = Vector3.zero;
        targetObj = null;
        foreach (var item in DungeonOS.instance.monsterGroup)
        {
            tempPoint += item.transform.position;
        }
        tempPoint = tempPoint / DungeonOS.instance.monsterGroup.Count;
        tempPoint = transform.position - tempPoint;
        targetPoint.transform.position = tempPoint;
    }
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
        DungeonOS.instance.partyUnit[unitNumber].isLive = false;
    }


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
    #endregion


    /// <summary>
    /// 공격 속도(재공격), 스킬 쿨다운을 체크하는 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator CooldownCheck(float cooldown, byte switchNumber)
    {
        switch (switchNumber)
        {
            case 0: // 일반공격
                while(cooldown-- > 0)
                {
                    yield return delay_10;
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

    #region 행동

    IEnumerator Moving()
    {
        isMoving = true;
        while (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPoint.position) >= 0.5f)
            {
                unitControl.Move(targetPoint.position * Time.deltaTime);
                transform.LookAt(targetPoint.position * Time.deltaTime);
                yield return delay_001;
            }
            else
            {
                isMoving = false;
            }
        }
    }
    #endregion
}
