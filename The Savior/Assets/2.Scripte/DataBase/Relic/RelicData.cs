using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class RelicData : MonoBehaviour
{
    public class Data
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
        public enum EffectTypeA { HEAL, PROTECT, ABILITY, DAMAGE, SPEIAL };
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
        public double effectCount;
        public byte EffectCycle; // 인겜용 데이터 값 할당 X
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
        public double negEffectCount;
        public byte negEffectCycle; // 인겜용 데이터 값 할당 X



        public Data(int num)
        {
            int tempSort;                       // 수정부분 
            jsonData = Resources.Load<TextAsset>("RelicDB/RelicData");
            json = jsonData.text;

            var jdata = JSON.Parse(json);
            idx = jdata[num]["Index"];
            switch (GameManager.instance.data.Language)
            {
                case 0:
                    relicName = jdata[num]["Name_Kr"];
                    content = jdata[num]["Content_1_Kr"];
                    addContent = jdata[num]["Content_2_Kr"];
                    break;
                case 1:
                    relicName = jdata[num]["Name_Eng"];
                    content = jdata[num]["Conten_1_Eng"];
                    addContent = jdata[num]["Content_2_Eng"];
                    break;
                default:
                    break;
            }
            usingTime = jdata[num]["Using_Time"];
            dataRange = jdata[num]["Data_Range"];
            attribute = jdata[num]["Attribute"];
            tempSort = jdata[num]["Effect_Type_A"];  // 수정부분
            effectTypeA = (EffectTypeA)tempSort;    // 수정부분
            tempSort = jdata[num]["Effect_Type_B"];  // 수정부분
            effectTypeB = (EffectTypeB)tempSort;    // 수정부분
            tempSort = jdata[num]["Effect_Type_C"];  // 수정부분
            effectTypeC = (EffectTypeC)tempSort;    // 수정부분
            tempSort = jdata[num]["Effect_Type_D"];  // 수정부분
            effectTypeD = (EffectTypeD)tempSort;    // 수정부분
            effectDataA1 = jdata[num]["Effect_Data_A1"];
            effectDataA2 = jdata[num]["Effect_Data_A2"];
            effectValue = jdata[num]["Effect_Value"];
            effectDataB1 = jdata[num]["Effect_Data_B1"];
            effectDataB2 = jdata[num]["Effect_Data_B2"];
            effectDataB3 = jdata[num]["Effect_Data_B3"];
            effectDataB4 = jdata[num]["Effect_Data_B4"];
            effectDataC1 = jdata[num]["Effect_Data_C1"];

            // 데이터 베이스도 같이 수정해야될거같아서 이부분은 추가를 안했어욥
        }
        private void Awake()
        {
            RelicData.Data data = new RelicData.Data(number);
            idx = data.idx;
        }
    }
}
