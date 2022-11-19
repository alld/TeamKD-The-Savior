using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Don'tDestroyOnLoad ���� �ʰ�, �� ������ ���, ���� �Ͽ� ����ϴ� �̱��� �Դϴ�.
 * ����� �� ����ϰ�, ������� ���� �� ������ �ϵ��� �մϴ�.
 */
public class Singleton_OneScene<T> : MonoBehaviour where T : MonoBehaviour
{
    private const string CAN_NOT_FIND_COMPONENT = " :: ������ ������Ʈ�� ã�� ���߽��ϴ�. �ν��Ͻ��� ���� �����մϴ�.";
    private const string NEED_UNREGIST = " :: ���� �ν��Ͻ��� ��������� ���� �ʾҽ��ϴ�.";
    private const string ALREADY_REGIST = " :: �̹� �ν��Ͻ��� ��ϵǾ� �ֽ��ϴ�.";
    private const string ALREADY_UNREGIST = " :: �̹� �ν��Ͻ��� �����Ǿ� �ֽ��ϴ�.";
    private const string INSTANCE_COMPLETE = " :: �ν��Ͻ��� �����Ǿ����ϴ�.";

    private static bool isComplete = false;    // �� �̱����� instance�� �����Ǿ��ִ��� Ȯ��.

    private static T _instance = null;
    public static T instance
    {
        get
        {
            T _component = GameObject.FindObjectOfType<T>();
            if(_component != null)
            {
                return _instance;
            }

            Debug.Log(CAN_NOT_FIND_COMPONENT);
            GameObject go = new GameObject { name = $"@{typeof(T).ToString()}" };
            _component = go.GetOrAddComponent<T>();
            Init(_component);
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
