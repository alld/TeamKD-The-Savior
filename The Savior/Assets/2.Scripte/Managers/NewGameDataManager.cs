using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class NewGameDataManager : Singleton<NewGameDataManager>
{
    string GAME_DATA_PATH = "";

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
        PlayerData playerData = new PlayerData();
        string data = DataManager.instance.ObjectToJson(playerData);
        WriteJsonData(GAME_DATA_PATH, data);
    }

    private void SetDataPath()
    {
        GAME_DATA_PATH = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    public void SaveGameData()
    {
        
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
    public List<int> myCharacter = new List<int>();
    public List<int> myCard = new List<int>();
}
