using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Don'tDestroyOnLoad 되지 않고, 한 씬에서 등록, 해제 하여 사용하는 싱글톤 입니다.
 * 사용할 때 등록하고, 사용하지 않을 때 릴리즈 하도록 합니다.
 */
public class Singleton_OneScene<T> : MonoBehaviour where T : MonoBehaviour
{
    private const string CAN_NOT_FIND_COMPONENT = " :: 씬에서 컴포넌트를 찾지 못했습니다. 인스턴스를 새로 생성합니다.";
    private const string NEED_UNREGIST = " :: 이전 인스턴스의 등록해제가 되지 않았습니다.";
    private const string ALREADY_REGIST = " :: 이미 인스턴스가 등록되어 있습니다.";
    private const string ALREADY_UNREGIST = " :: 이미 인스턴스가 해제되어 있습니다.";
    private const string INSTANCE_COMPLETE = " :: 인스턴스가 생성되었습니다.";

    private static bool isComplete = false;    // 이 싱글톤의 instance가 생성되어있는지 확인.

    private static T _instance = null;
    public static T instance
    {
        get
        {
            T _component = GameObject.FindObjectOfType<T>();
            if(_component != null)
            {
                Regist(_component);
                return _instance;
            }

            Debug.Log(CAN_NOT_FIND_COMPONENT);
            GameObject go = new GameObject { name = $"@{typeof(T).ToString()}" };
            _component = go.GetOrAddComponent<T>();
            Init(_instance);
            return _instance;
        }
    }
    static void Init(T init_instance)
    {
        _instance = init_instance;
        isComplete = true;
        Debug.Log(typeof(T).ToString() + INSTANCE_COMPLETE);
    }

    protected static void Regist(T instance)
    {
        if(instance == null)
        {
            Debug.Log("Instance is Null !!!");
            return;
        }

        if (_instance == null)
        {
            if (isComplete) Debug.Log(NEED_UNREGIST);

            Init(instance);
        }
        else
            Debug.Log(typeof(T).ToString() + ALREADY_REGIST);
    }

    protected static void UnRegist()
    {
        if (_instance != null)
        {
            _instance = null;
            isComplete = false;
        }
        else
            Debug.Log(typeof(T).ToString() + ALREADY_UNREGIST);

        Debug.Log("UnRegist Complete !!!");
    }

}
