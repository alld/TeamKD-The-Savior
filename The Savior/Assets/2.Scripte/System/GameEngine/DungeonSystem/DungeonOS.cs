using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonOS : MonoBehaviour
{
    public static DungeonOS instance = null;
    #region ȯ�� ����
    [Header("ȯ�� ����")]
    public int dungeonNumber = 0;
    //private PlayUIManager PUIManager;
    private GameObject timerArrowDG;
    private GameObject[] timerlevelUI;
    WaitForSeconds delay_01 = new WaitForSeconds(0.1f);
    WaitForSeconds delay_03 = new WaitForSeconds(0.3f);
    private DungeonController DungeonCtrl;
    public List<string> errorList;
    #endregion
    #region ���� �⺻ ������
    public Transform UnitGroupTr;
    public Transform MonsterGroupTr;

    List<int> rewardHeroBox = new List<int>();
    List<int> rewardCardBox = new List<int>();
    List<int> rewardRelicBox = new List<int>();

    List<UnitStateData> stageSlotPlayerBottom = new List<UnitStateData>(); 
    List<UnitStateData> stageSlotPlayerTop = new List<UnitStateData>();
    List<UnitStateData> stageSlotPlayerMid = new List<UnitStateData>();

    List<UnitStateData> stageSlotMonsterBottom = new List<UnitStateData>();
    List<UnitStateData> stageSlotMonsterTop = new List<UnitStateData>();
    List<UnitStateData> stageSlotMonsterMid = new List<UnitStateData>();


    public delegate void StateCheck();
    public StateCheck dele_stateCheck; // ����, ĳ���� ��ȭ üũ �̺�Ʈ // ���� �̺�Ʈ�� �����ȳ���
    public StateCheck dele_TimeALWAY, dele_TimeFIRST, dele_TimeMIDDLE, dele_TimeHALF, dele_TimeLAST;
    public StateCheck dele_RoundStart, dele_RoundEnd;
    //���� ���õ� ���� : DG
    /// <summary>
    /// ������ �������ִ� ��� �������� �׷�
    /// </summary>
    [Header("��������")]
    public GameObject[] stagePrefabGroupDG;
    public GameObject[] stageGroupDG;
    /// <summary>
    /// ���� ������ ������� ��������
    /// </summary>
    public GameObject slotStageDG;

    public GameObject playerStagePointGroup;
    public GameObject monsterStagePointGroup;
    private Transform[] playerStagePoint;
    private Transform[] monsterStagePoint;
    public DungeonData.data dungeonData;
    public bool[] handSlot = { false, false, false };
    /// <summary>
    /// �����尡 �������ִ� ����
    /// <br>1. �Ϲ�</br>
    /// <br>2. �߰�������</br>
    /// <br>3. �̺�Ʈ��</br>
    /// <br>4. Ư��������</br>
    /// <br>5. ����Ʈ��</br>
    /// <br>6. �б� ����</br>
    /// <br>10. ������ </br>
    /// </summary>
    [Header("�������� ����")]
    public int[] roundInfoDG;
    /// <summary>
    /// ���� ���忡 �������ִ� ���� �׷�
    /// </summary>
    public List<UnitStateData> monsterGroup = new List<UnitStateData>();
    /// <summary>
    /// �÷��̾� ���� �׷�
    /// </summary>
    public List<UnitStateData> characterGroup = new List<UnitStateData>(); // ������Ʈ�� ����
    /// <summary>
    /// ���� �б� Ȯ�� ���������� ������� ����ֱ⶧����, ���Ӻб� ���ý�Ű�� ����
    /// </summary>
    public int checkCountDGGame;
    public bool resurrectable;

    //�����ȿ��� �÷��̾���õ� ���� : DGP
    /// <summary>
    /// ���� ���� 
    /// </summary>
    public int roundDGP;
    /// <summary>
    /// ���� ���� ���� ����
    /// </summary>
    public bool ISRoundPlaying;
    public bool isRoundPlaying
    {
        get { return ISRoundPlaying; }
        set
        {
            ISRoundPlaying = value;
            if (value)
            {
                dele_RoundStart();
            }
            else
            {
                dele_RoundEnd();
            }
        }
    }
    /// <summary>
    /// ���� ���� ���� �������
    /// </summary>
    public int accrueGoldDGP;
    /// <summary>
    /// ������� ���� ���� �ҿ�(������ ���� �ҿ� ����)
    /// </summary>
    public int accrueSoulDGP;
    /// <summary>
    /// ���� ������ �ڽ�Ʈ (�ִ�ġ 10)
    /// </summary>
    private int CostDGP;
    public int costDGP
    {
        get { return CostDGP; }
        set 
        {
            CostDGP = value; 
            HandUIReset();
        }
    }
    /// <summary>
    /// ���� ���� ���ӵ� �ð� 
    /// </summary>
    private float ProgressTimeDGP;
    public float progressTimeDGP
    {
        get { return ProgressTimeDGP; }
        set 
        {
            ProgressTimeDGP = value;
            DungeonCtrl.gameTimerText.text = progressTimeDGP.ToString("F0");
        }
    }
    /// <summary>
    /// �ð� �帧�� ���� �ð��ܰ踦 ǥ�� 
    /// <br>0. �ʹ�</br>
    /// <br>1. �߹�</br>
    /// <br>2. �Ĺ�</br>
    /// </summary>
    public bool timerOnDGP;
    private int TimeLevelDGP;
    public int timeLevelDGP
    {
        get { return TimeLevelDGP; }
        set
        {
            TimeLevelDGP = value;
            switch (value)
            {
                case 1:
                    dele_TimeMIDDLE();
                    break;
                case 2:
                    dele_TimeHALF();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// ���� ī�� (���ܷ�)
    /// </summary>
    public int remainingCardDGP;
    /// <summary>
    /// ���������� ���� �ҿ� ����
    /// </summary>
    public int[] eaGetSoulDGP;
    /// <summary>
    /// ��Ƽ �׷��� ��� ���� �Ǵ�
    /// </summary>
    public int DieCount_Ally;
    public int DieCount_Enemy;
    /// <summary>
    /// [��赥����] ���� ���� �ǰݷ�
    /// </summary>
    public float[] eaDamagedDGP;
    /// <summary>
    /// [��赥����] ���� ���� ��������
    /// </summary>
    public float[] eaDamageDGP;
    /// <summary>
    /// [��赥����] ���� ���� ų��
    /// </summary>
    public float[] eaKillCountDGP;

    public List<CardDataBase.Data> handCard = new List<CardDataBase.Data>();
    #endregion
    #region ���޹��� GameManager�� Data
    //ĳ���� ���� 
    public List<UnitStateData> partyUnit = new List<UnitStateData>();
    public List<RelicData.Data> equipRelic = new List<RelicData.Data>();
    //������
    public List<int> useDeckDGP = new List<int>();
    //���� ����
    //��ȸ ���� ���� �����Ȳ�����ʹ� ����
    bool ClearkCheck;
    #endregion


    private void Awake()
    {
        dungeonData = new DungeonData.data(dungeonNumber);
        instance = this; 

    }
    //ī���� ����... ī�� �����ͺ��̽��� ��������. 
    void Start()
    {
        #region ĳ��ó�� //��ĥ�� �ٽ��ѹ� �����������..
        DungeonCtrl = DungeonController.instance;
        playerStagePoint = playerStagePointGroup.GetComponentsInChildren<Transform>();
        monsterStagePoint = monsterStagePointGroup.GetComponentsInChildren<Transform>();
        #endregion

        GameManager.instance.dungeonOS = this;
        GameManager.instance.dungeonPlayTime = 0;
        //���ӿ� UI Ȱ��ȭ 
        DungeonCtrl.dungeonUI.SetActive(true);
        GameSetting();
    }
    #region ���� �̺�Ʈ(���) // �ּ�ó�� ����
    public void OnStateCheck()
    {
        dele_stateCheck();
        if (monsterGroup.Count <= DieCount_Enemy)
        {
            OnRoundVictory();
        }
        else if (characterGroup.Count <= DieCount_Ally)
        {
            OnDungeonFailed();
        }
    }

    public void OnRoundVictory()
    {
        isRoundPlaying = false;
        if (roundDGP == 10)
        {
            OnDungeonAllClear();
            return;
        }
        if (roundDGP % 10 == 5) OnRest();
        StageSelectButtonSet();
    }

    void OnDungeonAllClear()
    {
        //���� UI ó��(���â)
    }

    void OnDungeonFailed()
    {
        //���� UI ó��(���â)
    }

    void NextRound(int num)
    {
        isRoundPlaying = true;
        StartCoroutine(FadeIn());
        if (++roundDGP % 10 == 5) StageReset(5);
        else if (roundDGP % 10 != 0)
        {
            if (roundInfoDG[roundDGP -1] == 6)
            {
                StageReset(num);
            }
            else NextRound(roundDGP);
        }
        else StageReset(10);
        HandRefill();
    }
    // ���콺 �Է� ��ư ���� ������
    void OnStageSelect(Vector2 clickPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(clickPoint);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("STAGEPOINT"))
            {
                int temp = hit.collider.GetComponent<PointInfo>().pointNumber;
                if(temp == 0)
                {
                    NextRound(roundDGP);
                }
                else
                {
                    if (roundDGP > 10) roundDGP -= 10;
                    else roundDGP += 10;
                    NextRound(roundDGP);
                }
            }
        }
    }

    void OnRest() //�޽�
    {
        foreach (var item in partyUnit)
        {
            if (!item.isLive) resurrectable = true;
            item.hp = item.maxHP;
        }
        HandReset();
    }

    void SelectResurrection(int partySlotN)
    {
        if(resurrectable)
        {
            if(partyUnit != null & !partyUnit[partySlotN].isLive)
            {
                resurrectable = false;
                partyUnit[partySlotN].isLive = true;
                partyUnit[partySlotN].hp = partyUnit[partySlotN].maxHP;
                // ĳ���� ���±�� ��ȯ �ʿ���
            }
        }
    }
    void SelectResurrection(int partySlotN, float recov)
    {
        if (resurrectable)
        {
            if (partyUnit != null & !partyUnit[partySlotN].isLive)
            {
                resurrectable = false;
                partyUnit[partySlotN].isLive = true;
                partyUnit[partySlotN].hp = recov;
                // ĳ���� ���±�� ��ȯ �ʿ���
            }
        }
    }
    // ��Ȱ�� ������� ���� ��� ���â ���; 
    #endregion
    #region ���� UI ó�� // �ּ���� ����
    void StageSelectButtonSet()
    {
        GameObject.Find("StageSelectGroup").SetActive(true);
        // ��ư Ŭ���ϰ��ؼ� NextRound �����Ŵ 
    }
    public void HandUIReset()
    {
        int temp = roundDGP % 10;
        DungeonCtrl.gameRoundbarArrow.transform.SetParent(DungeonCtrl.gameRoundbarPoint[temp].transform);
        DungeonCtrl.gameRoundbarArrow.transform.position = Vector3.zero;
        DungeonCtrl.playerCostGauage.fillAmount = (float)costDGP / 10f;
        DungeonCtrl.playerExpectationsGauage.fillAmount = DungeonCtrl.playerCostGauage.fillAmount;
        DungeonCtrl.playerLackCost.fillAmount = DungeonCtrl.playerCostGauage.fillAmount;
        DungeonCtrl.gameCostNumber.text = costDGP.ToString();
    }


    // �ܺο��� ü�� ������ �ش簪�� ȣ���Ұ� 
    public void PartyUIReset()
    {
        for (int i = 0; i < partyUnit.Count; i++)
        {
            if (partyUnit[i] != null)
            {
                DungeonCtrl.partySlotHPGauage[i].fillAmount = partyUnit[i].hp / partyUnit[i].maxHP;
            }
        }
    }

    #region ���̵���/�ƿ�
    /// <summary>
    /// ���� ������ ���̵��� ó��
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        bool check = true;
        DungeonCtrl.fadeObj.SetActive(true);
        float colorvalue = 0;
        while (check)
        {
            Color color = DungeonCtrl.fade.color;
            colorvalue += Time.deltaTime * 10;
            if (colorvalue < 1)
            {
                color.a = colorvalue;
                DungeonCtrl.fade.color = color;
            }
            else check = false;
            yield return delay_01;
        }
        DungeonCtrl.fade.color = new Color(0, 0, 0, 1);
        FadeOut();
    }
    /// <summary>
    /// ���̵��� ó���� ���̵�ƿ� 
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        bool check = true;
        float colorvalue = 1;
        while (check)
        {
            Color color = DungeonCtrl.fade.color;
            colorvalue -= Time.deltaTime * 10;
            if (colorvalue > 0)
            {
                color.a = colorvalue;
                DungeonCtrl.fade.color = color;
            }
            else check = false;
            yield return delay_01;
        }
        DungeonCtrl.fade.color = new Color(0, 0, 0, 0);
        DungeonCtrl.fadeObj.SetActive(false);
    }
    #endregion
    #endregion
    #region ���� ���� // �ּ���� ����
    /// <summary>
    /// ������ �����ϰ� �������� �⺻������ �����Ҷ� ���
    /// </summary>
    void GameSetting()
    {
        foreach (var item in GameManager.instance.data.equipRelic)
        {
            equipRelic.Add(new RelicData.Data(item));
        }

        foreach (var item in stagePrefabGroupDG.Select((value, index) => new { value, index }))
        {
            stageGroupDG[item.index] = Instantiate(item.value);
            SceneManager.MoveGameObjectToScene(stageGroupDG[item.index], SceneManager.GetSceneByName(GameManager.instance.currentlyScene));
        }
        roundDGP = 1;
        HandReset();
        PlayerUnitCreate();
        ////�������� ���� �ѹ� ������. 
        StageReset(roundDGP);
        //StageReset(checkCountDGGame); // ���óѹ� ���� �����
    }
    /// <summary>
    /// ���������� �����ǰ� ����Ǵ� ���
    /// �Ű������� ���° ���������� �������� ����
    /// </summary>
    /// <param name="stageNum"></param>
    void StageReset(int stageNum)
    {
        if (slotStageDG != null) slotStageDG.SetActive(false);
        slotStageDG = stageGroupDG[stageNum];
        slotStageDG.SetActive(true);
        Camera.main.transform.position = slotStageDG.GetComponentInChildren<Camera>().transform.position;
        Camera.main.transform.rotation = slotStageDG.GetComponentInChildren<Camera>().transform.rotation;
        DGTimerStart();
        PlayerUnitSetting();
        MonsterCreate();
        MonsterSetting();
        isRoundPlaying = true;
    }

    /// <summary>
    /// �÷��̾����� �������� �ڵ���ġ
    /// </summary>
    void PlayerUnitSetting()
    {
        Animator tempAnimator;
        foreach (var item in partyUnit)
        {
            item.TryGetComponent<Animator>(out tempAnimator);
            tempAnimator.enabled = false;
            switch (item.attackType)
            {
                case 1:
                    if (stageSlotPlayerBottom.Count < 3)
                    {
                        stageSlotPlayerBottom.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //���� �� �о��� �ڸ���ġ // ������ġ�� �켱
                        for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
                        {
                            if (stageSlotPlayerBottom[i].positionPri > moveSlot.positionPri)
                            {
                                tempSlot = stageSlotPlayerBottom[i];
                                stageSlotPlayerBottom.RemoveAt(i);
                                stageSlotPlayerBottom.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // ������ ���� 
                        if (stageSlotPlayerMid.Count < 3)
                        {
                            stageSlotPlayerMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotPlayerTop.Count < 3)
                            {
                                stageSlotPlayerTop.Add(moveSlot);
                            }
                            else
                            {
                                GameError("���ֹ�ġ : �ʰ��� ���� �߻�");
                            }
                        }
                    }
                    break;
                case 2:
                    if (stageSlotPlayerMid.Count < 3)
                    {
                        stageSlotPlayerMid.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        // ��ġ�� ���� ��� 
                        if (item.positionPri >= 30)
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotPlayerBottom.Count < 3)
                            {
                                stageSlotPlayerBottom.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
                                {
                                    if (stageSlotPlayerBottom[i].positionPri > moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotPlayerBottom[i];
                                        stageSlotPlayerBottom.RemoveAt(i);
                                        stageSlotPlayerBottom.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // ������ ���� 
                                if (stageSlotPlayerTop.Count < 3)
                                {
                                    stageSlotPlayerTop.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("���ֹ�ġ : �ʰ��� ���� �߻�");
                                }
                            }
                        }
                        else // ��ġ�� ���� ��� 
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotPlayerTop.Count < 3)
                            {
                                stageSlotPlayerTop.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotPlayerTop.Count; i++)
                                {
                                    if (stageSlotPlayerTop[i].positionPri < moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotPlayerTop[i];
                                        stageSlotPlayerTop.RemoveAt(i);
                                        stageSlotPlayerTop.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // ������ ���� 
                                if (stageSlotPlayerBottom.Count < 3)
                                {
                                    stageSlotPlayerBottom.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("���ֹ�ġ : �ʰ��� ���� �߻�");
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    if (stageSlotPlayerTop.Count < 3)
                    {
                        stageSlotPlayerTop.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //���� �� �о��� �ڸ���ġ // ū��ġ�� �켱
                        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
                        {
                            if (stageSlotPlayerTop[i].positionPri < moveSlot.positionPri)
                            {
                                tempSlot = stageSlotPlayerTop[i];
                                stageSlotPlayerTop.RemoveAt(i);
                                stageSlotPlayerTop.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // ������ ���� 
                        if (stageSlotPlayerMid.Count < 3)
                        {
                            stageSlotPlayerMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotPlayerBottom.Count < 3)
                            {
                                stageSlotPlayerBottom.Add(moveSlot);
                            }
                            else
                            {
                                GameError("���ֹ�ġ : �ʰ��� ���� �߻�");
                            }
                        }
                    }
                    break;
                default:
                    GameError("���ֹ�ġ : ����Ÿ���� �������� ���� ������ ������");
                    break;
            }
            tempAnimator.enabled = true;
        }
        for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
        {
            stageSlotPlayerBottom[i].gameObject.transform.position = playerStagePoint[i + 1].position;
        }
        for (int i = 0; i < stageSlotPlayerMid.Count; i++)
        {
            stageSlotPlayerMid[i].gameObject.transform.position = playerStagePoint[i + 4].position;
        }
        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
        {
            stageSlotPlayerTop[i].gameObject.transform.position = playerStagePoint[i + 7].position;
        }



        stageSlotPlayerBottom.Clear();
        stageSlotPlayerMid.Clear();
        stageSlotPlayerTop.Clear();
    }
    /// <summary>
    /// �÷��̾� ���� ������Ʈȭ
    /// </summary>
    void PlayerUnitCreate()
    {
        UnitInfo tempUnitInfo;
        GameObject tempUnit;
        foreach (var item in GameManager.instance.data.equipCharacter.Select((value, index) => new { value, index }))
        {
            tempUnit = Instantiate(new CharacterDatabase.Data(item.value).charObject);
            SceneManager.MoveGameObjectToScene(tempUnit, SceneManager.GetSceneByName(GameManager.instance.currentlyScene));
            partyUnit.Add(tempUnit.AddComponent<UnitStateData>());
            partyUnit[partyUnit.Count - 1].DataSetting(true, item.value);
            partyUnit[partyUnit.Count - 1].gameObject.AddComponent<CharacterController>();
            partyUnit[partyUnit.Count - 1].gameObject.AddComponent<UnitMelee>();
            partyUnit[partyUnit.Count - 1].gameObject.AddComponent<UnitAI>();
            tempUnitInfo = partyUnit[partyUnit.Count - 1].gameObject.AddComponent<UnitInfo>();
            tempUnitInfo.unitNumber = item.value;
            tempUnitInfo.partyNumber = partyUnit.Count - 1;
            partyUnit[partyUnit.Count - 1].isLive = true;
        }
    }

    /// <summary>
    /// ���� �������� �ڵ���ġ
    /// </summary>
    void MonsterSetting()
    {
        Animator tempAnimator;
        foreach (var item in monsterGroup)
        {
            item.TryGetComponent<Animator>(out tempAnimator);
            tempAnimator.enabled = false;
            switch (item.attackType)
            {
                case 1:
                    if (stageSlotMonsterBottom.Count < 4)
                    {
                        stageSlotMonsterBottom.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //���� �� �о��� �ڸ���ġ // ������ġ�� �켱
                        for (int i = 0; i < stageSlotMonsterBottom.Count; i++)
                        {
                            if (stageSlotMonsterBottom[i].positionPri > moveSlot.positionPri)
                            {
                                tempSlot = stageSlotMonsterBottom[i];
                                stageSlotMonsterBottom.RemoveAt(i);
                                stageSlotMonsterBottom.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // ������ ���� 
                        if (stageSlotMonsterMid.Count < 4)
                        {
                            stageSlotMonsterMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotMonsterTop.Count < 4)
                            {
                                stageSlotMonsterTop.Add(moveSlot);
                            }
                            else
                            {
                                GameError("���͹�ġ : �ʰ��� ���� �߻�");
                            }
                        }
                    }
                    break;
                case 2:
                    if (stageSlotMonsterMid.Count < 4)
                    {
                        stageSlotMonsterMid.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        // ��ġ�� ���� ��� 
                        if (item.attackType >= 30)
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotMonsterBottom.Count < 4)
                            {
                                stageSlotMonsterBottom.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotMonsterBottom.Count; i++)
                                {
                                    if (stageSlotMonsterBottom[i].positionPri > moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotMonsterBottom[i];
                                        stageSlotMonsterBottom.RemoveAt(i);
                                        stageSlotMonsterBottom.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // ������ ���� 
                                if (stageSlotMonsterTop.Count < 4)
                                {
                                    stageSlotMonsterTop.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("���͹�ġ : �ʰ��� ���� �߻�");
                                }
                            }
                        }
                        else // ��ġ�� ���� ��� 
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotMonsterTop.Count < 3)
                            {
                                stageSlotMonsterTop.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotMonsterTop.Count; i++)
                                {
                                    if (stageSlotMonsterTop[i].positionPri < moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotMonsterTop[i];
                                        stageSlotMonsterTop.RemoveAt(i);
                                        stageSlotMonsterTop.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // ������ ���� 
                                if (stageSlotMonsterBottom.Count < 3)
                                {
                                    stageSlotMonsterBottom.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("���� ��ġ : �ʰ��� ���� �߻�");
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    if (stageSlotMonsterTop.Count < 4)
                    {
                        stageSlotMonsterTop.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //���� �� �о��� �ڸ���ġ // ū��ġ�� �켱
                        for (int i = 0; i < stageSlotMonsterTop.Count; i++)
                        {
                            if (stageSlotMonsterTop[i].positionPri < moveSlot.positionPri)
                            {
                                tempSlot = stageSlotMonsterTop[i];
                                stageSlotMonsterTop.RemoveAt(i);
                                stageSlotMonsterTop.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // ������ ���� 
                        if (stageSlotMonsterMid.Count < 4)
                        {
                            stageSlotMonsterMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // ������ ���� 
                            if (stageSlotMonsterBottom.Count < 4)
                            {
                                stageSlotMonsterBottom.Add(moveSlot);
                            }
                            else
                            {
                                GameError("���� ��ġ : �ʰ��� ���� �߻�");
                            }
                        }
                    }
                    break;
                default:
                    GameError("���� ��ġ : ����Ÿ���� �������� ���� ���Ͱ� ������");
                    break;
            }
            tempAnimator.enabled = true;
        }
        for (int i = 0; i < stageSlotMonsterBottom.Count; i++)
        {
            stageSlotMonsterBottom[i].gameObject.transform.position = monsterStagePoint[i + 2].position;
        }
        for (int i = 0; i < stageSlotMonsterMid.Count; i++)
        {
            stageSlotMonsterMid[i].gameObject.transform.position = monsterStagePoint[i + 6].position;
        }
        for (int i = 0; i < stageSlotMonsterTop.Count; i++)
        {
            stageSlotMonsterTop[i].gameObject.transform.position = monsterStagePoint[i + 10].position;
        }

        stageSlotMonsterBottom.Clear();
        stageSlotMonsterMid.Clear();
        stageSlotMonsterTop.Clear();
    }


    /// <summary>
    /// ���� ���� ������Ʈȭ
    /// </summary>
    void MonsterCreate()
    {
        GameObject tempMonster;
        UnitInfo tempUnitInfo;
        if (roundDGP % 10 == 5)
        {
            tempMonster = Instantiate(dungeonData.dungeonMonsterBox[1].charObject);
            SceneManager.MoveGameObjectToScene(tempMonster, SceneManager.GetSceneByName(GameManager.instance.currentlyScene));
            monsterGroup.Add(tempMonster.AddComponent<UnitStateData>());
            monsterGroup[monsterGroup.Count - 1].DataSetting(false, dungeonData.dungeonMonsterBox[1].number);
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<CharacterController>();
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitMelee>();
            monsterGroup[0].gameObject.AddComponent<UnitAI>();
            tempUnitInfo = monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitInfo>();
            tempUnitInfo.unitNumber = monsterGroup[0].number;
            tempUnitInfo.partyNumber = 0;
            monsterGroup[0].transform.position = monsterStagePoint[1].position;
            monsterGroup[0].transform.rotation = monsterStagePoint[1].rotation;
            monsterGroup[monsterGroup.Count - 1].isLive = true;
        }
        else if (roundDGP % 10 == 0)
        {
            tempMonster = Instantiate(dungeonData.dungeonMonsterBox[0].charObject);
            SceneManager.MoveGameObjectToScene(tempMonster, SceneManager.GetSceneByName(GameManager.instance.currentlyScene));
            monsterGroup.Add(tempMonster.AddComponent<UnitStateData>());
            monsterGroup[monsterGroup.Count - 1].DataSetting(false, dungeonData.dungeonMonsterBox[0].number);
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<CharacterController>();
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitMelee>();
            monsterGroup[0].gameObject.AddComponent<UnitAI>();
            tempUnitInfo = monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitInfo>();
            tempUnitInfo.unitNumber = monsterGroup[0].number;
            tempUnitInfo.partyNumber = 0;
            monsterGroup[0].transform.position = monsterStagePoint[1].position;
            monsterGroup[0].transform.rotation = monsterStagePoint[1].rotation;
            monsterGroup[monsterGroup.Count - 1].isLive = true;
        }
        for (int i = 0; i < dungeonData.monsterBoxCount[roundDGP]; i++)
        {
            int tempint = Random.Range(dungeonData.monsterBoxMin[roundDGP], dungeonData.monsterBoxMax[roundDGP]);
            tempMonster = Instantiate(dungeonData.dungeonMonsterBox[tempint].charObject);
            SceneManager.MoveGameObjectToScene(tempMonster, SceneManager.GetSceneByName(GameManager.instance.currentlyScene));
            monsterGroup.Add(tempMonster.AddComponent<UnitStateData>());
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<CharacterController>();
            monsterGroup[monsterGroup.Count -1].DataSetting(false, dungeonData.dungeonMonsterBox[tempint].number);
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitMelee>();
            tempUnitInfo = monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitInfo>();
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitAI>();
            tempUnitInfo.unitNumber = monsterGroup[monsterGroup.Count - 1].number;
            tempUnitInfo.partyNumber = monsterGroup.Count - 1;
            monsterGroup[monsterGroup.Count - 1].isLive = true;
        }
    }

    /// <summary>
    /// ���� ������, �޽� Ÿ�ֿ̹� �� ���� ���
    /// </summary>
    public void DeckShuffle()
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < useDeckDGP.Count; i++)
        {
            int tempA = Random.Range(0, useDeckDGP.Count);
            tempList.Add(useDeckDGP[tempA]);
            useDeckDGP.RemoveAt(tempA);
        }
        useDeckDGP = tempList;
    }

    public void HandReset()
    {
        switch(GameManager.instance.data.preset)    // ������ ���� ���� �ϸ鼭 ���� ���ƽ��ϴ�.
        {
            case 1:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[0].preset);
                break;
            case 2:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[1].preset);
                break;
            case 3:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[2].preset);
                break;
            case 4:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[3].preset);
                break;
            case 5:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[4].preset);
                break;
        }
        DeckShuffle();
        handCard.Clear();
        
        HandRefill();
    }

    public void HandRefill()
    {
        CardEvent CasheCardEvent;
        for (int i = handCard.Count; i < 3; i++)
        {
            if (useDeckDGP.Count != 0)
            {
                CardDataBase.Data card = new CardDataBase.Data(useDeckDGP[0]+1);
                handCard.Add(card);
                if (!handSlot[0])
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[0].transform);
                    CasheCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                    CasheCardEvent.card_handnumber = 0;
                    handSlot[0] = true;
                    CasheCardEvent.cost = card.cost;
                }
                else if (!handSlot[1])
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[1].transform);
                    CasheCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                    CasheCardEvent.card_handnumber = 1;
                    handSlot[1] = true;
                    CasheCardEvent.cost = card.cost;
                }
                else
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[2].transform);
                    CasheCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                    CasheCardEvent.card_handnumber = 2;
                    handSlot[2] = true;
                    CasheCardEvent.cost = card.cost;
                }
                CasheCardEvent.card_number = card.number;
                useDeckDGP.RemoveAt(0);
                remainingCardDGP++;
            }
            else
            {
                //ī�� ����
                return;
            }
        }
        HandUIReset();
    }

    public void HandDraw()
    {
        if (useDeckDGP.Count != 0)
        {
            CardDataBase.Data card = new CardDataBase.Data(useDeckDGP[0]+1);
            CardEvent tempCardEvent;
            handCard.Add(card);
            if (handSlot[0])
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[0].transform);
                tempCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                tempCardEvent.card_handnumber = 0;
                handSlot[0] = true;
                tempCardEvent.cost = card.cost;
            }
            else if (handSlot[1])
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[1].transform);
                tempCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                tempCardEvent.card_handnumber = 1;
                handSlot[1] = true;
                tempCardEvent.cost = card.cost;
            }
            else
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[2].transform);
                tempCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                tempCardEvent.card_handnumber = 2;
                handSlot[2] = true;
                tempCardEvent.cost = card.cost;
            }
            tempCardEvent.card_number = card.number;
            useDeckDGP.RemoveAt(0);
            remainingCardDGP++;
        }
        else
        {
            //ī�� ����
            return;
        }
        HandUIReset();
    }

    #endregion 
    #region ���� Ÿ�̸� ��� // �ּ���� ����
    /// <summary>
    /// Ÿ�̸� ���� ���
    /// </summary>
    public void DGTimerStart() 
    {
        costDGP = 3;
        progressTimeDGP = 0;
        timerOnDGP = true;
        timeLevelDGP = 0;
        DungeonCtrl.gameTimerBG[0].fillAmount = 1;
        DungeonCtrl.gameTimerBG[1].fillAmount = 1;
        StartCoroutine(DGTimer());
    }
    /// <summary>
    /// Ÿ�̸� �۵� ���
    /// ���� �����Ǹ� �ڵ����� �ؽ�Ʈ�� �ݿ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator DGTimer()
    {
        float cycleTime = 0f;
        while (timerOnDGP)
        {
            cycleTime += 0.1f;
            progressTimeDGP += 0.1f;
            if (timeLevelDGP != 3)
            {
                DGTimerUIReset();
            }
            if (cycleTime >= 20)
            {
                cycleTime = 0f;
                HandRefill();
                if (costDGP <= 7) costDGP += 3;
                else costDGP = 10;                
                switch (timeLevelDGP)
                {
                    case 0:
                        timeLevelDGP = 1;
                        DungeonCtrl.gameTimerBG[0].fillAmount = 0;
                        break;
                    case 1:
                        timeLevelDGP = 2;
                        break;
                    default:
                        timeLevelDGP = 3;
                        break;
                }
            }
            yield return delay_01;
        }
    }
    /// <summary>
    /// �ܺο��� Ÿ�̸� ����� ���
    /// </summary>
    public void DGTimerEnd()
    {
        timerOnDGP = false;
    }
    /// <summary>
    /// <b>Ÿ�̸��� ȸ���ϴ� UI Ȯ��</b>
    ///
    /// </summary>
    public void DGTimerUIReset()
    {
        DungeonCtrl.gameTimerBG[timeLevelDGP].fillAmount = (20 - (progressTimeDGP % 20)) * 0.05f;
    }
    #endregion
    #region ���� ����
    /// <summary>
    /// ���� ����� �۵� 
    /// <br><b>���� : </b></br>���â���� �������ư Ŭ���� ����Ǵ� �Լ�
    /// <br></br>������ ��������� ���� ���ð��� �ǵ����� ���������� ������ ����
    /// ���ӸŴ����� ����ȭ��Ŵ 
    /// <br></br>
    /// <br><b>���� : </b></br> ���ӸŴ����� �������ִ� �ν��Ͻ��� �����Ͽ�
    /// DungeonOS�� �������ִ� �Ϻΰ��� �־���. 
    /// </summary>
    void DungeonEnd()
    {
        DungeonCtrl.dungeonUI.SetActive(false);
        GameManager.instance.dungeonOS = null;
        // ������ ���� 
        // ���̺� 1ȸ ����

        for (int i = 0; i < 4; i++)
        {
            if (GameManager.instance.partySlot[i] != null)
            {
                //GameManager.instance.partySlot[i].exp = partyUnit[i].exp;
            }
        }

        if(rewardCardBox.Count != 0)
        {
            foreach (var item in rewardCardBox)
            {
                if (GameManager.instance.currentCardList[item] == null)
                {
                    GameManager.instance.currentCardList.Add(item, new CardDataBase.Data(item));
                }
                else
                {
                    GameManager.instance.currentCardList[item].cardCount++;
                }
            }
        }
        if (rewardRelicBox.Count != 0)
        {
            foreach (var item in rewardRelicBox)
            {
                if (GameManager.instance.currentRelicList[item] == null)
                {
                    GameManager.instance.currentRelicList.Add(item, new RelicData.Data(item));
                }
                else
                {
                    //GameManager.instance.currentRelicList[item].overlapValueA += accrueGoldDGP;
                    //GameManager.instance.currentRelicList[item].overlapValueB += accrueSoulDGP;
                }
            }
        }
        if (rewardCardBox.Count != 0)
        {
            foreach (var item in rewardHeroBox)
            {
                if (GameManager.instance.currentHeroList[item] == null)
                {
                    GameManager.instance.currentHeroList.Add(item, new CharacterDatabase.Data(item));
                }
                else
                {
                    GameManager.instance.currentHeroList[item].overlapValueA += accrueGoldDGP;
                    GameManager.instance.currentHeroList[item].overlapValueB += accrueSoulDGP;
                }
            }
        }
        GameManager.instance.data.souls += accrueSoulDGP;
        GameManager.instance.data.golds += accrueGoldDGP;
    }
    #endregion
    #region ���� ���� ���
    /// <summary>
    /// ���������� �߻��� ������ ���� �����ϱ����� ����Ʈ ���
    /// <br><b>����</b> : ������ �߻��� �������� �ؽ�Ʈ�� �Է¹޾Ƽ�
    /// ����Ʈ�� �����ͻ󿡼��� Ȯ���Ҽ� �ְ� �ص�.</br> 
    /// <br></br>[errorList] : ��Ʈ������ �Ѱܹ��� �������ڵ��� �����ϴ°�
    /// </summary>
    /// <param name="str"></param>
    public void GameError(string str)
    {
        errorList.Add(str);
        foreach (var item in errorList)
        {
            Debug.Log(errorList);
        }
    }
    #endregion


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerUnitSetting();
            MonsterSetting();
        }
    }
}
