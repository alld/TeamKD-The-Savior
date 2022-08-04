using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapDungeon : MonoBehaviour
{
    DungeonInfo info;

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
    public Transform bossSlotTr1;
    public Transform rewardSlotTr1;
    public Transform rewardSlotTr2;

    [Header("���� �̹��� �� �Ӽ�")]
    public Transform dungeonSlot;

    private Image dungeonImg;

    void Start()
    {
        info = GameObject.Find("DungeonInfo").GetComponent<DungeonInfo>();

        dg0_Button.onClick.AddListener(() => OnClick_TutorialDungeonBtn());
        closeDungeon0_Button.onClick.AddListener(() => OnClick_CloseTutorialDungeonBtn());
        startDungeon0_Button.onClick.AddListener(() => OnClick_StartTutorialDungeonBtn());
    }

    /// <summary>
    /// Ʃ�丮�� ���� �̹��� Ȱ��ȭ.
    /// <br>���� �̹��� Ȱ��ȭ �ÿ� �ش� ������ �´� �����ܰ� ���� �̹����� �־���� ��.</br>
    /// </summary>
    private void OnClick_TutorialDungeonBtn()
    {
        dungeonImg = Instantiate(info.dungeonImg[0].GetComponent<Image>());
        //dungeonSlot.SetParent(dungeonImg.transform);

        dungeonImg.transform.SetParent(dungeonSlot);
        InitRectSize(dungeonImg);

        firstDungeon.SetActive(true);
    }

    private void InitRectSize(Image img)
    {
        
    }

    /// <summary>
    /// Ʃ�丮�� ���� �̹��� �ݱ�.
    /// </summary>
    private void OnClick_CloseTutorialDungeonBtn()
    {
        firstDungeon.SetActive(false);
        Destroy(dungeonImg);
    }

    /// <summary>
    /// Ʃ�丮�� ���� ����.
    /// </summary>
    private void OnClick_StartTutorialDungeonBtn()
    {
        Debug.Log("Start!!!");
    }
}
