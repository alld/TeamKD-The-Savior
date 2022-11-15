using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class NewGameDataManager : Singleton<NewGameDataManager>
{
    private string GAME_DATA_PATH = "";

    private Dictionary<int, MyCard> myCards = new Dictionary<int, MyCard>();
    private Dictionary<int, MyCharacter> myCharacters = new Dictionary<int, MyCharacter>();
    [SerializeField] private PlayerData playerData = new PlayerData();

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

    // 플레이어가 가지고 있는 카드
    private void LoadMyCardData()
    {
        if (playerData.myCards.Count == 0) return;

        foreach (var card in playerData.myCards)
            myCards.Add(card.id, card);
    }

    // 플레이어가 가지고 있는 캐릭터
    private void LoadMyCharacterData()
    {
        if (playerData.myCharacters.Count == 0) return;

        foreach (var character in playerData.myCharacters)
            myCharacters.Add(character.id, character);
    }

    // Json 데이터를 파일로 저장하기.
    private void WriteJsonData(string path, string json)
    {
        File.WriteAllText(path, json);
        Debug.Log("Data Write Complete");
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
}

[Serializable]
public class PlayerData
{
    // 플레이어 보유 재화
    public int gold = 1000;
    public int soul = 1000;
    // World Scene에서 자신의 위치를 알려주는 마크 포인트
    public int myPoint;
    // 카드 덱 프리셋
    public int currentCardPreset;
    public string[] presetName = new string[(int)Define.PresetName.Count];
    // 장착한 캐릭터, 유물
    public int[] equipRelic = new int[(int)Define.EquipCount.Relic];
    public int[] equipCharacter = new int[(int)Define.EquipCount.Character];
    // 플레이어가 가지고 있는 캐릭터, 유물
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