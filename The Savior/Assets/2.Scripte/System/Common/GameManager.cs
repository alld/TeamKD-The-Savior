using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameDataTable;
using System.IO;

public class GameManager : MonoBehaviour
{
    //싱글턴 패턴 적용 
    public static GameManager instance = null;

    //게임 버전 정보
    public static string GameVersion = "Ver.00";

    private GameDataManager dataManager;
    public DungeonOS dungeonOS = null;// 현재의 던전 매니저
    public float playTime;
    public float dungeonPlayTime;
    public bool isDungeon = false;
    public GameData data;

    public int maxCardCount = 23;      // 현재 게임에 적용되어있는 카드의 수
    public int maxCharacterCount = 4;  // 현재 게임에 적용되어있는 캐릭터의 수.
    public bool isSetting = false;

    public List<HaveCard> card = new List<HaveCard>();  // Json 파일에 저장되어야 하는 카드의 데이터

    public int cardIdx = 0;  // Json 파일에 저장되어있는 카드의 인덱스
    // Play씬의 UI를 특정 상황에 따라 활성화 하기 위한 함수.
    private PlayUI playUI;

    #region 유동 데이터 관리


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
        // 게임 시작시에 Main씬을 연결한다.
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        // Play씬에서 사용하는 UI 연결
        playUI = GameObject.Find("PUIManager").GetComponent<PlayUI>();

        StartCoroutine(GameStart());
    }

    /// <summary>
    /// 게임 데이터를 순차적으로 연결시킨다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameStart()
    {
        yield return StartCoroutine(GameLoad());
        yield return StartCoroutine(LoadCharExp());
        yield return StartCoroutine(AddCardData());
        yield return StartCoroutine(LoadOwnCardData());
        yield return StartCoroutine(LoadPresetData());
        yield return StartCoroutine(CharacterIndexSetting());
        yield return StartCoroutine(StartRelic());
        isSetting = true;
    }

    // 현재 카드 인벤토리가 초기화가 안됨.
    public CharacterInventory inven;
    PartySettingManager partySetting;
    CardDeck cardDeck;
    PlayToolBar tools;
    /// <summary>
    /// 데이터를 순차적으로 초기화 시킨다.
    /// </summary>
    public IEnumerator GameReset()
    {
        partySetting = GameObject.Find("PUIManager").GetComponent<PartySettingManager>();
        cardDeck = partySetting.gameObject.GetComponent<CardDeck>();
        tools = partySetting.gameObject.GetComponent<PlayToolBar>();
        // 게임 데이터 초기화
        data = dataManager.ResetGameData();
        yield return StartCoroutine(InitCharExp());
        yield return StartCoroutine(InitCardData());
        yield return StartCoroutine(InitPreset());
        //게임 내 오브젝트 세팅 초기화.
        yield return StartCoroutine(partySetting.PartySettingInit());
        yield return StartCoroutine(inven.DestroyCharacterInventory());
        yield return StartCoroutine(CharacterIndexSetting());
        yield return StartCoroutine(cardDeck.PresetInit(data.preset - 1));
        yield return StartCoroutine(tools.Gold());
        yield return StartCoroutine(cardDeck.DestroyCardDeck());
    }


    /// <summary>
    /// 게임 시작시 data의 haveRelic의 인덱스를 생성시킴.
    /// 현재 유물을 얻을 방법이 없어 모든 유물을 보유한 상태로 게임을 시작함.
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
    /// 게임 시작시 data의 haveCharacter의 인덱스를 생성시킴.
    /// 중복된 캐릭터를 뽑지 않기 위해 설계됨.
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

    #region 게임 데이터 저장, 불러오기, 리셋하기
    /// <summary>
    /// 데이터를 저장한다.
    /// </summary>
    public IEnumerator GameSave()
    {
        yield return StartCoroutine(dataManager.SaveGameDataToJson(data));
    }

    /// <summary>
    /// 데이터를 불러온다.
    /// </summary>
    public IEnumerator GameLoad()
    {
        data = dataManager.LoadGameDataFromJson();
        yield return null;
    }


   
    /// <summary>
    /// 카드 프리셋 초기화
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
    /// 보유 카드 데이터 초기화
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
    /// 보유중인 캐릭터의 데이터 초기화
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


    #region 카드 세이브, 로드


    public Dictionary<int, int> cardDic = new Dictionary<int, int>();       // 자신이 획득한 카드의 리스트.
    /// <summary>
    /// 카드를 획득 하였을 경우, 해당 id의 카드를 한장 증가시키고 저장합니다.
    /// <br> 현재 카드 개수를 그대로 대입 하기 때문에 카드를 얻었을 경우 데이터를 증가 시킨 후 함수를 호출해야합니다.</br>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="idx"></param>
    public IEnumerator SaveOrReviseCardData(int _key, int _value)
    {
        yield return StartCoroutine(dataManager.SaveCardData(_key, _value));
    }

    /// <summary>
    /// 획득하여 저장된 카드의 데이터를 불러옵니다.
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


    public List<ReadCardData> cardData = new List<ReadCardData>();  // 테이블에서 가져온 읽기전용 카드 데이터.
    /// <summary>
    /// 카드의 데이터가 저장되어있는 json파일에서 카드의 id와 type를 가져옵니다.
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

    #region 캐릭터 경험치, 레벨 저장

    public List<CharExp> charExp = new List<CharExp>();
    /// <summary>
    /// 캐릭터 경험치 / 레벨 저장 
    /// </summary>
    public IEnumerator SaveCharExp(int id)
    {
        dataManager.SaveCharExp(charExp[id - 1]);
        yield return StartCoroutine(dataManager.WriteCharExp());
    }

    /// <summary>
    /// 캐릭터 경험치 / 레벨 불러오기.
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

    #region 카드 프리셋

    public List<CardPreset> cardPreset = new List<CardPreset>(); // json 파일에 저장되어야 하는 카드 프리셋 데이터
    public IEnumerator PresetSave()
    {
        for (int i = 0; i < 5; i++)
        {
            dataManager.SavePreset(cardPreset[i]);
        }
        yield return StartCoroutine(dataManager.SavePresetToJson());
    }

    /// <summary>
    /// json파일에 저장된 프리셋의 데이터를 불러온다.
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

    #region 씬 관련
    public string currentlyScene = "Main";

    /// <summary>
    /// 기존 씬을 언로드 하고, 입력된 번호에 맞는 씬을 Additive해준다.
    /// <br>또한, 씬이 언로드 될 때, 로드 될 때 필요한 것들을 세팅해준다.</br>
    /// </summary>
    /// <param name="num"></param>
    public void SceneChange(int num)
    {
        switch (num)
        {
            // 메인씬으로 이동한다.
            case 0:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "Main";
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);

                playUI.topBar.SetActive(false);
                playUI.partyBar.SetActive(false);
                isDungeon = false;
                break;
            // 오프닝 씬으로 이동한다.
            case 1:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "Opening";
                data.CurrentScene = 1;
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
                isDungeon = false;
                break;
            // 월드맵 씬으로 이동한다.
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


    /// <summary>
    /// 메인 씬에서 언어 변환시 플레이 씬에도 값이 변경됩니다.
    /// </summary>
    public void MainSettingToPlay()
    {
        PlayOption op = GameObject.Find("PUIManager").GetComponent<PlayOption>();
        op.OnValueChanged_LanguageSetting();
    }
    private void Update()
    {
        if (currentlyScene == "Main") return;
        StartCoroutine(GameSave());
        playTime += Time.deltaTime;
        if (dungeonOS != null) dungeonPlayTime += Time.deltaTime;
    }
}
