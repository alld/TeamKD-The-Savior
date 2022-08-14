using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


public class RelicData : MonoBehaviour
{
    private TextAsset jsonData;
    private string json;

    public int number = 0;
    public int idx = 0;
    public string relicName;
    public string content;
    public string addContent;
    public int usingTime;
    public int dataRange;
    public int attribute;
    public enum EffectTypeA { HEAL, PROTECT, ABILITY, ATTACK, SPEIAL }; 
    public EffectTypeA effectTypeA; 
    public enum EffectTypeB { ALWAY, FIRST, MIDDLE, HALF, LAST }; 
    public EffectTypeB effectTypeB; 
    public enum EffectTypeC { ALLY, ALLIES, ENEMY, ENEMIES }; 
    public EffectTypeC effectTypeC;     
    public enum EffectTypeD { TOTAL, RANDOM, HP_HIGH, HP_LOW, DAMAGE_HIGH, DAMAGE_LOW };
    public EffectTypeD effectTypeD;
    public int effectDataA1;
    public int effectDataA2;
    public float effectValue;
    public float effectDataB1;
    public float effectDataB2;
    public float effectDataB3;
    public float effectDataB4;
    public float effectDataC1;
    public bool loopEffect;
    public EffectTypeA negEffectTypeA;
    public EffectTypeB negEffectTypeB;
    public EffectTypeC negEffectTypeC;
    public EffectTypeD negEffectTypeD;
    public int negEffectDataA1;
    public int negEffectDataA2;
    public float negEffectValue;
    public float negEffectDataB1;
    public float negEffectDataB2;
    public float negEffectDataB3;
    public float negEffectDataB4;
    public float negEffectDataC1;
    public bool negLoopEffect;


    public RelicData(int num)
    {
        int tempSort;                       // 수정부분 
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
        tempSort = data[num]["Effect_Type_A"];  // 수정부분
        effectTypeA = (EffectTypeA)tempSort;    // 수정부분
        tempSort = data[num]["Effect_Type_B"];  // 수정부분
        effectTypeB = (EffectTypeB)tempSort;    // 수정부분
        tempSort = data[num]["Effect_Type_C"];  // 수정부분
        effectTypeC = (EffectTypeC)tempSort;    // 수정부분
        tempSort = data[num]["Effect_Type_D"];  // 수정부분
        effectTypeD = (EffectTypeD)tempSort;    // 수정부분
        effectDataA1 = data[num]["Effect_Data_A1"];
        effectDataA2 = data[num]["Effect_Data_A2"];
        effectValue = data[num]["Effect_Value"];
        effectDataB1 = data[num]["Effect_Data_B1"];
        effectDataB2 = data[num]["Effect_Data_B2"];
        effectDataB3 = data[num]["Effect_Data_B3"];
        effectDataB4 = data[num]["Effect_Data_B4"];
        effectDataC1 = data[num]["Effect_Data_C1"];

        // 데이터 베이스도 같이 수정해야될거같아서 이부분은 추가를 안했어욥
    }
    private void Awake()
    {
        RelicData data = new RelicData(number);
        idx = data.idx;
    }
}
