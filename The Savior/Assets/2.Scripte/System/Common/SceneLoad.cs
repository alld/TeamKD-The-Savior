using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoad : MonoBehaviour
{
    AsyncOperation operation;
    public Transform fadeTr;
    [SerializeField]private string currentlyScene = "Main";
    public string _currentlyScene { get { return currentlyScene; } }
    private GameObject fadeScreen;
    private Slider loadingBar;

    [SerializeField] private GameObject _fadeScreen;
    [SerializeField] private Slider _loadingBar;

    void Start()
    {
        fadeScreen = Resources.Load<GameObject>("Fade");
        loadingBar = Resources.Load<Slider>("LoadingBar");
    }

    /// <summary>
    /// ·ÎµùÃ¢ »ý¼º
    /// </summary>
    private void CreateLodingScreen()
    {
        _fadeScreen = Instantiate(fadeScreen, fadeTr);
        _loadingBar = Instantiate(loadingBar, fadeTr);
        _loadingBar.value = 0;
    }
    /// <summary>
    /// ·ÎµùÃ¢ ÆÄ±«
    /// </summary>
    private void DestroyLoadingScreen()
    {
        Destroy(_fadeScreen);
        Destroy(_loadingBar.gameObject);
    }

    /// <summary>
    /// ¸ÞÀÎ ¾À ·Îµù
    /// </summary>
    /// <returns></returns>
    public IEnumerator MainSceneLoad()
    {
        CreateLodingScreen();
        operation = SceneManager.UnloadSceneAsync(currentlyScene);
        currentlyScene = "Main";
        operation = SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        if (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            Debug.Log(loadingBar.value * 100.0f);
            yield return null;
        }
        DestroyLoadingScreen();
        operation.allowSceneActivation = true;
    }

    /// <summary>
    /// ¿ÀÇÁ´× ¾À ·Îµù
    /// </summary>
    /// <returns></returns>
    public IEnumerator OpeningSceneLoad()
    {
        CreateLodingScreen();
        operation = SceneManager.UnloadSceneAsync(currentlyScene);
        currentlyScene = "Opening";
        operation = SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
        if (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            Debug.Log(loadingBar.value * 100.0f);
            yield return null;
        }
        DestroyLoadingScreen();
        operation.allowSceneActivation = true;
    }

    /// <summary>
    /// ¿ùµå ¸Ê ¾À ·Îµù
    /// </summary>
    /// <returns></returns>
    public IEnumerator WorldMapSceneLoad()
    {
        CreateLodingScreen();
        operation = SceneManager.UnloadSceneAsync(currentlyScene);
        currentlyScene = "WorldMap";
        operation = SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
        if (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
        DestroyLoadingScreen();
        operation.allowSceneActivation = true;
    }

    /// <summary>
    /// Æ©Åä¸®¾ó ¾À ·Îµù
    /// </summary>
    /// <returns></returns>
    public IEnumerator TutorialSceneLoad()
    {
        CreateLodingScreen();
        operation = SceneManager.UnloadSceneAsync(currentlyScene);
        currentlyScene = "Tutorial";
        operation = SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
        if (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            Debug.Log(loadingBar.value * 100.0f);
            yield return null;
        }
        DestroyLoadingScreen();
        operation.allowSceneActivation = true;
    }
}
