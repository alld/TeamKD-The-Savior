using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
public class PrefabManager : Singleton_OneScene<PrefabManager>
{
    const string CARD = "Card_";
    const string RELIC = "Relic_";

    private Dictionary<string, UnityEngine.Object> pool = new Dictionary<string, UnityEngine.Object>();
    private Action<string> createObject;

    private void Awake()
    {
        Regist(this);
    }

    private void Start()
    {
        for (int i = 1; i <= 12; i++) StartCoroutine(LoadGameObject($"{RELIC}{i}"));
        for (int i = 1; i <= 23; i++) StartCoroutine(LoadGameObject($"{CARD}{i}"));
    }

    /// <summary>
    /// 불러올 소스의 패스와, 로드가 끝났을 때 실행될 콜백을 등록함.
    /// </summary>
    public IEnumerator LoadGameObject(string path, Action<string> callback = null)
    {
        AddCallBack(callback);

        if (pool.ContainsKey(path))
        {
            createObject?.Invoke(path);
            InitCallBack();
            yield break;
        }
        AsyncOperationHandle<GameObject> goHandle = Addressables.LoadAssetAsync<GameObject>(path);
        yield return goHandle;
        
        if(goHandle.Status == AsyncOperationStatus.Succeeded)
        {
            pool.Add(path, goHandle.Result);
            createObject?.Invoke(path);
            InitCallBack();
            Addressables.Release(goHandle);
        }
    }

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if (!pool.ContainsKey(path)) return null;

        return pool[path] as T;
    }

    // 리소스 로드가 끝났을 때 실행될 함수를 등록함.
    private void AddCallBack(Action<string> call)
    {
        createObject -= call;
        createObject += call;
    }

    // 한번 실행했으면 비워준다.
    private void InitCallBack()
    {
        if (createObject == null) return;
        createObject = null;
    }
}
