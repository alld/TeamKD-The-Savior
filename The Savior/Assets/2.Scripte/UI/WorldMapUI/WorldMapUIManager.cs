using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class WorldMapUIManager : MonoBehaviour
{
    #region UI ȯ�� ����
    // ��� ������ ���� �迭
    [Header("UI ȯ�� ����")]
    public TMP_Text[] UI_TextList;
    private const int UI_fixedvalue = 250;

    private bool UI_blind_check = false;
    private GameObject UI_UnActive = null;
    private GameObject UI_Active = null;
    #endregion
    #region UI ������Ʈ
    [Header("---UI ������Ʈ---")]
    public GameObject UI_villageMap;
    public GameObject UI_shopWindow;
    public GameObject UI_churchWidnow;
    public GameObject UI_DungeonInfo;
    public GameObject UI_blind;
    [Header("---UI ��ư---")]

    [Header("Village �� ��ư ����")]
    public Button BT_WMCastle;
    public Button BT_WMDungeon00;
    public Button BT_WMDungeon01;
    public Button BT_WMDungeon02;
    public Button BT_WMDungeon03;
    public Button BT_WMDungeon04;
    public Button BT_WMDemonCastle;
    [Header("Village �� ��ư ����")]
    public Button BT_VMClose;
    public Button BT_VMCastle;
    public Button BT_VMChurch;
    public Button BT_VMShop;
    [Header("Shop Window ��ư ����")]
    public Button BT_SHOPClose;
    public Button BT_SHOPBuy;
    [Header("Church Window ��ư ����")]
    public Button BT_CHClose;
    public Button BT_CHBuy;
    [Header("DungeonInfo ��ư ����")]
    public Button BT_DungeonClose;
    public Button BT_DungeonStart;

    #endregion

    void Start()
    {
        #region ��ư �Լ� ���� �� ĳ��ó��
        BT_WMCastle.onClick.AddListener(OnWMCastleButton);
        BT_WMDungeon00.onClick.AddListener(()=>OnDungeonButton(1));
        BT_WMDungeon01.onClick.AddListener(() => OnDungeonButton(2));
        BT_WMDungeon02.onClick.AddListener(()=>OnDungeonButton(3));
        BT_WMDungeon03.onClick.AddListener(()=>OnDungeonButton(4));
        BT_WMDungeon04.onClick.AddListener(()=>OnDungeonButton(5));
        BT_WMDemonCastle.onClick.AddListener(()=>OnDungeonButton(6));
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

    #region UI ������ ��� & ��ư ���

    private void WindowUIReset()
    {
        UI_blind.SetActive(UI_blind_check);
        UI_UnActive?.SetActive(false);
        UI_Active?.SetActive(true);
        Debug.Log("����üũ");
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

    #region ���� ���
    private void Shopbuy()
    {

    }
    #endregion

    #region ��ȸ ���
    private void Churchbuy()
    {

    }
    #endregion

    #region ���� ���� ���  // ��� ���� ����˴ϴ� ������ �� �׾�ǿ� 
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
        //�̹��� ������Ʈ�κ����ϴ°� ���� 
        DGUI_name.text = DungeonInfo.instance.DG_info[num].nameDG;
        // �ؽ�Ʈ �Ŵ����� ���ؼ� �ؽ�Ʈ���� �޾ƿ;���. 
        DGUI_content.text = DungeonInfo.instance.DG_info[num].contentDG;
        // ���� ���� ���� �ʿ�.
    }
    #endregion




    #region UI ȯ�� ����
    /* UI ���� �ʱ�ȭ �Լ� 
     * ���� : ���� ������ UI��� �������� �������Ҷ� ȣ��Ǵ� �Լ�
     * 
     * - ��� �ؽ�Ʈ ��� ����
     * 
     * 
     */
    private void UIReset()
    {
        LanguageTranslate();
    }

    /* UI �ؽ�Ʈ ��� ���� �Լ�
     * ���� : ���ξ��� ���� ��� �ؽ�Ʈ�� �����Ͽ�, ������ ���� �°� �����Ű�� �Լ�
     * 
     * @UI_TextList : �ش� ���� �������ִ� ��� text
     * @UI_fixedvalue : �ش���� �Ҵ�� text ��ȣ�� ù��°��
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
