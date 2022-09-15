using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAdmin : MonoBehaviour
{
    [Header("�κ��丮")]
    public Button admin;
    public Button close;
    public GameObject adminObject; 

    [Space(10f)]

    [Header("�κ��丮 ���� ���")]
    [Tooltip("0 : ���� \n 1 : ĳ���� \n 2 : ī�� \n 3 : ����")]
    public Button[] inventorys = new Button[4];
    public GameObject[] inventorysObject = new GameObject[4];

    private bool inventoryOpen = false;

    private void Start()
    {
        admin.onClick.AddListener(() => OnClick_OpenInventoryBtn());
        close.onClick.AddListener(() => OnClick_OpenInventoryBtn());    // �ش� �Լ��� �κ��丮�� �������� �� �ѹ� �� ȣ��Ǹ� �κ��丮�� ����.

        for (int i = 0; i < inventorys.Length; i++)
        {
            int temp = i;
            inventorys[temp].onClick.AddListener(() => OnClick_OpenToolsBtn(temp));
        }
    }

    /// <summary>
    /// �κ��丮 ���� ����.
    /// </summary>
    private void OnClick_OpenInventoryBtn()
    {
        inventoryOpen = !inventoryOpen;
        adminObject.SetActive(inventoryOpen);
    }

    /// <summary>
    /// �κ��丮 �κ��丮 â�� �� ��� ����
    /// </summary>
    /// <param name="n"></param>
    private void OnClick_OpenToolsBtn(int n)
    {
        // �κ��丮 â ����.
        OnClick_OpenInventoryBtn();

        // ������ �����ִ� â�� ��� ����
        foreach (var item in inventorysObject)
        {
            item.SetActive(false);
        }

        inventorysObject[n].SetActive(true);
    }
}