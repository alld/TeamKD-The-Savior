using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class NewGameDataManager : Singleton<NewGameDataManager>
{
    string GAME_DATA_PATH = "";

    private Dictionary<int, MyCard> myCards = new Dictionary<int, MyCard>();
    private Dictionary<int, MyCharacter> myCharacter = new Dictionary<int, MyCharacter>();
    private PlayerData playerData = new PlayerData();

    private void Awake()
    {
        Regist(this);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetDataPath();
        LoadPlayerData();
        SetCardData();
    }

    /// <summary>
    /// 데이터 관련 테스트 함수
    /// </summary>
    private void SetCardData()
    {
        PlayerData playerData = new PlayerData();
        string data = DataManager.instance.ObjectToJson(playerData);

        PlayerData loadData = new PlayerData();
        loadData = DataManager.instance.JsonToObject<PlayerData>(data);
        MyCard myC1 = new MyCard();
        myC1.id = 1;
        myC1.count = 1;

        MyCard myC2 = new MyCard();
        myC2.id = 2;
        myC2.count = 2;

        MyCard myC3 = new MyCard();
        myC3.id = 3;
        myC3.count = 3;


        loadData.myCards.Add(myC1);
        loadData.myCards.Add(myC2);
        loadData.myCards.Add(myC3);

        Debug.Log("세이브 전");

        foreach (var item in loadData.myCards)
        {
            Debug.Log($"Id: {item.id}, Count : {item.count}");
        }

        string saveData = DataManager.instance.ObjectToJson(loadData);
        WriteJsonData(GAME_DATA_PATH, saveData);

        Debug.Log("데이터 로드 함.");

        string jsonData = File.ReadAllText(GAME_DATA_PATH);

        PlayerData jsonLoadTest = new PlayerData();

        jsonLoadTest = DataManager.instance.JsonToObject<PlayerData>(jsonData);

        foreach (var item in jsonLoadTest.myCards)
        {
            Debug.Log($"{item.id}, {item.count}");
        }
        
    }

    private void SetDataPath()
    {
        GAME_DATA_PATH = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    public void SaveGameData()
    {
        
    }

    /// <summary>
    /// 게임 시작 시 플레이어 데이터 로드.
    /// </summary>
    private void LoadPlayerData()
    {
        SetDataPath();
        string loadData = File.ReadAllText(GAME_DATA_PATH);

        if (loadData == null) return;

        playerData = DataManager.instance.JsonToObject<PlayerData>(loadData);
    }

    private void WriteJsonData(string path, string json)
    {
        File.WriteAllText(path, json);
        Debug.Log("Data Write Complete");
    }
}

[Serializable]
public class PlayerData
{
    public int gold;
    public int soul;
    public int currentCardPreset;
    public int[] equipRelic = new int[(int)Define.EquipCount.Relic];
    public int[] equipCharacter = new int[(int)Define.EquipCount.Character];
    public List<MyCharacter> myCharacters = new List<MyCharacter>();
    public List<MyCard> myCards = new List<MyCard>();
}

[Serializable]
public class MyCard
{
    public int id;
    public int count;
}

[Serializable]
public class MyCharacter
{
    public int id;
    public int count;
}

