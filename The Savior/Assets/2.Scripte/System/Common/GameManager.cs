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

    public int maxCardCount = 23;      // ���� ���ӿ� ����Ǿ��ִ� ī���� ��


    public List<HaveCard> card = new List<HaveCard>();  // Json ���Ͽ� ����Ǿ�� �ϴ� ī���� ������

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

        //LoadCardData();

        // ���� ���� �����͸� �ҷ��´�.
        GameLoad();
        // ĳ������ �����͸� �ҷ��´�.
        LoadCharExp();
        // ī���� �����͸� �ҷ��´�.
        AddCardData();
        LoadOwnCardData();
        // ī�� ������ �����͸� �ҷ��´�.
        LoadPresetData();
    }

    #region ���� ������ ����, �ҷ�����, �����ϱ�
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
    #endregion


    #region ī�� ���̺�, �ε�


    public Dictionary<int, int> cardDic = new Dictionary<int, int>();       // �ڽ��� ȹ���� ī���� ����Ʈ.
    /// <summary>
    /// ī�带 ȹ�� �Ͽ��� ���, �ش� id�� ī�带 ���� ������Ű�� �����մϴ�.
    /// <br> ���� ī�� ������ �״�� ���� �ϱ� ������ ī�带 ����� ��� �����͸� ���� ��Ų �� �Լ��� ȣ���ؾ��մϴ�.</br>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="idx"></param>
    public void SaveOrReviseCardData(int _key, int _value)
    {
        dataManager.SaveCardData(_key, _value);
        dataManager.WriteCardDataToJson();
    }

    /// <summary>
    /// ȹ���Ͽ� ����� ī���� �����͸� �ҷ��ɴϴ�.
    /// </summary>
    public void LoadOwnCardData()
    {
        //int idx = dataManager.CountMyCardData();
        for (int i = 1; i <= maxCardCount; i++)
        {
            cardDic.Add(i, dataManager.LoadMyCardData(i).ownCard);
        }
    }


    public List<ReadCardData> cardData = new List<ReadCardData>();  // ���̺��� ������ �б����� ī�� ������.
    /// <summary>
    /// ī���� �����Ͱ� ����Ǿ��ִ� json���Ͽ��� ī���� id�� type�� �����ɴϴ�.
    /// </summary>
    public void AddCardData()
    {
        cardData.Clear();
        int index = dataManager.CountCardData();
        for (int i = 0; i < index; i++)
        {
            cardData.Add(dataManager.ReadCardDataFromJson(i));
        }
    }
    #endregion


    #region ĳ���� ����ġ, ���� ����

    public List<CharExp> charExp = new List<CharExp>();
    /// <summary>
    /// ĳ���� ����ġ / ���� ���� 
    /// </summary>
    public void SaveCharExp(int id)
    {
        dataManager.SaveCharExp(charExp[id]);
        dataManager.WriteCharExp();
    }

    /// <summary>
    /// ĳ���� ����ġ / ���� �ҷ�����.
    /// </summary>
    public void LoadCharExp()
    {
        charExp.Clear();
        int idx = dataManager.CurrentCharIndex();
        for (int i = 0; i < idx; i++)
        {
            charExp.Add(dataManager.LoadCharExp(i));
        }
    }
    #endregion


    #region ī�� ������

    public List<CardPreset> cardPreset = new List<CardPreset>(); // json ���Ͽ� ����Ǿ�� �ϴ� ī�� ������ ������
    public void PresetSave()
    {
        for (int i = 0; i < 5; i++)
        {
            dataManager.SavePreset(cardPreset[i]);
        }
        dataManager.SavePresetToJson();
    }

    /// <summary>
    /// json���Ͽ� ����� �������� �����͸� �ҷ��´�.
    /// </summary>
    public void LoadPresetData()
    {
        InitPresetData();
        int idx = dataManager.CountPresetData();
        for (int i = 0; i < idx; i++)
        {
            cardPreset[i] = dataManager.LoadCardPreset(i + 1);
        }
    }

    public void InitPresetData()
    {
        cardPreset.Clear();
        for (int i = 1; i <= 5; i++)
        {
            cardPreset.Add(dataManager.InitCardPreset(i));
        }
    }

    #endregion


    ///// <summary>
    ///// ���� ī�� ��Ȳ�� Json ���Ϸ� �����Ѵ�.
    ///// </summary>
    ///// <param name="n"></param>
    //public void CardSave()
    //{
    //    cardIdx = dataManager.CurrentCardData();
    //    // ����Ʈ�� �ε����� 0���� ����
    //    // ��ȯ�� cardIdx�� 1���� �����Ͽ� ������ ���� +1 �̱� ������
    //    // cardIdx -2�� �ִ� ����Ʈ �ε���
    //    for (int cardArr = 0; cardArr < (cardIdx - 1); cardArr++)
    //    {
    //        dataManager.GainCard(card[cardArr]);
    //    }
    //    dataManager.SaveCard();
    //}

    ///// <summary>
    ///// Json ���Ͽ� ī�� �ε����� ���̳� �ִ��� ���� �ް�,
    ///// <br>�ε��� �� ��ŭ �����͸� �ҷ��ɴϴ�.</br>
    ///// </summary>
    //public void LoadCardData()
    //{
    //    card.Clear();
    //    cardIdx = dataManager.CurrentCardData();
    //    for (int cardArr = 1; cardArr <= cardIdx; cardArr++)
    //    {
    //        card.Add(dataManager.CardDataLoad(cardArr));
    //    }
    //}


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
                isDungeon = false;
                break;
            // ������ ������ �̵��Ѵ�.
            case 1:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "Opening";
                data.CurrentScene = 1;
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
                isDungeon = false;
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
                isDungeon = false;
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
    #endregion
    private void Update()
    {
        if (currentlyScene == "Main") return;
        if (data != dataManager.LoadGameDataFromJson())
        {
            GameSave();
        }

        playTime += Time.deltaTime;
        if (dungeonOS != null) dungeonPlayTime += Time.deltaTime;
    }
}
