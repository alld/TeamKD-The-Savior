using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Don'tDestroyOnLoad 되지 않고, 한 씬에서 등록, 해제 하여 사용하는 싱글톤 입니다.
 * 사용할 때 등록하고, 사용하지 않을 때 릴리즈 하도록 합니다.
 */
public class Singleton_OneScene<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T instance
    {
        get
        {
            return null;
        }
    }
    protected void Init()
    {

    }

    protected void Regist()
    {

    }

    protected void UnRegist()
    {

    }

}
