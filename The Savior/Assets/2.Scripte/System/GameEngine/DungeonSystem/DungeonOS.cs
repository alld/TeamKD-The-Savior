using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleJSON;
using UnityEngine.UI;

public class DungeonOS : MonoBehaviour
{
    public static DungeonOS instance = null;
    public GameObject DungeonOSObj = null;
    #region ȯ�� ����
    [Header("ȯ�� ����")]
    public int dungeonNumber = 0;
    //private PlayUIManager PUIManager;
    private GameObject timerArrowDG;
    private GameObject[] timerlevelUI;
    private WaitForSeconds delay = new WaitForSeconds(0.1f);
    private DungeonController DungeonCtrl;
    public List<string> errorList;
    #endregion

    #region ���� �⺻ ������

    List<UnitTable> monsterBox = new List<UnitTable>();
    List<int> rewardHeroBox = new List<int>();
    List<int> rewardCardBox = new List<int>();
    List<int> rewardRelicBox = new List<int>();

    List<UnitTable> stageSlotPlayerBottom = new List<UnitTable>(); 
    List<UnitTable> stageSlotPlayerTop = new List<UnitTable>();
    List<UnitTable> stageSlotPlayerMid = new List<UnitTable>();

    List<UnitTable> stageSlotMonsterBottom = new List<UnitTable>();
    List<UnitTable> stageSlotMonsterTop = new List<UnitTable>();
    List<UnitTable> stageSlotMonsterMid = new List<UnitTable>();


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
    public int[] monsterBoxMin; // �����������
    public int[] monsterBoxMax; // �����������
    public int[] monsterBoxCount; // �����������
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
    public List<UnitTable> monsterGroup = new List<UnitTable>();
    /// <summary>
    /// �÷��̾� ���� �׷�
    /// </summary>
    public List<UnitTable> characterGroup = new List<UnitTable>(); // ������Ʈ�� ����
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
    public bool isRoundPlaying
    {
        get { return isRoundPlaying; }
        set
        {
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
    public int costDGP;
    /// <summary>
    /// ���� ���� ���ӵ� �ð� 
    /// </summary>
    public float progressTimeDGP 
    {
        get { return progressTimeDGP; }
        set 
        { 
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
    public int timeLevelDGP
    {
        get { return timeLevelDGP; }
        set
        {
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

    public List<CardDataBase> handCard = new List<CardDataBase>();
    #endregion

    #region ���� ����ġ ������
    // ����ġ Ƚ�� üũ ����
    public int addCount;
    public class WeightUnit
    {
        // �Ʊ�
        /// <summary>
        /// ����ġ :: �߰� ���ݷ�
        /// </summary>
        public float Add_damage;
        /// <summary>
        /// ����ġ :: ���� �߰� ���ݷ�
        /// </summary>
        public float Add_fianlDamage = 1;
        /// <summary>
        /// ����ġ :: ���ط� ���ҷ�
        /// </summary>
        public float Add_dropDamage = 1;
        /// <summary>
        /// ����ġ(�߰��ɷ�) :: ��ȣ�� ��ġ
        /// </summary>
        public float Current_protect;
        /// <summary>
        /// ����ġ(�߰��ɷ�) :: ��ȣ�� �ִ� ��ġ
        /// </summary>
        public float Current_protectMax;
        /// <summary>
        /// ����ġ :: ���� �ӵ�
        /// </summary>
        public float Add_attackSpeed;
        /// <summary>
        /// ����ġ :: �̵� �ӵ�
        /// </summary>
        public float Add_moveSpeed;
        /// <summary>
        /// ����ġ :: ����
        /// </summary>
        public float Add_defense;
        /// <summary>
        /// ����ġ :: ���� ����(��Ÿ�)
        /// </summary>
        public float Add_attackRange;
        /// <summary>
        /// ����ġ :: �Ӽ� ���� ���� (bool)
        /// </summary>
        public bool Add_attributeCheck;
        /// <summary>
        /// ����ġ :: ����� �Ӽ��� 
        /// </summary>
        public int Add_attribute;
        /// <summary>
        /// ����ġ :: �Ӽ� �߰� ������
        /// </summary>
        public float[] Add_attributeVlaue = {1, 1,1,1 };
        /// <summary>
        /// ����ġ :: ���� �ν� ����
        /// </summary>
        public float Add_priRange;
        /// <summary>
        /// ����ġ :: ���� �켱��
        /// </summary>
        public int Add_priorities;
        /// <summary>
        /// ����ġ :: ��ų ��ٿ�
        /// </summary>
        public float Add_skilcoldown = 1;
        /// <summary>
        /// ����ġ :: ���� ����
        /// </summary>
        public bool isinvincible;
        public List<BuffDataBase> Current_buff = new List<BuffDataBase>();
    }

    public class WeightEnemy : WeightUnit
    {
        // ��
        public int Add_rewardGold;
        public int Add_rewardSoul;
    }
    public WeightUnit weightAlly = new WeightUnit();
    public WeightUnit[] weightAllyUnit = new WeightUnit[3];
    public WeightEnemy weightEnemy = new WeightEnemy();
    public List<WeightEnemy> weightEnemyGroup = new List<WeightEnemy>();
    #endregion

    #region ���޹��� GameManager�� Data
    //ĳ���� ���� 
    //public CharacterDatabase.InfoCharacter[] partyUnit = 
    //    { 
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[0].number),
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[1].number),
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[2].number),
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[3].number)
    //    };
    public List<UnitTable> partyUnit = new List<UnitTable>();
    public List<RelicData> equipRelic = new List<RelicData>();
    //������
    public List<int> useDeckDGP = new List<int>();
    //���� ����
    //��ȸ ���� ���� �����Ȳ�����ʹ� ����
    bool ClearkCheck;
    #endregion
    private TextAsset jsonData;
    private string jsonText;
    private JSONNode jsonCH;

    private void Awake()
    {
        instance = this; 
        jsonData = Resources.Load<TextAsset>("CharacterData");
        jsonText = jsonData.text;
        jsonCH = JSON.Parse(jsonText);
    }
    //ī���� ����... ī�� �����ͺ��̽��� ��������. 
    void Start()
    {
        #region ĳ��ó�� //��ĥ�� �ٽ��ѹ� �����������..
        DungeonCtrl = DungeonController.instance;
        DungeonDatabase.InfoDungeon infoDungeon = new DungeonDatabase.InfoDungeon(dungeonNumber);
        foreach (var item in infoDungeon.dungeonMonsterBox)
        {
            monsterBox.Add(new UnitTable(item));
        }
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
        DungeonCtrl.playerCostGauage.fillAmount = costDGP / 10;
        DungeonCtrl.playerExpectationsGauage.fillAmount = costDGP / 10;
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
            yield return delay;
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
            yield return delay;
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
        int tempCharNumber;
        for (int i = 0; i < 4; i++) // ���� ������ ó�� ��� ���濡���� ���ļ� ����
        {
            tempCharNumber = GameManager.instance.data.equipCharacter[i];
            if (tempCharNumber != 0)
            {
                partyUnit.Add(new UnitTable(new CharacterDatabase(tempCharNumber)));
                partyUnit[partyUnit.Count-1].isLive = true;
            }
        }
        foreach (var item in GameManager.instance.data.equipRelic)
        {
            equipRelic.Add(new RelicData(item));
        }

        int temp = 0;
        foreach (var item in stagePrefabGroupDG)
        {
            stageGroupDG[temp++] = Instantiate(item);
        }
        HandReset();
        PlayerUnitCreate();
        roundDGP = 1;
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
        slotStageDG?.SetActive(false);
        slotStageDG = stageGroupDG[stageNum];
        slotStageDG.SetActive(true);
        Camera.main.transform.position = slotStageDG.GetComponentInChildren<Camera>().transform.position;
        Camera.main.transform.rotation = slotStageDG.GetComponentInChildren<Camera>().transform.rotation;
        DGTimerStart();
        PlayerUnitSetting();
        MonsterCreate();
        MonsterSetting();

    }

    /// <summary>
    /// �÷��̾����� �������� �ڵ���ġ
    /// </summary>
    void PlayerUnitSetting()
    {
        foreach (var item in partyUnit)
        {
            switch (item.attackType)
            {
                case 1:
                    if (stageSlotPlayerBottom.Count < 3)
                    {
                        stageSlotPlayerBottom.Add(item);
                    }
                    else
                    {
                        UnitTable moveSlot = item;
                        UnitTable tempSlot = item;
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
                        if(stageSlotPlayerMid.Count < 3)
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
                        UnitTable moveSlot = item;
                        UnitTable tempSlot = item;
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
                        UnitTable moveSlot = item;
                        UnitTable tempSlot = item;
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
    }
    /// <summary>
    /// �÷��̾� ���� ������Ʈȭ
    /// </summary>
    void PlayerUnitCreate()
    {
        foreach (var item in partyUnit.Select((value, index) => new { value, index }))
        {
            item.value.charObject = Instantiate(item.value.gameObject);
            item.value.GetComponent<UnitAI>().unitNumber = item.value.number;
            item.value.GetComponent<UnitAI>().partyNumber = item.index;
        }
    }

    /// <summary>
    /// ���� �������� �ڵ���ġ
    /// </summary>
    void MonsterSetting()
    {
        foreach (var item in monsterGroup)
        {
            switch (item.attackType)
            {
                case 1:
                    if (stageSlotMonsterBottom.Count < 4)
                    {
                        stageSlotMonsterBottom.Add(item);
                    }
                    else
                    {
                        UnitTable moveSlot = item;
                        UnitTable tempSlot = item;
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
                        UnitTable moveSlot = item;
                        UnitTable tempSlot = item;
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
                        UnitTable moveSlot = item;
                        UnitTable tempSlot = item;
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
    }


    /// <summary>
    /// ���� ���� ������Ʈȭ
    /// </summary>
    void MonsterCreate()
    {
        for (int i = 0; i < monsterBoxCount[roundDGP]; i++)
        {
            int tempint = Random.Range(monsterBoxMin[roundDGP], monsterBoxMax[roundDGP]);
            monsterGroup.Add(new UnitTable(new MonsterDatabase(monsterBox[tempint].number)));
            monsterGroup[i].charObject = Instantiate(monsterGroup[i].charObject);
            monsterGroup[i].GetComponent<UnitAI>().unitNumber = monsterGroup[i].number;
            monsterGroup[i].GetComponent<UnitAI>().partyNumber = i;
        }
        if (roundDGP % 10 == 5)
        {
            monsterGroup.Add(new UnitTable(new MonsterDatabase(monsterBox[1].number)));
            monsterGroup[monsterBox.Count].charObject = Instantiate(monsterBox[1].charObject);
            monsterGroup[monsterBox.Count].gameObject.transform.position = monsterStagePoint[1].position;
            monsterGroup[monsterBox.Count].gameObject.transform.rotation = monsterStagePoint[1].rotation;
        }
        else if (roundDGP % 10 == 0)
        {
            monsterGroup.Add(new UnitTable(new MonsterDatabase(monsterBox[0].number)));
            monsterGroup[monsterBox.Count].charObject = Instantiate(monsterBox[0].charObject);
            monsterGroup[monsterBox.Count].gameObject.transform.position = monsterStagePoint[1].position;
            monsterGroup[monsterBox.Count].gameObject.transform.rotation = monsterStagePoint[1].rotation;
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
        switch(GameManager.instance.data.presset)
        {
            case 1:
                useDeckDGP.AddRange(GameManager.instance.data.equipCard1);
                break;
            case 2:
                useDeckDGP.AddRange(GameManager.instance.data.equipCard2);
                break;
            case 3:
                useDeckDGP.AddRange(GameManager.instance.data.equipCard3);
                break;
            case 4:
                useDeckDGP.AddRange(GameManager.instance.data.equipCard4);
                break;
            case 5:
                useDeckDGP.AddRange(GameManager.instance.data.equipCard5);
                break;
        }
        DeckShuffle();
        handCard.Clear();
        
        HandRefill();
    }

    public void HandRefill()
    {
        for (int i = handCard.Count; i < 3; i++)
        {
            if (useDeckDGP.Count != 0)
            {
                CardDataBase card = new CardDataBase(useDeckDGP[0]);
                handCard.Add(card);
                if (handSlot[0])
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[0].transform);
                    card.Icon.gameObject.AddComponent<CardEvent>();
                    card.GetComponent<CardEvent>().card_handnumber = 0;
                    handSlot[0] = true;
                    card.GetComponent<CardEvent>().cost = card.cost;
                }
                else if (handSlot[1])
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[1].transform);
                    card.Icon.gameObject.AddComponent<CardEvent>();
                    card.GetComponent<CardEvent>().card_handnumber = 1;
                    handSlot[1] = true;
                    card.GetComponent<CardEvent>().cost = card.cost;
                }
                else
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[2].transform);
                    card.Icon.gameObject.AddComponent<CardEvent>();
                    card.GetComponent<CardEvent>().card_handnumber = 2;
                    handSlot[2] = true;
                    card.GetComponent<CardEvent>().cost = card.cost;
                }
                card.GetComponent<CardEvent>().card_number = card.number;
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
            CardDataBase card = new CardDataBase(useDeckDGP[0]);
            handCard.Add(card);
            if (handSlot[0])
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[0].transform);
                card.Icon.gameObject.AddComponent<CardEvent>();
                card.GetComponent<CardEvent>().card_handnumber = 0;
                handSlot[0] = true;
                card.GetComponent<CardEvent>().cost = card.cost;
            }
            else if (handSlot[1])
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[1].transform);
                card.Icon.gameObject.AddComponent<CardEvent>();
                card.GetComponent<CardEvent>().card_handnumber = 1;
                handSlot[1] = true;
                card.GetComponent<CardEvent>().cost = card.cost;
            }
            else
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[2].transform);
                card.Icon.gameObject.AddComponent<CardEvent>();
                card.GetComponent<CardEvent>().card_handnumber = 2;
                handSlot[2] = true;
                card.GetComponent<CardEvent>().cost = card.cost;
            }
            card.GetComponent<CardEvent>().card_number = card.number;
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
            yield return null;
            cycleTime += Time.deltaTime;
            progressTimeDGP += Time.deltaTime;
            if (timeLevelDGP == 3)
            {
                DGTimerUIReset();
            }
            if (cycleTime >= 20)
            {
                cycleTime = 0f;
                HandRefill();
                if (costDGP <= 7) costDGP += 3;
                else costDGP = 10;
                HandUIReset();
                
                switch (timeLevelDGP)
                {
                    case 0:
                        timeLevelDGP = 1;
                        DGTimerUIReset();
                        break;
                    case 1:
                        timeLevelDGP = 2;
                        DGTimerUIReset();
                        break;
                    default:
                        timeLevelDGP = 3;
                        break;
                }
            }
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
                GameManager.instance.partySlot[i].exp = partyUnit[i].exp;
            }
        }

        if(rewardCardBox.Count != 0)
        {
            foreach (var item in rewardCardBox)
            {
                if (GameManager.instance.currentCardList[item] == null)
                {
                    GameManager.instance.currentCardList.Add(item, new CardDataBase(item));
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
                    GameManager.instance.currentRelicList.Add(item, new RelicDataBase.InfoRelic(item));
                }
                else
                {
                    GameManager.instance.currentRelicList[item].overlapValueA += accrueGoldDGP;
                    GameManager.instance.currentRelicList[item].overlapValueB += accrueSoulDGP;
                }
            }
        }
        if (rewardCardBox.Count != 0)
        {
            foreach (var item in rewardHeroBox)
            {
                if (GameManager.instance.currentHeroList[item] == null)
                {
                    GameManager.instance.currentHeroList.Add(item, new CharacterDatabase(item));
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

}
