using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldMapCastle : MonoBehaviour
{
    SummonCharacter summon;
    BuyCardPack buy;
    [Header("��ư - ��")]
    public Button castleButton;
    public Button closeCastleButton;

    [Header("��ư - �� ����")]
    public Button shopButton;
    public Button shopBuyButton;
    public Button closeShopButton;
    public Button churchButton;
    public Button churchSummonButton;
    public Button closeChurchButton;
    public Button summonConfirmButton;
    public Button reSummonButton;
    public Button castle;       // �� ������ �ռ�

    //public Button kingsCastle;

    [Header("�� �̹���")]
    public GameObject castleImg;
    public GameObject churchImg;
    public GameObject shopImg;

    // �ش� â�� Ȱ��ȭ �Ǿ��ִ°�?
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
    /// ����ʿ��� ������ �̵��ϴ� ��ư
    /// </summary>
    private void OnClick_VisitCastleBtn()
    {
        castleImg.SetActive(true);
    }

    /// <summary>
    /// ������ ��������� ������ ��ư
    /// </summary>
    public void OnClick_OutCastleBtn()
    {
        castleImg.SetActive(false);
    }

    /// <summary>
    /// ����â Ȱ��ȭ ��ư
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
    /// ����â���� ���� ���� ��ư 
    /// </summary>
    private void OnClick_OnBuyBtn()
    {
        StartCoroutine(buy.BuyCard());
    }

    /// <summary>
    /// ��ȸâ Ȱ��ȭ ��ư
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
    /// ��ȸ���� ��ȯ ��ư
    /// </summary>
    private void OnClick_OnSummonBtn()
    {

        if(GameManager.instance.data.haveCharacter.Count >= 27)
        {
            Debug.Log("�κ��丮�� ĳ���Ͱ� ���� á���ϴ�.");
            return;
        }
        summon.SummonRandom(summonPrice);

        if (summon.ishave)      // ��� ĳ���͸� ������ ���� ��.
        {
            return;
        }

        churchSummonButton.gameObject.SetActive(false);
        summonConfirmButton.gameObject.SetActive(true);
        reSummonButton.gameObject.SetActive(true);
    }


    /// <summary>
    /// ĳ���� ��ȯ �� Ȯ�� ��ư Ŭ���� ȣ��
    /// </summary>
    private void OnClick_ConfirmSummonBtn()
    {
        summon.InitSummon();
        InitChurchButton();
    }

    /// <summary>
    /// ĳ���� ��ȯ �� �ѹ� �� ��ȯ�ϱ� ��ư Ŭ���� ȣ��
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
