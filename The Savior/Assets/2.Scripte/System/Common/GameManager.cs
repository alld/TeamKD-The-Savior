using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameDataTable;

public class GameManager : MonoBehaviour
{
    //�̱��� ���� ���� 
    public static GameManager instance = null;

    //���� ���� ����
    public const string GameVersion = "Ver.00";

    private GameDataManager dataManager;
    public GameData data;

    // Play���� UI�� Ư�� ��Ȳ�� ���� Ȱ��ȭ �ϱ� ���� �Լ�.
    private PlayUI playUI;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this.gameObject);

        dataManager = GetComponent<GameDataManager>();

        // ���� ���۽ÿ� Main���� �����Ѵ�.
        //SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        // Play������ ����ϴ� UI ����
        playUI = GameObject.Find("PUIManager").GetComponent<PlayUI>();

        GameLoad();
    }

    private void Start()
    {

    }
    #region ������ ����, �ҷ�����
    /// <summary>
    /// �����͸� �����Ѵ�.
    /// </summary>
    public void GameSave()
    {
        dataManager.SaveGameDataToJson(data);
    }

    /// <summary>
    /// �����͸� �ҷ��´�.
    /// </summary>
    public void GameLoad()
    {
        data = dataManager.LoadGameDataFromJson();
    }
    #endregion

    #region �� ����
    public string currentlyScene = "Main";

    /// <summary>
    /// ���� ���� ��ε� �ϰ�, �Էµ� ��ȣ�� �´� ���� Additive���ش�.
    /// <br>����, ���� ��ε� �� ��, �ε� �� �� �ʿ��� �͵��� �������ش�.</br>
    /// </summary>
    /// <param name="num"></param>
    public void SceneChange(int num)
    {
        switch (num)
        {
            // ���ξ����� �̵��Ѵ�.
            case 0:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "Main";
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);

                playUI.topBar.SetActive(false);
                playUI.partyBar.SetActive(false);
                break;
            // ������ ������ �̵��Ѵ�.
            case 1:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "Opening";
                data.CurrentScene = 1;
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
                break;
            // ����� ������ �̵��Ѵ�.
            case 2:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "WorldMap";
                data.CurrentScene = 2;
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);

                playUI.topBar.SetActive(true);
                playUI.partyBar.SetActive(true);
                playUI.dungeonBar.SetActive(false);
                break;
            case 3:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "Tutorial";
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
                break;
        }
    }

    private void Update()
    {
        if (currentlyScene == "Main") return;
        if (data != dataManager.LoadGameDataFromJson()) GameSave();
    }
    #endregion
}
