using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldMapDungeon : MonoBehaviour
{
    DungeonInfo info;   // ���� ���� ��ũ��Ʈ

    public enum EDungeonName { Grotta, First, Second, Third, Fourth, Fifth }    // ���� �̸�

    [Header("���� ���� ��ư")]
    public Button dg0_Button; // dg0 : Dungeon0
    public Button dg1_Button; // dg1 : Dungeon1
    public Button dg2_Button; // dg2 : Dungeon2
    public Button dg3_Button; // dg3 : Dungeon3
    public Button dg4_Button; // dg4 : Dungeon4
    public Button demonCastleButton;

    [Header("���� �̹��� �ݱ� ��ư")]
    public Button closeDungeon0_Button;

    [Header("���� ���� ��ư")]
    public Button startDungeon0_Button;

    [Header("���� ���� �̹���")]
    public GameObject firstDungeon;

    [Header("���� ���� ���� ���� / ��� ���� ����")]
    public Transform bossSlotTr;
    public Transform rewardSlotTr1;
    public Transform rewardSlotTr2;

    [Header("���� �̹��� �� ����")]
    public Transform dungeonSlot;
    public TMP_Text contactText;

    private Image[] dungeonImg = new Image[4];      // 0 : ���� �̹��� //// 1 : ���� ���� //// 2 : ��� ���� 1 //// 3 : ��� ���� 2
    void Start()
    {
        info = GameObject.Find("DungeonInfo").GetComponent<DungeonInfo>();

        dg0_Button.onClick.AddListener(() => OnClick_EnterTheGrottaBtn());
        closeDungeon0_Button.onClick.AddListener(() => OnClick_CloseGrottaBtn());
        startDungeon0_Button.onClick.AddListener(() => OnClick_StartGrottaBtn());
    }

    /// <summary>
    /// Ʃ�丮�� ���� �̹��� Ȱ��ȭ.
    /// <br>���� �̹��� Ȱ��ȭ �ÿ� �ش� ������ �´� �����ܰ� ���� �̹����� �־���� ��.</br>
    /// </summary>
    private void OnClick_EnterTheGrottaBtn()
    {
        // �̹��� ����
        dungeonImg[0] = Instantiate(info.dungeonImg[(int)EDungeonName.Grotta].GetComponent<Image>());
        dungeonImg[1] = Instantiate(info.bossImg[(int)EDungeonName.Grotta].GetComponent<Image>());
        dungeonImg[2] = Instantiate(info.rewardImg1[(int)EDungeonName.Grotta].GetComponent<Image>());
        dungeonImg[3] = Instantiate(info.rewardImg2[(int)EDungeonName.Grotta].GetComponent<Image>());

        // �̹��� ����
        dungeonImg[0].transform.SetParent(dungeonSlot);
        dungeonImg[1].transform.SetParent(bossSlotTr);
        dungeonImg[2].transform.SetParent(rewardSlotTr1);
        dungeonImg[3].transform.SetParent(rewardSlotTr2);

        // ������ ����
        InitRectSize(dungeonImg[0]);
        InitRectSize(dungeonImg[1]);
        InitRectSize(dungeonImg[2]);
        InitRectSize(dungeonImg[3]);

        // ���� ����
        contactText.text = info.contactDungeon[(int)EDungeonName.Grotta];

        // ���� �̹��� Ȱ��ȭ.
        firstDungeon.SetActive(true);
    }

    /// <summary>
    /// ��Ʈ��ġ�� ����� �̹����� ����� �θ� ��ü�� ����� ����.
    /// </summary>
    /// <param name="img"></param>
    private void InitRectSize(Image img)
    {
        img.rectTransform.offsetMin = Vector2.zero;
        img.rectTransform.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// Ʃ�丮�� ���� �̹��� �ݱ�.
    /// </summary>
    private void OnClick_CloseGrottaBtn()
    {
        firstDungeon.SetActive(false);
        Destroy(dungeonImg[0]);
        Destroy(dungeonImg[1]);
        Destroy(dungeonImg[2]);
        Destroy(dungeonImg[3]);
    }

    /// <summary>
    /// Ʃ�丮�� ���� ����.
    /// </summary>
    private void OnClick_StartGrottaBtn()
    {
        Debug.Log("Start!!!");
    }
}
