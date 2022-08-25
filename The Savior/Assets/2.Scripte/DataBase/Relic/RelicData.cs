using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class RelicData : MonoBehaviour
{
    public class Data
    {
        private TextAsset jsonData, jsonTextData;

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
            number = num;
            int tempSort;                       // 수정부분 
            jsonData = Resources.Load<TextAsset>("RelicDB/RelicData");
            jsonTextData = Resources.Load<TextAsset>("RelicDB/RelicText");
            JArray jdata = JArray.Parse(jsonData.text);
            JArray textdata = JArray.Parse(jsonTextData.text);
            idx = int.Parse(jdata[num - 1]["Index"].ToObject<string>());
            switch (GameManager.instance.data.Language)
            {
                case 0:
                    relicName = textdata[num]["Name_Kr"].ToObject<string>();
                    content = textdata[num]["Positive_Kr"].ToObject<string>();
                    addContent = textdata[num]["Negative_Kr"].ToObject<string>();
                    break;
                case 1:
                    relicName = textdata[num]["Name_Eng"].ToObject<string>();
                    content = textdata[num]["Positive_Eng"].ToObject<string>();
                    addContent = textdata[num]["Negative_Eng"].ToObject<string>();
                    break;
                default:
                    break;
            }


            usingTime = int.Parse(jdata[num]["usingTime"].ToObject<string>());
            dataRange = int.Parse(jdata[num]["dataRange"].ToObject<string>());
            attribute = int.Parse(jdata[num]["attribute"].ToObject<string>());
            tempSort = byte.Parse(jdata[num]["EffectTypeA"].ToObject<string>());
            effectTypeA = (EffectTypeA)tempSort;
            tempSort = byte.Parse(jdata[num]["EffectTypeB"].ToObject<string>());
            effectTypeB = (EffectTypeB)tempSort;
            tempSort = byte.Parse(jdata[num]["EffectTypeC"].ToObject<string>());
            effectTypeC = (EffectTypeC)tempSort;
            tempSort = byte.Parse(jdata[num]["EffectTypeD"].ToObject<string>());
            effectTypeD = (EffectTypeD)tempSort;
            effectDataA1 = int.Parse(jdata[num]["effectDataA1"].ToObject<string>());
            effectDataA2 = int.Parse(jdata[num]["effectDataA2"].ToObject<string>());
            effectValue = float.Parse(jdata[num]["effectValue"].ToObject<string>());
            effectDataB1 = float.Parse(jdata[num]["effectDataB1"].ToObject<string>());
            effectDataB2 = float.Parse(jdata[num]["effectDataB2"].ToObject<string>());
            effectDataB3 = float.Parse(jdata[num]["effectDataB3"].ToObject<string>());
            effectDataB4 = float.Parse(jdata[num]["effectDataB4"].ToObject<string>());
            effectDataC1 = float.Parse(jdata[num]["effectDataC1"].ToObject<string>());
            loopEffect = bool.Parse(jdata[num]["loopEffect"].ToObject<string>());
            effectCount = double.Parse(jdata[num]["effectCount"].ToObject<string>());
            EffectCycle = 0; // 인겜용 데이터 값 할당 X
            tempSort = byte.Parse(jdata[num]["NegEffectTypeA"].ToObject<string>());
            negEffectTypeA = (EffectTypeA)tempSort;
            tempSort = byte.Parse(jdata[num]["NegEffectTypeB"].ToObject<string>());
            negEffectTypeB = (EffectTypeB)tempSort;
            tempSort = byte.Parse(jdata[num]["NegEffectTypeC"].ToObject<string>());
            negEffectTypeC = (EffectTypeC)tempSort;
            tempSort = byte.Parse(jdata[num]["NegEffectTypeD"].ToObject<string>());
            negEffectTypeD = (EffectTypeD)tempSort;
            negEffectDataA1 = int.Parse(jdata[num]["negEffectDataA1"].ToObject<string>());
            negEffectDataA2 = int.Parse(jdata[num]["negEffectDataA2"].ToObject<string>());
            negEffectValue = float.Parse(jdata[num]["negEffectValue"].ToObject<string>());
            negEffectDataB1 = float.Parse(jdata[num]["negEffectDataB1"].ToObject<string>());
            negEffectDataB2 = float.Parse(jdata[num]["negEffectDataB2"].ToObject<string>());
            negEffectDataB3 = float.Parse(jdata[num]["negEffectDataB3"].ToObject<string>());
            negEffectDataB4 = float.Parse(jdata[num]["negEffectDataB4"].ToObject<string>());
            negEffectDataC1 = float.Parse(jdata[num]["negEffectDataC1"].ToObject<string>());
            negLoopEffect = bool.Parse(jdata[num]["negLoopEffect"].ToObject<string>());
            negEffectCount= double.Parse(jdata[num]["negEffectCount"].ToObject<string>());
            negEffectCycle = 0; // 인겜용 데이터 값 할당 X

            // 데이터 베이스도 같이 수정해야될거같아서 이부분은 추가를 안했어욥
        }
        private void Awake()
        {
            RelicData.Data data = new RelicData.Data(number);
            idx = data.idx;
        }
    }
}
