using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * 유물 데이터 리팩토링.
 * 임시로 만든겁니다. 추후 수정 예정
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
            Debug.Log("해당 키의 유물 정보는 존재하지 않습니다.");
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