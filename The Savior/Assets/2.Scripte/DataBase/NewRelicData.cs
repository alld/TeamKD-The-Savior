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
    private Dictionary<int, RelicTable> relicTable = new Dictionary<int, RelicTable>();

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
        relicTable = DataManager.instance.LoadJson<RelicTableData, int, RelicTable>("RelicDB/RelicText").MakeDick();
        Debug.Log("Relic Text Data Load Complete");
    }

    public RelicTable GetRelicTextData(int key)
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

[Serializable]
public class RelicTable
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

public class RelicTableData : ILoader<int, RelicTable>
{
    public List<RelicTable> relicTables = new List<RelicTable>();
    public Dictionary<int, RelicTable> MakeDick()
    {
        Dictionary<int, RelicTable> dic = new Dictionary<int, RelicTable>();

        foreach (RelicTable relic in relicTables)
            dic.Add(relic.Index, relic);

        return dic;
    }
}