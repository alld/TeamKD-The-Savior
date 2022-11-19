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

    protected abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] obj = new UnityEngine.Object[names.Length];

        _obj.Add(type, obj);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                obj[i] = Util.FindChild(gameObject, names[i], true);
            else
                obj[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (obj[i] == null)
                Debug.Log($"Failed to bind{names[i]}");
        }
    }

    protected List<T> Get<T>(Type type)where T : UnityEngine.Object
    {
        List<T> list = new List<T>();
        foreach (var item in _obj[type])
        {
            list.Add(item as T);
        }

        return list;
    }

    protected T Get<T>(Type type, int idx)where T : UnityEngine.Object
    {
        UnityEngine.Object[] obj = null;
        if (_obj.TryGetValue(type, out obj) == false)
            return null;

        return obj[idx] as T;
    }

    protected TMP_Text GetText(Type type, int idx) { return Get<TMP_Text>(type, idx); }
    protected Button GetButton(Type type, int idx) { return Get<Button>(type, idx); }
    protected Image GetImage(Type type, int idx) { return Get<Image>(type, idx); }
    protected GameObject GetGameObject(Type type, int idx) { return Get<GameObject>(type, idx); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        EventHandle eventHandle = go.GetOrAddComponent<EventHandle>();
        switch (type)
        {
            case Define.UIEvent.Click:
                eventHandle.OnClickEvent -= action;
                eventHandle.OnClickEvent += action;
                break;
            case Define.UIEvent.Drag:
                eventHandle.OnDragEvent -= action;
                eventHandle.OnDragEvent += action;
                break;
            default:
                break;
        }
    }
}
