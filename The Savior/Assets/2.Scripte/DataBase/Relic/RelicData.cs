using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


public class RelicData : MonoBehaviour
{
    private TextAsset jsonData;
    private string json;

    public int num = 0;
    public int idx = 0;
    public string relicName;

    private void Awake()
    {
        jsonData = Resources.Load<TextAsset>("RelicData");
        json = jsonData.text;

        var data = JSON.Parse(json);
        idx = data[num]["Index"];
        relicName = data[num]["Name_Kr"];
    }
}
