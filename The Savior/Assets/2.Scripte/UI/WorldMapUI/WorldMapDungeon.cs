using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using SimpleJSON;

public class WorldMapDungeon : MonoBehaviour
{
    DungeonInfo info;   // 던전 정보 스크립트

    private TextAsset jsonData;
    private string json;

    public enum EDungeonName { Grotta, First, Second, Third, Fourth, Fifth }    // 던전 이름
    public EDungeonName curDungeon = EDungeonName.Grotta;

    [Header("던전 입장 버튼")]
    public Button[] EnterTheDungeonButton = new Button[6];

    [Header("던전 이미지 닫기 버튼")]
    public Button closeDungeonButton;

    [Header("던전 시작 버튼")]
    public Button startDungeonButton;

    [Header("던전 입장 이미지")]
    public GameObject dungeon;

    [Header("던전 출현 몬스터 슬롯 / 기대 보상 슬롯")]
    public Transform bossSlotTr;
    public Transform rewardSlotTr1;
    public Transform rewardSlotTr2;

    [Header("던전 이미지 및 설명")]
    public Transform dungeonSlot;
    public TMP_Text dungeonName;
    public TMP_Text contactText;

    private string dgName;

    [Header("카드 프리셋")]
    public GameObject cardPreset;
    public Button yesPresetButton;
    public Button noPresetButton;
    public Button[] curPresetButton = new Button[5];
    public TMP_Text presetName;
    public TMP_Text[] presetCard;
    private CardDeck cardDeck;
    public GameObject warningPreset;
    public Button warningButton;

    private Image[] dungeonImg = new Image[4];      // 0 : 던전 이미지 //// 1 : 등장 보스 //// 2 : 기대 보상 1 //// 3 : 기대 보상 2
    void Start()
    {
        info = GameObject.Find("DungeonInfo").GetComponent<DungeonInfo>();

        jsonData = Resources.Load<TextAsset>("DungeonNameData");
        json = jsonData.text;

        // 튜토리얼 던전
        EnterTheDungeonButton[(int)EDungeonName.Grotta].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(0));
        // 첫 번째 던전
        EnterTheDungeonButton[(int)EDungeonName.First].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(1));
        // 두 번째 던전
        EnterTheDungeonButton[(int)EDungeonName.Second].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(2));
        // 세 번째 던전
        EnterTheDungeonButton[(int)EDungeonName.Third].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(3));
        // 네 번째 던전
        EnterTheDungeonButton[(int)EDungeonName.Fourth].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(4));
        // 마왕성 던전
        EnterTheDungeonButton[(int)EDungeonName.Fifth].onClick.AddListener(() => OnClick_EnterTheDungeonBtn(5));

        // 던전창 닫기, 던전 들어가기.
        closeDungeonButton.onClick.AddListener(() => OnClick_CloseDungeonBtn());
        startDungeonButton.onClick.AddListener(() => OnClick_StartDungeonBtn());

        // 프리셋 => 던전 들어가기, 던전 창으로 돌아가기
        yesPresetButton.onClick.AddListener(() => OnClick_YesPreset());
        noPresetButton.onClick.AddListener(() => OnClick_NoPreset());

        // 카드 프리셋
        curPresetButton[0].onClick.AddListener(() => OnClick_Preset(0));
        curPresetButton[1].onClick.AddListener(() => OnClick_Preset(1));
        curPresetButton[2].onClick.AddListener(() => OnClick_Preset(2));
        curPresetButton[3].onClick.AddListener(() => OnClick_Preset(3));
        curPresetButton[4].onClick.AddListener(() => OnClick_Preset(4));
        warningButton.onClick.AddListener(() => OnClick_CloseWarningBtn());

        cardDeck = GameObject.Find("PUIManager").GetComponent<CardDeck>();

    }

    /// <summary>
    /// 튜토리얼 던전 이미지 활성화.
    /// <br>던전 이미지 활성화 시에 해당 던전에 맞는 아이콘과 던전 이미지를 넣어줘야 함.</br>
    /// </summary>
    private void OnClick_EnterTheDungeonBtn(int idx)
    {
        var data = JSON.Parse(json);
        // 이미지 생성
        dungeonImg[0] = Instantiate(info.dungeonImg[idx].GetComponent<Image>());
        dungeonImg[1] = Instantiate(info.bossImg[idx].GetComponent<Image>());
        dungeonImg[2] = Instantiate(info.rewardImg1[idx].GetComponent<Image>());
        dungeonImg[3] = Instantiate(info.rewardImg2[idx].GetComponent<Image>());

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
        contactText.text = info.contactDungeon[idx];
        dungeonName.text = data[idx]["던전이름"];

        curDungeon = (EDungeonName)idx;

        // 던전 이미지 활성화.
        dungeon.SetActive(true);
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
    private void OnClick_CloseDungeonBtn()
    {
        dungeon.SetActive(false);
        Destroy(dungeonImg[0]);
        Destroy(dungeonImg[1]);
        Destroy(dungeonImg[2]);
        Destroy(dungeonImg[3]);
    }

    /// <summary>
    ///  던전 입장 버튼 클릭시 카드 프리셋 활성화
    /// </summary>
    private void OnClick_StartDungeonBtn()
    {
        cardPreset.SetActive(true);
        curPresetButton[GameManager.instance.data.preset - 1].Select();
        OnClick_Preset(GameManager.instance.data.preset - 1);
    }

    private void OnClick_CloseWarningBtn()
    {
        warningPreset.SetActive(false);
    }

    // 카드 프리셋 y버튼 클릭시 던전 입장
    private void OnClick_YesPreset()
    {
        // 현재 프리셋의 장착된 카드가 10개 이하라면 던전에 들어 갈 수 없음.
        int presetIdx = 0;
        for (int i = 0; i < 15; i++)
        {
            if (GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].preset[i] != 0)
            {
                presetIdx++;
            }
        }
        if (presetIdx < 10)
        {
            warningPreset.SetActive(true);
            return;     // 팝업 창 제작 후 팝업창을 띄움.
        }
        // 튜토리얼 씬 이동
        GameManager.instance.SceneChange(3);
    }

    // n버튼 클릭시 다시 던전 정보창으로 돌아감.
    private void OnClick_NoPreset()
    {
        cardPreset.SetActive(false);
    }

    // 카드 프리셋의 이름.
    private void OnClick_Preset(int idx)
    {
        GameManager.instance.data.preset = (idx + 1);
        curPresetButton[idx].Select();
        presetName.text = GameManager.instance.data.presetName[idx];

        cardDeck.OnClick_PresetChangeBtn(idx);

        presetCard[0].text = cardDeck.type_Heal.ToString();
        presetCard[1].text = cardDeck.type_Shield.ToString();
        presetCard[2].text = cardDeck.type_Buff.ToString();
        presetCard[3].text = cardDeck.type_Debuff.ToString();
        presetCard[4].text = cardDeck.type_Attack.ToString();
    }
}
