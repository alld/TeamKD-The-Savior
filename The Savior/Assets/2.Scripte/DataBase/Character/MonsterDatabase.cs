using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;


public class MonsterDatabase : MonoBehaviour {
    public class Data
    {
        private TextAsset jsonData;
        private TextAsset jsonTextData;
        /// <summary>
        /// ������ �⺻ �������� �������ִ� ������ ���̽� 
        /// <br>InfoMT�� ���� �ν��Ͻ��� ���� ����ؾ���. </br>
        /// <br>InfoMT(int) : int == �����ͺ��̽��� ĳ���� �����ѹ� </br>
        /// </summary>

        #region ������ �⺻ ������
        /// <summary>
        /// ���� Ÿ�� ����
        /// </summary>
        public enum MonsterType { MONSTER, BOSS, LASTBOSS }
        public MonsterType monsterType;
        public int number;
        /// <summary>
        /// �����տ� �ִ� ĳ���� ������Ʈ, ��
        /// </summary>
        public GameObject charObject;
        /// <summary>
        /// ���� �⺻ �̸�
        /// </summary>
        public string charName;
        /// <summary>
        /// ���� �⺻ ü��
        /// </summary>
        public float hp;
        /// <summary>
        /// ���� �ִ� ü��
        /// </summary>
        public float maxHP;
        /// <summary>
        /// ���� �⺻ ������
        /// </summary>
        public float damage;
        /// <summary>
        /// ���� �⺻ ���ݼӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeed;
        /// <summary>
        /// ���� �⺻ �̵��ӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// ���� �⺻ ����
        /// </summary>
        public float defense;
        /// <summary>
        /// ���� �⺻ ����Ÿ��
        /// <br>1. �ٰŸ�</br>
        /// <br>2. ���Ÿ�</br>
        /// <br>3. ��Ŀ</br>
        /// </summary>
        public int attackType;
        /// <summary>
        /// ���� �⺻ ���� ����
        /// </summary>
        public float attackRange;
        /// <summary>
        /// ���� �⺻ �Ӽ�
        /// <br>0. �� �Ӽ�</br>
        /// <br>1. �� �Ӽ�</br>
        /// <br>2. �� �Ӽ�</br>
        /// <br>3. Ǯ �Ӽ�</br>
        /// </summary>
        public int attribute;
        /// <summary>
        /// ���� �ν� ���� : ���� ����� �ν��ϴ� ����
        /// </summary>
        public float priRange;
        /// <summary>
        /// ���� �켱�� : ���� �ν� �켱��ġ
        /// </summary>
        public int priorities;
        /// <summary>
        /// ���� �켱�� : ��� �ν� ����
        /// </summary>
        public float defRange;
        /// <summary>
        /// ���� �⺻ �ڸ� �켱�� : �ڸ� ��ġ
        /// </summary>
        public int positionPri;
        /// <summary>
        /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int basicSkillA;
        /// <summary>
        /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int basicSkillB;
        /// <summary>
        /// ���Ͱ� ���� �ñر� �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int speialSkill;
        public int rewardGold;
        public int rewardSoul;
        public List<int> rewardCard = new List<int>();
        public List<int> rewardUnit = new List<int>();
        public List<int> rewardRelic = new List<int>();
        #endregion

        /// <summary>
        /// ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num) : ���� ������ �ѹ�����</br>
        /// </summary>
        /// <param name="num"></param>
        public Data(int num)
        {
            jsonData = Resources.Load<TextAsset>("MonsterData");
            jsonTextData = Resources.Load<TextAsset>("MonsterTextData");
            JArray json = JArray.Parse(jsonData.text);
            JArray jsonText = JArray.Parse(jsonTextData.text);
            switch (GameManager.instance.data.Language)
            {
                case 0:
                    charName = jsonText[num - 1]["Name_Kr"].ToObject<string>();
                    break;
                case 1:
                    charName = jsonText[num - 1]["Name_Eng"].ToObject<string>();
                    break;
            }
            number = num;
            maxHP = float.Parse(json[num - 1]["Hp_Total"].ToObject<string>());
            hp = maxHP;
            int temp = int.Parse(json[num - 1]["MonsterType"].ToObject<string>());
            monsterType = (MonsterType)temp;
            charObject = Resources.Load<GameObject>(string.Format("Unit/Monster/Monster{0}_{1}", (num).ToString("00"), jsonText[num - 1]["Name_Eng"].ToObject<string>()));
            damage = float.Parse(json[num - 1]["Chr_Power"].ToObject<string>());
            attackSpeed = float.Parse(json[num - 1]["Chr_AtkSpeed"].ToObject<string>());
            moveSpeed = float.Parse(json[num - 1]["Chr_MS"].ToObject<string>());
            defense = float.Parse(json[num - 1]["Chr_DF"].ToObject<string>());
            attackType = int.Parse(json[num - 1]["Attack_Type"].ToObject<string>());
            attackRange = float.Parse(json[num - 1]["Chr_AtkRange"].ToObject<string>());
            attribute = int.Parse(json[num - 1]["Attribute"].ToObject<string>());
            priRange = float.Parse(json[num - 1]["Atk_Know_Range"].ToObject<string>());
            defRange = float.Parse(json[num - 1]["Def_Know_Range"].ToObject<string>());
            priorities = int.Parse(json[num - 1]["Attack_Priority"].ToObject<string>());
            positionPri = int.Parse(json[num - 1]["Place_Priority"].ToObject<string>());
            basicSkillA = int.Parse(json[num - 1]["basicSkillA"].ToObject<string>());
            int.TryParse(json[num - 1]["basicSkillB"].ToObject<string>(), out basicSkillB);
            int.TryParse(json[num - 1]["specialSkill"].ToObject<string>(), out speialSkill);
            int.TryParse(json[num - 1]["rewardGold"].ToObject<string>(), out rewardGold);
            int.TryParse(json[num - 1]["rewardSoul"].ToObject<string>(), out rewardSoul);
            int.TryParse(json[num - 1]["rewardCard"].ToObject<string>(), out temp);
            string tempString = temp.ToString();
            if (tempString.Length < 1)
            {
                for (int i = 0; i < tempString.Length / 2; i++)
                {
                    rewardCard.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
                }
            }
            int.TryParse(json[num - 1]["rewardHero"].ToObject<string>(), out temp);
            tempString = temp.ToString();
            if (tempString.Length < 1)
            {
                for (int i = 0; i < tempString.Length; i++)
                {
                    rewardUnit.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
                }
            }
            int.TryParse(json[num - 1]["rewardRelic"].ToObject<string>(), out temp);
            tempString = temp.ToString();
            if (tempString.Length < 1)
            {
                for (int i = 0; i < tempString.Length; i++)
                {
                    rewardRelic.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
                }
            }
        }
    }
}
