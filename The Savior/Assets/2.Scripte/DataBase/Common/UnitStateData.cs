using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStateData : MonoBehaviour
{

    #region UnitUI
    private Canvas canvas;
    Image partySlotHPGauage;
    Image unitHPGauage;
    bool UISettingCheck = false;

    #endregion

    #region 유닛 설정값
    public bool playerUnit;
    MonsterDatabase.Data.MonsterType monsterType;
    public int number = 0;
    /// <summary>
    /// 캐릭터 기본 이름
    /// </summary>
    public string charName = "";
    /// <summary>
    /// 캐릭터 기본 체력
    /// </summary>
    public float hp 
    {
        get { return display_hp; }
        set
        { 
            display_hp = value;
            HPUI();
        }

    }
    public float display_hp = 0;
    /// <summary>
    /// 캐릭터 최대 체력
    /// </summary>
    public float maxHP = 0;
    /// <summary>
    /// 캐릭터 기본 데미지
    /// </summary>
    public float damage = 0;
    /// <summary>
    /// 캐릭터 기본 공격속도
    /// <br>1.0f = 100%</br>
    /// </summary>
    public float attackSpeed = 0;
    /// <summary>
    /// 캐릭터 기본 이동속도
    /// <br>1.0f = 100%</br>
    /// </summary>
    public float moveSpeed = 0;
    /// <summary>
    /// 캐릭터 기본 방어력
    /// </summary>
    public float defense = 0;
    /// <summary>
    /// 캐릭터 기본 공격타입
    /// <br>1. 근거리</br>
    /// <br>2. 원거리</br>
    /// <br>3. 탱커</br>
    /// </summary>
    public int attackType = 0;
    /// <summary>
    /// 캐릭터 기본 공격 범위
    /// </summary>
    public float attackRange = 0;
    /// <summary>
    /// 캐릭터 기본 속성
    /// <br>0. 무 속성</br>
    /// <br>1. 불 속성</br>
    /// <br>2. 물 속성</br>
    /// <br>3. 풀 속성</br>
    /// </summary>
    public int attribute = 0;
    /// <summary>
    /// 캐릭터 인식 범위 : 공격 대상을 인식하는 범위
    /// </summary>
    public float priRange = 0;
    /// <summary>
    /// 캐릭터 우선도 : 공격 인식 우선수치
    /// </summary>
    public int priorities = 0;
    /// <summary>
    /// 유닛 우선도 : 방어 인식 범위
    /// </summary>
    public float defRange = 1;
    /// <summary>
    /// 캐릭터 기본 자리 우선도 : 자리 배치
    /// </summary>
    public int positionPri = 0;
    /// <summary>
    /// [saveData] 캐릭터 클래스
    /// </summary>
    public int unitClass = 0;
    /// <summary>
    /// [saveData] 캐릭터 기본 등급 
    /// <br>1~3 단계로 구성</br>
    /// </summary>
    public int level = 0;
    /// <summary>
    /// [saveData] 캐릭터 소지 소울 
    /// </summary>
    public int exp = 0;
    /// <summary>
    /// 캐릭터 파티 참여 여부 
    /// <para>세이브 여부 검토 </para>
    /// </summary>
    public bool partySet = false;
    /// <summary>
    /// 캐릭터 해금 여부 
    /// <br> 업적데이트 기준으로 설정할것이라, 해금여부 저장할필요없음.</br>
    /// </summary>
    public bool islock = false;
    /// <summary>
    /// 캐릭터 생존 여부 
    /// </summary>
    public bool isLive = false;
    /// <summary>
    /// 유물의 중복보상 A형(골드)
    /// </summary>
    public int overlapValueA = 0;
    /// <summary>
    /// 유물의 중복보상 B형(소울)
    /// </summary>
    public int overlapValueB = 0;
    public int identity = 0; // 캐릭터 특성
    public int basicSkillA = 0;
    /// <summary>
    /// 몬스터가 가진 기본 스킬 인덱스 넘버
    /// <br> 0 은 스킬이 없는것 </br>
    /// </summary>
    public int basicSkillB = 0;
    /// <summary>
    /// 몬스터가 가진 궁극기 인덱스 넘버
    /// <br> 0 은 스킬이 없는것 </br>
    /// </summary>
    public int speialSkill = 0;
    public int rewardGold = 0;
    public int rewardSoul = 0;
    public List<int> rewardCard = new List<int>();
    public List<int> rewardUnit = new List<int>();
    public List<int> rewardRelic = new List<int>();
    public GameObject unitObj;

    #endregion
    #region 유닛 가중치
    // 아군
    /// <summary>
    /// 가중치 :: 추가 공격력
    /// </summary>
    public float Add_damage = 0;
    /// <summary>
    /// 가중치 :: 최종 추가 공격력
    /// </summary>
    public float Add_fianlDamage = 1;
    /// <summary>
    /// 가중치 :: 피해량 감소량
    /// </summary>
    public float Add_dropDamage = 1;
    /// <summary>
    /// 가중치(추가능력) :: 보호막 수치
    /// </summary>
    public float Current_protect = 0;
    /// <summary>
    /// 가중치(추가능력) :: 보호막 최대 수치
    /// </summary>
    public float Current_protectMax = 0;
    /// <summary>
    /// 가중치 :: 공격 속도
    /// </summary>
    public float Add_attackSpeed = 1;
    /// <summary>
    /// 가중치 :: 이동 속도
    /// </summary>
    public float Add_moveSpeed = 1;
    /// <summary>
    /// 가중치 :: 방어력
    /// </summary>
    public float Add_defense = 0;
    /// <summary>
    /// 가중치 :: 공격 범위(사거리)
    /// </summary>
    public float Add_attackRange = 1;
    /// <summary>
    /// 가중치 :: 속성 변경 여부 (bool)
    /// </summary>
    public bool Add_attributeCheck = false;
    /// <summary>
    /// 가중치 :: 변경된 속성값 
    /// </summary>
    public int Add_attribute = 0;
    /// <summary>
    /// 가중치 :: 속성 추가 데미지
    /// </summary>
    public float[] Add_attributeVlaue = { 1, 1, 1, 1 };
    /// <summary>
    /// 가중치 :: 공격 인식 범위
    /// </summary>
    public float Add_priRange = 1;
    /// <summary>
    /// 가중치 :: 공격 우선도
    /// </summary>
    public int Add_priorities = 0;
    /// <summary>
    /// 가중치 :: 스킬 쿨다운
    /// </summary>
    public float Add_skilcoldown = 1;
    /// <summary>
    /// 가중치 :: 무적 유무
    /// </summary>
    public bool isinvincible = false;
    public List<BuffDataBase> Current_buff = new List<BuffDataBase>();
    //적
    public int Add_rewardGold = 0;
    public int Add_rewardSoul = 0;
    #endregion
    public int partyNumber;

    public void DataSetting(bool playerCheck, int num)
    {
        playerUnit = playerCheck;
        if (playerCheck)
        {
            gameObject.layer = 6;
            CharacterDatabase.Data unit = new CharacterDatabase.Data(num);
            unitObj = unit.charObject;
            number = unit.number;
            charName = unit.charName;
            maxHP = unit.maxHP;
            hp = maxHP;
            damage = unit.damage;
            attackSpeed = unit.attackSpeed;
            moveSpeed = unit.moveSpeed;
            defense = unit.defense;
            attackType = unit.attackType;
            attackRange = unit.attackRange;
            attribute = unit.attribute;
            priRange = unit.priRange;
            defRange = unit.defRange;
            priorities = unit.priorities;
            positionPri = unit.positionPri;
            unitClass = unit.unitClass;
            level = unit.level;
            exp = unit.exp;
            partySet = unit.partySet;
            islock = unit.islock;
            isLive = unit.isLive;
            basicSkillA = unit.basicSkill;
            speialSkill = unit.speialSkill;
            overlapValueA = unit.overlapValueA;
            overlapValueB = unit.overlapValueB;
        }
        else
        {
            gameObject.layer = 7;
            MonsterDatabase.Data unit = new MonsterDatabase.Data(num);
            unitObj = unit.charObject;
            number = unit.number;
            monsterType = unit.monsterType;
            charName = unit.charName;
            maxHP = unit.maxHP;
            hp = maxHP;
            damage = unit.damage;
            attackSpeed = unit.attackSpeed;
            moveSpeed = unit.moveSpeed;
            defense = unit.defense;
            attackType = unit.attackType;
            attackRange = unit.attackRange;
            attribute = unit.attribute;
            priRange = unit.priRange;
            defRange = unit.defRange;
            priorities = unit.priorities;
            positionPri = unit.positionPri;
            basicSkillA = unit.basicSkillA;
            basicSkillB = unit.basicSkillB;
            speialSkill = unit.speialSkill;
            rewardGold = unit.rewardGold;
            rewardSoul = unit.rewardSoul;
            rewardCard = unit.rewardCard;
            rewardUnit = unit.rewardUnit;
            rewardRelic = unit.rewardRelic;
        }
    }

    public void UISetting()
    {
        UISettingCheck = true;
        canvas = GameObject.Find("DungeonCanvas").GetComponent<Canvas>();
        unitHPGauage = Instantiate(Resources.Load<Image>("Unit/UI/HpGauage"),canvas.transform);
        if (playerUnit) partySlotHPGauage = DungeonController.instance.partySlotHPGauage[partyNumber]; HPUIMove();
    }

    public void StagePositionReset()
    {
        
    }

    public void HPUI()
    {
        if (!UISettingCheck) UISetting();
        float temp = hp / maxHP;
        unitHPGauage.fillAmount = temp;
        if(playerUnit) partySlotHPGauage.fillAmount = temp;
        if (hp <= 0) unitHPGauage.gameObject.SetActive(false);
        else if (hp > 1) unitHPGauage.gameObject.SetActive(true);
        // 파티슬롯 기준 UI 지정/ 몬스터는 별도 UI 그룹 생성 
    }

    public void HPUIMove()
    {
        unitHPGauage.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));
    }


    public void SelfDestory()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Destroy(unitHPGauage.gameObject);
    }
    /* 체력게이지 UI 관리
     * 피격 UI도 관리 (데미지 유동텍스트)
     * 
     * 데이터베이스 정리 및 연동
     * 가중치 변동 확인
     * 변동된 카드 적용 확인
     * 변동된 유물 적용 확인
     * 던전 상단 진행바 확인
     * 
     * 
     */

}

