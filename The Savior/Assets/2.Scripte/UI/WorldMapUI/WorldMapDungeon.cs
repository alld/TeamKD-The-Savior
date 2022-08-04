using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldMapDungeon : MonoBehaviour
{
    DungeonInfo info;   // 던전 정보 스크립트

    public enum EDungeonName { Grotta, First, Second, Third, Fourth, Fifth }    // 던전 이름

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
    public Transform bossSlotTr;
    public Transform rewardSlotTr1;
    public Transform rewardSlotTr2;

    [Header("던전 이미지 및 설명")]
    public Transform dungeonSlot;
    public TMP_Text contactText;

    private Image[] dungeonImg = new Image[4];      // 0 : 던전 이미지 //// 1 : 등장 보스 //// 2 : 기대 보상 1 //// 3 : 기대 보상 2
    void Start()
    {
        info = GameObject.Find("DungeonInfo").GetComponent<DungeonInfo>();

        dg0_Button.onClick.AddListener(() => OnClick_EnterTheGrottaBtn());
        closeDungeon0_Button.onClick.AddListener(() => OnClick_CloseGrottaBtn());
        startDungeon0_Button.onClick.AddListener(() => OnClick_StartGrottaBtn());
    }

    /// <summary>
    /// 튜토리얼 던전 이미지 활성화.
    /// <br>던전 이미지 활성화 시에 해당 던전에 맞는 아이콘과 던전 이미지를 넣어줘야 함.</br>
    /// </summary>
    private void OnClick_EnterTheGrottaBtn()
    {
        // 이미지 생성
        dungeonImg[0] = Instantiate(info.dungeonImg[(int)EDungeonName.Grotta].GetComponent<Image>());
        dungeonImg[1] = Instantiate(info.bossImg[(int)EDungeonName.Grotta].GetComponent<Image>());
        dungeonImg[2] = Instantiate(info.rewardImg1[(int)EDungeonName.Grotta].GetComponent<Image>());
        dungeonImg[3] = Instantiate(info.rewardImg2[(int)EDungeonName.Grotta].GetComponent<Image>());

        // 이미지 세팅
        dungeonImg[0].transform.SetParent(dungeonSlot);
        dungeonImg[1].transform.SetParent(bossSlotTr);
        dungeonImg[2].transform.SetParent(rewardSlotTr1);
        dungeonImg[3].transform.SetParent(rewardSlotTr2);

        // 사이즈 조정
        InitRectSize(dungeonImg[0]);
        InitRectSize(dungeonImg[1]);
        InitRectSize(dungeonImg[2]);
        InitRectSize(dungeonImg[3]);

        // 던전 설명
        contactText.text = info.contactDungeon[(int)EDungeonName.Grotta];

        // 던전 이미지 활성화.
        firstDungeon.SetActive(true);
    }

    /// <summary>
    /// 스트레치가 적용된 이미지의 사이즈를 부모 객체의 사이즈에 맞춤.
    /// </summary>
    /// <param name="img"></param>
    private void InitRectSize(Image img)
    {
        img.rectTransform.offsetMin = Vector2.zero;
        img.rectTransform.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// 튜토리얼 던전 이미지 닫기.
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
    /// 튜토리얼 던전 입장.
    /// </summary>
    private void OnClick_StartGrottaBtn()
    {
        Debug.Log("Start!!!");
    }
}
