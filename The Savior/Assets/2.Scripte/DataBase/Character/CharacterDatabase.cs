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


        #region 캐릭터 기본 설정값
        public int number;
        /// <summary>
        /// 캐릭터 기본 아이콘
        /// </summary>
        public Image icon;
        /// <summary>
        /// 캐릭터 기본 이름
        /// </summary>
        public string charName;
        public string charNameKor;
        public string charNameEng;
        /// <summary>
        /// 캐릭터 기본 체력
        /// </summary>
        public float hp;
        /// <summary>
        /// 캐릭터 최대 체력
        /// </summary>
        public float maxHP;
        /// <summary>
        /// 캐릭터 기본 데미지
        /// </summary>
        public float damage;
        /// <summary>
        /// 프리팹에 있는 캐릭터 오브젝트, 모델
        /// </summary>
        public GameObject charObject;
        /// <summary>
        /// 캐릭터 기본 공격속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeed;
        /// <summary>
        /// 캐릭터 기본 이동속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// 캐릭터 기본 방어력
        /// </summary>
        public float defense;
        /// <summary>
        /// 캐릭터 기본 공격타입
        /// <br>1. 근거리</br>
        /// <br>2. 원거리</br>
        /// <br>3. 탱커</br>
        /// </summary>
        public int attackType;
        /// <summary>
        /// 캐릭터 기본 공격 범위
        /// </summary>
        public float attackRange;
        /// <summary>
        /// 캐릭터 기본 속성
        /// <br>0. 무 속성</br>
        /// <br>1. 불 속성</br>
        /// <br>2. 물 속성</br>
        /// <br>3. 풀 속성</br>
        /// </summary>
        public int attribute;
        /// <summary>
        /// 캐릭터 인식 범위 : 공격 대상을 인식하는 범위
        /// </summary>
        public float priRange;
        /// <summary>
        /// 캐릭터 우선도 : 공격 인식 우선수치
        /// </summary>
        public int priorities;
        /// <summary>
        /// 유닛 우선도 : 방어 인식 범위
        /// </summary>
        public float defRange;
        /// <summary>
        /// 캐릭터 기본 자리 우선도 : 자리 배치
        /// </summary>
        public int positionPri;
        /// <summary>
        /// [saveData] 캐릭터 클래스
        /// </summary>
        public int unitClass;
        /// <summary>
        /// [saveData] 캐릭터 기본 등급 
        /// <br>1~3 단계로 구성</br>
        /// </summary>
        public int level;
        /// <summary>
        /// [saveData] 캐릭터 소지 소울 
        /// </summary>
        public int exp;
        /// <summary>
        /// 캐릭터 파티 참여 여부 
        /// <para>세이브 여부 검토 </para>
        /// </summary>
        public bool partySet;
        /// <summary>
        /// 캐릭터 해금 여부 
        /// <br> 업적데이트 기준으로 설정할것이라, 해금여부 저장할필요없음.</br>
        /// </summary>
        public bool islock;
        /// <summary>
        /// 캐릭터 생존 여부 
        /// </summary>
        public bool isLive;
        /// <summary>
        /// 캐릭터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicSkill;
        /// <summary>
        /// 캐릭터가 가진 궁극기 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int speialSkill;
        /// <summary>
        /// 유물의 중복보상 A형(골드)
        /// </summary>
        public int overlapValueA;
        /// <summary>
        /// 유물의 중복보상 B형(소울)
        /// </summary>
        public int overlapValueB;
        public int identity; // 캐릭터 특성
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