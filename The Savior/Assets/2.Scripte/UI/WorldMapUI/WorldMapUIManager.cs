using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapUIManager : MonoBehaviour
{
    #region UI 환경 변수
    // 언어 변경을 위한 배열
    [Header("UI 환경 변수")]
    public TMP_Text[] UI_TextList;
    private const int UI_fixedvalue = 250;

    private bool UI_blind_check = false;
    private GameObject UI_UnActive = null;
    private GameObject UI_Active = null;
    #endregion
    #region UI 오브젝트
    [Header("---UI 오브젝트---")]
    public GameObject UI_villageMap;
    public GameObject UI_shopWindow;
    public GameObject UI_churchWidnow;
    public GameObject UI_DungeonInfo;
    public GameObject UI_blind;
    [Header("---UI 버튼---")]

    [Header("Village 맵 버튼 구성")]
    public Button BT_WMCastle;
    public Button BT_WMDungeon00;
    public Button BT_WMDungeon01;
    public Button BT_WMDungeon02;
    public Button BT_WMDungeon03;
    public Button BT_WMDungeon04;
    public Button BT_WMDemonCastle;
    [Header("Village 맵 버튼 구성")]
    public Button BT_VMClose;
    public Button BT_VMCastle;
    public Button BT_VMChurch;
    public Button BT_VMShop;
    [Header("Shop Window 버튼 구성")]
    public Button BT_SHOPClose;
    public Button BT_SHOPBuy;
    [Header("Church Window 버튼 구성")]
    public Button BT_CHClose;
    public Button BT_CHBuy;
    [Header("DungeonInfo 버튼 구성")]
    public Button BT_DungeonClose;
    public Button BT_DungeonStart;

    #endregion

    void Start()
    {
        #region 버튼 함수 연결 및 캐시처리
        BT_WMCastle.onClick.AddListener(OnWMCastleButton);
        //BT_WMDungeon00.onClick.AddListener(OnDungeonButton);
        //BT_WMDungeon01.onClick.AddListener(OnDungeonButton);
        //BT_WMDungeon02.onClick.AddListener(OnDungeonButton);
        //BT_WMDungeon03.onClick.AddListener(OnDungeonButton);
        //BT_WMDungeon04.onClick.AddListener(OnDungeonButton);
        //BT_WMDemonCastle.onClick.AddListener(OnDungeonButton);
        BT_VMClose.onClick.AddListener(OnVMCloseButton);
        BT_VMCastle.onClick.AddListener(OnVMCloseButton); 
        BT_VMChurch.onClick.AddListener(OnVMChurchButton);
        BT_VMShop.onClick.AddListener(OnVMShopButton);
        BT_SHOPClose.onClick.AddListener(OnShopCloseButton);
        BT_SHOPBuy.onClick.AddListener(OnShopCloseButton);
        BT_CHClose.onClick.AddListener(OnChurchCloseButton);
        BT_CHBuy.onClick.AddListener(OnChurchBuyButton);
        BT_DungeonClose.onClick.AddListener(OnDungeonCloseButton);
        BT_DungeonStart.onClick.AddListener(OnDungeonStartButton);
        #endregion

        UIReset();
    }

    #region UI 윈도우 기능 & 버튼 기능

    private void WindowUIReset()
    {
        UI_blind.SetActive(UI_blind_check);
        UI_UnActive?.SetActive(false);
        UI_Active?.SetActive(true);
        Debug.Log("실행체크");
    }

    public void OnWMCastleButton()
    {
        UI_Active = UI_villageMap;
        UI_UnActive = null;
        UI_blind_check = true;
        WindowUIReset();
    }
    public void OnVMCloseButton()
    {
        UI_UnActive = UI_villageMap;
        UI_Active = null;
        UI_blind_check = false;
        WindowUIReset();
    }

    public void OnVMShopButton()
    {
        UI_Active = UI_shopWindow;
        UI_UnActive = null;
        UI_blind_check = true;
        WindowUIReset();
    }
    public void OnShopCloseButton()
    {
        UI_UnActive = UI_shopWindow;
        UI_Active = UI_villageMap;
        UI_blind_check = true;
        WindowUIReset();
    }
    public void OnShopBuyButton()
    {
        Shopbuy();
    }

    public void OnVMChurchButton()
    {
        UI_Active = UI_churchWidnow;
        UI_UnActive = null;
        UI_blind_check = true;
        WindowUIReset();
    }
    public void OnChurchBuyButton()
    {
        Churchbuy();
    }
    public void OnChurchCloseButton()
    {
        UI_UnActive = UI_churchWidnow;
        UI_Active = UI_villageMap;
        UI_blind_check = true;
        WindowUIReset();
    }
    public void OnDungeonButton(int num)
    {
        UI_Active = UI_DungeonInfo;
        UI_UnActive = null;
        UI_blind_check = true;
        DungeonWindowSetting(num);
        WindowUIReset();
    }
    public void OnDungeonCloseButton()
    {
        UI_UnActive = UI_DungeonInfo;
        UI_Active = null;
        UI_blind_check = false;
        WindowUIReset();
    }
    public void OnDungeonStartButton()
    {
        UI_UnActive = UI_DungeonInfo;
        UI_Active = null;
        UI_blind_check = false;
        WindowUIReset();
    }



    #endregion

    #region 상점 기능
    private void Shopbuy()
    {

    }
    #endregion

    #region 교회 기능
    private void Churchbuy()
    {

    }
    #endregion

    #region 던전 정보 기능
    public static int DGSelectNumber = 0;
    public Image DGUI_IconSlot1;
    public Image DGUI_IconSlot2;
    public Image DGUI_IconSlot3;
    public Image DGUI_MainIcon;
    public TMP_Text DGUI_name;
    public TMP_Text DGUI_content;
    private void DungeonWindowSetting(int num)
    {
        DGSelectNumber = num;
        //DGUI_MainIcon.sprite = DungeonInfo.instance.DG_info[num].DG_Icon;
        //DGUI_IconSlot1 = DungeonInfo.instance.DG_info[num].DG_reward1;
        //DGUI_IconSlot2 = DungeonInfo.instance.DG_info[num].DG_reward2;
        //DGUI_IconSlot3 = DungeonInfo.instance.DG_info[num].DG_reward3;
        //이미지 오브젝트로변경하는걸 검토 
        DGUI_name.text = DungeonInfo.instance.DG_info[num].DG_name;
        // 텍스트 매니저를 통해서 텍스트값을 받아와야함. 
        DGUI_content.text = DungeonInfo.instance.DG_info[num].DG_content;
        // 위와 동일 수정 필요.
    }
    #endregion




    #region UI 환경 관련
    /* UI 설정 초기화 함수 
     * 역할 : 메인 씬에서 UI기능 설정들을 재정립할때 호출되는 함수
     * 
     * - 모든 텍스트 언어 설정
     * 
     * 
     */
    private void UIReset()
    {
        LanguageTranslate();
    }

    /* UI 텍스트 언어 변경 함수
     * 역할 : 메인씬에 사용된 모든 텍스트에 접근하여, 설정된 언어값에 맞게 변경시키는 함수
     * 
     * @UI_TextList : 해당 씬이 가지고있는 모든 text
     * @UI_fixedvalue : 해당씬에 할당된 text 번호중 첫번째값
     * 
     */
    public void LanguageTranslate()
    {
        for (int i = 0; i < UI_TextList.Length; i++)
        {
            //UI_TextList[i].text = TextManager.instance.LanguageChange(i + UI_fixedvalue);
        }
    }
    #endregion
}
