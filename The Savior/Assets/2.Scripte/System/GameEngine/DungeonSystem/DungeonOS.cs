using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOS : MonoBehaviour
{

    #region ȯ�� ����
    [Header("ȯ�� ����")]
    //private PlayUIManager PUIManager;
    private GameObject DGUI;
    private GameObject DGtimerArrow;
    private GameObject[] DGtimerlevel;
    #endregion

    #region ���� �⺻ ������
    //���� ���õ� ���� : DG

    /// <summary>
    /// ������ �������ִ� ��� �������� �׷�
    /// </summary>
    [Header("��������")]
    public GameObject[] DG_stageGroup;
    /// <summary>
    /// ���� ������ ������� ��������
    /// </summary>
    public GameObject DG_slotStage;
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
    public int[] DG_roundInfo;
    /// <summary>
    /// ���� ���忡 �������ִ� ���� �׷�
    /// </summary>
    public List<int> DG_monsterGroup;
    /// <summary>
    /// ���� �б� Ȯ�� ���������� ������� ����ֱ⶧����, ���Ӻб� ���ý�Ű�� ����
    /// </summary>
    public int DGGame_checkCount;
    
    //�����ȿ��� �÷��̾���õ� ���� : DGP
    /// <summary>
    /// ���� ���� 
    /// </summary>
    public int DGP_round;
    /// <summary>
    /// ���� ���� ���� �������
    /// </summary>
    public int DGP_accrueGold;
    /// <summary>
    /// ������� ���� ���� �ҿ�(������ ���� �ҿ� ����)
    /// </summary>
    public int DGP_accrueSoul;
    /// <summary>
    /// ���� ������ �ڽ�Ʈ (�ִ�ġ 10)
    /// </summary>
    public int DGP_cost;
    /// <summary>
    /// ���� ���� ���ӵ� �ð� 
    /// </summary>
    public float DGP_progressTime;
    /// <summary>
    /// �ð� �帧�� ���� �ð��ܰ踦 ǥ�� 
    /// <br>0. �ʹ�</br>
    /// <br>1. �߹�</br>
    /// <br>2. �Ĺ�</br>
    /// </summary>
    public bool DGP_timerOn;
    public int DGP_timeLevel;
    /// <summary>
    /// ���� ī�� (���ܷ�)
    /// </summary>
    public int DGP_remainingCard;
    /// <summary>
    /// ��Ƽ�� ������ ���� ����
    /// </summary>
    public int[] DGP_unitGroup;
    /// <summary>
    /// ���������� ���� �ҿ� ����
    /// </summary>
    public int[] DGP_eaGetSoul;
    /// <summary>
    /// ��Ƽ �׷��� ��� ���� �Ǵ�
    /// <br>1. �������</br>
    /// <br>2. �׾���</br>
    /// <br>3. Ư������(����..)</br>
    /// </summary>
    public int[] DGP_eaIsDie;
    /// <summary>
    /// [��赥����] ���� ���� �ǰݷ�
    /// </summary>
    public float[] DGP_eaDamaged;
    /// <summary>
    /// [��赥����] ���� ���� ��������
    /// </summary>
    public float[] DGP_eaDamage;
    /// <summary>
    /// [��赥����] ���� ���� ų��
    /// </summary>
    public float[] DGP_eaKillCount;
    #endregion

    #region ���޹��� GameManager�� Data
    //ĳ���� ���� 
    public CharacterDatabase.CH_Info[] DGP_Party = 
        { 
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0)
        };
    //������
    public List<int> DGP_useDeck;
    //���� ����
    //��ȸ ���� ���� �����Ȳ�����ʹ� ����
    bool ClearkCheck;
    #endregion
    //ī���� ����... ī�� �����ͺ��̽��� ��������. 
    void Start()
    {
        #region ĳ��ó�� //��ĥ�� �ٽ��ѹ� �����������..
        //PUIManager = GameObject.Find("PUIManaer").GetComponent<PlayUIManager>();
        //DGUI = PUIManager.DungeonUI;
        //DGtimerArrow = PUIManager.DungeonTimerArrow;
        //DGtimerlevel = PUIManager.DungeonTimerColor;
        #endregion
        //���ӿ� UI Ȱ��ȭ 
        DGUI.SetActive(true);
        GameSetting();
    }


    #region ���� ����
    void GameSetting()
    {
        DeckShuffle();
        ////�������� ���� �ѹ� ������. 
        StageReset(DGGame_checkCount);
    }
    void StageReset(int stageNum)
    {
        
    }

    void DeckShuffle()
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < DGP_useDeck.Count; i++)
        {
            int tempA = Random.Range(0, DGP_useDeck.Count);
            tempList.Add(DGP_useDeck[tempA]);
            DGP_useDeck.RemoveAt(tempA);
        }
        DGP_useDeck = tempList;
    }
    #endregion
    #region ���� Ÿ�̸� ���
    public void DGTimerStart() 
    {
        DGP_progressTime = 0;
        DGP_timerOn = true;
        DGP_timeLevel = 0;
        StartCoroutine(DGTimer());
    }

    public IEnumerator DGTimer()
    {
        while (DGP_timerOn)
        {
            DGP_progressTime += Time.deltaTime;
            if((DGP_progressTime % 2) >= 1)
            {
                DGTimerUIReset();
            }
            if(DGP_progressTime >= 20)
            {
                DGP_timeLevel++;
                if (DGP_progressTime >= 100)
                {
                    DGTimerEnd();
                }
            }
        }
        yield return null;
    }

    public void DGTimerEnd()
    {
        DGP_timerOn = false;
    }
    public void DGTimerUIReset()
    {
        
    }
    #endregion
}
