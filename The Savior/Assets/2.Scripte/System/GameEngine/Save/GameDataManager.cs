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

    #region 캐릭터

    /*
    * *************************************************************************************
    *  캐릭터 정보 저장하기.
    * *************************************************************************************
    */

    public JObject saveData = new JObject();        // 저장될 캐릭터의 정보 오브젝트.
    /// <summary>
    /// 캐릭터의 데이터를 저장합니다.
    /// </summary>
    /// <param name="charExp">캐릭터의 id, level, exp가 포함된 클래스입니다. </param>
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
    /// 저장된 캐릭터의 데이터를 json 파일에 작성합니다.
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
    /// json 파일에서 캐릭터의 데이터를 불러옵니다.
    /// </summary>
    /// <param name="n"> 캐릭터의 id입니다. </param>
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
    /// json 파일에 저장된 캐릭터의 데이터 개수를 반환합니다.
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
    *  캐릭터 정보 저장하기.
    * *************************************************************************************
    */


    #endregion

    #region 카드


    /*
     * *************************************************************************************
     *  카드 저장, 불러오기
     * *************************************************************************************
     */

    private JObject ownCard = new JObject();        // 현재 자신이 보유중인 카드
    /// <summary>
    /// 자신이 획득한 카드를 저장한다.
    /// </summary>
    /// <param name="n"> 카드의 번호 </param>
    /// <param name="idx"> 카드의 개수 </param>
    public void SaveCardData(int id, int idx)
    {
        if (ownCard.ContainsKey(id.ToString())) // 이미 동일한 키가 JObject에 있다면 해당 키의 값을 수정.
        {
            ownCard[id.ToString()] = idx;
            return;
        }
        ownCard.Add(id.ToString(), idx);        // JObject에 키와 값을 저장.
    }
    /// <summary>
    /// JObject에 저장된 데이터를 json 파일에 작성한다.
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
    /// json파일에 저장되어있는 자신의 카드 개수를 클래스에 담아 반환한다.
    /// <br>1번 인덱스 부터 시작한다.(카드의 id값으로 찾아야 하므로 0은 넣지 않는다.)</br>
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
    /// json 파일에서 카드의 데이터를 불러온다.
    /// <br> 0번 배열부터 시작한다.</br>
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
    /// CardJsonData에 카드의 인덱스가 몇 인지 반환함.
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
    /// 카드의 데이터를 Json 데이터로 변환합니다.
    /// </summary>
    /// <param name="card"></param>
    public void GainCard(HaveCard card)
    {
        /*
         * 게임 내에서 저장 된 데이터를 불러온다.
         * 제이슨으로 변환한다.
         * 텍스트로 저장한다.
         */
        string path = Path.Combine(Application.dataPath, "Resources/HaveCard.json");
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        if (cardJson.ContainsKey(card.id.ToString()))
        {
            // json 파일에 업데이트
            cardJson[card.id.ToString()]["haveCard"] = JsonConvert.SerializeObject(card.haveCard);
            return;
        }
        cardJson.Add(card.id.ToString(), JObject.FromObject(card));
    }
    /// <summary>
    /// Json 으로 변환된 데이터를 파일로 저장합니다.
    /// </summary>
    public void SaveCard()
    {
        string path = Path.Combine(Application.dataPath, "Resources/HaveCard.json");
        File.WriteAllText(path, cardJson.ToString());
    }
    /// <summary>
    /// Json파일에 저장된 카드의 데이터를 불러옵니다.
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
    /// 현재 Json파일에 카드 인덱스가 몇인지 반환합니다.
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

    #region 카드 프리셋
    JObject jPreset = new JObject();
    /// <summary>
    /// 카드 프리셋을 저장합니다.
    /// 현재 프리셋이 저장되어 있을 경우 value를 업데이트합니다.
    /// </summary>
    /// <param name="cardPreset"></param>
    public void SavePreset(CardPreset cardPreset)
    {
        if (jPreset.ContainsKey(cardPreset.index.ToString()))
        {
            // json 파일에 업데이트
            jPreset[cardPreset.index.ToString()] = JObject.FromObject(cardPreset);
            return;
        }
    }
    /// <summary>
    /// JObject에 저장되어있는 데이터를 Json파일로 저장합니다.
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
    /// 현재 저장되어있는 카드 프리셋 데이터를 불러옵니다.
    /// 인덱스는 1부터 시작합니다.
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
    /// 프리셋의 데이터를 불러 오기 전 초기화 시킨다.
    /// 프리셋의 인덱스 번호로 값을 초기화 시킨다.
    /// <br> 1번 인덱스 부터 넣도롭 합시다.</br>
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
        public string[] presetName = new string[] { "1번 프리셋", "2번 프리셋", "3번 프리셋", "4번 프리셋", "5번 프리셋" };
        public int preset = 1;
        // 장착한 데이터
        public int[] equipRelic = new int[5];
        public int[] equipCharacter = new int[4];
        // 유물과 캐릭터는 중복이 되어도 개별 칸으로 들어감.
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