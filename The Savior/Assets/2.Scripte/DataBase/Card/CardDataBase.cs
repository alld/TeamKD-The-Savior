using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class CardDataBase : MonoBehaviour
{
    public class Data
    {
        private TextAsset jsonData, jsonTextData;
        /// <summary>
        /// 카드의 기본 원형값을 가지고있는 데이터 베이스 
        /// <br>InfoCard를 통해 인스턴스를 만들어서 사용해야함. </br>
        /// <br>InfoCard(int) : int == 데이터베이스상 캐릭터 고유넘버 </br>
        /// </summary>
        /// //public Sprite[] ImagefulSet;
        //public static List<Sprite> Imageful = null;

        #region 카드 기본 설정값
        /// <summary>
        /// 카드 데이터가 어떤데이터인지 구분하기위한 넘버
        /// </summary>
        public int number;
        /// <summary>
        /// 카드의 그림 이미지
        /// </summary>
        public Image Icon;
        /// <summary>
        /// 카드의 이름
        /// </summary>
        public string cardName;
        /// <summary>
        /// [saveData]카드 소지 갯수
        /// </summary>
        public byte cardCount;
        /// <summary>
        /// 카드를 사용하기위한 비용
        /// </summary>
        public byte cost;
        /// <summary>
        /// 카드의 속성
        /// </summary>
        public byte propertie;
        /// <summary>
        /// 카드의 효과 종류 분류
        /// </summary>
        public enum EffectTypeA { HEAL, PROTECT, BUFF, DEBUFF, DAMAGE, SPEIAL }
        /// <summary>
        /// 카드의 효과 종류 분류
        /// </summary>
        public EffectTypeA effectTypeA;
        /// <summary>
        /// 카드의 효과 시간 분류
        /// </summary>
        public enum EffectTypeB { NOW, CONTINUE, DELAY }
        /// <summary>
        /// 카드의 효과 시간 분류
        /// </summary>
        public EffectTypeB effectTypeB;
        /// <summary>
        /// 카드의 효과 대상 분류 
        /// </summary>
        public enum EffectTypeC { ALLY, ALLIES, ENEMY, ENEMIES }
        /// <summary>
        /// 카드 효과 대상 분류
        /// </summary>
        public EffectTypeC effectTypeC;
        /// <summary>
        /// 카드의 효과 단일대상 분류 
        /// </summary>     
        public enum EffectTypeD { TOTAL, RANDOM, HP_HIGH, HP_LOW, DAMAGE_HIGH, DAMAGE_LOW }
        /// <summary>
        /// 카드의 효과 단일대상 분류 
        /// </summary>
        public EffectTypeD effectTypeD;
        /// <summary>
        /// 카드 효과 메인효과값
        /// </summary>
        public float effectValue;
        /// <summary>
        /// 카드의 설명 1
        /// </summary>
        public string cardString1;
        /// <summary>
        /// 카드의 설명 2
        /// </summary>
        public string cardString2;
        /// <summary>
        /// 카드 효과 int형 A
        /// </summary>
        public int effectValue_intA;
        /// <summary>
        /// 카드 효과 int형 B
        /// </summary>
        public int effectValue_intB;
        /// <summary>
        /// 카드 효과 float형 A
        /// </summary>
        public float effectValue_floatA;
        /// <summary>
        /// 카드 효과 float형 B
        /// </summary>
        public float effectValue_floatB;
        /// <summary>
        /// 카드 효과 float형 C
        /// </summary>
        public float effectValue_floatC;
        /// <summary>
        /// 카드 효과 float형 D
        /// <br></br> [역할] 지속시간, 지연시간에 사용됨. 이외에 사용가능 
        /// </summary>
        public float effectValue_floatD;
        /// <summary>
        /// 카드 효과 bool형
        /// </summary>
        public bool effectValue_bool;
        /// <summary>
        /// 카드의 판매가격 A형(골드)
        /// </summary>
        public int sellValueA;
        /// <summary>
        /// 카드의 판매가격 B형(소울)
        /// </summary>
        public int sellValueB;
        /// <summary>
        /// 카드의 재활용 수치 
        /// </summary>
        public float recycle;
        /// <summary>
        /// 카드의 효과가 몇번 중첩되어있는지에 관한 수치
        /// </summary>
        public int dataCount;
        /// <summary>
        /// 버프에 중첩되어 적용되는 효과 분류
        /// </summary>
        public double buffCount;

        public int buffindex;

        #endregion

        /// <summary>
        /// 카드의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
        /// <br>(int = num) : 카드 고유의 넘버링값</br>
        /// </summary>
        /// <param name="num"></param>
        public Data(int num)
        {
            int temp;
            jsonData = Resources.Load<TextAsset>("CardDB/CardDataTable");
            jsonTextData = Resources.Load<TextAsset>("CardData");
            JArray jdata = JArray.Parse(jsonData.text);
            JArray textdata = JArray.Parse(jsonTextData.text);
            number = num;
            Icon = Resources.Load<Image>("Card/Card_" + (num).ToString());
            switch (GameManager.instance.data.Language)
            {
                case 0:
                    cardName = textdata[num - 1]["Name_Kr"].ToObject<string>();
                    break;
                case 1:
                    cardName = textdata[num - 1]["Name_Eng"].ToObject<string>();
                    break;
                default:
                    break;
            }
            cardCount = 0;

            cost = byte.Parse(jdata[num - 1]["Card_Cost"].ToObject<string>());
            propertie = 0;
            temp = byte.Parse(jdata[num - 1]["EffectTypeA"].ToObject<string>());
            effectTypeA = (EffectTypeA)temp;
            temp = byte.Parse(jdata[num - 1]["EffectTypeB"].ToObject<string>());
            effectTypeB = (EffectTypeB)temp;
            temp = byte.Parse(jdata[num - 1]["EffectTypeC"].ToObject<string>());
            effectTypeC = (EffectTypeC)temp;
            temp = byte.Parse(jdata[num - 1]["EffectTypeD"].ToObject<string>());
            effectTypeD = (EffectTypeD)temp;
            cardString1 = "";
            cardString2 = "";
            float.TryParse(jdata[num - 1]["effectValue"].ToObject<string>(), out effectValue);
            int.TryParse(jdata[num - 1]["effectDataA1"].ToObject<string>(), out effectValue_intA);
            int.TryParse(jdata[num - 1]["effectDataA2"].ToObject<string>(), out effectValue_intB);
            float.TryParse(jdata[num - 1]["effectDataB1"].ToObject<string>(), out effectValue_floatA);
            float.TryParse(jdata[num - 1]["effectDataB2"].ToObject<string>(), out effectValue_floatB);
            float.TryParse(jdata[num - 1]["effectDataB3"].ToObject<string>(), out effectValue_floatC);
            float.TryParse(jdata[num - 1]["effectDataB4"].ToObject<string>(), out effectValue_floatD);
            bool.TryParse(jdata[num - 1]["effectDataD1"].ToObject<string>(), out effectValue_bool);
            sellValueA = 0;
            sellValueB = 0;
            recycle = 0;
            dataCount = 0;
            double.TryParse(jdata[num - 1]["effectCount"].ToObject<string>(), out buffCount);
            int.TryParse(jdata[num - 1]["BF_Num"].ToObject<string>(), out buffindex);
        }
    }
}

