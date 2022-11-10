using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �Ŵ��� �����丵.
 * �ӽ÷� ���̺� �����ʹ� ���ҽ� �������� �о���� �������� ����.
 * ��巹����� �����͸� �ε��ϴ� ��İ�,
 * ���� ��Ʈ�� ����Ͽ� ������ ����ϴ� ��� �߿� ������Դϴ�.
 */
public abstract class DataManager : Singleton<DataManager>
{
    private void Awake()
    {
        Regist(this);
    }

    protected Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    protected abstract void Init();
    protected abstract void Load();
}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDick();
}
