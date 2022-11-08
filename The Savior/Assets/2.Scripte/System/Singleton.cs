using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// �̱��� Ŭ���� ����
//
// 1. ����� �޴´�. 
// => public class TestClass : Singleton<TestClass>
//
// 2. ȣ���� �Ѵ� or Awake���� �ν��Ͻ��� ������ش�. 
// => TestClass.instance of Awake(this);
// 
// ���� ���� �ʿ��� �ν��Ͻ��� �����ϴ� Ŭ�����Դϴ�.

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    const string CAN_NOT_FIND_INSTANCE_AND_CREATE = "������Ʈ�� ã�� ���߽��ϴ�. �ش� ��ü�� �����մϴ�.";

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
