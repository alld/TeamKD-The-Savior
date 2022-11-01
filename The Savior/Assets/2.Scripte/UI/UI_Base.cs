using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class UI_Base : MonoBehaviour
{
    private Dictionary<Type, UnityEngine.Object[]> _obj = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] obj = new UnityEngine.Object[names.Length];

        _obj.Add(typeof(T), obj);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                obj[i] = Util.FindChild(gameObject, names[i], true);
            else
                obj[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (obj[i] == null)
                Debug.Log($"Failedto bind{names[i]}");
        }
    } 

    protected T Get<T>(int idx)where T : UnityEngine.Object
    {
        UnityEngine.Object[] obj = null;
        if (_obj.TryGetValue(typeof(T), out obj) == false)
            return null;

        return obj[idx] as T;
    }

    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected GameObject GetGameObject(int idx) { return Get<GameObject>(idx); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        // UI 클릭, 드래그 이벤트 처리
    }
}
