using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameDataTable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleJSON;

public class GameDataManager : MonoBehaviour
{
    public void SaveGameDataToJson(GameData gameData)
    {
        string jsonData = JsonUtility.ToJson(gameData, true);
        string path = Path.Combine(Application.dataPath, "Resources/gameData.json");
        if (File.Exists(path))
        {
            File.WriteAllText(path, jsonData);
        }
        else
        {
            File.Create(path);
            File.WriteAllText(path, jsonData);
        }
    }

    public GameData LoadGameDataFromJson()
    {
        GameData data = new GameData();
        string path = Path.Combine(Application.dataPath, "Resources/gameData.json");
        if (!File.Exists(path))
        {
            File.Create(path);
        }
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

    #region ĳ����

    /*
    * *************************************************************************************
    *  ĳ���� ���� �����ϱ�.
    * *************************************************************************************
    */

    public JObject saveData = new JObject();        // ����� ĳ������ ���� ������Ʈ.
    /// <summary>
    /// ĳ������ �����͸� �����մϴ�.
    /// </summary>
    /// <param name="charExp">ĳ������ id, level, exp�� ���Ե� Ŭ�����Դϴ�. </param>
    public void SaveCharExp(CharExp charExp)
    {
        if (saveData.ContainsKey(charExp.id.ToString()))
        {
            saveData[charExp.id.ToString()] = JObject.FromObject(charExp);
            return;
        }
        saveData.Add(charExp.id.ToString(), JObject.FromObject(charExp));
    }

    /// <summary>
    /// ����� ĳ������ �����͸� json ���Ͽ� �ۼ��մϴ�.
    /// </summary>
    public void WriteCharExp()
    {
        string path = Path.Combine(Application.dataPath, "Resources/CharacterDB/CharacterExperience.json");

        if (!File.Exists(path))
        {
            File.Create(path);
        }
        File.WriteAllText(path, saveData.ToString());
    }

    /// <summary>
    /// json ���Ͽ��� ĳ������ �����͸� �ҷ��ɴϴ�.
    /// </summary>
    /// <param name="n"> ĳ������ id�Դϴ�. </param>
    /// <returns></returns>
    public CharExp LoadCharExp(int n)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("CharacterDB/CharacterExperience.json");
        CharExp charExp = new CharExp();

        if(textAsset == null)
        {
            return charExp;
        }

        JObject json = JObject.Parse(textAsset.text);
        charExp = json[(n + 1).ToString()].ToObject<CharExp>();

        return charExp;
    }

    /// <summary>
    /// json ���Ͽ� ����� ĳ������ ������ ������ ��ȯ�մϴ�.
    /// </summary>
    /// <returns></returns>
    public int CurrentCharIndex()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("CharacterDB/CharacterExperience");
        if(textAsset == null)
        {
            return 0;
        }

