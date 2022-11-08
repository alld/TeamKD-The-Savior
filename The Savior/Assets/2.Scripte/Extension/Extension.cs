using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
