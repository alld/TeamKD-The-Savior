using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOS : MonoBehaviour
{

    #region ȯ�� ����
    [Header("ȯ�� ����")]
    //private PlayUIManager PUIManager;
    private GameObject timerArrowDG;
    private GameObject[] timerlevelDG;
    private WaitForSeconds delay = new WaitForSeconds(0.1f);
    private DungeonController DungeonCtrl;
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
    public CharacterDatabase.InfoCharacter[] PartyDGP = 
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
        //PUIManager = GameObject.Find("PUIManaer").GetComponent<PlayUIManager>();
        //DGUI = PUIManager.DungeonUI;
        //DGtimerArrow = PUIManager.DungeonTimerArrow;
        //DGtimerlevel = PUIManager.DungeonTimerColor;
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
        while (timerOnDGP)
        {
            progressTimeDGP += Time.deltaTime;

            if (progressTimeDGP < 20)
            {
                timeLevelDGP = 1;
                DGTimerUIReset();
            }
            else if (progressTimeDGP < 40)
            {
                timeLevelDGP = 2;
                DGTimerUIReset();
            }
            else
            {
                timeLevelDGP = 3;
            }
            
        }
        yield return null;
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
}
