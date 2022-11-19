using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameDataTable;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    //���� ���� ����
    public static string GameVersion = "Ver.00";

    private GameDataManager dataManager;
    public DungeonOS dungeonOS = null;// ������ ���� �Ŵ���
    public float playTime;
    public float dungeonPlayTime;
    public bool isDungeon = false;
    public GameData data;

    public int maxCardCount = 23;      // ���� ���ӿ� ����Ǿ��ִ� ī���� ��
    public int maxCharacterCount = 4;  // ���� ���ӿ� ����Ǿ��ִ� ĳ������ ��.
    public bool isSetting = false;

    public List<HaveCard> card = new List<HaveCard>();  // Json ���Ͽ� ����Ǿ�� �ϴ� ī���� ������

    public int cardIdx = 0;  // Json ���Ͽ� ����Ǿ��ִ� ī���� �ε���
    // Play���� UI�� Ư�� ��Ȳ�� ���� Ȱ��ȭ �ϱ� ���� �Լ�.
    private PlayUI playUI;
    private SceneLoad sceneLoad;

    public delegate void SoulEvent(int souls);
    public event SoulEvent soulEvent;

    #region ���� ������ ����


    #endregion

    private void Awake()
    {
        Regist(this);

        dataManager = GetComponent<GameDataManager>();
        sceneLoad = GetComponent<SceneLoad>();
        // ���� ���۽ÿ� Main���� �����Ѵ�.
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        // Play������ ����ϴ� UI ����
        playUI = GameObject.Find("PUIManager").GetComponent<PlayUI>();

        StartCoroutine(GameStart());
    }

    private void Start()
    {
        StartCoroutine(GameSave());
    }

    /// <summary>
    /// ���� �����͸� ���������� �����Ų��.
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameStart()
    {
        yield return StartCoroutine(GameLoad());
        yield return StartCoroutine(LoadCharExp());
        //yield return StartCoroutine(AddCardData());
        yield return StartCoroutine(LoadOwnCardData());
        yield return StartCoroutine(LoadPresetData());
        yield return StartCoroutine(CharacterIndexSetting());
        yield return StartCoroutine(StartRelic());
        isSetting = true;
    }

    // ���� ī�� �κ��丮�� �ʱ�ȭ�� �ȵ�.
    public CharacterInventory inven;
    PartySettingManager partySetting;
    CardDeck cardDeck;
    PlayToolBar tools;
    /// <summary>
    /// �����͸� ���������� �ʱ�ȭ ��Ų��.
    /// </summary>
    public IEnumerator GameReset()
    {
        partySetting = GameObject.Find("PUIManager").GetComponent<PartySettingManager>();
        cardDeck = partySetting.gameObject.GetComponent<CardDeck>();
        tools = partySetting.gameObject.GetComponent<PlayToolBar>();
        // ���� ������ �ʱ�ȭ
        data = dataManager.ResetGameData();
        yield return StartCoroutine(InitCharExp());
        yield return StartCoroutine(InitCardData());
        yield return StartCoroutine(InitPreset());
        //���� �� ������Ʈ ���� �ʱ�ȭ.
        yield return StartCoroutine(partySetting.PartySettingInit());
        yield return StartCoroutine(inven.DestroyCharacterInventory());
        yield return StartCoroutine(CharacterIndexSetting());
        yield return StartCoroutine(cardDeck.PresetInit(data.preset - 1));
        yield return StartCoroutine(tools.Gold());
        yield return StartCoroutine(cardDeck.DestroyCardDeck());
    }


    /// <summary>
    /// ���� ���۽� data�� haveRelic�� �ε����� ������Ŵ.
    /// ���� ������ ���� ����� ���� ��� ������ ������ ���·� ������ ������.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartRelic()
    {
        if (data.haveRelic.Count < 12)
        {
            for (int i = 1; i <= 12; i++)
            {
                data.haveRelic.Add(i);
            }
        }
        yield return null;
    }

    /// <summary>
    /// ���� ���۽� data�� haveCharacter�� �ε����� ������Ŵ.
    /// �ߺ��� ĳ���͸� ���� �ʱ� ���� �����.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CharacterIndexSetting()
    {
        if (data.haveCharacter.Count < maxCharacterCount)
        {
            for (int i = 0; i < maxCharacterCount; i++)
            {
                data.haveCharacter.Add(0);
            }
        }
        yield return null;
    }

    #region ���� ������ ����, �ҷ�����, �����ϱ�
    /// <summary>
    /// �����͸� �����Ѵ�.
    /// </summary>
    public IEnumerator GameSave()
    {
        yield return null;
        //while (true)
        //{
        //    yield return StartCoroutine(dataManager.SaveGameDataToJson(data));
        //}
    }
    /// <summary>
    /// �����͸� �ҷ��´�.
    /// </summary>
    public IEnumerator GameLoad()
    {
        data = dataManager.LoadGameDataFromJson();
        yield return null;
    }
    /// <summary>
    /// ī�� ������ �ʱ�ȭ
    /// </summary>
    private IEnumerator InitPreset()
    {
        for (int i = 0; i < 5; i++)
        {
            cardPreset[i] = dataManager.ResetPreset(i);
        }
        StartCoroutine(dataManager.SavePresetToJson());
        yield return null;
    }
    /// <summary>
    /// ���� ī�� ������ �ʱ�ȭ
    /// </summary>
    private IEnumerator InitCardData()
    {
        for (int i = 0; i < maxCardCount; i++)
        {
            cardDic[i + 1] = 0;
        }
        yield return StartCoroutine(dataManager.ResetCardData());
        yield return StartCoroutine(dataManager.WriteCardDataToJson());
        yield return null;
    }
    /// <summary>
    /// �������� ĳ������ ������ �ʱ�ȭ
    /// </summary>
    private IEnumerator InitCharExp()
    {
        for (int i = 0; i < maxCharacterCount; i++)
        {
            charExp[i] = dataManager.ResetCharExp(i);
        }
        yield return StartCoroutine(dataManager.WriteCharExp());
        yield return null;
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
    public IEnumerator SaveOrReviseCardData(int _key, int _value)
    {
        yield return StartCoroutine(dataManager.SaveCardData(_key, _value));
    }

    /// <summary>
    /// ȹ���Ͽ� ����� ī���� �����͸� �ҷ��ɴϴ�.
    /// </summary>
    private IEnumerator LoadOwnCardData()
    {
        //int idx = dataManager.CountMyCardData();
        for (int i = 1; i <= maxCardCount; i++)
        {
            cardDic.Add(i, dataManager.LoadMyCardData(i).ownCard);
        }
        yield return null;
    }


    public List<ReadCardData> cardData = new List<ReadCardData>();  // ���̺����� ������ �б����� ī�� ������.
    /// <summary>
    /// ī���� �����Ͱ� ����Ǿ��ִ� json���Ͽ��� ī���� id�� type�� �����ɴϴ�.
    /// </summary>
    public IEnumerator AddCardData()
    {
        cardData.Clear();
        int index = dataManager.CountCardData();
        for (int i = 0; i < index; i++)
        {
            cardData.Add(dataManager.ReadCardDataFromJson(i));
        }
        yield return null;
    }
    #endregion

    #region ĳ���� ����ġ, ���� ����

    public List<CharExp> charExp = new List<CharExp>();
    /// <summary>
    /// ĳ���� ����ġ / ���� ���� 
    /// </summary>
    public IEnumerator SaveCharExp(int id)
    {
        dataManager.SaveCharExp(charExp[id - 1]);
        dataManager.WriteCharExp();
        yield return null;
    }

    /// <summary>
    /// ĳ���� ����ġ / ���� �ҷ�����.
    /// </summary>
    public IEnumerator LoadCharExp()
    {
        charExp.Clear();
        //int idx = dataManager.CurrentCharIndex();
        for (int i = 0; i < maxCharacterCount; i++)
        {
            charExp.Add(dataManager.LoadCharExp(i));
        }
        yield return null;
    }
    #endregion

    #region ī�� ������

    public List<CardPreset> cardPreset = new List<CardPreset>(); // json ���Ͽ� ����Ǿ�� �ϴ� ī�� ������ ������
    public IEnumerator PresetSave()
    {
        for (int i = 0; i < 5; i++)
        {
            dataManager.SavePreset(cardPreset[i]);
        }
        yield return StartCoroutine(dataManager.SavePresetToJson());
    }

    /// <summary>
    /// json���Ͽ� ����� �������� �����͸� �ҷ��´�.
    /// </summary>
    public IEnumerator LoadPresetData()
    {
        for (int i = 1; i <= 5; i++)
        {
            cardPreset.Add(dataManager.LoadCardPreset(i));
        }
        yield return null;
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
                StartCoroutine(sceneLoad.MainSceneLoad());

                playUI.topBar.SetActive(false);
                playUI.partyBar.SetActive(false);
                isDungeon = false;
                break;
            // ������ ������ �̵��Ѵ�.
            case 1:
                StartCoroutine(sceneLoad.OpeningSceneLoad());

                data.CurrentScene = 1;
                isDungeon = false;
                break;
            // ����� ������ �̵��Ѵ�.
            case 2:
                StartCoroutine(sceneLoad.WorldMapSceneLoad());

                data.CurrentScene = 2;
                playUI.topBar.SetActive(true);
                playUI.partyBar.SetActive(true);
                playUI.dungeonBar.SetActive(false);
                isDungeon = false;

                PlayStory story = GameObject.Find("PUIManager").GetComponent<PlayStory>();

                switch (data.storyProgress)
                {
                    case 0:
                        story.OnDiaLog();
                        break;
                    case 69:
                        story.OnDiaLog();
                        break;                    
                    default:
                        break;
                }
                break;
            case 3:
                StartCoroutine(sceneLoad.TutorialSceneLoad());
                data.CurrentScene = 3;
                isDungeon = true;
                break;
        }
    }
    #endregion


    /// <summary>
    /// ���� ������ ��ȯ�� �÷��̾����� ����.
    /// </summary>
    public void MainSettingToPlay()
    {
        PlayOption playOp = GameObject.Find("PUIManager").GetComponent<PlayOption>();
        playOp.OnValueChanged_LanguageSetting();
    }
    private void Update()
    {
        if (currentlyScene == "Main") return;
        playTime += Time.deltaTime;
        if (dungeonOS != null) dungeonPlayTime += Time.deltaTime;
    }
}