using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
public class PrefabManager : Singleton_OneScene<PrefabManager>
{
    private Dictionary<string, UnityEngine.Object> pool = new Dictionary<string, UnityEngine.Object>();
    private Action<string> createObject;

    public IEnumerator LoadGameObject(string path)
    {
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

    public T GetPool<T>(string key) where T : UnityEngine.Object
    {
        if (pool.ContainsKey(key) == false) return null;
        return pool[key] as T;
    }

    /// <summary>
    /// Addressable Load가 완료 되면 구독해둔 함수를 실행한다.
    /// </summary>
    public void AddCallBack(Action<string> call)
    {
        createObject -= call;
        createObject += call;
    }

    // 한번 실행했으면 비워준다.
    private void InitCallBack()
    {
        createObject = null;
    }
}
