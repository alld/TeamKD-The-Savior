using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * ī�� ������ �����丵.
 * �ӽ÷� ����̴ϴ�. ���� ���� ����
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

    // ���ҽ� ������ �ִ� ī�� ������ ���̺��� �ҷ����� ī�� ���̺� ��ųʸ��� �����Ѵ�.
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
    /// �ش� Ű�� ī�� �����͸� ��ȯ�Ѵ�.
    /// </summary>
    public CardTextTable GetCardTextData(int key)
    {
        if (cardTable == null) return null;

        if (cardTable.ContainsKey(key))
            return cardTable[key];
        else
        {
            Debug.Log("�ش� Ű�� ī�� ������ �������� �ʽ��ϴ�.");
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
