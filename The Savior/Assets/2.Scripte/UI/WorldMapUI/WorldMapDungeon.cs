using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapDungeon : MonoBehaviour
{
    DungeonInfo info;

    [Header("던전 입장 버튼")]
    public Button dg0_Button; // dg0 : Dungeon0
    public Button dg1_Button; // dg1 : Dungeon1
    public Button dg2_Button; // dg2 : Dungeon2
    public Button dg3_Button; // dg3 : Dungeon3
    public Button dg4_Button; // dg4 : Dungeon4
    public Button demonCastleButton;

    [Header("던전 이미지 닫기 버튼")]
    public Button closeDungeon0_Button;

    [Header("던전 시작 버튼")]
    public Button startDungeon0_Button;

    [Header("던전 입장 이미지")]
    public GameObject firstDungeon;

    [Header("던전 출현 몬스터 슬롯 / 기대 보상 슬롯")]
    public Transform bossSlotTr1;
    public Transform rewardSlotTr1;
    public Transform rewardSlotTr2;

    [Header("던전 이미지 및 속성")]
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
    /// 튜토리얼 던전 이미지 활성화.
    /// <br>던전 이미지 활성화 시에 해당 던전에 맞는 아이콘과 던전 이미지를 넣어줘야 함.</br>
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
    /// 튜토리얼 던전 이미지 닫기.
    /// </summary>
    private void OnClick_CloseTutorialDungeonBtn()
    {
        firstDungeon.SetActive(false);
        Destroy(dungeonImg);
    }

    /// <summary>
    /// 튜토리얼 던전 입장.
    /// </summary>
    private void OnClick_StartTutorialDungeonBtn()
    {
        Debug.Log("Start!!!");
    }
}
