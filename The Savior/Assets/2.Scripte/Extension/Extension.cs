using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 *  확장 함수를 관리하는 클래스입니다.
 *  staic class입니다. 함수도 모두 static으로 선언해주세요.
 *  멤버 변수는 선언은 금지입니다.
 *  
 *  선언 방식 : 인자 앞에 this를 붙이면 됩니다.
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
