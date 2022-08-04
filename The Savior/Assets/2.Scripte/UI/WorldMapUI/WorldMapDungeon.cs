using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldMapDungeon : MonoBehaviour
{
    DungeonInfo info;   // ���� ���� ��ũ��Ʈ

    public enum EDungeonName { Grotta, First, Second, Third, Fourth, Fifth }    // ���� �̸�
    public EDungeonName curDungeon = EDungeonName.Grotta;

    [Header("���� ���� ��ư")]
    public Button[] EnterTheDungeonButton = new Button[6];

    [Header("���� �̹��� �ݱ� ��ư")]
    public Button closeDungeonButton;

    [Header("���� ���� ��ư")]
    public Button startDungeonButton;

    [Header("���� ���� �̹���")]
    public GameObject dungeon;

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

        // Ʃ�丮�� ����
        EnterTheDungeonButton[(int)EDungeonName.Grotta].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(0));
        // ù ��° ����
        EnterTheDungeonButton[(int)EDungeonName.First].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(1));
        // �� ��° ����
        EnterTheDungeonButton[(int)EDungeonName.Second].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(2));
        // �� ��° ����
        EnterTheDungeonButton[(int)EDungeonName.Third].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(3));
        // �� ��° ����
        EnterTheDungeonButton[(int)EDungeonName.Fourth].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(4));
        // ���ռ� ����
        EnterTheDungeonButton[(int)EDungeonName.Fifth].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(5));

        // ����â �ݱ�, ���� ����.
        closeDungeonButton.onClick.AddListener(() => OnClick_CloseDungeonBtn());
        startDungeonButton.onClick.AddListener(() => OnClick_StartDungeonBtn());

    }

    /// <summary>
    /// Ʃ�丮�� ���� �̹��� Ȱ��ȭ.
    /// <br>���� �̹��� Ȱ��ȭ �ÿ� �ش� ������ �´� �����ܰ� ���� �̹����� �־���� ��.</br>
    /// </summary>
    private void OnClick_EnterTheDungeonBtn(int idx)
    {
        // �̹��� ����
        dungeonImg[0] = Instantiate(info.dungeonImg[idx].GetComponent<Image>());
        dungeonImg[1] = Instantiate(info.bossImg[idx].GetComponent<Image>());
        dungeonImg[2] = Instantiate(info.rewardImg1[idx].GetComponent<Image>());
        dungeonImg[3] = Instantiate(info.rewardImg2[idx].GetComponent<Image>());

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
        contactText.text = info.contactDungeon[idx];

        curDungeon = (EDungeonName)idx;

        // ���� �̹��� Ȱ��ȭ.
        dungeon.SetActive(true);
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
    private void OnClick_CloseDungeonBtn()
    {
        dungeon.SetActive(false);
        Destroy(dungeonImg[0]);
        Destroy(dungeonImg[1]);
        Destroy(dungeonImg[2]);
        Destroy(dungeonImg[3]);
    }

    /// <summary>
    /// Ʃ�丮�� ���� ����.
    /// </summary>
    private void OnClick_StartDungeonBtn()
    {
        Debug.Log("EnterTheDungeon : " + curDungeon);
    }
}
