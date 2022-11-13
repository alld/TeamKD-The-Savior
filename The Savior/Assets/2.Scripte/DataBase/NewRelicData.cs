using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * ���� ������ �����丵.
 * �ӽ÷� ����̴ϴ�. ���� ���� ����
 */
public class NewRelicData : Singleton<NewRelicData>
{
    private string RELIC_DATA_PATH = "RelicDB/RelicText";
    private Dictionary<int, RelicTextTable> relicTable = new Dictionary<int, RelicTextTable>();

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
        relicTable.Clear();
        LoadRelicData();
        Debug.Log("Relic Text Data Load Complete");
    }

    //���ҽ� ������ �ִ� ���� ������ ���̺��� �ҷ����� ���� ���̺� ��ųʸ��� �����Ѵ�.
    private void LoadRelicData()
    {
        string jsonData = NewGameDataManager.instance.LoadJsonDataToResources(RELIC_DATA_PATH);
        jsonData = "{\"relicTableList\":" + jsonData + "}";

        RelicTableList relicTableList = new RelicTableList();
        relicTableList = NewGameDataManager.instance.JsonToObject<RelicTableList>(jsonData);

        foreach (var relic in relicTableList.relicTableList)
        {
            relicTable.Add(relic.Index, relic);
        }
    }

    /// <summary>
    /// �ش� Ű�� ���� �����͸� ��ȯ�Ѵ�.
    /// </summary>
    public RelicTextTable GetRelicTextData(int key)
    {
        if (relicTable == null) return null;

        if (relicTable.ContainsKey(key))
            return relicTable[key];
        else
        {
            Debug.Log("�ش� Ű�� ���� ������ �������� �ʽ��ϴ�.");
            return null;
        }
    }

}

[SerializeField]
public class RelicTableList
{
    public List<RelicTextTable> relicTableList = new List<RelicTextTable>();
}

[Serializable]
public class RelicTextTable
{
    public int Index;
    public string Name_Kr;
    public string Name_Eng;
    public string Positive_Kr;
    public string Positive_Eng;
    public string Negative_Kr;
    public string Negative_Eng;
    public string Condition_kor;
    public string Condition_Eng;
}
