using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CharacterDatabase : MonoBehaviour
{
    //private TextAsset jsonData;
    //private string json;


    //#region ĳ���� �⺻ ������
    //public int number;
    ///// <summary>
    ///// ĳ���� �⺻ ������
    ///// </summary>
    //public Image icon;
    ///// <summary>
    ///// ĳ���� �⺻ �̸�
    ///// </summary>
    //public string charName;
    ///// <summary>
    ///// ĳ���� �⺻ ü��
    ///// </summary>
    //public float hp;
    ///// <summary>
    ///// ĳ���� �ִ� ü��
    ///// </summary>
    //public float maxHP;
    ///// <summary>
    ///// ĳ���� �⺻ ������
    ///// </summary>
    //public float damage;
    ///// <summary>
    ///// �����տ� �ִ� ĳ���� ������Ʈ, ��
    ///// </summary>
    //public GameObject charObject;
    ///// <summary>
    ///// ĳ���� �⺻ ���ݼӵ�
    ///// <br>1.0f = 100%</br>
    ///// </summary>
    //public float attackSpeed;
    ///// <summary>
    ///// ĳ���� �⺻ �̵��ӵ�
    ///// <br>1.0f = 100%</br>
    ///// </summary>
    //public float moveSpeed;
    ///// <summary>
    ///// ĳ���� �⺻ ����
    ///// </summary>
    //public float defense;
    ///// <summary>
    ///// ĳ���� �⺻ ����Ÿ��
    ///// <br>1. �ٰŸ�</br>
    ///// <br>2. ���Ÿ�</br>
    ///// <br>3. ��Ŀ</br>
    ///// </summary>
    //public int attackType;
    ///// <summary>
    ///// ĳ���� �⺻ ���� ����
    ///// </summary>
    //public float attackRange;
    ///// <summary>
    ///// ĳ���� �⺻ �Ӽ�
    ///// <br>0. �� �Ӽ�</br>
    ///// <br>1. �� �Ӽ�</br>
    ///// <br>2. �� �Ӽ�</br>
    ///// <br>3. Ǯ �Ӽ�</br>
    ///// </summary>
    //public int attribute;
    ///// <summary>
    ///// ĳ���� �ν� ���� : ���� ����� �ν��ϴ� ����
    ///// </summary>
    //public float priRange;
    ///// <summary>
    ///// ĳ���� �켱�� : ���� �ν� �켱��ġ
    ///// </summary>
    //public int priorities;
    ///// <summary>
    ///// ���� �켱�� : ��� �ν� ����
    ///// </summary>
    //public float defRange;
    ///// <summary>
    ///// ĳ���� �⺻ �ڸ� �켱�� : �ڸ� ��ġ
    ///// </summary>
    //public int positionPri;
    ///// <summary>
    ///// [saveData] ĳ���� Ŭ����
    ///// </summary>
    //public int unitClass;
    ///// <summary>
    ///// [saveData] ĳ���� �⺻ ��� 
    ///// <br>1~3 �ܰ�� ����</br>
    ///// </summary>
    //public int level;
    ///// <summary>
    ///// [saveData] ĳ���� ���� �ҿ� 
    ///// </summary>
    //public int exp;
    ///// <summary>
    ///// ĳ���� ��Ƽ ���� ���� 
    ///// <para>���̺� ���� ���� </para>
    ///// </summary>
    //public bool partySet;
    ///// <summary>
    ///// ĳ���� �ر� ���� 
    ///// <br> ��������Ʈ �������� �����Ұ��̶�, �رݿ��� �������ʿ����.</br>
    ///// </summary>
    //public bool islock;
    ///// <summary>
    ///// ĳ���� ���� ���� 
    ///// </summary>
    //public bool isLive;
    ///// <summary>
    ///// ĳ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    ///// <br> 0 �� ��ų�� ���°� </br>
    ///// </summary>
    //public int basicSkill;
    ///// <summary>
    ///// ĳ���Ͱ� ���� �ñر� �ε��� �ѹ�
    ///// <br> 0 �� ��ų�� ���°� </br>
    ///// </summary>
    //public int speialSkill;
    ///// <summary>
    ///// ������ �ߺ����� A��(���)
    ///// </summary>
    //public int overlapValueA;
    ///// <summary>
    ///// ������ �ߺ����� B��(�ҿ�)
    ///// </summary>
    //public int overlapValueB;
    //public int identity; // ĳ���� Ư��
    //#endregion
    ///// <summary>
    ///// ĳ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
    ///// <br>(int = num -1) : ĳ���� ������ �ѹ�����</br>
    ///// </summary>
    ///// <param name="num"></param>

    public class Data
    {
        private TextAsset jsonData;
        private string json;


        #region ĳ���� �⺻ ������
        public int number;
        /// <summary>
        /// ĳ���� �⺻ ������
        /// </summary>
        public Image icon;
        /// <summary>
        /// ĳ���� �⺻ �̸�
        /// </summary>
        public string charName;
        /// <summary>
        /// ĳ���� �⺻ ü��
        /// </summary>
        public float hp;
        /// <summary>
        /// ĳ���� �ִ� ü��
        /// </summary>
        public float maxHP;
        /// <summary>
        /// ĳ���� �⺻ ������
        /// </summary>
        public float damage;
        /// <summary>
        /// �����տ� �ִ� ĳ���� ������Ʈ, ��
        /// </summary>
        public GameObject charObject;
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
        public int attribute;
        /// <summary>
        /// ĳ���� �ν� ���� : ���� ����� �ν��ϴ� ����
        /// </summary>
        public float priRange;
        /// <summary>
        /// ĳ���� �켱�� : ���� �ν� �켱��ġ
        /// </summary>
        public int priorities;
        /// <summary>
        /// ���� �켱�� : ��� �ν� ����
        /// </summary>
        public float defRange;
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
        public int exp;
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
        public int basicSkill;
        /// <summary>
        /// ĳ���Ͱ� ���� �ñر� �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int speialSkill;
        /// <summary>
        /// ������ �ߺ����� A��(���)
        /// </summary>
        public int overlapValueA;
        /// <summary>
        /// ������ �ߺ����� B��(�ҿ�)
        /// </summary>
        public int overlapValueB;
        public int identity; // ĳ���� Ư��
        #endregion
        /// <summary>
        /// ĳ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num -1) : ĳ���� ������ �ѹ�����</br>
        /// </summary>
        /// <param name="num"></param>



        public Data(int num)
        {
            jsonData = Resources.Load<TextAsset>("CharacterData");
            json = jsonData.text;
            var jdata = JSON.Parse(json);
            number = num;
            icon = Resources.Load<Image>("Unit/Character_" + num.ToString());
            switch (GameManager.instance.data.Language)
            {
                case 0:
                    charName = jdata[num]["Name_Kr"];
                    break;
                case 1:
                    charName = jdata[num]["Name_Eng"];
                    break;
                default:
                    break;
            }
            hp = jdata[num]["Hp_Cur"];
            maxHP = jdata[num]["Hp_Total"];
            damage = jdata[num]["Chr_Power"];
            charObject = Resources.Load<GameObject>("Unit/" + jdata[num]["Name_Eng"]);
            attackSpeed = jdata[num]["Chr_AtkSpeed"];
            moveSpeed = jdata[num]["Chr_MS"];
            defense = jdata[num]["Chr_DF"];
            attackType = jdata[num]["Attack_Type"];
            attackRange = jdata[num]["Chr_AtkRange"];
            defRange = 0;
            attribute = 0;
            priRange = jdata[num]["Atk_Know_Range"];
            priorities = jdata[num]["Attack_Priority"];
            positionPri = jdata[num]["Place_Priority"];
            unitClass = 0;
            level = 1;
            exp = 0;
            islock = false;
            isLive = true;
            basicSkill = 0;
            speialSkill = 0;
            overlapValueA = 0;
            overlapValueB = 0;
        }

    }
    //public CharacterDatabase(int num)
    //{
    //    jsonData = Resources.Load<TextAsset>("CharacterData");
    //    json = jsonData.text;
    //    var data = JSON.Parse(json);
    //    number = num;
    //    icon = Resources.Load<Image>("Unit/Character_" + num.ToString());
    //    switch (GameManager.instance.data.Language)
    //    {
    //        case 0:
    //            charName = data[num]["Name_Kr"];
    //            break;
    //        case 1:
    //            charName = data[num]["Name_Eng"];
    //            break;
    //        default:
    //            break;
    //    }
    //    hp = data[num]["Hp_Cur"];
    //    maxHP = data[num]["Hp_Total"];
    //    damage = data[num]["Chr_Power"];
    //    charObject = Resources.Load<GameObject>("Unit/" + data[num]["Name_Eng"]);
    //    attackSpeed = data[num]["Chr_AtkSpeed"];
    //    moveSpeed = data[num]["Chr_MS"];
    //    defense = data[num]["Chr_DF"];
    //    attackType = data[num]["Attack_Type"];
    //    attackRange = data[num]["Chr_AtkRange"];
    //    defRange = 0;
    //    attribute = 0;
    //    priRange = data[num]["Atk_Know_Range"];
    //    priorities = data[num]["Attack_Priority"];
    //    positionPri = data[num]["Place_Priority"];
    //    unitClass = 0;
    //    level = 1;
    //    exp = 0;
    //    islock = false;
    //    isLive = true;
    //    basicSkill = 0;
    //    speialSkill = 0;
    //    overlapValueA = 0;
    //    overlapValueB = 0;
    //}
}