using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 데이터 매니저 리팩토링.
 * 임시로 테이블 데이터는 리소스 폴더에서 읽어오는 형식으로 구현.
 * 어드레서블로 데이터를 로드하는 방식과,
 * 구글 시트를 사용하여 데이터 통신하는 방식 중에 고민중입니다.
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
