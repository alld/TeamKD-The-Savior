using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

// 플레이어가 가지고 있어야하는 데이터를 관리하는 클래스입니다.
// 여기서 가지고 있는 데이터를 기반으로 데이터 테이블에 접근해서 필요한 데이터를 가져옵니다.

public class NewGameDataManager : Singleton<NewGameDataManager>
{
    private string GAME_DATA_PATH = "";

    private Dictionary<int, MyCard> myCards = new Dictionary<int, MyCard>();
    private Dictionary<int, MyCharacter> myCharacters = new Dictionary<int, MyCharacter>();
    private Dictionary<int, MyRelic> myRelic = new Dictionary<int, MyRelic>();
    [SerializeField] private PlayerData playerData = new PlayerData();

    private Action languageChange;

    private void Awake()
    {
        Regist(this);
    }

    private void Start()
    {
        Init();
    }

    // 게임 시작 시 데이터 세팅 함수.
    private void Init()
    {
        SetDataPath();
        LoadPlayerData();
    }

    // 세이브, 로드 해야하는 데이터의 경로를 설정함.
    private void SetDataPath()
    {
        GAME_DATA_PATH = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    /// <summary>
    /// PlayerData 저장 함수.
    /// </summary>
    public void SaveGameData()
    {
        string saveData = ObjectToJson(playerData);
        WriteJsonData(GAME_DATA_PATH, saveData);
    }

    // 게임 시작 시 플레이어 데이터 로드.
    private void LoadPlayerData()
    {
        SetDataPath();
        if (!File.Exists(GAME_DATA_PATH))
        {
            Debug.Log($"파일을 찾지 못했습니다. 새롭게 생성합니다. {GAME_DATA_PATH}");
            SaveGameData();
        }

        string loadData = File.ReadAllText(GAME_DATA_PATH);
        if (loadData == null) return;

        playerData = JsonToObject<PlayerData>(loadData);

        LoadMyCardData();
        LoadMyRelicData();
        LoadMyCharacterData();
    }

    /// <summary>
    /// 카드 획득 시 해당 id의 카드를 저장한다.
    /// </summary>
    public void AddMyCard(int id)
    {
        if (myCards.ContainsKey(id))
        {
            myCards[id].count++;
            return;
        }
        MyCard card = new MyCard(id);
        myCards.Add(card.id, card);
    }

    /// <summary>
    /// 캐릭터 획득 시 해당 캐릭터의 정보를 저장한다.
    /// </summary>
    public void AddMyCharacter(int id)
    {
        // 캐릭터는 중복으로 획득이 불가능함.
        MyCharacter character = new MyCharacter(id);
        myCharacters.Add(character.id, character);
    }

    /// <summary>
    /// 유물 획득 시 해당 유물의 정보를 저장한다.
    /// </summary>
    public void AddMyRelic(int id)
    {
        if (myRelic.ContainsKey(id))
        {
            Debug.Log("이미 해당 유물을 가지고있습니다.");
            return;
        }

        MyRelic relic = new MyRelic(id);
        myRelic.Add(relic.id, relic);
        playerData.myRelic.Add(relic);
    }

    // 플레이어가 가지고 있는 카드
    private void LoadMyCardData()
    {
        if (playerData.myCards.Count == 0) return;

        foreach (var card in playerData.myCards)
            myCards.Add(card.id, card);
    }

    private void LoadMyRelicData()
    {
        if (playerData.myRelic.Count == 0) return;

        foreach (var relic in playerData.myRelic)
            myRelic.Add(relic.id, relic);
    }

    // 플레이어가 가지고 있는 캐릭터
    private void LoadMyCharacterData()
    {
        if (playerData.myCharacters.Count == 0) return;

        foreach (var character in playerData.myCharacters)
            myCharacters.Add(character.id, character);
    }
    
    // 카드 데이터 반환
    public Dictionary<int, MyCard> GetCardData()
    {
        return myCards;
    }
    
    // 캐릭터 데이터를 반환
    public Dictionary<int, MyCharacter> GetCharacterData()
    {
        return myCharacters;
    }

    public Dictionary<int, MyRelic> GetRelicData()
    {
        return myRelic;
    }
    // Json 데이터를 파일로 저장하기.
    private void WriteJsonData(string path, string json)
    {
        File.WriteAllText(path, json);
        Debug.Log("SaveComplete!!!");
    }

    // 데이터를 JSON 형식으로 변환.
    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj, true);
    }
    // JSON 데이터를 T 형식으로 변환.
    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    /// <summary>
    /// Resources 폴더에 있는 Json 데이터를 string 타입으로 반환함.
    /// </summary>
    public string LoadJsonDataToResources(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return textAsset.text;
    }

    /// <summary>
    /// PlayerData를 가져온다.
    /// </summary>
    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    /// <summary>
    /// PlayerData의 값은 파라미터로 받아온 데이터로 변경한다.
    /// </summary>
    public void SetPlayerData(PlayerData _playerData)
    {
        playerData = _playerData;
    }

    /// <summary>
    /// 언어 변환 함수를 연결하는 함수. 이 함수를 통해 구독이 되었다면 LanguageChange 함수를 통해 구독한 함수를 호출한다.
    /// </summary>
    public void ObserberLanguage(Action refreshLanguage)
    {
        languageChange -= refreshLanguage;
        languageChange += refreshLanguage;
    }

    /// <summary>
    /// ObserberLanguage를 통해 구독한 함수를 실행한다.
    /// </summary>
    public void LanguageChange()
    {
        languageChange?.Invoke();
    }
}

[Serializable]
public class PlayerData
{
    // 플레이어 보유 재화
    public int gold = 1000;
    public int soul = 1000;
    // 플레이어 인벤토리 아이템 개수.
    public int cardCount = 27;
    public int relicCount = 28;
    public int partyCount = 28;
    // World Scene에서 자신의 위치를 알려주는 마크 포인트
    public int myPoint;
    // 카드 덱 프리셋
    public int currentCardPreset = 0;
    public string[] presetName = new string[(int)Define.PresetName.Count];
    public MyPreset[] presetData = new MyPreset[5];
    // 장착한 캐릭터, 유물
    public int[] equipRelic = new int[(int)Define.EquipCount.Relic];
    public int[] equipCharacter = new int[(int)Define.EquipCount.Character];
    // 플레이어가 가지고 있는 캐릭터, 유물
    public List<MyRelic> myRelic = new List<MyRelic>();
    public List<MyCharacter> myCharacters = new List<MyCharacter>();
    public List<MyCard> myCards = new List<MyCard>();
    // 옵션
    public int language;
    public int sfx = 100;
    public int bgm = 100;
    public int sound = 100;
    // 게임 진행도
    public int storyProgress;
    public int dungeonFirstClear;
    // 현재 플레이어의 상태
    public Define.LastGameViewPoint lastGameViewPoint = Define.LastGameViewPoint.Main;
}

[Serializable]
public class MyRelic
{
    public int id;

    public MyRelic(int _id)
    {
        id = _id;
    }
}

[Serializable]
public class MyCard
{
    public int id;
    public int count;

    public MyCard(int _id)
    {
        id = _id;
        count = 1;
    }
}

// 뭔가 데이터가 더 들어가야 할 것 만 같 은 느낌.
[Serializable]
public class MyCharacter
{
    public int id;
    public int exp;
    public int level;

    public MyCharacter(int _id)
    {
        id = _id;
        exp = 0;
        level = 1;
    }
}

[Serializable]
public class MyPreset
{
    public int[] id = new int[28];
}