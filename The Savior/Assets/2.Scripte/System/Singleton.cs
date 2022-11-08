using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 싱글톤 클래스 사용법
//
// 1. 상속을 받는다. 
// => public class TestClass : Singleton<TestClass>
//
// 2. 호출을 한다 or Awake에서 인스턴스를 등록해준다. 
// => TestClass.instance of Awake(this);
// 
// 게임 내내 필요한 인스턴스만 생성하는 클래스입니다.

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    const string CAN_NOT_FIND_INSTANCE_AND_CREATE = "컴포넌트를 찾지 못했습니다. 해당 객체를 생성합니다.";

    private static T _instance = null;
    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                T component_find = FindObjectOfType<T>();
                if (component_find != null)
                {
                    _instance = component_find;
                }
                else if (component_find == null)
                {
                    Debug.Log(CAN_NOT_FIND_INSTANCE_AND_CREATE);

                    GameObject go = new GameObject { name = typeof(T).ToString() };
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected static void Regist(T instance)
    {
        if(instance == null)
        {
            Debug.Log("Instance is Null..");
            return;
        }

        if(_instance == null)
        {
            DontDestroyOnLoad(instance.gameObject);
            instance.gameObject.name = typeof(T).ToString();
            _instance = instance;

            Debug.Log("Regist Complete!!");
        }
        else
        {
            Debug.Log("Instance Already Registed..");
            return;
        }
    }
}
