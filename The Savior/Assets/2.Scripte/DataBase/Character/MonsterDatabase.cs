using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;


public class MonsterDatabase : MonoBehaviour {
    public class Data
    {
        private TextAsset jsonData;
        private string json;
        private TextAsset jsonTextData;
        private string jsonText;
        /// <summary>
        /// 몬스터의 기본 원형값을 가지고있는 데이터 베이스 
        /// <br>InfoMT를 통해 인스턴스를 만들어서 사용해야함. </br>
        /// <br>InfoMT(int) : int == 데이터베이스상 캐릭터 고유넘버 </br>
        /// </summary>

        #region 몬스터의 기본 설정값
        /// <summary>
        /// 몬스터 타입 구분
        /// </summary>
        public enum MonsterType { MONSTER, BOSS, LASTBOSS }
        public MonsterType monsterType;
        public int number;
        /// <summary>
        /// 프리팹에 있는 캐릭터 오브젝트, 모델
        /// </summary>
        public GameObject charObject;
        /// <summary>
        /// 몬스터 기본 이름
        /// </summary>
        public string charName;
        /// <summary>
        /// 몬스터 기본 체력
        /// </summary>
        public float hp;
        /// <summary>
        /// 몬스터 최대 체력
        /// </summary>
        public float maxHP;
        /// <summary>
        /// 몬스터 기본 데미지
        /// </summary>
        public float damage;
        /// <summary>
        /// 몬스터 기본 공격속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeed;
        /// <summary>
        /// 몬스터 기본 이동속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// 몬스터 기본 방어력
        /// </summary>
        public float defense;
        /// <summary>
        /// 몬스터 기본 공격타입
        /// <br>1. 근거리</br>
        /// <br>2. 원거리</br>
        /// <br>3. 탱커</br>
        /// </summary>
        public int attackType;
        /// <summary>
        /// 몬스터 기본 공격 범위
        /// </summary>
        public float attackRange;
        /// <summary>
        /// 몬스터 기본 속성
        /// <br>0. 무 속성</br>
        /// <br>1. 불 속성</br>
        /// <br>2. 물 속성</br>
        /// <br>3. 풀 속성</br>
        /// </summary>
        public int attribute;
        /// <summary>
        /// 몬스터 인식 범위 : 공격 대상을 인식하는 범위
        /// </summary>
        public float priRange;
        /// <summary>
        /// 몬스터 우선도 : 공격 인식 우선수치
        /// </summary>
        public int priorities;
        /// <summary>
        /// 유닛 우선도 : 방어 인식 범위
        /// </summary>
        public float defRange;
        /// <summary>
        /// 몬스터 기본 자리 우선도 : 자리 배치
        /// </summary>
        public int positionPri;
        /// <summary>
        /// 몬스터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicSkillA;
        /// <summary>
        /// 몬스터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicSkillB;
        /// <summary>
        /// 몬스터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicSkillC;
        /// <summary>
        /// 몬스터가 가진 궁극기 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int speialSkill;
        public int rewardGold;
        public int rewardSoul;
        public int rewardExp;
        public List<int> rewardCard = new List<int>();
        public List<int> rewardUnit = new List<int>();
        public List<int> rewardRelic = new List<int>();
        #endregion

        /// <summary>
        /// 몬스터의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
        /// <br>(int = num) : 몬스터 고유의 넘버링값</br>
        /// </summary>
        /// <param name="num"></param>
        public Data(int num)
        {
            jsonData = Resources.Load<TextAsset>("MonsterData");
            json = jsonData.text;
            jsonTextData = Resources.Load<TextAsset>("MonsterTextData");
            jsonText = jsonTextData.text;
            var textdata = JSON.Parse(jsonText);
            var jdata = JSON.Parse(json);
            switch (GameManager.instance.data.Language)
            {
                case 0:
                    charName = textdata[num]["Name_Kr"];
                    break;
                case 1:
                    charName = textdata[num]["Name_Eng"];
                    break;
            }
            number = num;
            maxHP = jdata[num]["Hp_Total"];
            hp = maxHP;
            int temp = jdata[num][""];
            monsterType = (MonsterType)temp;
            charObject = Resources.Load<GameObject>("Unit/Monster/Stage01_Monster_0" + num);
            damage = jdata[num]["Total_Damage"];
            attackSpeed = jdata[num]["Chr_AtkSpeed"];
            moveSpeed = jdata[num]["Chr_MS"];
            defense = jdata[num]["Chr_DF"];
            attackType = jdata[num]["Attack_Type"];
            attackRange = jdata[num]["Chr_AtkRange"];
            attribute = jdata[num]["Attribute"];
            priRange = jdata[num]["Atk_Know_Range"];
            defRange = jdata[num]["Def_Know_Range"];
            priorities = jdata[num]["Attack_Priority"];
            positionPri = jdata[num]["Place_Priority"];
            basicSkillA = jdata[num][""];
            basicSkillB = jdata[num][""];
            basicSkillC = jdata[num][""];
            speialSkill = jdata[num][""];
            rewardGold = jdata[num][""];
            rewardSoul = jdata[num][""];
            rewardExp = jdata[num][""];
            temp = jdata[num][""];
            string tempString = temp.ToString();
            if (tempString.Length < 1)
            {
                for (int i = 0; i < tempString.Length / 2; i++)
                {
                    rewardCard.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
                }
            }
            temp = jdata[num][""];
            tempString = temp.ToString();
            if (tempString.Length < 1)
            {
                for (int i = 0; i < tempString.Length; i++)
                {
                    rewardUnit.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
                }
            }
            temp = jdata[num][""];
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
