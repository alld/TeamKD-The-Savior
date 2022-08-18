using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CharacterDatabase : MonoBehaviour
{
    //private TextAsset jsonData;
    //private string json;


    //#region 캐릭터 기본 설정값
    //public int number;
    ///// <summary>
    ///// 캐릭터 기본 아이콘
    ///// </summary>
    //public Image icon;
    ///// <summary>
    ///// 캐릭터 기본 이름
    ///// </summary>
    //public string charName;
    ///// <summary>
    ///// 캐릭터 기본 체력
    ///// </summary>
    //public float hp;
    ///// <summary>
    ///// 캐릭터 최대 체력
    ///// </summary>
    //public float maxHP;
    ///// <summary>
    ///// 캐릭터 기본 데미지
    ///// </summary>
    //public float damage;
    ///// <summary>
    ///// 프리팹에 있는 캐릭터 오브젝트, 모델
    ///// </summary>
    //public GameObject charObject;
    ///// <summary>
    ///// 캐릭터 기본 공격속도
    ///// <br>1.0f = 100%</br>
    ///// </summary>
    //public float attackSpeed;
    ///// <summary>
    ///// 캐릭터 기본 이동속도
    ///// <br>1.0f = 100%</br>
    ///// </summary>
    //public float moveSpeed;
    ///// <summary>
    ///// 캐릭터 기본 방어력
    ///// </summary>
    //public float defense;
    ///// <summary>
    ///// 캐릭터 기본 공격타입
    ///// <br>1. 근거리</br>
    ///// <br>2. 원거리</br>
    ///// <br>3. 탱커</br>
    ///// </summary>
    //public int attackType;
    ///// <summary>
    ///// 캐릭터 기본 공격 범위
    ///// </summary>
    //public float attackRange;
    ///// <summary>
    ///// 캐릭터 기본 속성
    ///// <br>0. 무 속성</br>
    ///// <br>1. 불 속성</br>
    ///// <br>2. 물 속성</br>
    ///// <br>3. 풀 속성</br>
    ///// </summary>
    //public int attribute;
    ///// <summary>
    ///// 캐릭터 인식 범위 : 공격 대상을 인식하는 범위
    ///// </summary>
    //public float priRange;
    ///// <summary>
    ///// 캐릭터 우선도 : 공격 인식 우선수치
    ///// </summary>
    //public int priorities;
    ///// <summary>
    ///// 유닛 우선도 : 방어 인식 범위
    ///// </summary>
    //public float defRange;
    ///// <summary>
    ///// 캐릭터 기본 자리 우선도 : 자리 배치
    ///// </summary>
    //public int positionPri;
    ///// <summary>
    ///// [saveData] 캐릭터 클래스
    ///// </summary>
    //public int unitClass;
    ///// <summary>
    ///// [saveData] 캐릭터 기본 등급 
    ///// <br>1~3 단계로 구성</br>
    ///// </summary>
    //public int level;
    ///// <summary>
    ///// [saveData] 캐릭터 소지 소울 
    ///// </summary>
    //public int exp;
    ///// <summary>
    ///// 캐릭터 파티 참여 여부 
    ///// <para>세이브 여부 검토 </para>
    ///// </summary>
    //public bool partySet;
    ///// <summary>
    ///// 캐릭터 해금 여부 
    ///// <br> 업적데이트 기준으로 설정할것이라, 해금여부 저장할필요없음.</br>
    ///// </summary>
    //public bool islock;
    ///// <summary>
    ///// 캐릭터 생존 여부 
    ///// </summary>
    //public bool isLive;
    ///// <summary>
    ///// 캐릭터가 가진 기본 스킬 인덱스 넘버
    ///// <br> 0 은 스킬이 없는것 </br>
    ///// </summary>
    //public int basicSkill;
    ///// <summary>
    ///// 캐릭터가 가진 궁극기 인덱스 넘버
    ///// <br> 0 은 스킬이 없는것 </br>
    ///// </summary>
    //public int speialSkill;
    ///// <summary>
    ///// 유물의 중복보상 A형(골드)
    ///// </summary>
    //public int overlapValueA;
    ///// <summary>
    ///// 유물의 중복보상 B형(소울)
    ///// </summary>
    //public int overlapValueB;
    //public int identity; // 캐릭터 특성
    //#endregion
    ///// <summary>
    ///// 캐릭터의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
    ///// <br>(int = num -1) : 캐릭터 고유의 넘버링값</br>
    ///// </summary>
    ///// <param name="num"></param>

    public class Data
    {
        private TextAsset jsonData;
        private string json;


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
        /// <summary>
        /// 캐릭터의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
        /// <br>(int = num -1) : 캐릭터 고유의 넘버링값</br>
        /// </summary>
        /// <param name="num"></param>



        public Data(int num)
        {
            jsonData = Resources.Load<TextAsset>("CharacterData");
            json = jsonData.text;
            var jdata = JSON.Parse(json);
            number = num;
            icon = Resources.Load<Image>("Unit/Character_" + num.ToString());
            switch (GameManager.instance?.data.Language)
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
            //charObject = Resources.Load<GameObject>("Unit/TestBox");
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