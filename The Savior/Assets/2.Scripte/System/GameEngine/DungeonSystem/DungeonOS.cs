using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOS : MonoBehaviour
{

    #region ȯ�� ����
    [Header("ȯ�� ����")]
    //private PlayUIManager PUIManager;
    private GameObject DGUI;
    private GameObject timerArrowDG;
    private GameObject[] timerlevelDG;
    #endregion

    #region ���� �⺻ ������
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
    public List<int> monsterGroupDG;
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
    public float progressTimeDGP;
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
    /// ��Ƽ�� ������ ���� ����
    /// </summary>
    public int[] unitGroupDGP;
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
    public CharacterDatabase.CH_Info[] PartyDGP = 
        { 
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0)
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
        StageReset(checkCountDGGame);
    }
    void StageReset(int stageNum)
    {
        
    }

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
    public void DGTimerStart() 
    {
        progressTimeDGP = 0;
        timerOnDGP = true;
        timeLevelDGP = 0;
        StartCoroutine(DGTimer());
    }

    public IEnumerator DGTimer()
    {
        while (timerOnDGP)
        {
            progressTimeDGP += Time.deltaTime;
            if((progressTimeDGP % 2) >= 1)
            {
                DGTimerUIReset();
            }
            if(progressTimeDGP >= 20)
            {
                timeLevelDGP++;
                if (progressTimeDGP >= 100)
                {
                    DGTimerEnd();
                }
            }
        }
        yield return null;
    }

    public void DGTimerEnd()
    {
        timerOnDGP = false;
    }
    public void DGTimerUIReset()
    {
        
    }
    #endregion
}
