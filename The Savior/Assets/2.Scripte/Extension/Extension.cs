using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 *  Ȯ�� �Լ��� �����ϴ� Ŭ�����Դϴ�.
 *  staic class�Դϴ�. �Լ��� ��� static���� �������ּ���.
 *  ��� ������ ������ �����Դϴ�.
 *  
 *  ���� ��� : ���� �տ� this�� ���̸� �˴ϴ�.
 */

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false) where T : MonoBehaviour
    {
        T component = Util.FindChild<T>(go, name, recursive);
        if (component = null)
            return null;

        return component;
    }

    public static void SetToScreenSize(this Image image)
    {
        image.rectTransform.sizeDelta = new Vector2(1920, 1080);
        image.rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}
