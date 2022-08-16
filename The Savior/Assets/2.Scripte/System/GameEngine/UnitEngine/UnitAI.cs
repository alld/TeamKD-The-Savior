using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    //ȸ�� ����, ���� ����, �ڸ� �缱��
    //�ǰݽ� �Ǵ�, ���ݴ�� �缳�� (�ο���ִ� ���, �Ʊ��� �ǰݵ�)

    #region AI ��갪
    public Transform targetPoint;
    private UnitTable targetObj;
    private UnitMelee unitMelee;

    private float targetDistance;
    private UnitTable unit;
    private CharacterController unitControl;
    #endregion

    #region AI ���� ����
    
    #endregion

    //enum UnitState { COMMON, ATTACK, ATTACK_MOVE, MOVE, SKILL, DIE, SPEIAL_SKILL }
    //UnitState unitState;
    private WaitForSeconds delay_001 = new WaitForSeconds(0.01f);
    private WaitForSeconds delay_03 = new WaitForSeconds(0.3f);
    private WaitForSeconds delay_05 = new WaitForSeconds(0.5f);
    private WaitForSeconds delay_10 = new WaitForSeconds(1.0f);
    /// <summary>
    /// ��� : �ΰ����� ������ �����ϱ����� �ѹ�, �ش� ������Ʈ���� �� ���� �������� ���سѹ��� �����
    /// <br></br>��� : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    public int unitNumber 
    {
        get { return unitNumber; }
        set { GetComponent<UnitMelee>().unitNumber = value; }
    }
    public int partyNumber
    {
        get { return partyNumber; }
        set { GetComponent<UnitMelee>().partyNumber = value; }
    }
    public bool isplayer;

    public List<AIPattern> aiSchedule = new List<AIPattern>();
    /// <summary>
    /// <br>�����쿡 �ԷµǴ� �ൿ ����</br>
    /// <br><paramref name="Attacking"/> :: �����ϱ�</br>
    /// <br><paramref name="Avoidng"/> :: ȸ���ϱ�</br>
    /// <br><paramref name="RuuningAway"/> :: ��������</br>
    /// <br><paramref name="Follow"/> :: ���󰡱�</br>
    /// <br><paramref name="Push"/> :: ��ġ��</br>
    /// <br><paramref name="Stern"/> :: ����</br>
    /// <br><paramref name="Skill"/> :: �Ϲ� ��ų</br>
    /// <br><paramref name="SpecialSkill"/> :: �ñر�</br>
    /// <br><paramref name="Moving"/> :: �̵��ϱ�</br>
    /// <br><paramref name="Provocation"/> :: ����</br>
    /// <br><paramref name="Death"/> :: ����</br>
    /// <br><paramref name="Stand"/> :: ���</br>
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

    // ���������� ���� ������Ʈ�� ����ϱ⶧����, Start���� ����ó�� �ʼ�
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
        if (isplayer)
        {
            unit = DungeonOS.instance.partyUnit[partyNumber];
        }
        else unit = DungeonOS.instance.monsterGroup[partyNumber];
    }

    public void OnEndAI()
    {
        isOnScheduler = false;
        ResetAISetting();
    }

    #region �����췯

    /// <summary>
    /// <br><paramref name="PatternNumber"/> :: </br>
    /// <br> ������ ����� ����</br>
    /// </summary>
    /// <param name="PatternNumber">
    /// <br> - 0 : �����췯 ����</br>
    /// <br> - 1 : �ൿ �߰�</br>
    /// <br> - 2 : ���� ���</br>
    /// <br> - 3 : �ൿ Ž��</br>
    /// <br> - 4 : ���� �ൿ ����</br>
    /// <returns></returns>
    public bool AutoScheduler(int PatternNumber, AIPattern Pattern)
    {
        switch (PatternNumber)
        {
            case 0: // �����츮 ����
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

            case 1: // �ൿ �߰�
                aiSchedule.Add(Pattern);
                if (!isOnScheduler) AutoScheduler(0, 0);
                return true;

            case 2: // ���� ���
                if (aiSchedule.Count == 0) return AutoScheduler(1, Pattern);
                aiSchedule.Insert(1, Pattern);

                if (isOnGoing) Queueing();
                else AutoScheduler(4, 0);
                return true;

            case 3: // �ൿ Ž��
                aiSchedule.Add(ThinkOverPattern());
                return AutoScheduler(0, 0);

            case 4: // �ൿ ����
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
    /// ��ų ������ ���¿��� ������ �����, ������ ���������� ���
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
    /// �� �ൿ�� ���� ������ ������, �ൿ�� ���������� ����Ŵ
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
    /// 0.5�� ��ų ������ 
    /// </summary>
    /// <returns></returns>
    IEnumerator IsOnGoing()
    {
        isOnGoing = true;
        yield return delay_05;
        isOnGoing = false;
    }

    /// <summary>
    /// AI ���� �ڵ� Ž�� �� �߰�
    /// </summary>
    /// <returns></returns>
    private AIPattern ThinkOverPattern()
    {
        return AIPattern.Death;
    }
    #endregion

    #region ����

    IEnumerator State_Stand() // ���
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        Action_Stand();
        //�ִϸ��̼� �۵�
        while (!isRemove)
        {
            // �߰� �������� �������ð� :: ���� ���� ����, Ư������ ���� ������Ȳ
            // ���� �ٰ��� 
            yield return delay_05;
        }

        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(0, 0);
    }

    IEnumerator State_Attacking() // ����
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
        //�ִϸ��̼� �۵�
        while (!isRemove)
        {
            // �߰� �������� �������ð� :: ���� ���� ����, Ư������ ���� ������Ȳ
            // ���� �ٰ��� 
            yield return delay_05;
        }
        isMoving = false;
        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(0, 0);
    }

    IEnumerator State_Avoiding() // ȸ��
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

    IEnumerator State_RuuningAway() // ���� 
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

    IEnumerator State_Follow(float time, Vector3 Target) // ���󰡱� 
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

    IEnumerator State_Moving(Vector3 Target) // �̵��ϱ� 
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

    IEnumerator State_Stern(float time) // ����
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
        //�ִϸ��̼� �۵�
        while (!isRemove)
        {
            // �߰� �������� �������ð� :: ���� ���� ����, Ư������ ���� ������Ȳ
            // ���� �ٰ��� 
            yield return delay_05;
        }

        isOnScheduler = false;
        if (aiSchedule.Count == 0) AutoScheduler(0, 0);
    }

    IEnumerator State_Death() // ���
    {
        Action_Die();
        //�ִϸ��̼� �۵�
        yield return delay_03;
    }
    #endregion

    #region ��� Ž��
    /* �����ؾ��� �κ�
     * 
     * ���ָ��� Ÿ�ٿ켱���� ������ �ο������� ����
     * �Ÿ���, �ܿ�ü��, ������, ��������, ���� Ÿ����, Ÿ�� ��������, 
     * ���� ���·� �����Ұ�. �� ����� ��������Ʈ�� ���� 
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

    private void FollowTargetSearch(Vector3 followTargetPoint)
    {
        if(followTargetPoint == Vector3.zero)
        {
            float tempDistance = 99999;
            Vector3 tempPoint = Vector3.zero;
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
        else targetPoint.transform.position = followTargetPoint;
    }

    /// <summary>
    /// ���� ���� ���� ������ ���ؼ� �̵���Ŵ 
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

    #region ���� ����
    /// <summary>
    /// ������ �����ϱ����� �̵��ϴ� �Լ�
    /// </summary>
    public void Action_AttackMove()
    {
        unitState = UnitState.AttackMove;
        if (!isMoving) StartCoroutine(Moving());
    }

    /// <summary>
    /// ������ ������ ������ ���Ǵ� �Լ�
    /// </summary>
    public void Action_Stand()
    {
        unitState = UnitState.Stand;
    }


    /// <summary>
    /// ������ �׾����� ȣ���ϴ� �Լ�
    /// </summary>
    public void Action_Die()
    {
        unitState = UnitState.Die;
        unit.isLive = false;
        if (isplayer) DungeonOS.instance.DieCount_Ally--;
        else DungeonOS.instance.DieCount_Enemy--;
        DungeonOS.instance.OnStateCheck();
    }


    /// <summary>
    /// ������ �ڵ����� �Ϲݽ�ų�� ���� �Լ�
    /// </summary>
    public bool Action_Skil()
    {
        unitState = UnitState.Skill;
        //UnitSkill.instance
        return true;
    }

    /// <summary>
    /// ������ �ñر��� ���� �Լ�
    /// </summary>
    public bool Action_SpeialSkil()
    {
        unitState = UnitState.SpeialSkill;
        //UnitSkill.instance
        return true;
    }

    /// <summary>
    /// ������ �����ϰ� �ϴ� �Լ�
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
    /// ������ �ڵ����� �����̰� �ϴ� �Լ�
    /// </summary>
    public void Action_Move()
    {
        unitState = UnitState.Move;
        if (!isMoving) StartCoroutine(Moving());
    }
    #endregion


    /// <summary>
    /// ���� �ӵ�(�����), ��ų ��ٿ��� üũ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator CooldownCheck(float cooldown, byte switchNumber)
    {
        switch (switchNumber)
        {
            case 0: // �Ϲݰ���
                while(cooldown-- > 0)
                {
                    yield return delay_03;
                }
                onAttackAvailable = true;
                break;
            case 1: // ��ų
                while (cooldown-- > 0)
                {
                    yield return delay_10;
                }
                onSkillAvailable = true;
                break;
            case 2: // �ñر�
                while (cooldown-- > 0)
                {
                    yield return delay_10;
                }
                onSpecialSkillAvailable = true;
                break;
        }
    }

    #region �ൿ

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
