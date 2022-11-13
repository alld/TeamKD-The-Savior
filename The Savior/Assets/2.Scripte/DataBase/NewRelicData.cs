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

    //리소스 폴더에 있는 유물 데이터 테이블을 불러오고 유물 테이블 딕셔너리에 저장한다.
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
    /// 해당 키의 유물 데이터를 반환한다.
    /// </summary>
    public RelicTextTable GetRelicTextData(int key)
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
