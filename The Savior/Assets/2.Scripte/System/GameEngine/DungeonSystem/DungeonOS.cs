using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOS : MonoBehaviour
{
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

    List<MonsterDatabase.InfoMonster> monsterBox = new List<MonsterDatabase.InfoMonster>();

    List<CharacterDatabase.InfoCharacter> stageSlotPlayerBottom;
    List<CharacterDatabase.InfoCharacter> stageSlotPlayerTop;
    List<CharacterDatabase.InfoCharacter> stageSlotPlayerMid;

    //���� ���õ� ���� : DG
    /// <summary>
    /// ������ �������ִ� ��� �������� �׷�
    /// </summary>
    [Header("��������")]
    public GameObject[] stageGroupDG;
    /// <summary>
    /// ���� ������ ������� ��������
    /// </summary>
    public GameObject slotStageDG;

    public GameObject playerStagePointGroup;
    public GameObject monsterStagePointGroup;
    private Transform[] playerStagePoint;
    private Transform[] monsterStagePoint;


    /// <summary>
    /// �����尡 �������ִ� ����
    /// <br>1. �Ϲ�</br>
    /// <br>2. �߰�������</br>
    /// <br>3. �̺�Ʈ��</br>
    /// <br>4. Ư��������</br>
    /// <br>5. ����Ʈ��</br>
    /// <br>10. ������ </br>
    /// </summary>
    [Header("�������� ����")]
    public int[] roundInfoDG;
    /// <summary>
    /// ���� ���忡 �������ִ� ���� �׷�
    /// </summary>
    public List<MonsterDatabase.InfoMonster> monsterGroup = new List<MonsterDatabase.InfoMonster>();
    /// <summary>
    /// �÷��̾� ���� �׷�
    /// </summary>
    public List<CharacterDatabase.InfoCharacter> characterGroup = new List<CharacterDatabase.InfoCharacter>(); // ������Ʈ�� ����
    /// <summary>
    /// ���� �б� Ȯ�� ���������� ������� ����ֱ⶧����, ���Ӻб� ���ý�Ű�� ����
    /// </summary>
    public int checkCountDGGame;
    
    //�����ȿ��� �÷��̾���õ� ���� : DGP
    /// <summary>
    /// ���� ���� 
    /// </summary>
    public int roundDGP;
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
            progressTimeDGP = value;
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
    public int timeLevelDGP;
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
    /// <br>1. �������</br>
    /// <br>2. �׾���</br>
    /// <br>3. Ư������(����..)</br>
    /// </summary>
    public int[] eaIsDieDGP;
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
    #endregion

    #region ���޹��� GameManager�� Data
    //ĳ���� ���� 
    public CharacterDatabase.InfoCharacter[] partyUnit = 
        { 
            new CharacterDatabase.InfoCharacter(0),
            new CharacterDatabase.InfoCharacter(0),
            new CharacterDatabase.InfoCharacter(0),
            new CharacterDatabase.InfoCharacter(0)
        };
    //������
    public List<int> useDeckDGP;
    //���� ����
    //��ȸ ���� ���� �����Ȳ�����ʹ� ����
    bool ClearkCheck;
    #endregion
    //ī���� ����... ī�� �����ͺ��̽��� ��������. 
    void Start()
    {
        #region ĳ��ó�� //��ĥ�� �ٽ��ѹ� �����������..
        DungeonCtrl = DungeonController.instance;
        DungeonDatabase.InfoDungeon infoDungeon = new DungeonDatabase.InfoDungeon(dungeonNumber);
        monsterBox = infoDungeon.dungeonMonsterBox;
        //PUIManager = GameObject.Find("PUIManaer").GetComponent<PlayUIManager>();
        //DGUI = PUIManager.DungeonUI;
        //DGtimerArrow = PUIManager.DungeonTimerArrow;
        //DGtimerlevel = PUIManager.DungeonTimerColor;

        playerStagePoint = playerStagePointGroup.GetComponentsInChildren<Transform>();
        monsterStagePoint = monsterStagePointGroup.GetComponentsInChildren<Transform>();
        #endregion
        //���ӿ� UI Ȱ��ȭ 
        DungeonCtrl.dungeonUI.SetActive(true);
        GameSetting();
    }

    #region ���� UI ó��
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
    #region ���� ����
    /// <summary>
    /// ������ �����ϰ� �������� �⺻������ �����Ҷ� ���
    /// </summary>
    void GameSetting()
    {
        DeckShuffle();
        PlayerUnitCreate();
        ////�������� ���� �ѹ� ������. 
        StageReset(checkCountDGGame);
    }
    /// <summary>
    /// ���������� �����ǰ� ����Ǵ� ���
    /// �Ű������� ���° ���������� �������� ����
    /// </summary>
    /// <param name="stageNum"></param>
    void StageReset(int stageNum)
    {
        DGTimerStart();
        PlayerUnitSetting();
    }

    /// <summary>
    /// �÷��̾����� �������� �ڵ���ġ
    /// </summary>
    void PlayerUnitSetting()
    {
        foreach (var item in partyUnit)
        {
            switch (item.attackTypeCH)
            {
                case 1:
                    if (stageSlotPlayerBottom.Count < 3)
                    {
                        stageSlotPlayerBottom.Add(item);
                    }
                    else
                    {
                        CharacterDatabase.InfoCharacter moveSlot = item;
                        CharacterDatabase.InfoCharacter tempSlot = item;
                        //���� �� �о��� �ڸ���ġ // ������ġ�� �켱
                        for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
                        {
                            if (stageSlotPlayerBottom[i].positionPerCH > moveSlot.positionPerCH)
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
                                if (stageSlotPlayerMid[i].positionPerCH > moveSlot.positionPerCH)
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
                        CharacterDatabase.InfoCharacter moveSlot = item;
                        CharacterDatabase.InfoCharacter tempSlot = item;
                        // ��ġ�� ���� ��� 
                        if (item.positionPerCH >= 30)
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPerCH > moveSlot.positionPerCH)
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
                                    if (stageSlotPlayerBottom[i].positionPerCH > moveSlot.positionPerCH)
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
                                if (stageSlotPlayerMid[i].positionPerCH < moveSlot.positionPerCH)
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
                                    if (stageSlotPlayerTop[i].positionPerCH < moveSlot.positionPerCH)
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
                        CharacterDatabase.InfoCharacter moveSlot = item;
                        CharacterDatabase.InfoCharacter tempSlot = item;
                        //���� �� �о��� �ڸ���ġ // ū��ġ�� �켱
                        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
                        {
                            if (stageSlotPlayerTop[i].positionPerCH < moveSlot.positionPerCH)
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
                                if (stageSlotPlayerMid[i].positionPerCH < moveSlot.positionPerCH)
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
            stageSlotPlayerBottom[i].objectCH.transform.position = playerStagePoint[i + 1].position;
        }
        for (int i = 0; i < stageSlotPlayerMid.Count; i++)
        {
            stageSlotPlayerMid[i].objectCH.transform.position = playerStagePoint[i + 4].position;
        }
        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
        {
            stageSlotPlayerTop[i].objectCH.transform.position = playerStagePoint[i + 7].position;
        }
    }
    /// <summary>
    /// �÷��̾� ���� ������Ʈȭ
    /// </summary>
    void PlayerUnitCreate()
    {
        foreach (var item in partyUnit)
        {
            item.objectCH = Instantiate(item.objectCH);
        }
    }
    /// <summary>
    /// ���� ���� ������Ʈȭ
    /// </summary>
    void MonsterCreate()
    {
        
    }

    /// <summary>
    /// ���� ������, �޽� Ÿ�ֿ̹� �� ���� ���
    /// </summary>
    void DeckShuffle()
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
    #endregion
    #region ���� Ÿ�̸� ���
    /// <summary>
    /// Ÿ�̸� ���� ���
    /// </summary>
    public void DGTimerStart() 
    {
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
    /// Ÿ�̸��� ȸ���ϴ� UI Ȯ�� 
    /// </summary>
    public void DGTimerUIReset()
    {
        DungeonCtrl.gameTimerBG[timeLevelDGP].fillAmount = (20 - (progressTimeDGP % 20)) * 0.05f;
    }
    #endregion
    #region ���� ����
    /// <summary>
    /// ���� ����� �۵� // ���â���� �������ư Ŭ���� ����Ǵ� �Լ�
    /// </summary>
    void DungeonEnd()
    {
        DungeonCtrl.dungeonUI.SetActive(false);
        // ������ ���� 
        // ���̺� 1ȸ ����
    }
    #endregion
    #region ���� ���� ���
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
