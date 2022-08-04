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
        /// ī���� �Ӽ�
        /// </summary>
        public int propertie;
        /// <summary>
        /// ī���� ȿ�� ��з�
        /// </summary>
        public int effectSortA;
        /// <summary>
        /// ī���� ȿ�� �Һз�
        /// </summary>
        public int effectSortB;
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
        /// </summary>
        public int effectValue_floatD;
        /// <summary>
        /// ī�� ȿ�� bool��
        /// </summary>
        public int effectValue_bool;
        public enum Conditions { �Ϲ�, �ʹ�, �߹�, �Ĺ� };
        /// <summary>
        /// ī�� ������� �з�
        /// </summary>
        Conditions condition;
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
                    propertie = 0;
                    effectSortA = 0;
                    effectSortB = 0;
                    cardString1 = 0;
                    cardString2 = 0;
                    effectValue_intA = 0;
                    effectValue_intB = 0;
                    effectValue_floatA = 0;
                    effectValue_floatB = 0;
                    effectValue_floatC = 0;
                    effectValue_floatD = 0;
                    effectValue_bool = 0;
                    condition = Conditions.�Ϲ�;
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

