using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldMapCastle : MonoBehaviour
{
    SummonCharacter summon;

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

        summon = GetComponent<SummonCharacter>();
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
    }

    /// <summary>
    /// ����â���� ���� ���� ��ư 
    /// </summary>
    private void OnClick_OnBuyBtn()
    {
        Debug.Log("����");
    }

    /// <summary>
    /// ��ȸâ Ȱ��ȭ ��ư
    /// </summary>
    private void OnClick_OnChurchBtn()
    {
        isChurch = !isChurch;
        churchImg.SetActive(isChurch);
    }

    /// <summary>
    /// ��ȸ���� ��ȯ ��ư
    /// </summary>
    private void OnClick_OnSummonBtn()
    {
        summon.SummonRandom(gold);
    }

}
