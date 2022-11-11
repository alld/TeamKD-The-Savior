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
    private Dictionary<int, CardTable> cardTable = new Dictionary<int, CardTable>();

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
        cardTable = DataManager.instance.LoadJson<CardTableData, int, CardTable>("CardDB/CardJsonData").MakeDick();
        Debug.Log("Card Text Data Load Complete");
    }

    public CardTable GetCardTextData(int key)
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
public class CardTable
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

[Serializable]
public class CardTableData : ILoader<int, CardTable>
{
    public List<CardTable> cardTables = new List<CardTable>();
    public Dictionary<int, CardTable> MakeDick()
    {
        Dictionary<int, CardTable> dic = new Dictionary<int, CardTable>();

        foreach (CardTable card in cardTables)
            dic.Add(card.Index, card);

        return dic;
    }
}