using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �Ŵ��� �����丵.
 * �ӽ÷� ���̺� �����ʹ� ���ҽ� �������� �о���� �������� ����.
 * ��巹����� �����͸� �ε��ϴ� ��İ�,
 * ���� ��Ʈ�� ����Ͽ� ������ ����ϴ� ��� �߿� ������Դϴ�.
 */
public class DataManager : Singleton<DataManager>
{
    private void Awake()
    {
        Regist(this);
    }
    public Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj, true);
    }
    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDick();
}
