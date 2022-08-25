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
        /// ī���� �⺻ �������� �������ִ� ������ ���̽� 
        /// <br>InfoCard�� ���� �ν��Ͻ��� ���� ����ؾ���. </br>
        /// <br>InfoCard(int) : int == �����ͺ��̽��� ĳ���� �����ѹ� </br>
        /// </summary>
        /// //public Sprite[] ImagefulSet;
        //public static List<Sprite> Imageful = null;

        #region ī�� �⺻ ������
        /// <summary>
        /// ī�� �����Ͱ� ����������� �����ϱ����� �ѹ�
        /// </summary>
        public int number;
        /// <summary>
        /// ī���� �׸� �̹���
        /// </summary>
        public Image Icon;
        /// <summary>
        /// ī���� �̸�
        /// </summary>
        public string cardName;
        /// <summary>
        /// [saveData]ī�� ���� ����
        /// </summary>
        public byte cardCount;
        /// <summary>
        /// ī�带 ����ϱ����� ���
        /// </summary>
        public byte cost;
        /// <summary>
        /// ī���� �Ӽ�
        /// </summary>
        public byte propertie;
        /// <summary>
        /// ī���� ȿ�� ���� �з�
        /// </summary>
        public enum EffectTypeA { HEAL, PROTECT, BUFF, DEBUFF, DAMAGE, SPEIAL }
        /// <summary>
        /// ī���� ȿ�� ���� �з�
        /// </summary>
        public EffectTypeA effectTypeA;
        /// <summary>
        /// ī���� ȿ�� �ð� �з�
        /// </summary>
        public enum EffectTypeB { NOW, CONTINUE, DELAY }
        /// <summary>
        /// ī���� ȿ�� �ð� �з�
        /// </summary>
        public EffectTypeB effectTypeB;
        /// <summary>
        /// ī���� ȿ�� ��� �з� 
        /// </summary>
        public enum EffectTypeC { ALLY, ALLIES, ENEMY, ENEMIES }
        /// <summary>
        /// ī�� ȿ�� ��� �з�
        /// </summary>
        public EffectTypeC effectTypeC;
        /// <summary>
        /// ī���� ȿ�� ���ϴ�� �з� 
        /// </summary>     
        public enum EffectTypeD { TOTAL, RANDOM, HP_HIGH, HP_LOW, DAMAGE_HIGH, DAMAGE_LOW }
        /// <summary>
        /// ī���� ȿ�� ���ϴ�� �з� 
        /// </summary>
        public EffectTypeD effectTypeD;
        /// <summary>
        /// ī�� ȿ�� ����ȿ����
        /// </summary>
        public float effectValue;
        /// <summary>
        /// ī���� ���� 1
        /// </summary>
        public string cardString1;
        /// <summary>
        /// ī���� ���� 2
        /// </summary>
        public string cardString2;
        /// <summary>
        /// ī�� ȿ�� int�� A
        /// </summary>
        public int effectValue_intA;
        /// <summary>
        /// ī�� ȿ�� int�� B
        /// </summary>
        public int effectValue_intB;
        /// <summary>
        /// ī�� ȿ�� float�� A
        /// </summary>
        public float effectValue_floatA;
        /// <summary>
        /// ī�� ȿ�� float�� B
        /// </summary>
        public float effectValue_floatB;
        /// <summary>
        /// ī�� ȿ�� float�� C
        /// </summary>
        public float effectValue_floatC;
        /// <summary>
        /// ī�� ȿ�� float�� D
        /// <br></br> [����] ���ӽð�, �����ð��� ����. �̿ܿ� ��밡�� 
        /// </summary>
        public float effectValue_floatD;
        /// <summary>
        /// ī�� ȿ�� bool��
        /// </summary>
        public bool effectValue_bool;
        /// <summary>
        /// ī���� �ǸŰ��� A��(���)
        /// </summary>
        public int sellValueA;
        /// <summary>
        /// ī���� �ǸŰ��� B��(�ҿ�)
        /// </summary>
        public int sellValueB;
        /// <summary>
        /// ī���� ��Ȱ�� ��ġ 
        /// </summary>
        public float recycle;
        /// <summary>
        /// ī���� ȿ���� ��� ��ø�Ǿ��ִ����� ���� ��ġ
        /// </summary>
        public int dataCount;
        /// <summary>
        /// ������ ��ø�Ǿ� ����Ǵ� ȿ�� �з�
        /// </summary>
        public double buffCount;

        public int buffindex;

        #endregion

        /// <summary>
        /// ī���� �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num) : ī�� ������ �ѹ�����</br>
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

