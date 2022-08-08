using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;


public class CharacterDatabase : MonoBehaviour
{
    /// <summary>
    /// ĳ������ �⺻ �������� �������ִ� ������ ���̽� 
    /// <br>InfoCh�� ���� �ν��Ͻ��� ���� ����ؾ���. </br>
    /// <br>InfoCH(int) : int == �����ͺ��̽��� ĳ���� �����ѹ� </br>
    /// </summary>
    public static CharacterDatabase instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;
    public class InfoCharacter
    {
        #region ĳ���� �⺻ ������
        public int number;
        /// <summary>
        /// ĳ���� �⺻ ������
        /// </summary>
        public Image icon;
        /// <summary>
        /// ĳ���� �⺻ �̸�
        /// </summary>
        public string name;
        /// <summary>
        /// ĳ���� �⺻ ü��
        /// </summary>
        public float hP;
        /// <summary>
        /// ĳ���� �ִ� ü��
        /// </summary>
        public float maxHP;
        /// <summary>
        /// ĳ���� �⺻ ������
        /// </summary>
        public float damage;
        /// <summary>
        /// ĳ���� ��Ÿ ������
        /// </summary>
        public float meleDmg;
        /// <summary>
        /// �����տ� �ִ� ĳ���� ������Ʈ, ��
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// ĳ���� �⺻ ���ݼӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeed;
        /// <summary>
        /// ĳ���� �⺻ �̵��ӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// ĳ���� �⺻ ����
        /// </summary>
        public float defense;
        /// <summary>
        /// ĳ���� �⺻ ����Ÿ��
        /// <br>1. �ٰŸ�</br>
        /// <br>2. ���Ÿ�</br>
        /// <br>3. ��Ŀ</br>
        /// </summary>
        public int attackType;
        /// <summary>
        /// ĳ���� �⺻ ���� ����
        /// </summary>
        public float attackRange;
        /// <summary>
        /// ĳ���� �⺻ �Ӽ�
        /// <br>0. �� �Ӽ�</br>
        /// <br>1. �� �Ӽ�</br>
        /// <br>2. �� �Ӽ�</br>
        /// <br>3. Ǯ �Ӽ�</br>
        /// </summary>
        public int Attribute;
        /// <summary>
        /// ĳ���� �ν� ���� : ���� ����� �ν��ϴ� ����
        /// </summary>
        public float priRange;
        /// <summary>
        /// ĳ���� �켱�� : ���� �ν� �켱��ġ
        /// </summary>
        public int priorities;
        /// <summary>
        /// ĳ���� �⺻ �ڸ� �켱�� : �ڸ� ��ġ
        /// </summary>
        public int positionPri;
        /// <summary>
        /// [saveData] ĳ���� Ŭ����
        /// </summary>
        public int unitClass;
        /// <summary>
        /// [saveData] ĳ���� �⺻ ��� 
        /// <br>1~3 �ܰ�� ����</br>
        /// </summary>
        public int level;
        /// <summary>
        /// [saveData] ĳ���� ���� �ҿ� 
        /// </summary>
        public int soul;
        /// <summary>
        /// ĳ���� ��Ƽ ���� ���� 
        /// <para>���̺� ���� ���� </para>
        /// </summary>
        public bool partySet;
        /// <summary>
        /// ĳ���� �ر� ���� 
        /// <br> ��������Ʈ �������� �����Ұ��̶�, �رݿ��� �������ʿ����.</br>
        /// </summary>
        public bool islock;
        /// <summary>
        /// ĳ���� ���� ���� 
        /// </summary>
        public bool isLive;
        /// <summary>
        /// ĳ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int basicskill;
        /// <summary>
        /// ĳ���Ͱ� ���� �ñر� �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int speialskill;
        /// <summary>
        /// ������ �ߺ����� A��(���)
        /// </summary>
        public int overlapValueA;
        /// <summary>
        /// ������ �ߺ����� B��(�ҿ�)
        /// </summary>
        public int overlapValueB;
        #endregion


        public InfoCharacter()
        {
            //�� ����
            number = 0;
            icon = null;
            name = null;
            hP = 0;
            maxHP = 100;
            damage = 0;
            meleDmg = 0;
            gameObject = null;
            attackSpeed = 0;
            moveSpeed = 0;
            defense = 0;
            attackType = 0;
            attackRange = 0;
            Attribute = 0;
            priRange = 0;
            priorities = 0;
            positionPri = 0;
            unitClass = 0;
            level = 1;
            soul = 0;
            partySet = false;
            islock = false;
            isLive = true;
            basicskill = 0;
            speialskill = 0;
            overlapValueA = 0;
            overlapValueB = 0;
        }
        /// <summary>
        /// ĳ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num) : ĳ���� ������ �ѹ�����</br>
        /// </summary>
        /// <param name="num"></param>
        public InfoCharacter(int num, JSONNode data)
        {
            //var data = JSON.Parse(jsonData.text));
            number = num;
            icon = null; // �̹��� ���� ����
            switch (GameManager.instance.data.Language)
            {
                case 0:
                    name = data[num]["Name_Kr"];
                    break;
                case 1:
                    name = data[num]["Name_Eng"];
                    break;
                default:
                    break;
            }
            hP = data[num]["Hp_Cur"];
            maxHP = data[num]["Hp_Total"];
            damage = data[num]["Chr_power"];
            meleDmg = data[num]["Total_Damage"];
            gameObject = Resources.Load<GameObject>("Unit/TestUnit");
            attackSpeed = data[num]["Chr_AtkSpeed"];
            moveSpeed = data[num]["Chr_MS"];
            defense = data[num]["Chr_DF"];
            attackType = data[num]["Attack_Type"];
            attackRange = data[num]["Chr_AtkRange"];
            Attribute = data[num]["Attribute"];
            priRange = data[num]["Atk_Know_Range"];
            priorities = data[num]["Attack_Priority"];
            positionPri = data[num]["Place_Priority"];
            unitClass = 0;
            level = 1;
            soul = 0;
            partySet = false;
            islock = false;
            isLive = true;
            basicskill = 0;
            speialskill = 0;
            overlapValueA = 0;
            overlapValueB = 0;


            //switch (num)
            //{
            //    case 1: // �Ӽ��Ӽ� ĳ���� ���� ����
            //        number = num;
            //        icon = null; // �̹��� ���� ����
            //        name = "�Ӽ��Ӽ� ĳ����"; // ���������� ���������� �ؽ�Ʈ ������� ����
            //        hP = 100;
            //        maxHP = 100;
            //        damage = 10.0f;
            //        gameObject = Resources.Load<GameObject>("Unit/TestUnit");
            //        attackSpeed = 1.0f;
            //        moveSpeed = 1.0f;
            //        defense = 10.0f;
            //        attackType = 1;
            //        attackRange = 10.0f;
            //        propertie = 0;
            //        priRange = 10.0f;
            //        priorities = 20;
            //        positionPer = 30;
            //        unitClass = 0;
            //        level = 1;
            //        soul = 0;
            //        partySet = false;
            //        islock = false;
            //        isLive = true;
            //        basicskill = 0;
            //        speialskill = 0;
            //        overlapValueA = 0;
            //        overlapValueB = 0;
            //        break;
            //    default:
            //        break;
            //}
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
