using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldMapCastle : MonoBehaviour
{
    [Header("버튼 - 성")]
    public Button castleButton;
    public Button closeCastleButton;

    [Header("버튼 - 성 내부")]
    public Button shopButton;
    public Button shopBuyButton;
    public Button closeShopButton;
    public Button churchButton;
    public Button churchsummonButton;
    public Button closeChurchButton;
    //public Button kingsCastle;

    [Header("성 이미지")]
    public GameObject castleImg;
    public GameObject churchImg;
    public GameObject shopImg;

    // 해당 창이 활성화 되어있는가?
    private bool isChurch = false;
    private bool isShop = false;



    void Start()
    {
        castleButton.onClick.AddListener(() => OnClick_VisitCastleBtn());
        closeCastleButton.onClick.AddListener(() => OnClick_OutCastleBtn());
        churchButton.onClick.AddListener(() => OnClick_OnChurchBtn());
        shopButton.onClick.AddListener(() => OnClick_OnShopBtn());
        closeShopButton.onClick.AddListener(() => OnClick_OnShopBtn());
        closeChurchButton.onClick.AddListener(() => OnClick_OnChurchBtn());
        shopBuyButton.onClick.AddListener(() => OnClick_OnBuyBtn());
        churchsummonButton.onClick.AddListener(() => OnClick_OnSummonBtn());
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
    private void OnClick_OutCastleBtn()
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
    }

    /// <summary>
    /// 상점창에서 물건 구매 버튼 
    /// </summary>
    private void OnClick_OnBuyBtn()
    {
        Debug.Log("구매");
    }

    /// <summary>
    /// 교회창 활성화 버튼
    /// </summary>
    private void OnClick_OnChurchBtn()
    {
        isChurch = !isChurch;
        churchImg.SetActive(isChurch);
    }

    /// <summary>
    /// 교회에서 소환 버튼
    /// </summary>
    private void OnClick_OnSummonBtn()
    {
        Debug.Log("소환");
    }

}
