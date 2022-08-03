using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayToolBar : MonoBehaviour
{
    // 버튼
    [Header("Button")]
    public Button closeRelicButton;
    public Button closeCardButton;
    public Button closePartyButton;
    public Button closeCollectButton;
    public Button relicButton;
    public Button cardButton;
    public Button partyButton;
    public Button collectButton;

    // 해당 창
    [Header("ActiveObject")]
    public GameObject relicImg;
    public GameObject cardImg;
    public GameObject partyImg;
    public GameObject collectImg;

    // 골드와 소울이 표시될 텍스트
    public TMP_Text souls;
    public TMP_Text golds;

    // 해당 창이 열려있는가?
    private bool isRelic = false;
    private bool isCard = false;
    private bool isParty = false;
    private bool isCollect = false;

    private void Start()
    {
        // 유물 인벤토리 On, Off
        relicButton.onClick.AddListener(() => OnClick_RelicBtn());
        closeRelicButton.onClick.AddListener(() => OnClick_RelicBtn());
        // 카드 인벤토리 On, Off
        cardButton.onClick.AddListener(() => OnClick_CardBtn());
        closeCardButton.onClick.AddListener(() => OnClick_CardBtn());
        // 캐릭터 인벤토리 On, Off
        partyButton.onClick.AddListener(() => OnClick_PartyBtn());
        closePartyButton.onClick.AddListener(() => OnClick_PartyBtn());
        // 업적 인벤토리 On, Off
        collectButton.onClick.AddListener(() => OnClick_CollectBtn());
        closeCollectButton.onClick.AddListener(() => OnClick_CollectBtn());

        Gold();
    }

    public void Gold()
    {
        souls.text = GameManager.instance.data.souls.ToString();
        golds.text = GameManager.instance.data.golds.ToString();
    }

    /// <summary>
    /// 유물 인벤토리를 On, Off 해주는 함수.
    /// </summary>
    private void OnClick_RelicBtn()
    {
        cardImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);

        isCard = false;
        isParty = false;
        isCollect = false;

        isRelic = !isRelic;
        relicImg.SetActive(isRelic);
    }

    /// <summary>
    /// 카드 인벤토리를 On, Off 해주는 함수.
    /// </summary>
    private void OnClick_CardBtn()
    {
        relicImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);

        isRelic = false;
        isParty = false;
        isCollect = false;

        isCard = !isCard;
        cardImg.SetActive(isCard);
    }

    /// <summary>
    /// 캐릭터 인벤토리를 On, Off 해주는 함수.
    /// </summary>
    private void OnClick_PartyBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        collectImg.SetActive(false);

        isRelic = false;
        isCard = false;
        isCollect = false;

        isParty = !isParty;
        partyImg.SetActive(isParty);
    }

    /// <summary>
    /// 업적 인벤토리를 On, Off 해주는 함수.
    /// </summary>
    private void OnClick_CollectBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        partyImg.SetActive(false);

        isRelic = false;
        isCard = false;
        isParty = false;

        isCollect = !isCollect;
        collectImg.SetActive(isCollect);
    }

}
