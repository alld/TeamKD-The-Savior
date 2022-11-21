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

    // ���� ���� �� ������ ���� �Լ�.
    private void Init()
    {
        SetDataPath();
        LoadPlayerData();
    }

    // ���̺�, �ε� �ؾ��ϴ� �������� ��θ� ������.
    private void SetDataPath()
    {
        GAME_DATA_PATH = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    /// <summary>
    /// PlayerData ���� �Լ�.
    /// </summary>
    public void SaveGameData()
    {
        string saveData = ObjectToJson(playerData);
        WriteJsonData(GAME_DATA_PATH, saveData);
    }

    // ���� ���� �� �÷��̾� ������ �ε�.
    private void LoadPlayerData()
    {
        SetDataPath();
        if (!File.Exists(GAME_DATA_PATH))
        {
            Debug.Log($"������ ã�� ���߽��ϴ�. ���Ӱ� �����մϴ�. {GAME_DATA_PATH}");
            SaveGameData();
        }

        string loadData = File.ReadAllText(GAME_DATA_PATH);
        if (loadData == null) return;

        playerData = JsonToObject<PlayerData>(loadData);

        LoadMyCardData();
        LoadMyCharacterData();
    }

    /// <summary>
    /// ī�� ȹ�� �� �ش� id�� ī�带 �����Ѵ�.
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
    /// ĳ���� ȹ�� �� �ش� ĳ������ ������ �����Ѵ�.
    /// </summary>
    public void AddMyCharacter(int id)
    {
        // ĳ���ʹ� �ߺ����� ȹ���� �Ұ�����.
        MyCharacter character = new MyCharacter(id);
        myCharacters.Add(character.id, character);
    }

    /// <summary>
    /// ���� ȹ�� �� �ش� ������ ������ �����Ѵ�.
    /// </summary>
    public void AddMyRelic(int id)
    {
        MyRelic relic = new MyRelic(id);
        myRelic.Add(relic.id, relic);
    }

    // �÷��̾ ������ �ִ� ī��
    private void LoadMyCardData()
    {
        if (playerData.myCards.Count == 0) return;

        foreach (var card in playerData.myCards)
            myCards.Add(card.id, card);
    }

    // �÷��̾ ������ �ִ� ĳ����
    private void LoadMyCharacterData()
    {
        if (playerData.myCharacters.Count == 0) return;

        foreach (var character in playerData.myCharacters)
            myCharacters.Add(character.id, character);
    }
    
    // ī�� ������ ��ȯ
    public Dictionary<int, MyCard> GetCardData()
    {
        return myCards;
    }
    
    // ĳ���� �����͸� ��ȯ
    public Dictionary<int, MyCharacter> GetCharacterData()
    {
        return myCharacters;
    }

    public Dictionary<int, MyRelic> GetRelicData()
    {
        return myRelic;
    }
    // Json �����͸� ���Ϸ� �����ϱ�.
    private void WriteJsonData(string path, string json)
    {
        File.WriteAllText(path, json);
        Debug.Log("SaveComplete!!!");
    }

    // �����͸� JSON �������� ��ȯ.
    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj, true);
    }
    // JSON �����͸� T �������� ��ȯ.
    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    /// <summary>
    /// Resources ������ �ִ� Json �����͸� string Ÿ������ ��ȯ��.
    /// </summary>
    public string LoadJsonDataToResources(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return textAsset.text;
    }

    /// <summary>
    /// PlayerData�� �����´�.
    /// </summary>
    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    /// <summary>
    /// PlayerData�� ���� �Ķ���ͷ� �޾ƿ� �����ͷ� �����Ѵ�.
    /// </summary>
    public void SetPlayerData(PlayerData _playerData)
    {
        playerData = _playerData;
    }

    /// <summary>
    /// ��� ��ȯ �Լ��� �����ϴ� �Լ�. �� �Լ��� ���� ������ �Ǿ��ٸ� LanguageChange �Լ��� ���� ������ �Լ��� ȣ���Ѵ�.
    /// </summary>
    public void ObserberLanguage(Action refreshLanguage)
    {
        languageChange -= refreshLanguage;
        languageChange += refreshLanguage;
    }

    /// <summary>
    /// ObserberLanguage�� ���� ������ �Լ��� �����Ѵ�.
    /// </summary>
    public void LanguageChange()
    {
        languageChange?.Invoke();
    }
}

[Serializable]
public class PlayerData
{
    // �÷��̾� ���� ��ȭ
    public int gold = 1000;
    public int soul = 1000;
    // �÷��̾� �κ��丮 ������ ����.
    public int cardCount = 27;
    public int relicCount = 28;
    public int partyCount = 28;
    // World Scene���� �ڽ��� ��ġ�� �˷��ִ� ��ũ ����Ʈ
    public int myPoint;
    // ī�� �� ������
    public int currentCardPreset = 0;
    public string[] presetName = new string[(int)Define.PresetName.Count];
    public MyPreset[] presetData = new MyPreset[5];
    // ������ ĳ����, ����
    public int[] equipRelic = new int[(int)Define.EquipCount.Relic];
    public int[] equipCharacter = new int[(int)Define.EquipCount.Character];
    // �÷��̾ ������ �ִ� ĳ����, ����
    public List<MyRelic> myRelic = new List<MyRelic>();
    public List<MyCharacter> myCharacters = new List<MyCharacter>();
    public List<MyCard> myCards = new List<MyCard>();
    // �ɼ�
    public int language;
    public int sfx = 100;
    public int bgm = 100;
    public int sound = 100;
    // ���� ���൵
    public int storyProgress;
    public int dungeonFirstClear;
    // ���� �÷��̾��� ����
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

// ���� �����Ͱ� �� ���� �� �� �� �� �� ����.
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