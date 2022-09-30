using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class UnitAI : MonoBehaviour
{
    //ȸ�� ����, ���� ����, �ڸ� �缱��
    //�ǰݽ� �Ǵ�, ���ݴ�� �缳�� (�ο���ִ� ���, �Ʊ��� �ǰݵ�)

    #region ĳ��ó��

    private UnitSkill unitSkill;
    private CapsuleCollider unit_collider;
    public UnitStateData unit;
    private UnitMelee unitMelee;
    private CharacterController unitControl;
    private Ray ray;
    private RaycastHit hit;
    private Animator animator;
    private string ani_Attack = "Attack";
    private string ani_Walk = "Walk";
    private string ani_Skill = "Skill";
    //private string ani_Ultimate = "Ultimate";
    private string ani_Death = "Death";
    //private string ani_Stun = "Stun";
    #endregion


    #region AI ��갪
    public Transform targetPoint;
    public Transform targetTransform;
    /// <summary>
    /// �ֿ� Ÿ�� 
    /// </summary>
    [SerializeField]
    public UnitStateData targetObj;

    private float targetDistance;
    /// <summary>
    /// ������ ����� �������� �����̸鼭, ���ݴ���� �ڽ��ϰ�� 
    /// �ش� ��󿡼� �������� �����ɶ� ������Ʈ�� �����Ͽ� ����� ���ܽ�Ŵ.
    /// </summary>
    private List<int> attackers = new List<int>(); // �����ϰ� �ִ� ���ֵ� 
    /// <summary>
    /// �� �������� ���������� �Ű����� ���� ��ġ (�ֱ������� ���� �ǰݽ� ����(������ ���±���))
    /// </summary>
    private List<int> threatUnit; //��� ������ġ
    /// <summary>
    /// Ʈ���� ������ ���Ե� ���� ������ �����ϸ� ���Ÿ� ��Ÿ��� ����
    /// </summary>
    private List<int> perceptionAllyUnit = new List<int>(); // �ν� ��� ����
    private List<int> perceptionEnemyUnit = new List<int>(); // �ν� �� ����
    private List<UnitStateData> AllyUnit;
    private List<UnitStateData> EnemyUnit;

    //Vector3 Move_Yzero = new Vector3();
    Vector3 Movetemp = new Vector3();

    #endregion

    #region AI ���� ����

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
    /// ��� : �ΰ����� ������ �����ϱ����� �ѹ�, �ش� ������Ʈ���� �� ���� �������� ���سѹ��� �����
    /// <br></br>��� : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    public int unitNumber;
    public int partyNumber;
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
    public enum AIPattern { Stand, Attacking, Avoiding, RuuningAway, Follow, Push, Stern, Skill, SpecialSkill, Moving, Provocation, Death, Pass }
    public AIPattern aiPattern;
    public enum UnitState { Attack, Move, AttackMove, Skill, SpeialSkill, Die, Stand }
    public UnitState unitState;
    private bool isOnScheduler;
    private bool isOnGoing;
    private bool isGaze;
    private bool isRemove;
    private bool isRoundCheck = false;
    private bool isMoving;
    private bool onAttackAvailable = false;
    private bool onSkillAvailable = false;
    private bool onSpecialSkillAvailable = false;
    private bool settingCehck = true;
    private SceneLoad sceneLoader;

    // ���������� ���� ������Ʈ�� ����ϱ⶧����, Start���� ����ó�� �ʼ�
    public void Start()
    {
        if (GameManager.instance.dungeonOS != null)
        {
            unitMelee = GetComponent<UnitMelee>();
            unitControl = GetComponent<CharacterController>();
            unit_collider = gameObject.AddComponent<CapsuleCollider>();
            unit_collider.isTrigger = true;
            unit = GetComponent<UnitStateData>();
            animator = GetComponentInChildren<Animator>();
            targetPoint = Instantiate(Resources.Load<GameObject>("Unit/MovePoint")).transform;
            sceneLoader = GameObject.Find("GameManager").GetComponent<SceneLoad>();
            SceneManager.MoveGameObjectToScene(targetPoint.gameObject, SceneManager.GetSceneByName(sceneLoader._currentlyScene));   // �� ���� ���������ϴ�.
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
        settingCehck = false;
    }

    private void AIDeleDestroy()
    {
        DungeonOS.instance.dele_RoundStart -= OnStartAI;
        DungeonOS.instance.dele_RoundEnd -= OnEndAI;
        dele_attacked = null;
    }


    public void AITargetLiveCheck()
    {
        if(targetObj != null) if (!targetObj.GetComponent<UnitStateData>().isLive) targetObj = null;
    }


    private void OnDestroy()
    {
        //AIDeleDestroy();
    }


    private void ResetAISetting()
    {
        unit_collider.radius = unit.Add_priRange;
        onAttackAvailable = true;
        onSkillAvailable = true;
        onSpecialSkillAvailable = true;
        aiSchedule.Clear();
        isMoving = false;
        isRemove = true;
        isOnScheduler = false;
        isRoundCheck = true;
        unitSkill = GetComponent<UnitSkill>();
    }


    public void OnStartAI()
    {
        StartCoroutine(delay_StartAI());
        GetComponent<UnitInfo>().attackTriggerBox.SetActive(false);//�ӽ�
    }

    IEnumerator delay_StartAI()
    {
        if (settingCehck)AIDeleSetting();
        ResetAISetting();
        yield return delay_10;
        AutoScheduler(3, 0);
        isRoundCheck = false;
    }

    public void OnEndAI()
    {
        if(!unit.isLive) AIDeleDestroy();
        isOnScheduler = true;
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
    /// <br> - 4 : ���� �ൿ ����</br></param>
    /// <returns></returns>
    public bool AutoScheduler(int PatternNumber, AIPattern Pattern)
    {
        switch (PatternNumber)
        {
            case 0: // �����츮 ����
                // �Ұ� :: ������ ���� ó�� ����;;, ���ó�� �и� 
                if (!DungeonOS.instance.isRoundPlaying)
                {
                    if (unitState != UnitState.Die)
                    {
                        aiSchedule.Clear();
                        aiSchedule.Add(AIPattern.Stand);
                    }
                }
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
                            StartCoroutine(State_Attacking2()); // ���� ���� �ʿ�
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
                        case AIPattern.Pass:
                            //StartCoroutine(State_Stand());
                            break;
                        default:
                            break;
                    }
                    return true;
                }
                else return false;

            case 1: // �ൿ �߰�
                aiSchedule.Add(Pattern);
                if (!isOnScheduler) AutoScheduler(0, AIPattern.Pass);
                return true;

            case 2: // ���� ���
                if (aiSchedule.Count == 0) return AutoScheduler(1, Pattern);
                aiSchedule.Insert(1, Pattern);

                if (isOnGoing) Queueing();
                else AutoScheduler(4, 0);
                return true;

            case 3: // �ൿ Ž��
                aiSchedule.Add(ThinkOverPattern());
                return AutoScheduler(0, AIPattern.Pass);

            case 4: // �ൿ ����
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
                if (onSkillAvailable)
                {
                    if (unitSkill.SkillCheck(unit.basicSkillA))
                    {
                        return AIPattern.Skill;
                    }
                }
            }
            else
            {
                if (onSkillAvailable)
                {
                    if (unitSkill.SkillCheck(unit.basicSkillA))
                    {
                        return AIPattern.Skill;
                    }
                }
            }
            return AIPattern.Attacking;

        }


        /* �ֺ� ���� ���� �ο���
         * 
         * ��� ���� �����ο�
         * 
         * �ڽ��� �����ϰ� �ִ� üũ
         * 
         * �νĹ����� �Ʊ��� ���ݹ޴��� üũ
         * 
         * ���� �ñ�  - ��ų ��ٿ� ���ִٸ� ��ų / ���ٸ� �Ϲݽ�ų 
         * 
         * ü���� �ʹ� ������ ���� ���� (Ȯ��) ���� ��ġ / ���� �ʹ����� ���
         * 
         * ���� ������ �ʹ� ���� �ްų�, ���Ÿ� �����ϼ��� ȸ��, �ֺ� �ٰŸ� �Ʊ����������(�Ÿ�������)
         * 
         * �Ϲ��� �� Ž���ؼ�, ��Ÿ� �ȿ������� ���� ������ 
         * 
         * �켱 ���� ���(�켱�� / �Ÿ� ��� / ����ġ / �ڽ� �ǰ���)
         * 
         * ���� ����ñ� ���� (�ֱ��� ���� �켱�� �˻�) // �����췯 �Ź� �˻� ����
         * 
         * ���� ���� ��� ��� ���� 
         * 
         * �ֺ� �Ʊ��� �ٱ� ���ҽ� ��ȣ (�ǰݽ� �ֺ� �Ʊ��� ��������Ʈ ȣ��)
         * 
         * ���� �ܿ� ü�� / ������ / ���ݼӵ� �� ��� ���� ���� ���� ��
         * 
         * ��ǥ�� �������� �ش� ���ؿ� ���ļ� �̵��� ������
         * 
         * ������ �̵��������� �̵��ϸ� �߰��� ���θ��� ���� �켱������(����ĳ��Ʈ)
         * 
         */



        //return AIPattern.Death;
    }
    #endregion

    #region ����

    IEnumerator State_Stand() // ���
    {

        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        animator.SetBool(ani_Attack, false);
        animator.SetBool(ani_Walk, false);
        animator.SetBool(ani_Skill, false);
        Action_Stand();
        //�ִϸ��̼� �۵�
        while (!isRemove)
        {
            if (isRoundCheck)
            {
                break;
            }
            // �߰� �������� �������ð� :: ���� ���� ����, Ư������ ���� ������Ȳ
            // ���� �ٰ��� 
            yield return delay_05;
        }

        isOnScheduler = false;
        if(!isRoundCheck) AutoScheduler(0, AIPattern.Pass);
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

            yield return delay_03;
        }
        isMoving = false;
        animator.SetBool(ani_Walk, false);
        //�ִϸ��̼� �۵�
        while (!isRemove)
        {
            if (onAttackAvailable)
            {
                Action_Attack();
            }
            if (targetObj == null)
            {
                isRemove = true;
                break;
            }
            else if (!targetObj.isLive)
            {
                isRemove = true; 
                break;
            }
            yield return delay_05;
        }
        //animator.SetBool(ani_Attack, true);
        isMoving = false;
        isOnScheduler = false;
        if(aiSchedule.Count != 0)aiSchedule.RemoveAt(0);
        AutoScheduler(0, AIPattern.Pass);
    }

    // Ÿ������ �Ǿ���ϴ���, � ��ų �������������� ������ �ʿ���. 
    // �̻��� ������ ����Ҷ�, ��ų������ AI�ൿ�� ���ٽ�Ű�� AI���� �� ��ų�� ���� �ൿ����� �־����.
    // AI ��ų �۵� -> ��ų���� ���� -> ��ų �������� ��ų�� ������ AI�ൿ�� AI�� ��� -> AI �ൿ ��� 
    // ��ų ��ٿ��� ������ �����ϰ�, �˻�� [AI��ų �۵�] �������� �ؾ���. 
    IEnumerator State_Attacking2() // ����
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
        animator.SetBool(ani_Walk, false);
        //�ִϸ��̼� �۵�
        while (!isRemove)
        {
            if (onSkillAvailable)
            {
                Action_Skill();
            }
            else break;
            if (targetObj == null)
            {
                isRemove = true;
                break;
            }
            else if (!targetObj.isLive)
            {
                isRemove = true;
                break;
            }
            yield return delay_05;
        }
        //animator.SetBool(ani_Attack, true);
        isMoving = false;
        isOnScheduler = false;
        if (aiSchedule.Count != 0) aiSchedule.RemoveAt(0);
        AutoScheduler(0, AIPattern.Pass);
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
        animator.SetTrigger(ani_Death);
        if (!unit.isLive)
        {
            OnEndAI();
        }
        //�ִϸ��̼� �۵�
        yield return delay_03;
    }

    IEnumerator State_test() // �ӽÿ�
    {
        isOnScheduler = true;
        StartCoroutine(IsOnGoing());
        Action_Stand();

        while (isOnScheduler)
        {
            if (onSpecialSkillAvailable && onSkillAvailable & onAttackAvailable) break;
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

    public void AttackTargetSearch(out UnitStateData targetN)
    {
        int temp = 0;
        targetObj = null;
        targetN = null;
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
                    targetN = item;
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
    /// ���� ���� ���� ������ ���ؼ� �̵���Ŵ 
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

    #region �ν� ���� Ž�� 

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
        animator.SetTrigger(ani_Death);
        unit.isLive = false;
        if (isplayer) DungeonOS.instance.DieCount_Ally++;
        else DungeonOS.instance.DieCount_Enemy++;
        DungeonOS.instance.OnStateCheck();
    }
    // �Ұ� :: ����� �ش� AI �����Ͽ� �ν� �׷쿡�� ����

    /// <summary>
    /// ������ �ڵ����� �Ϲݽ�ų�� ���� �Լ�
    /// </summary>
    public bool Action_Skill()
    {
        unitState = UnitState.Skill;
        // ��ų �������� out���� ����������� ��ȯ, ���⼭ ���� �˻�, �б� �߻�
        animator.SetTrigger(ani_Skill); // �ִϸ����� Ʈ���ŷ� �Ǿ��ִ��� Ȯ�� �ʿ�
        if (!unitSkill.OnSkill(unit.basicSkillA, out skill_cooldown)) 
        {
            Debug.Log("ads");
            return false;
        }
        Debug.Log("ads2" + skill_cooldown);

        onSkillAvailable = false;
        if (!isGaze) StartCoroutine(TargetGaze());
        StartCoroutine(CooldownCheck(skill_cooldown, 1));
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
    float skill_cooldown;
    public bool Action_Attack()
    {
        unitState = UnitState.Attack;
        animator.SetTrigger(ani_Attack);
        unitMelee.OnAttack();
        onAttackAvailable = false;
        if (!isGaze) StartCoroutine(TargetGaze());
        StartCoroutine(CooldownCheck(unit.attackSpeed, 0));
        return true;
    }

    /// <summary>
    /// �ӽÿ�
    /// </summary>
    public bool Action_Attack2()
    {
        unitState = UnitState.Attack;
        animator.SetTrigger(ani_Attack);
        unitMelee.OnAttack();
        onAttackAvailable = false;
        if (!isGaze) StartCoroutine(TargetGaze());
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
    /// <summary>
    /// ���� �ӵ�(�����), ��ų ��ٿ��� üũ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator CooldownCheck(float cooldown, byte switchNumber)
    {
        switch (switchNumber)
        {
            case 0: // �Ϲݰ���
                while (cooldown-- > 0)
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
    #endregion

    #region �ൿ

    IEnumerator Moving()
    {
        isMoving = true;
        while (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPoint.position) >= 1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPoint.position - transform.position), 20);
                Movetemp = (targetPoint.position - transform.position).normalized  * unit.moveSpeed;
                //Move_Yzero = Movetemp - (Vector3.up * Movetemp.y);
                animator.SetBool(ani_Walk, true);
                unitControl.SimpleMove(Movetemp);
                unit.HPUIMove();
                yield return delay_001;

                // �Ʊ� ���ܳ��� ����ĳ��Ʈ ��� 
            }
            else
            {
                isMoving = false;
            }
        }
        animator.SetBool(ani_Walk, false);
    }

    IEnumerator TargetGaze()
    {
        isGaze = true;
        transform.LookAt(targetPoint);
        yield return delay_03;
        isGaze = false;
    }

    public void Resurrection(float percent)
    {
        unit.isLive = true;
        if(percent == 0)
        {
            unit.hp = unit.maxHP;
        }
        else if (percent < 1 && percent > 0){
            unit.hp = unit.maxHP * percent;
        }
        else
        {
            if(percent >= unit.maxHP)
            {
                unit.hp = unit.maxHP;
            }
            else
            {
                unit.hp = percent;
            }
        }
        AutoScheduler(2, AIPattern.Pass);
    }

    #endregion
}
