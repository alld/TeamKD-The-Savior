using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicDataBase : MonoBehaviour
{
    /// <summary>
    /// ������ �⺻ �������� �������ִ� ������ ���̽� 
    /// <br>InfoRelic�� ���� �ν��Ͻ��� ���� ����ؾ���. </br>
    /// <br>InfoRelic(int) : int == �����ͺ��̽��� ĳ���� �����ѹ� </br>
    /// </summary>
    public static RelicDataBase instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

    public class InfoRelic
    {
        #region ī�� �⺻ ������
        /// <summary>
        /// ���� �����Ͱ� ����������� �����ϱ����� �ѹ�
        /// </summary>
        public int number;
        /// <summary>
        /// ������ �׸� �̹���
        /// </summary>
        public Image Icon;
        /// <summary>
        /// ������ �̸�
        /// </summary>
        public string name;
        /// <summary>
        /// [saveData]���� ���� ����
        /// </summary>
        public bool relicCount;
        /// <summary>
        /// ������ �Ӽ�
        /// </summary>
        public int propertie;
        /// <summary>
        /// ������ ȿ�� ��з�
        /// </summary>
        public int effectSortA;
        /// <summary>
        /// ������ ȿ�� �Һз�
        /// </summary>
        public int effectSortB;
        /// <summary>
        /// ������ ���� 1
        /// </summary>
        public int relicString1;
        /// <summary>
        /// ������ ���� 2
        /// </summary>
        public int relicString2;
        /// <summary>
        /// ���� ȿ�� int�� A
        /// </summary>
        public int effectValue_intA;
        /// <summary>
        /// ���� ȿ�� int�� B
        /// </summary>
        public int effectValue_intB;
        /// <summary>
        /// ���� ȿ�� float�� A
        /// </summary>
        public int effectValue_floatA;
        /// <summary>
        /// ���� ȿ�� float�� B
        /// </summary>
        public int effectValue_floatB;
        /// <summary>
        /// ���� ȿ�� float�� C
        /// </summary>
        public int effectValue_floatC;
        /// <summary>
        /// ���� ȿ�� float�� D
        /// </summary>
        public int effectValue_floatD;
        /// <summary>
        /// ���� ȿ�� bool��
        /// </summary>
        public int effectValue_bool;
        public enum Conditions { �Ϲ�, �ʹ�, �߹�, �Ĺ� };
        /// <summary>
        /// ���� ������� �з�
        /// </summary>
        Conditions condition;
        /// <summary>
        /// ������ �ߺ����� A��(���)
        /// </summary>
        public int overlapValueA;
        /// <summary>
        /// ������ �ߺ����� B��(�ҿ�)
        /// </summary>
        public int overlapValueB;
        /// <summary>
        /// ������ ��Ȱ�� ��ġ 
        /// </summary>
        public float recycle;
        /// <summary>
        /// ������ ȿ���� ��� ��ø�Ǿ��ִ����� ���� ��ġ
        /// </summary>
        public int dataCount;
        #endregion

        /// <summary>
        /// ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num) : ���� ������ �ѹ�����</br>
        /// </summary>
        /// <param name="num"></param>
        public InfoRelic(int num)
        {
            switch (num)
            {
                case 1: // �Ӽ��Ӽ� ī�� ���� ����
                    number = num;
                    Icon = null;
                    name = "";
                    relicCount = false;
                    propertie = 0;
                    effectSortA = 0;
                    effectSortB = 0;
                    relicString1 = 0;
                    relicString2 = 0;
                    effectValue_intA = 0;
                    effectValue_intB = 0;
                    effectValue_floatA = 0;
                    effectValue_floatB = 0;
                    effectValue_floatC = 0;
                    effectValue_floatD = 0;
                    effectValue_bool = 0;
                    condition = Conditions.�Ϲ�;
                    overlapValueA = 0;
                    overlapValueB = 0;
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