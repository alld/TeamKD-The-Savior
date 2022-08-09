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

    private void Start()
    {
        // 유물 인벤토리 On, Off
        relicButton.onClick.AddListener(() => OnClick_RelicBtn());
        closeRelicButton.onClick.AddListener(() => OnClick_CloseRelicBtn());
        // 카드 인벤토리 On, Off
        cardButton.onClick.AddListener(() => OnClick_CardBtn());
        closeCardButton.onClick.AddListener(() => OnClick_CloseCardBtn());
        // 캐릭터 인벤토리 On, Off
        partyButton.onClick.AddListener(() => OnClick_PartyBtn());
        closePartyButton.onClick.AddListener(() => OnClick_ClosePartyBtn());
        // 업적 인벤토리 On, Off
        collectButton.onClick.AddListener(() => OnClick_CollectBtn());
        closeCollectButton.onClick.AddListener(() => OnClick_CloseCollectBtn());

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
        relicImg.SetActive(true);
    }

    /// <summary>
    /// 카드 인벤토리를 On, Off 해주는 함수.
    /// </summary>
    private void OnClick_CardBtn()
    {
        relicImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);
        cardImg.SetActive(true);
    }

    /// <summary>
    /// 캐릭터 인벤토리를 On, Off 해주는 함수.
    /// </summary>
    private void OnClick_PartyBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        collectImg.SetActive(false);
        partyImg.SetActive(true);
    }

    /// <summary>
    /// 업적 인벤토리를 On, Off 해주는 함수.
    /// </summary>
    private void OnClick_CollectBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(true);
    }

    private void OnClick_CloseRelicBtn()
    {
        relicImg.SetActive(false);
    }

    private void OnClick_CloseCardBtn()
    {
        cardImg.SetActive(false);
    }
    private void OnClick_ClosePartyBtn()
    {
        partyImg.SetActive(false);
    }
    private void OnClick_CloseCollectBtn()
    {
        collectImg.SetActive(false);
    }
}
