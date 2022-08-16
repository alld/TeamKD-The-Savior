using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameDataTable;

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
    public GameData data;
    public List<CharExp> charExp = new List<CharExp>(); // Json 파일에 저장되어야 하는 캐릭터의 데이터
    public List<HaveCard> card = new List<HaveCard>();  // Json 파일에 저장되어야 하는 카드의 데이터
    public int cardIdx = 0;  // Json 파일에 저장되어있는 카드의 인덱스

    // Play씬의 UI를 특정 상황에 따라 활성화 하기 위한 함수.
    private PlayUI playUI;

    #region 유동 데이터 관리


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
        // 게임 시작시에 Main씬을 연결한다.
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        // Play씬에서 사용하는 UI 연결
        playUI = GameObject.Find("PUIManager").GetComponent<PlayUI>();

        GameLoad();
        LoadCardData();
    }

    #region 데이터 저장, 불러오기, 리셋하기
    /// <summary>
    /// 데이터를 저장한다.
    /// </summary>
    public void GameSave()
    {
        dataManager.SaveGameDataToJson(data);
    }

    /// <summary>
    /// 데이터를 불러온다.
    /// </summary>
    public void GameLoad()
    {
        data = dataManager.LoadGameDataFromJson();
    }

    /// <summary>
    /// 데이터를 초기화 시킨다.
    /// </summary>
    public void GameReset()
    {
        data = dataManager.ResetGameData();
    }


    /// <summary>
    /// 현재 카드 상황을 Json 파일로 저장한다.
    /// </summary>
    /// <param name="n"></param>
    public void CardSave()
    {
        cardIdx = dataManager.CurrentCardData();
        // 리스트의 인덱스는 0부터 시작
        // 반환된 cardIdx는 1부터 시작하여 마지막 값의 +1 이기 때문에
        // cardIdx -2가 최대 리스트 인덱스
        for (int cardArr = 0; cardArr < (cardIdx-1); cardArr++)
        {
            dataManager.GainCard(card[cardArr]);
        }
        dataManager.SaveCard();
    }

    /// <summary>
    /// Json 파일에 카드 인덱스가 몇이나 있는지 값을 받고,
    /// <br>인덱스 수 만큼 데이터를 불러옵니다.</br>
    /// </summary>
    public void LoadCardData()
    {
        card.Clear();        
        cardIdx = dataManager.CurrentCardData();
        for(int cardArr = 1; cardArr < cardIdx; cardArr++)
        {
            card.Add(dataManager.CardDataLoad(cardArr));
        }
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
                break;
            // 오프닝 씬으로 이동한다.
            case 1:
                SceneManager.UnloadSceneAsync(currentlyScene);
                currentlyScene = "Opening";
                data.CurrentScene = 1;
                SceneManager.LoadSceneAsync(currentlyScene, LoadSceneMode.Additive);
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
