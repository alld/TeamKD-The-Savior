using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��ƿ�� ���� �Լ����� �����ϴ� Ŭ�����Դϴ�.
 * ���� ���� �Լ����� �����մϴ�.
 * ��� ������ ������ �����մϴ�.
 */

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform tr = FindChild<Transform>(go, name, recursive);
        if (tr = null)
            return null;

        return tr.gameObject;
    }

    // Recursive�� true�� ��� �ڽ��� �ڽ��� �ڽ��� �ڽı��� �� �ݺ��Ͽ� Ž���մϴ�.
    // Recursive�� false�� ��� �ڽ��� �ٷ� �Ʒ� ���̾�(�ٷ� �Ʒ� �ڽĵ�)�� �˻��մϴ�.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive == false)
        {
            for(int i  = 0; i < go.transform.childCount; i++)
            {
                Transform tr = go.transform.GetChild(i);
                if(string.IsNullOrEmpty(name) || tr.name == name)
                {
                    T component = tr.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
}
