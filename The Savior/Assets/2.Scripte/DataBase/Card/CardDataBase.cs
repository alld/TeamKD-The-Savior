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
    public CardDataBase(int num)
    {
        number = num;
        Icon = null;
        name = " ";
        cardCount = 0;
        cost = 0;
        propertie = 0;
        effectTypeA = 0;
        effectTypeB = 0;
        effectTypeC = 0;
        effectTypeD = 0;
        cardString1 = "";
        cardString2 = "";
        effectValue_intA = 0;
        effectValue_intB = 0;
        effectValue_floatA = 0;
        effectValue_floatB = 0;
        effectValue_floatC = 0;
        effectValue_floatD = 0;
        effectValue_bool = false;
        sellValueA = 0;
        sellValueB = 0;
        recycle = 0;
        dataCount = 0;
        buffCount = 0;
        buffindex = 0;
    }

    private void Awake()
    {
        instance = this;
    }
}

