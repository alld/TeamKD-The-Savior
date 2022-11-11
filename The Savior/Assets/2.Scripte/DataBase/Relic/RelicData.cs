using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class RelicData : MonoBehaviour
{
    public class Data
    {
        private TextAsset jsonData;//, jsonTextData;

        public int number;
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
            if (num == 0) return;
            number = num;
            byte tempSort;                       // 수정부분 
            jsonData = Resources.Load<TextAsset>("RelicDB/RelicData");
            //jsonTextData = Resources.Load<TextAsset>("RelicDB/RelicText");
            JArray jdata = JArray.Parse(jsonData.text);
            //JArray textdata = JArray.Parse(jsonTextData.text);
            idx = int.Parse(jdata[num - 1]["Index"].ToObject<string>());
            //switch (GameManager.instance.data.Language)
            //{
            //    case 0:
            //        relicName = textdata[num - 1]["Name_Kr"].ToObject<string>();
            //        content = textdata[num - 1]["Positive_Kr"].ToObject<string>();
            //        addContent = textdata[num - 1]["Negative_Kr"].ToObject<string>();
            //        break;
            //    case 1:
            //        relicName = textdata[num - 1]["Name_Eng"].ToObject<string>();
            //        content = textdata[num - 1]["Positive_Eng"].ToObject<string>();
            //        addContent = textdata[num - 1]["Negative_Eng"].ToObject<string>();
            //        break;
            //    default:
            //        break;
            //}


            usingTime = int.Parse(jdata[num - 1]["usingTime"].ToObject<string>());
            dataRange = int.Parse(jdata[num - 1]["dataRange"].ToObject<string>());
            int.TryParse(jdata[num - 1]["attribute"].ToObject<string>(), out attribute);
            byte.TryParse(jdata[num - 1]["EffectTypeA"].ToObject<string>(), out tempSort);
            effectTypeA = (EffectTypeA)tempSort;
            byte.TryParse(jdata[num - 1]["EffectTypeB"].ToObject<string>(), out tempSort);
            effectTypeB = (EffectTypeB)tempSort;
            byte.TryParse(jdata[num - 1]["EffectTypeC"].ToObject<string>(), out tempSort);
            effectTypeC = (EffectTypeC)tempSort;
            byte.TryParse(jdata[num - 1]["EffectTypeD"].ToObject<string>(), out tempSort);
            effectTypeD = (EffectTypeD)tempSort;
            int.TryParse(jdata[num - 1]["effectDataA1"].ToObject<string>(), out effectDataA1);
            int.TryParse(jdata[num - 1]["effectDataA2"].ToObject<string>(), out effectDataA2);
            float.TryParse(jdata[num - 1]["effectValue"].ToObject<string>(), out effectValue);
            float.TryParse(jdata[num - 1]["effectDataB1"].ToObject<string>(), out effectDataB1);
            float.TryParse(jdata[num - 1]["effectDataB2"].ToObject<string>(), out effectDataB2);
            float.TryParse(jdata[num - 1]["effectDataB3"].ToObject<string>(), out effectDataB3);
            float.TryParse(jdata[num - 1]["effectDataB4"].ToObject<string>(), out effectDataB4);
            float.TryParse(jdata[num - 1]["effectDataC1"].ToObject<string>(), out effectDataC1);
            bool.TryParse(jdata[num - 1]["loopEffect"].ToObject<string>(), out loopEffect);
            double.TryParse(jdata[num - 1]["effectCount"].ToObject<string>(), out effectCount);
            EffectCycle = 0; // 인겜용 데이터 값 할당 X
            byte.TryParse(jdata[num - 1]["NegEffectTypeA"].ToObject<string>(), out tempSort);
            negEffectTypeA = (EffectTypeA)tempSort;
            byte.TryParse(jdata[num - 1]["NegEffectTypeB"].ToObject<string>(), out tempSort);
            negEffectTypeB = (EffectTypeB)tempSort;
            byte.TryParse(jdata[num - 1]["NegEffectTypeC"].ToObject<string>(), out tempSort);
            negEffectTypeC = (EffectTypeC)tempSort;
            byte.TryParse(jdata[num - 1]["NegEffectTypeD"].ToObject<string>(), out tempSort);
            negEffectTypeD = (EffectTypeD)tempSort;
            int.TryParse(jdata[num - 1]["negEffectDataA1"].ToObject<string>(), out negEffectDataA1);
            int.TryParse(jdata[num - 1]["negEffectDataA2"].ToObject<string>(), out negEffectDataA2);
            float.TryParse(jdata[num - 1]["negEffectValue"].ToObject<string>(), out negEffectValue);
            float.TryParse(jdata[num - 1]["negEffectDataB1"].ToObject<string>(), out negEffectDataB1);
            float.TryParse(jdata[num - 1]["negEffectDataB2"].ToObject<string>(), out negEffectDataB2);
            float.TryParse(jdata[num - 1]["negEffectDataB3"].ToObject<string>(), out negEffectDataB3);
            float.TryParse(jdata[num - 1]["negEffectDataB4"].ToObject<string>(), out negEffectDataB4);
            float.TryParse(jdata[num - 1]["negEffectDataC1"].ToObject<string>(), out negEffectDataC1);
            bool.TryParse(jdata[num - 1]["negLoopEffect"].ToObject<string>(), out negLoopEffect);
            double.TryParse(jdata[num - 1]["negEffectCount"].ToObject<string>(), out negEffectCount);
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
