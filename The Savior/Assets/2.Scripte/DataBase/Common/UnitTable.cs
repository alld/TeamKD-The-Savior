using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTable : MonoBehaviour
{
    MonsterDatabase.MonsterType monsterType;
    public int number;
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
    /// ������ �ߺ����� A��(���)
    /// </summary>
    public int overlapValueA;
    /// <summary>
    /// ������ �ߺ����� B��(�ҿ�)
    /// </summary>
    public int overlapValueB;
    public int identity; // ĳ���� Ư��
    public int basicSkillA;
    /// <summary>
    /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int basicSkillB;
    /// <summary>
    /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int basicSkillC;
    /// <summary>
    /// ���Ͱ� ���� �ñر� �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int speialSkill;
    public int rewardGold;
    public int rewardSoul;
    public int rewardExp;
    public int[] rewardCard;
    public int[] rewardUnit;
    public int[] rewardRelic;

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
