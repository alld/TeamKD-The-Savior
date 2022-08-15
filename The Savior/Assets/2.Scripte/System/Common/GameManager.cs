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
    public static string GameVersion = "Ver.00";

    private GameDataManager dataManager;
    public DungeonOS dungeonOS = null;// ������ ���� �Ŵ���
    public float playTime;
    public float dungeonPlayTime;
    public GameData data;
    public CharExp charData;
    public List<CharExp> charExp = new List<CharExp>();

    // Play���� UI�� Ư�� ��Ȳ�� ���� Ȱ��ȭ �ϱ� ���� �Լ�.
    private PlayUI playUI;

    #region ���� ������ ����
    

    public Dictionary<int, CardDataBase> currentCardList = new Dictionary<int, CardDataBase>();
    public Dictionary<int, CharacterDatabase> currentHeroList = new Dictionary<int, CharacterDatabase>();
    public Dictionary<int, RelicDataBase.InfoRelic> currentRelicList = new Dictionary<int, RelicDataBase.InfoRelic>();
    public List<int>[] currentDeck =
        {
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>(),
        };
    public CharacterDatabase[] partySlot =
        {

        };

    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this.gameObject);
        dataManager = GetComponent<GameDataManager>();
        // ���� ���۽ÿ� Main���� �����Ѵ�.
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        // Play������ ����ϴ� UI ����
        playUI = GameObject.Find("PUIManager").GetComponent<PlayUI>();

        GameLoad();
        //CharacterDataLoad();

    }

    #region ������ ����, �ҷ�����, �����ϱ�
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

    /// <summary>
    /// �����͸� �ʱ�ȭ ��Ų��.
    /// </summary>
    public void GameReset()
    {
        data = dataManager.ResetGameData();
    }

    /// <summary>
    /// n�� ĳ������ �����͸� Json���Ͽ� �����Ѵ�.
    /// </summary>
    /// <param name="n"></param>
    public void CharacterDataSave(int n)
    {
        dataManager.SaveCharExp(charExp[n]);
    }

    /// <summary>
    /// n�� ĳ������ �����͸� �ӽ� �����Ѵ�.
    /// </summary>
    /// <param name="n"></param>
    public void SaveExp(int n)
    {
        charExp.Add(new CharExp { id = n, exp = 10, level = 1 });
    }

    /// <summary>
    /// ĳ������ ��ȣ�� �ҷ��´�.
    /// </summary>
    public void CharacterDataLoad()
    {
        for (int i = 0; i < data.haveCharacter.Count; i++)
        {
            charExp.Add(dataManager.LoadCharExp(i));
            Debug.Log("id : " + charExp[i].id);
        }
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

        playTime += Time.deltaTime;
        if (dungeonOS != null) dungeonPlayTime += Time.deltaTime;
    }
    #endregion
}
