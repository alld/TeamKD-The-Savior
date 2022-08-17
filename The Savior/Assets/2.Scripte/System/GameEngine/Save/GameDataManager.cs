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



    // ����� Json �������� �迭
    public JArray jAry = new JArray();
    public JObject jObj = new JObject();
    private int idx;
    /// <summary>
    /// Json ������ ���ٸ� �����ϰ�, ĳ������ �����͸� �����մϴ�.
    /// �̿ϼ��Դϴ�.
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
    /// Json ���Ͽ��� ĳ������ �����͸� �ҷ��ɴϴ�.
    /// �̿ϼ��Դϴ�.
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



    public JObject cardJson = new JObject();
    /// <summary>
    /// ī���� �����͸� Json �����ͷ� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="card"></param>
    public void GainCard(HaveCard card)
    {
        /*
         * ���� ������ ���� �� �����͸� �ҷ��´�.
         * ���̽����� ��ȯ�Ѵ�.
         * �ؽ�Ʈ�� �����Ѵ�.
         */
        string path = Path.Combine(Application.dataPath, "Resources/HaveCard.json");
        if (!File.Exists(path))
        {
            File.Create(path);
        }

        cardJson.Add(card.id.ToString(), JObject.FromObject(card)); 
    }
    /// <summary>
    /// Json ���� ��ȯ�� �����͸� ���Ϸ� �����մϴ�.
    /// </summary>
    public void SaveCard()
    {
        string path = Path.Combine(Application.dataPath, "Resources/HaveCard.json");
        File.WriteAllText(path, cardJson.ToString());
    }
    /// <summary>
    /// Json���Ͽ� ����� ī���� �����͸� �ҷ��ɴϴ�.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public HaveCard CardDataLoad(int n)
    {
        HaveCard card = new HaveCard();
        TextAsset textAsset = Resources.Load<TextAsset>("HaveCard");
        JObject j = JObject.Parse(textAsset.text);

        card = j[n.ToString()].ToObject<HaveCard>();
        return card;
    }

    /// <summary>
    /// ���� Json���Ͽ� ī�� �ε����� ������ ��ȯ�մϴ�.
    /// </summary>
    /// <returns></returns>
    public int CurrentCardData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("HaveCard");
        JObject j = JObject.Parse(textAsset.text);
        int i = 0;
        while (true)
        {
            i++;
            if (j[i.ToString()] == null)
            {
                break;
            }
        }
        return i;
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
        public int myPoint = 0;
        public string[] presetName = new string[] { "1�� ������", "2�� ������", "3�� ������", "4�� ������", "5�� ������" };
        public int presset = 1;
        // ���� ī�� ������
        public int[] equipCard1 = new int[15];
        public int[] equipCard2 = new int[15];
        public int[] equipCard3 = new int[15];
        public int[] equipCard4 = new int[15];
        public int[] equipCard5 = new int[15];
        // ������ ������
        public int[] equipRelic = new int[5];
        public int[] equipCharacter = new int[4];
        // ������ ĳ���ʹ� �ߺ��� �Ǿ ���� ĭ���� ��.
        public List<int> haveCharacter = new List<int>();
        public List<int> haveRelic = new List<int>();
    }

    [System.Serializable]
    public class HaveCard
    {
        public int id = 0;
        public int haveCard = 0;
    }

    [System.Serializable]
    public class CharExp
    {
        public int id = 0;
        public int level = 1;
        public int exp = 0;
    }
}