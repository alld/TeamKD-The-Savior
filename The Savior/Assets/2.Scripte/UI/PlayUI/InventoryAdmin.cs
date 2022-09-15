using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAdmin : MonoBehaviour
{
    [Header("인벤토리")]
    public Button admin;
    public Button close;
    public GameObject adminObject; 

    [Space(10f)]

    [Header("인벤토리 내부 기능")]
    [Tooltip("0 : 유물 \n 1 : 캐릭터 \n 2 : 카드 \n 3 : 업적")]
    public Button[] inventorys = new Button[4];
    public GameObject[] inventorysObject = new GameObject[4];

    private bool inventoryOpen = false;

    private void Start()
    {
        admin.onClick.AddListener(() => OnClick_OpenInventoryBtn());
        close.onClick.AddListener(() => OnClick_OpenInventoryBtn());    // 해당 함수는 인벤토리가 열려있을 때 한번 더 호출되면 인벤토리가 닫힘.

        for (int i = 0; i < inventorys.Length; i++)
        {
            int temp = i;
            inventorys[temp].onClick.AddListener(() => OnClick_OpenToolsBtn(temp));
        }
    }

    /// <summary>
    /// 인벤토리 열고 닫음.
    /// </summary>
    private void OnClick_OpenInventoryBtn()
    {
        inventoryOpen = !inventoryOpen;
        adminObject.SetActive(inventoryOpen);
    }

    /// <summary>
    /// 인벤토리 인벤토리 창의 각 기능 열기
    /// </summary>
    /// <param name="n"></param>
    private void OnClick_OpenToolsBtn(int n)
    {
        // 인벤토리 창 닫음.
        OnClick_OpenInventoryBtn();

        // 기존의 열려있는 창은 모두 닫음
        foreach (var item in inventorysObject)
        {
            item.SetActive(false);
        }

        inventorysObject[n].SetActive(true);
    }
}