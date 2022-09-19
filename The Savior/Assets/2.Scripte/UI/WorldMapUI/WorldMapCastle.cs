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

    [Header("던전 - 그로타")]
    public Button grottaButton;

    // 해당 창이 활성화 되어있는가?
    private bool isChurch = false;
    private bool isShop = false;

    private int summonPrice = 100;


    PlayStory story;


    // 왕성을 강조하는 이미지.
    public GameObject[] storyBackGound;


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
        grottaButton.onClick.AddListener(() => OnClick_GrottaBtn());

        story = GameObject.Find("PUIManager").GetComponent<PlayStory>();

        summon = GetComponent<SummonCharacter>();
        buy = GetComponent<BuyCardPack>();
    }

    // 스토리에서 던전 클릭시 던전을 가르키는 이미지 삭제.
    // 던전 툴팁 활성화.
    private void OnClick_GrottaBtn()
    {
        if (GameManager.instance.data.storyProgress == 92)
        {
            storyBackGound[4].SetActive(false);
            story.toolTip[5].SetActive(true);
            story.toolTip[6].SetActive(true);
        }
    }

    /// <summary>
    /// 월드맵 마을 지도 오픈 함수.
    /// 스토리 진행중일경우 해당 이벤트를 발생.
    /// 왕성을 가르키는 이미지를 활성화하고 대화창을 활성화한다.
    /// </summary>
    private void OnClick_CastleBtn()
    {
        if(GameManager.instance.data.storyProgress == 3)
        {
            storyBackGound[1].SetActive(false);
            story.OnDiaLog();
        }
    }

    /// <summary>
    /// 월드맵에서 성으로 이동하는 버튼
    /// 스토리 진행중일경우 해당 이벤트 발생.
    /// 월드맵에서 왕성을 가르키는 이미지를 비활성화 하고, 마을 지도에서 왕성을 가르키는 이미지를 활성화한다.
    /// </summary>
    public void OnClick_VisitCastleBtn()
    {
        castleImg.SetActive(true);
        if(GameManager.instance.data.storyProgress == 3)
        {
            // 왕성을 강조하는 이미지.
            storyBackGound[0].SetActive(false);
            storyBackGound[1].SetActive(true);
        }
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

        // 스토리 진행도가 58일 때 발생하는 이벤트
        // 대화창을 활성화하고 상점을 가르키는 이미지를 비활성화한다.
        if(GameManager.instance.data.storyProgress == 58)
        {
            story.OnDiaLog();
            storyBackGound[3].SetActive(false);
        }
    }


    /// <summary>
    /// 상점창을 비활성화 하는 버튼.
    /// </summary>
    private void OnClick_CloseShopBtn()
    {
        buy.DestroyCard();
        isShop = false;
        shopImg.SetActive(false);
        
        // 스토리 진행도 64일 때 발생하는 이벤트
        // 대화창을 활성화 한다.
        if (GameManager.instance.data.storyProgress == 64)
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

        // 스토리 진행도 49일 때 발생하는 이벤트.
        // 교회를 가르키는 이미지를 비활성화한다.
        if(GameManager.instance.data.storyProgress == 49)
        {
            story.OnDiaLog();
            storyBackGound[2].SetActive(false);
        }
    }

    /// <summary>
    /// 교회창 비활성화 버튼
    /// </summary>
    private void OnClick_CloseChurchBtn()
    {
        isChurch = false;
        churchImg.SetActive(false);

        if(GameManager.instance.data.storyProgress == 54)
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