        JObject jsonData = JObject.Parse(textAsset.text);
        int i = jsonData.Count;
        return i;
    }

    /*
    * *************************************************************************************
    *  ĳ���� ���� �����ϱ�.
    * *************************************************************************************
    */


    #endregion

    #region ī��


    /*
     * *************************************************************************************
     *  ī�� ����, �ҷ�����
     * *************************************************************************************
     */

    private JObject ownCard = new JObject();        // ���� �ڽ��� �������� ī��
    /// <summary>
    /// �ڽ��� ȹ���� ī�带 �����Ѵ�.
    /// </summary>
    /// <param name="n"> ī���� ��ȣ </param>
    /// <param name="idx"> ī���� ���� </param>
    public void SaveCardData(int id, int idx)
    {
        if (ownCard.ContainsKey(id.ToString())) // �̹� ������ Ű�� JObject�� �ִٸ� �ش� Ű�� ���� ����.
        {
            ownCard[id.ToString()] = idx;
            return;
        }
        ownCard.Add(id.ToString(), idx);        // JObject�� Ű�� ���� ����.
    }
    /// <summary>
    /// JObject�� ����� �����͸� json ���Ͽ� �ۼ��Ѵ�.
    /// </summary>
    public void WriteCardDataToJson()
    {
        string path = Path.Combine(Application.dataPath, "Resources/CardDB/MyCardData.json");
        if (!File.Exists(path))
        {
            File.Create(path);
        }

        File.WriteAllText(path, ownCard.ToString());
    }

    /// <summary>
    /// json���Ͽ� ����Ǿ��ִ� �ڽ��� ī�� ������ Ŭ������ ��� ��ȯ�Ѵ�.
    /// <br>1�� �ε��� ���� �����Ѵ�.(ī���� id������ ã�ƾ� �ϹǷ� 0�� ���� �ʴ´�.)</br>
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public SaveCardData LoadMyCardData(int n)
    {
        SaveCardData data = new SaveCardData();
        TextAsset textAsset = Resources.Load<TextAsset>("CardDB/MyCardData");

        if(textAsset == null)
        {
            return data;
        }

        JObject jsonData = JObject.Parse(textAsset.text);

        if (!jsonData.ContainsKey(n.ToString()))
        {
            data.id = n;
            data.ownCard = 0;
            return data;
        }

        data.id = n;
        data.ownCard = jsonData[n.ToString()].ToObject<int>();

        return data;
    }

    public int CountMyCardData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("CardDB/MyCardData");
        if(textAsset == null)
        {
            return 0;
        }
        JObject jsonData = JObject.Parse(textAsset.text);

        int idx = jsonData.Count;

        return idx;
    }
    /// <summary>
    /// json ���Ͽ��� ī���� �����͸� �ҷ��´�.
    /// <br> 0�� �迭���� �����Ѵ�.</br>
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public ReadCardData ReadCardDataFromJson(int n)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("CardDB/CardJsonData");
        ReadCardData data = new ReadCardData();
        JArray jsonArray = JArray.Parse(textAsset.text);

        data.id = jsonArray[n]["Index"].ToObject<int>();
        data.type = jsonArray[n]["Type"].ToObject<int>();

        return data;
    }
    /// <summary>
    /// CardJsonData�� ī���� �ε����� �� ���� ��ȯ��.
    /// </summary>
    /// <returns></returns>
    public int CountCardData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("CardDB/CardJsonData");
        JArray jsonArray = JArray.Parse(textAsset.text);
        int idx = jsonArray.Count;

        return idx;
    }

    /*
     * *************************************************************************************
     * 
     ***************************************************************************************
     */


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
        if (cardJson.ContainsKey(card.id.ToString()))
        {
            // json ���Ͽ� ������Ʈ
            cardJson[card.id.ToString()]["haveCard"] = JsonConvert.SerializeObject(card.haveCard);
            return;
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
        string path = Path.Combine(Application.dataPath, "Resources/HaveCard.json");

        if (!File.Exists(path))
        {
            File.Create(path);

        }


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

        int i = j.Count;
        return i;
    }


    #endregion

    #region ī�� ������
    JObject jPreset = new JObject();
    /// <summary>
    /// ī�� �������� �����մϴ�.
    /// ���� �������� ����Ǿ� ���� ��� value�� ������Ʈ�մϴ�.
    /// </summary>
    /// <param name="cardPreset"></param>
    public void SavePreset(CardPreset cardPreset)
    {
        if (jPreset.ContainsKey(cardPreset.index.ToString()))
        {
            // json ���Ͽ� ������Ʈ
            jPreset[cardPreset.index.ToString()] = JObject.FromObject(cardPreset);
            return;
        }
    }
    /// <summary>
    /// JObject�� ����Ǿ��ִ� �����͸� Json���Ϸ� �����մϴ�.
    /// </summary>
    public void SavePresetToJson()
    {
        string path = Path.Combine(Application.dataPath, "Resources/CardDB/CardPreset.json");
        if (!File.Exists(path))
        {
            File.Create(path);
        }

        File.WriteAllText(path, jPreset.ToString());
    }

    /// <summary>
    /// ���� ����Ǿ��ִ� ī�� ������ �����͸� �ҷ��ɴϴ�.
    /// �ε����� 1���� �����մϴ�.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public CardPreset LoadCardPreset(int n)
    {
        CardPreset cardPreset = new CardPreset();
        TextAsset textAsset = Resources.Load<TextAsset>("CardDB/CardPreset");
        if (textAsset == null)
        {
            return cardPreset;
        }
        JObject j = JObject.Parse(textAsset.text);
        cardPreset = j[n.ToString()].ToObject<CardPreset>();
        return cardPreset;
    }

    public int CountPresetData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("CardDB/CardPreset");
        if(textAsset == null)
        {
            return 0;
        }
        JObject j = JObject.Parse(textAsset.text);

        int idx = j.Count;
        return idx;
    }

    /// <summary>
    /// �������� �����͸� �ҷ� ���� �� �ʱ�ȭ ��Ų��.
    /// �������� �ε��� ��ȣ�� ���� �ʱ�ȭ ��Ų��.
    /// <br> 1�� �ε��� ���� �ֵ��� �սô�.</br>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public CardPreset InitCardPreset(int index)
    {
        CardPreset preset = new CardPreset();
        jPreset.Add(index.ToString(), JObject.FromObject(preset));
        preset = jPreset[index.ToString()].ToObject<CardPreset>();

        return preset;
    }

    #endregion
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
        public int preset = 1;
        // ������ ������
        public int[] equipRelic = new int[5];
        public int[] equipCharacter = new int[4];
        // ������ ĳ���ʹ� �ߺ��� �Ǿ ���� ĭ���� ��.
        public List<int> haveCharacter = new List<int>();
        public List<int> haveRelic = new List<int>();
    }

    [System.Serializable]
    public class SaveCardData
    {
        public int id;
        public int ownCard;
    }

    [System.Serializable]
    public class ReadCardData
    {
        public int id;
        public int type;
    }


    [System.Serializable]
    public class HaveCard
    {
        public int id = 0;
        public int haveCard = 0;
        public int cardType = 0;
    }
    [System.Serializable]
    public class CardPreset
    {
        public int index = 0;
        public int[] preset = new int[15];
    }

    [System.Serializable]
    public class CharExp
    {
        public int id = 0;
        public int level = 1;
        public int exp = 0;
    }
}