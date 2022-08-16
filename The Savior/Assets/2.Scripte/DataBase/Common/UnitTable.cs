using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTable : MonoBehaviour
{
    MonsterDatabase.MonsterType monsterType;
    public int number = 0;
    /// <summary>
    /// 캐릭터 기본 이름
    /// </summary>
    public string charName = "";
    /// <summary>
    /// 캐릭터 기본 체력
    /// </summary>
    public float hp = 0;
    /// <summary>
    /// 캐릭터 최대 체력
    /// </summary>
    public float maxHP = 0;
    /// <summary>
    /// 캐릭터 기본 데미지
    /// </summary>
    public float damage = 0;
    /// <summary>
    /// 프리팹에 있는 캐릭터 오브젝트, 모델
    /// </summary>
    public GameObject charObject = null;
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
    /// 몬스터가 가진 기본 스킬 인덱스 넘버
    /// <br> 0 은 스킬이 없는것 </br>
    /// </summary>
    public int basicSkillC = 0;
    /// <summary>
    /// 몬스터가 가진 궁극기 인덱스 넘버
    /// <br> 0 은 스킬이 없는것 </br>
    /// </summary>
    public int speialSkill = 0;
    public int rewardGold = 0;
    public int rewardSoul = 0;
    public int rewardExp = 0;
    public int[] rewardCard = null;
    public int[] rewardUnit = null;
    public int[] rewardRelic = null;

    public UnitTable(CharacterDatabase unit)
    {
        number = unit.number;
        charName = unit.charName;
        hp = unit.hp;
        maxHP = unit.maxHP;
        damage = unit.damage;
        charObject = unit.charObject;
        attackSpeed = unit.attackSpeed;
        moveSpeed = unit.moveSpeed;
        defense = unit.defense;
        attackType = unit.attackType;
        attackRange = unit.attackRange;
        attribute = unit.attribute;
        priRange = unit.priRange;
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

    public UnitTable(MonsterDatabase unit)
    {
        number = unit.number;
        monsterType = unit.monsterType;
        charName = unit.charName;
        hp = unit.hp;
        maxHP = unit.maxHP;
        damage = unit.damage;
        charObject = unit.charObject;
        attackSpeed = unit.attackSpeed;
        moveSpeed = unit.moveSpeed;
        defense = unit.defense;
        attackType = unit.attackType;
        attackRange = unit.attackRange;
        attribute = unit.attribute;
        priRange = unit.priRange;
        priorities = unit.priorities;
        positionPri = unit.positionPri;
        basicSkillA = unit.basicSkillA;
        basicSkillB = unit.basicSkillB;
        basicSkillC = unit.basicSkillC;
        speialSkill = unit.speialSkill;
        rewardGold = unit.rewardGold;
        rewardSoul = unit.rewardSoul;
        rewardExp = unit.rewardExp;
        rewardCard = unit.rewardCard;
        rewardUnit = unit.rewardUnit;
        rewardRelic = unit.rewardRelic;
    }

    public void GetUnitTable(CharacterDatabase unit)
    {
        unit.number = number;
        unit.charName = charName;
        unit.hp = hp;
        unit.maxHP = maxHP;
        unit.damage = damage;
        unit.charObject = charObject;
        unit.attackSpeed = attackSpeed;
        unit.moveSpeed = moveSpeed;
        unit.defense = defense;
        unit.attackType = attackType;
        unit.attackRange = attackRange;
        unit.attribute = attribute;
        unit.priRange = priRange;
        unit.priorities = priorities;
        unit.positionPri = positionPri;
        unit.unitClass = unitClass;
        unit.level = level;
        unit.exp = exp;
        unit.partySet = partySet;
        unit.islock = islock;
        unit.isLive = isLive;
        unit.basicSkill = basicSkillA;
        unit.speialSkill = speialSkill;
        unit.overlapValueA = overlapValueA;
        unit.overlapValueB = overlapValueB;
    }

    public void GetUnitTable(MonsterDatabase unit)
    {
        unit.number = number;
        unit.monsterType = monsterType;
        unit.charName = charName;
        unit.hp = hp;
        unit.maxHP = maxHP;
        unit.damage = damage;
        unit.charObject = charObject;
        unit.attackSpeed = attackSpeed;
        unit.moveSpeed = moveSpeed;
        unit.defense = defense;
        unit.attackType = attackType;
        unit.attackRange = attackRange;
        unit.attribute = attribute;
        unit.priRange = priRange;
        unit.priorities = priorities;
        unit.positionPri = positionPri;
        unit.basicSkillA = basicSkillA;
        unit.basicSkillB = basicSkillB;
        unit.basicSkillC = basicSkillC;
        unit.speialSkill = speialSkill;
        unit.rewardGold = rewardGold;
        unit.rewardSoul = rewardSoul;
        unit.rewardExp = rewardExp;
        unit.rewardCard = rewardCard;
        unit.rewardUnit = rewardUnit;
        unit.rewardRelic = rewardRelic;
    }
}
