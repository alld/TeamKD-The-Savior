using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유틸성 높은 함수들을 관리하는 클래스입니다.
 * 자주 사용될 함수들을 관리합니다.
 * 멤버 변수의 선언을 금지합니다.
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

    // Recursive가 true일 경우 자식의 자식의 자식의 자식까지 쭉 반복하여 탐색합니다.
    // Recursive가 false일 경우 자신의 바로 아래 레이어(바로 아래 자식들)만 검색합니다.
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
