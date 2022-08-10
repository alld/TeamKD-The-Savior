using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDataBase : MonoBehaviour
{
    /// <summary>
    /// ī���� �⺻ �������� �������ִ� ������ ���̽� 
    /// <br>InfoCard�� ���� �ν��Ͻ��� ���� ����ؾ���. </br>
    /// <br>InfoCard(int) : int == �����ͺ��̽��� ĳ���� �����ѹ� </br>
    /// </summary>
    public static CardDataBase instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

    public class InfoCard
    {
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
        public string name;
        /// <summary>
        /// [saveData]ī�� ���� ����
        /// </summary>
        public int cardCount;
        /// <summary>
        /// ī�带 ����ϱ����� ���
        /// </summary>
        public int cost;
        /// <summary>
        /// ī���� �Ӽ�
        /// </summary>
        public int propertie;
        /// <summary>
        /// ī���� ȿ�� ���� �з�
        /// </summary>
        //public int effectSortA;
        public enum EffectSortA { HEAL, PROTECT, BUFF, DEBUFF, ATTACK, SPEIAL}
        public EffectSortA effectSortA;
        /// <summary>
        /// ī���� ȿ�� �ð� �з�
        /// </summary>
        public enum EffectSortB { NOW, CONTINUE, DELAY }
        public EffectSortB effectSortB;
        /// <summary>
        /// ī���� ȿ�� ��� �з� 
        /// </summary>
        public enum EffectSortC { ALLY, ALLIES, ENEMY, ENEMIES}
        public EffectSortC effectSortC;
        /// <summary>
        /// ī���� ȿ�� ���ϴ�� �з� 
        /// </summary>
        public enum EffectSortD { TOTAL, RANDOM, HP_HIGH, HP_LOW, DAMAGE_HIGH, DAMAGE_LOW }
        public EffectSortD effectSortD;
        /// <summary>
        /// ī���� ���� 1
        /// </summary>
        public int cardString1;
        /// <summary>
        /// ī���� ���� 2
        /// </summary>
        public int cardString2;
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
        public int effectValue_floatA;
        /// <summary>
        /// ī�� ȿ�� float�� B
        /// </summary>
        public int effectValue_floatB;
        /// <summary>
        /// ī�� ȿ�� float�� C
        /// </summary>
        public int effectValue_floatC;
        /// <summary>
        /// ī�� ȿ�� float�� D
        /// <br></br> [����] ���ӽð�, �����ð��� ����. �̿ܿ� ��밡�� 
        /// </summary>
        public int effectValue_floatD;
        /// <summary>
        /// ī�� ȿ�� bool��
        /// </summary>
        public int effectValue_bool;
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
        #endregion

        /// <summary>
        /// ī���� �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num) : ī�� ������ �ѹ�����</br>
        /// </summary>
        /// <param name="num"></param>
        public InfoCard(int num)
        {
            switch (num)
            {
                case 1: // �Ӽ��Ӽ� ī�� ���� ����
                    number = num;
                    Icon = null;
                    name = "";
                    cardCount = 0;
                    cost = 0;
                    propertie = 0;
                    effectSortA = 0;
                    effectSortB = 0;
                    effectSortC = 0;
                    effectSortD = 0;
                    cardString1 = 0;
                    cardString2 = 0;
                    effectValue_intA = 0;
                    effectValue_intB = 0;
                    effectValue_floatA = 0;
                    effectValue_floatB = 0;
                    effectValue_floatC = 0;
                    effectValue_floatD = 0;
                    effectValue_bool = 0;
                    sellValueA = 0;
                    sellValueB = 0;
                    recycle = 0;
                    dataCount = 0;
                    break;
                default:
                    break;
            }
        }
    }
    private void Awake()
    {
        instance = this;
    }
}

