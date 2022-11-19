using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // ĵ������ ���� ������ ���� popup UI�� �����ϵ��� ��������ϴ�.
    // popupTransform = Canvas�� Transform�Դϴ�.
    private Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    private Transform popupTransform;

    private void Awake()
    {
        Regist(this);
    }

    private void Start()
    {
        popupTransform = GameObject.Find("PopupCanvas").transform;
    }

    public void Show(string path)
    {
        PrefabManager.instance.AddCallBack(path => CreatePopup(path));
        StartCoroutine(PrefabManager.instance.LoadGameObject(path));
    }

    private void CreatePopup(string path)
    {
        UI_Popup popup = Instantiate( PrefabManager.instance.Load<GameObject>(path), popupTransform).GetOrAddComponent<UI_Popup>();
        popupStack.Push(popup);
    }

    public void Hide()
    {
        if (popupStack.Count == 0) return;
        UI_Popup popup = popupStack.Pop();

        Destroy(popup.gameObject);
    }

}
