using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameDataTable;
using Newtonsoft.Json;
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

    public GameData ResetGameData()
    {
        GameData data = new GameData();
        string jsonData = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.dataPath, "Resources/gameData.json");
        File.WriteAllText(path, jsonData);

        return data;
    }

    //public void SaveJson(CharGoods goods) 
    //{
    //    string json = JsonConvert.SerializeObject(goods);
    //    string path = Path.Combine(Application.dataPath, "Resources/saveData.json");
    //    File.WriteAllText(path, json);
    //}
           
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
        public string[] presetName = new string[] { "1�� ������", "2�� ������", "3�� ������", "4�� ������", "5�� ������"};
        public int presset = 1;
        // ������ ������
        public int[] equipRelic = new int[5];
        public int[] equipCard = new int[15];
        public int[] equipCharacter = new int[4];
        // ������ ĳ���ʹ� �ߺ��� �Ǿ ���� ĭ���� ��.
        public List<int> haveCharacter = new List<int>();
        public List<int> haveRelic = new List<int>();
    }

    //[System.Serializable]
    //public class CharGoods
    //{
    //    public Dictionary<int, int[,]> charDic = new Dictionary<int, int[,]>();
    //}
}