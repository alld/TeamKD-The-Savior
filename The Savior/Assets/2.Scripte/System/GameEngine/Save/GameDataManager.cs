using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameDataTable;

public class GameDataManager : MonoBehaviour
{
    public void SaveGameDataToJson(GameData gameData)
    {
        string jsonData = JsonUtility.ToJson(gameData, true);
        string path = Path.Combine(Application.dataPath, "Resources/gameData.json");
        File.WriteAllText(path, jsonData);
    }

    public GameData LoadGameDataFromJson()
    {
        GameData data = new GameData();
        string path = Path.Combine(Application.dataPath, "Resources/gameData.json");
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<GameData>(jsonData);

        return data;
    }
}

namespace GameDataTable
{
    [System.Serializable]
    public class GameData
    {
        public int CurrentScene = -1;
        public int gameProgress = -1;
        public int Language = 0;
        public int SFX = 100;
        public int BGM = 100;
        public int Sound = 100;
        public int souls = 0;
        public int golds = 0;
        //public int[] haveCharNum = new int[6]; // : Have Character Number
    }
}