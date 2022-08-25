using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class CharacterDatabase : MonoBehaviour
{
    public class Data
    {
        private TextAsset jsonData;
        private TextAsset nameData;


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
        public string charNameKor;
        public string charNameEng;
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

        public Data(int num)
        {
            jsonData = Resources.Load<TextAsset>("CharacterDB/CharacterData");
            nameData = Resources.Load<TextAsset>("CharacterDB/CharacterNameData");
            JArray json = JArray.Parse(jsonData.text);
            JArray jName = JArray.Parse(nameData.text);



            icon = Resources.Load<Image>("Unit/Character_" + num.ToString());
            number = num;

            maxHP = float.Parse(json[num - 1]["Hp_Total"].ToObject<string>());
            damage = float.Parse(json[num - 1]["Chr_Power"].ToObject<string>());
            attackSpeed = float.Parse(json[num - 1]["Chr_AtkSpeed"].ToObject<string>());
            attackRange = float.Parse(json[num - 1]["Chr_AtkRange"].ToObject<string>());
            defense = float.Parse(json[num - 1]["Chr_DF"].ToObject<string>());
            moveSpeed = float.Parse(json[num - 1]["Chr_MS"].ToObject<string>());
            attackType = int.Parse(json[num - 1]["Attack_Type"].ToObject<string>());
            priorities = int.Parse(json[num - 1]["Attack_Priority"].ToObject<string>());
            positionPri = int.Parse(json[num - 1]["Place_Priority"].ToObject<string>());
            attribute = int.Parse(json[num - 1]["Attribute"].ToObject<string>());
            priRange = float.Parse(json[num - 1]["Atk_Know_Range"].ToObject<string>());
            defRange = float.Parse(json[num - 1]["Def_Know_Range"].ToObject<string>());

            //switch (GameManager.instance.data.Language)
            //{
            //    case 0:
            //        charName = jName[num - 1]["Name_Kr"].ToObject<string>().ToString();
            //        break;
            //    case 1:
            //        charName = jName[num - 1]["Name_Eng"].ToObject<string>().ToString();
            //        break;
            //    default:
            //        break;
            //}
            charNameEng = jName[num - 1]["Name_Eng"].ToObject<string>().ToString();
            charNameKor = jName[num - 1]["Name_Kr"].ToObject<string>().ToString();
            //charObject = Resources.Load<GameObject>("Unit/TestBox");
            //Debug.Log(string.Format("Unit/Hero{0}_{1}", num.ToString("00"), charName));
            charObject = Resources.Load<GameObject>(string.Format("Unit/Hero{0}_{1}", num.ToString("00"), charNameEng));
            hp = maxHP;
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
}