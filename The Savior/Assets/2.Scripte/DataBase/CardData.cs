using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * 카드 데이터 리팩토링.
 * 임시로 만든겁니다. 추후 수정 예정
 */

public class CardData : Singleton<CardData>
{
    private string CARD_DATA_PATH = "CardDB/CardJsonData";

    private Dictionary<int, CardTextTable> cardTable = new Dictionary<int, CardTextTable>();

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
        cardTable.Clear();
        LoadCardData();
        Debug.Log("Card Text Data Load Complete");
    }

    // 리소스 폴더에 있는 카드 데이터 테이블을 불러오고 카드 테이블 딕셔너리에 저장한다.
    private void LoadCardData()
    {
        string jsonData = NewGameDataManager.instance.LoadJsonDataToResources(CARD_DATA_PATH);
        jsonData = "{\"cardTableList\":" + jsonData + "}";

        CardTableList cardTableList = new CardTableList();
        cardTableList = NewGameDataManager.instance.JsonToObject<CardTableList>(jsonData);

        foreach (var card in cardTableList.cardTableList)
        {
            cardTable.Add(card.Index, card);
        }
    }

    /// <summary>
    /// 해당 키의 카드 데이터를 반환한다.
    /// </summary>
    public CardTextTable GetCardTextData(int key)
    {
        if (cardTable == null) return null;

        if (cardTable.ContainsKey(key))
            return cardTable[key];
        else
        {
            Debug.Log("해당 키의 카드 정보는 존재하지 않습니다.");
            return null;
        }
    }
}

[Serializable]
public class CardTableList
{
    public List<CardTextTable> cardTableList = new List<CardTextTable>();
}

[Serializable]
public class CardTextTable
{
    public int Index;
    public string Name_Kr;
    public string Name_Eng;
    public int Type;
    public string Content_1_Kr;
    public string Content_1_Eng;
    public string Content_2_Kr;
    public string Content_2_Eng;
}
