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
    //public Button kingsCastle;

    [Header("�� �̹���")]
    public GameObject castleImg;
    public GameObject churchImg;
    public GameObject shopImg;

    // �ش� â�� Ȱ��ȭ �Ǿ��ִ°�?
    private bool isChurch = false;
    private bool isShop = false;

    private int summonPrice = 100;



    void Start()
    {
        castleButton.onClick.AddListener(() => OnClick_VisitCastleBtn());
        closeCastleButton.onClick.AddListener(() => OnClick_OutCastleBtn());
        churchButton.onClick.AddListener(() => OnClick_OnChurchBtn());
        shopButton.onClick.AddListener(() => OnClick_OnShopBtn());
        closeShopButton.onClick.AddListener(() => OnClick_OnShopBtn());
        closeChurchButton.onClick.AddListener(() => OnClick_OnChurchBtn());
        shopBuyButton.onClick.AddListener(() => OnClick_OnBuyBtn());
        churchSummonButton.onClick.AddListener(() => OnClick_OnSummonBtn());
        summonConfirmButton.onClick.AddListener(() => OnClick_ConfirmSummonBtn());
        reSummonButton.onClick.AddListener(() => OnClick_ReSummonBtn());


        summon = GetComponent<SummonCharacter>();
        buy = GetComponent<BuyCardPack>();
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
    private void OnClick_OutCastleBtn()
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
    }

    /// <summary>
    /// ����â���� ���� ���� ��ư 
    /// </summary>
    private void OnClick_OnBuyBtn()
    {
        buy.BuyCard();
    }

    /// <summary>
    /// ��ȸâ Ȱ��ȭ ��ư
    /// </summary>
    private void OnClick_OnChurchBtn()
    {
        isChurch = !isChurch;
        churchImg.SetActive(isChurch);
        summon.InitSummon();
        InitChurchButton();
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

        if(summon.Stop == 1)
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
