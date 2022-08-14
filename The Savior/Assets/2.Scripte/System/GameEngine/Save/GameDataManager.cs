using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameDataTable;
using Newtonsoft.Json.Linq;
using SimpleJSON;

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



    // 저장될 Json 데이터의 배열
    public JArray jAry = new JArray();
    public JObject jObj = new JObject();
    private int idx;
    /// <summary>
    /// Json 파일이 없다면 생성하고, 캐릭터의 데이터를 저장합니다.
    /// </summary>
    /// <param name="charExp"></param>
    public void SaveCharExp(CharExp charExp)
    {
        string path = Path.Combine(Application.dataPath, "Resources/CharacterExperience.json");
        idx = charExp.id;
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        jObj.Add(idx.ToString(), JObject.FromObject(charExp));
        File.WriteAllText(path, jObj.ToString());
    }


    /// <summary>
    /// Json 파일에서 캐릭터의 데이터를 불러옵니다.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public CharExp LoadCharExp(int n)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("CharacterExperience");
        CharExp charExp = new CharExp();
        var json = JSON.Parse(textAsset.text);
        charExp.id = json[n]["id"];
        charExp.exp = json[n]["exp"];
        charExp.level = json[n]["level"];
        return charExp;
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
        public string[] presetName = new string[] { "1번 프리셋", "2번 프리셋", "3번 프리셋", "4번 프리셋", "5번 프리셋"};
        public int presset = 1;
        // 장착한 데이터
        public int[] equipRelic = new int[5];
        public int[] equipCard = new int[15];
        public int[] equipCharacter = new int[4];
        // 유물과 캐릭터는 중복이 되어도 개별 칸으로 들어감.
        public List<int> haveCharacter = new List<int>();
        public List<int> haveRelic = new List<int>();
    }

    [System.Serializable]
    public class CharExp
    {
        public int id = 0;
        public int level = 1;
        public int exp = 0;
    }
}