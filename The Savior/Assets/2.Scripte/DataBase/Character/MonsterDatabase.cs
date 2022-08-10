using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDatabase : MonoBehaviour
{
    /// <summary>
    /// 몬스터의 기본 원형값을 가지고있는 데이터 베이스 
    /// <br>InfoMT를 통해 인스턴스를 만들어서 사용해야함. </br>
    /// <br>InfoMT(int) : int == 데이터베이스상 캐릭터 고유넘버 </br>
    /// </summary>

        #region 몬스터의 기본 설정값
        /// <summary>
        /// 몬스터 타입 구분
        /// </summary>
        public enum MonsterType { MONSTER, BOSS, LASTBOSS}
        MonsterType monsterType;
        public int number;
        /// <summary>
        /// 프리팹에 있는 캐릭터 오브젝트, 모델
        /// </summary>
        public GameObject charObject;
        /// <summary>
        /// 몬스터 기본 아이콘
        /// </summary>
        public Image icon;
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
        /// 몬스터 기본 자리 우선도 : 자리 배치
        /// </summary>
        public int positionPer;
        /// <summary>
        /// 몬스터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicSkill1;
        /// <summary>
        /// 몬스터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicSkill2;
        /// <summary>
        /// 몬스터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicSkill3;
        /// <summary>
        /// 몬스터가 가진 궁극기 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int speialSkill;
        public int rewardGold;
        public int rewardSoul;
        public int[] rewardCard;
        public int[] rewardUnit;
        public int[] rewardRelic;
    #endregion

    /// <summary>
    /// 몬스터의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
    /// <br>(int = num) : 몬스터 고유의 넘버링값</br>
    /// </summary>
    /// <param name="num"></param>
    public MonsterDatabase(int num)
    {
        switch (num)
        {
            case 1: // 머선머선 몬스터 아직 미정
                number = num;
                monsterType = MonsterType.MONSTER;
                icon = null; // 이미지 설정 검토
                charName = "머선머선 캐릭터"; // 던전정보와 마찬가지로 텍스트 구동방식 검토
                hp = 100;
                damage = 10.0f;
                charObject = Resources.Load<GameObject>("Unit/TestUnit");
                attackSpeed = 1.0f;
                moveSpeed = 1.0f;
                defense = 10.0f;
                attackType = 1;
                attackRange = 10.0f;
                attribute = 0;
                priRange = 10.0f;
                priorities = 20;
                positionPer = 30;
                basicSkill1 = 0;
                basicSkill2 = 0;
                basicSkill3 = 0;
                speialSkill = 0;
                rewardGold = 1;
                rewardSoul = 1;
                rewardCard = null;
                rewardUnit = null;
                rewardRelic = null;
                break;
        }
    }

}
