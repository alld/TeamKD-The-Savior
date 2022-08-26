using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldMapCastle : MonoBehaviour
{
    SummonCharacter summon;
    BuyCardPack buy;
    [Header("버튼 - 성")]
    public Button castleButton;
    public Button closeCastleButton;

    [Header("버튼 - 성 내부")]
    public Button shopButton;
    public Button shopBuyButton;
    public Button closeShopButton;
    public Button churchButton;
    public Button churchSummonButton;
    public Button closeChurchButton;
    public Button summonConfirmButton;
    public Button reSummonButton;
    public Button castle;       // 성 내부의 왕성

    //public Button kingsCastle;

    [Header("성 이미지")]
    public GameObject castleImg;
    public GameObject churchImg;
    public GameObject shopImg;

    // 해당 창이 활성화 되어있는가?
    private bool isChurch = false;
    private bool isShop = false;

    private int summonPrice = 100;


    PlayStory story;



    void Start()
    {
        castleButton.onClick.AddListener(() => OnClick_VisitCastleBtn());
        closeCastleButton.onClick.AddListener(() => OnClick_OutCastleBtn());
        churchButton.onClick.AddListener(() => OnClick_OnChurchBtn());
        shopButton.onClick.AddListener(() => OnClick_OnShopBtn());
        closeShopButton.onClick.AddListener(() => OnClick_CloseShopBtn());
        closeChurchButton.onClick.AddListener(() => OnClick_CloseChurchBtn());
        shopBuyButton.onClick.AddListener(() => OnClick_OnBuyBtn());
        churchSummonButton.onClick.AddListener(() => OnClick_OnSummonBtn());
        summonConfirmButton.onClick.AddListener(() => OnClick_ConfirmSummonBtn());
        reSummonButton.onClick.AddListener(() => OnClick_ReSummonBtn());
        castle.onClick.AddListener(() => OnClick_CastleBtn());

        story = GameObject.Find("PUIManager").GetComponent<PlayStory>();

        summon = GetComponent<SummonCharacter>();
        buy = GetComponent<BuyCardPack>();
    }

    private void OnClick_CastleBtn()
    {
        if(GameManager.instance.data.storyProgress >= 0 && GameManager.instance.data.storyProgress <= 44)
        {
            story.OnDiaLog();
        }
    }

    /// <summary>
    /// 월드맵에서 성으로 이동하는 버튼
    /// </summary>
    private void OnClick_VisitCastleBtn()
    {
        castleImg.SetActive(true);
    }

    /// <summary>
    /// 성에서 월드맵으로 나가는 버튼
    /// </summary>
    public void OnClick_OutCastleBtn()
    {
        castleImg.SetActive(false);
    }

    /// <summary>
    /// 상점창 활성화 버튼
    /// </summary>
    private void OnClick_OnShopBtn()
    {
        isShop = !isShop;
        shopImg.SetActive(isShop);
        buy.OpenShop();

        if(GameManager.instance.data.storyProgress >= 56 && GameManager.instance.data.storyProgress <= 61)
        {
            story.OnDiaLog();
        }
    }

    private void OnClick_CloseShopBtn()
    {
        buy.InitBuyCard();
        isShop = false;
        shopImg.SetActive(false);

        if (GameManager.instance.data.storyProgress == 62)
        {
            story.OnDiaLog();
        }
    }

    /// <summary>
    /// 상점창에서 물건 구매 버튼 
    /// </summary>
    private void OnClick_OnBuyBtn()
    {
        StartCoroutine(buy.BuyCard());
    }

    /// <summary>
    /// 교회창 활성화 버튼
    /// </summary>
    private void OnClick_OnChurchBtn()
    {
        isChurch = !isChurch;
        churchImg.SetActive(true);
        summon.InitSummon();
        InitChurchButton();

        if(GameManager.instance.data.storyProgress >= 46 && GameManager.instance.data.storyProgress <= 51)
        {
            story.OnDiaLog();
        }
    }

    private void OnClick_CloseChurchBtn()
    {
        isChurch = false;
        churchImg.SetActive(false);

        if(GameManager.instance.data.storyProgress == 52)
        {
            story.OnDiaLog();
        }
    }

    /// <summary>
    /// 교회에서 소환 버튼
    /// </summary>
    private void OnClick_OnSummonBtn()
    {

        if(GameManager.instance.data.haveCharacter.Count >= 27)
        {
            Debug.Log("인벤토리에 캐릭터가 가득 찼습니다.");
            return;
        }
        summon.SummonRandom(summonPrice);

        if (summon.ishave)      // 모든 캐릭터를 가지고 있을 때.
        {
            return;
        }

        churchSummonButton.gameObject.SetActive(false);
        summonConfirmButton.gameObject.SetActive(true);
        reSummonButton.gameObject.SetActive(true);
    }


    /// <summary>
    /// 캐릭터 소환 후 확인 버튼 클릭시 호출
    /// </summary>
    private void OnClick_ConfirmSummonBtn()
    {
        summon.InitSummon();
        InitChurchButton();
    }

    /// <summary>
    /// 캐릭터 소환 후 한번 더 소환하기 버튼 클릭시 호출
    /// </summary>
    private void OnClick_ReSummonBtn()
    {
        summon.InitSummon();
        OnClick_OnSummonBtn();
    }

    private void InitChurchButton()
    {
        churchSummonButton.gameObject.SetActive(true);
        summonConfirmButton.gameObject.SetActive(false); ;
        reSummonButton.gameObject.SetActive(false);
    }

}
