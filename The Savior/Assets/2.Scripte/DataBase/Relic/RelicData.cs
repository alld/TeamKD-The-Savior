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
    public string content;
    public string addContent;
    public int usingTime;
    public int dataRange;
    public int attribute;
    public int effectTypeA;
    public int effectTypeB;
    public int useCondition;
    public int effectDataA1;
    public int effectDataA2;
    public float effectValue;
    public float effectDataB1;
    public float effectDataB2;
    public float effectDataB3;
    public float effectDataB4;
    public float effectDataC1;

    private void Awake()
    {
        jsonData = Resources.Load<TextAsset>("RelicData");
        json = jsonData.text;

        var data = JSON.Parse(json);
        idx = data[num]["Index"];
        switch (GameManager.instance.data.Language)
        {
            case 0:
                relicName = data[num]["Name_Kr"];
                content = data[num]["Content_1_Kr"];
                addContent = data[num]["Content_2_Kr"];
                break;
            case 1:
                relicName = data[num]["Name_Eng"];
                content = data[num]["Conten_1_Eng"];
                addContent = data[num]["Content_2_Eng"];
                break;
            default:
                break;
        }
        usingTime = data[num]["Using_Time"];
        dataRange = data[num]["Data_Range"];
        attribute = data[num]["Attribute"];
        effectTypeA = data[num]["Effect_Type_A"];
        effectTypeB = data[num]["Effect_Type_B"];
        useCondition = data[num]["Use_Condition"];
        effectDataA1 = data[num]["Effect_Data_A1"];
        effectDataA2 = data[num]["Effect_Data_A2"];
        effectValue = data[num]["Effect_Value"];
        effectDataB1 = data[num]["Effect_Data_B1"];
        effectDataB2 = data[num]["Effect_Data_B2"];
        effectDataB3 = data[num]["Effect_Data_B3"];
        effectDataB4 = data[num]["Effect_Data_B4"];
        effectDataC1 = data[num]["Effect_Data_C1"];
    }
}
