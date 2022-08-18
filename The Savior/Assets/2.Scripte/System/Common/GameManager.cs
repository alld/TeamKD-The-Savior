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
    public bool isDungeon = false;
    public GameData data;
    public List<CharExp> charExp = new List<CharExp>(); // Json ���Ͽ� ����Ǿ�� �ϴ� ĳ������ ������
    public List<HaveCard> card = new List<HaveCard>();  // Json ���Ͽ� ����Ǿ�� �ϴ� ī���� ������
    public List<CardPreset> cardPreset = new List<CardPreset>(); // json ���Ͽ� ����Ǿ�� �ϴ� ī�� ������ ������
    public int cardIdx = 0;  // Json ���Ͽ� ����Ǿ��ִ� ī���� �ε���
    // Play���� UI�� Ư�� ��Ȳ�� ���� Ȱ��ȭ �ϱ� ���� �Լ�.
    private PlayUI playUI;

    #region ���� ������ ����


    public Dictionary<int, CardDataBase.Data> currentCardList = new Dictionary<int, CardDataBase.Data>();
    public Dictionary<int, CharacterDatabase.Data> currentHeroList = new Dictionary<int, CharacterDatabase.Data>();
    public Dictionary<int, RelicData.Data> currentRelicList = new Dictionary<int, RelicData.Data>();
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
        LoadCardData();
        LoadPresetData();
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
    /// ���� ī�� ��Ȳ�� Json ���Ϸ� �����Ѵ�.
    /// </summary>
    /// <param name="n"></param>
    public void CardSave()
    {
        cardIdx = dataManager.CurrentCardData();
        // ����Ʈ�� �ε����� 0���� ����
        // ��ȯ�� cardIdx�� 1���� �����Ͽ� ������ ���� +1 �̱� ������
        // cardIdx -2�� �ִ� ����Ʈ �ε���
        for (int cardArr = 0; cardArr < (cardIdx - 1); cardArr++)
        {
            dataManager.GainCard(card[cardArr]);
        }
        dataManager.SaveCard();
    }

    /// <summary>
    /// Json ���Ͽ� ī�� �ε����� ���̳� �ִ��� ���� �ް�,
    /// <br>�ε��� �� ��ŭ �����͸� �ҷ��ɴϴ�.</br>
    /// </summary>
    public void LoadCardData()
    {
        card.Clear();
        cardIdx = dataManager.CurrentCardData();
        for (int cardArr = 1; cardArr <= cardIdx; cardArr++)
        {
            card.Add(dataManager.CardDataLoad(cardArr));
        }
    }

    public void PresetSave()
    {
        for (int i = 0; i < 5; i++)
        {
            dataManager.SavePreset(cardPreset[i]);
        }
        dataManager.SavePresetToJson();
    }

    public void LoadPresetData()
    {
        cardPreset.Clear();
        for (int i = 0; i < 5; i++)
        {
            cardPreset.Add(dataManager.LoadCardPreset(i + 1));
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
                data.CurrentScene = 3;
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
                isDungeon = true;
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
