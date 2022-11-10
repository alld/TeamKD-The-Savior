using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * 카드 데이터 리팩토링.
 * 테이블 데이터는 임시로 리소스에서 읽어 오는 형식으로 구현.
 * 데이터를 저장하는 부분은 json과 스크립터블 오브젝트 중에 고민중.
 */

public class CardData : DataManager
{
    public Dictionary<int, CardTable> cardTable = new Dictionary<int, CardTable>();

    private void Start()
    {
        Init();
        Load();
    }

    protected override void Init()
    {
        cardTable.Clear();
    }

    protected override void Load()
    {
        cardTable = LoadJson<CardTableData, int, CardTable>("CardDB/CardJsonData").MakeDick();
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