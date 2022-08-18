using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataTest : MonoBehaviour
{

    #region ���� ������
    public bool playerUnit;
    MonsterDatabase.Data.MonsterType monsterType;
    public int number = 0;
    /// <summary>
    /// ĳ���� �⺻ �̸�
    /// </summary>
    public string charName = "";
    /// <summary>
    /// ĳ���� �⺻ ü��
    /// </summary>
    public float hp = 0;
    /// <summary>
    /// ĳ���� �ִ� ü��
    /// </summary>
    public float maxHP = 0;
    /// <summary>
    /// ĳ���� �⺻ ������
    /// </summary>
    public float damage = 0;
    /// <summary>
    /// ĳ���� �⺻ ���ݼӵ�
    /// <br>1.0f = 100%</br>
    /// </summary>
    public float attackSpeed = 0;
    /// <summary>
    /// ĳ���� �⺻ �̵��ӵ�
    /// <br>1.0f = 100%</br>
    /// </summary>
    public float moveSpeed = 0;
    /// <summary>
    /// ĳ���� �⺻ ����
    /// </summary>
    public float defense = 0;
    /// <summary>
    /// ĳ���� �⺻ ����Ÿ��
    /// <br>1. �ٰŸ�</br>
    /// <br>2. ���Ÿ�</br>
    /// <br>3. ��Ŀ</br>
    /// </summary>
    public int attackType = 0;
    /// <summary>
    /// ĳ���� �⺻ ���� ����
    /// </summary>
    public float attackRange = 0;
    /// <summary>
    /// ĳ���� �⺻ �Ӽ�
    /// <br>0. �� �Ӽ�</br>
    /// <br>1. �� �Ӽ�</br>
    /// <br>2. �� �Ӽ�</br>
    /// <br>3. Ǯ �Ӽ�</br>
    /// </summary>
    public int attribute = 0;
    /// <summary>
    /// ĳ���� �ν� ���� : ���� ����� �ν��ϴ� ����
    /// </summary>
    public float priRange = 0;
    /// <summary>
    /// ĳ���� �켱�� : ���� �ν� �켱��ġ
    /// </summary>
    public int priorities = 0;
    /// <summary>
    /// ���� �켱�� : ��� �ν� ����
    /// </summary>
    public float defRange;
    /// <summary>
    /// ĳ���� �⺻ �ڸ� �켱�� : �ڸ� ��ġ
    /// </summary>
    public int positionPri = 0;
    /// <summary>
    /// [saveData] ĳ���� Ŭ����
    /// </summary>
    public int unitClass = 0;
    /// <summary>
    /// [saveData] ĳ���� �⺻ ��� 
    /// <br>1~3 �ܰ�� ����</br>
    /// </summary>
    public int level = 0;
    /// <summary>
    /// [saveData] ĳ���� ���� �ҿ� 
    /// </summary>
    public int exp = 0;
    /// <summary>
    /// ĳ���� ��Ƽ ���� ���� 
    /// <para>���̺� ���� ���� </para>
    /// </summary>
    public bool partySet = false;
    /// <summary>
    /// ĳ���� �ر� ���� 
    /// <br> ��������Ʈ �������� �����Ұ��̶�, �رݿ��� �������ʿ����.</br>
    /// </summary>
    public bool islock = false;
    /// <summary>
    /// ĳ���� ���� ���� 
    /// </summary>
    public bool isLive = false;
    /// <summary>
    /// ������ �ߺ����� A��(���)
    /// </summary>
    public int overlapValueA = 0;
    /// <summary>
    /// ������ �ߺ����� B��(�ҿ�)
    /// </summary>
    public int overlapValueB = 0;
    public int identity = 0; // ĳ���� Ư��
    public int basicSkillA = 0;
    /// <summary>
    /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int basicSkillB = 0;
    /// <summary>
    /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int basicSkillC = 0;
    /// <summary>
    /// ���Ͱ� ���� �ñر� �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int speialSkill = 0;
    public int rewardGold = 0;
    public int rewardSoul = 0;
    public int rewardExp = 0;
    public List<int> rewardCard = new List<int>();
    public List<int> rewardUnit = new List<int>();
    public List<int> rewardRelic = new List<int>();
    #endregion

    public void DataSetting(bool playerCheck,int num)
    {
        playerUnit = playerCheck;
        if (playerCheck)
        {
            CharacterDatabase.Data unit = new CharacterDatabase.Data(num);
            number = unit.number;
            charName = unit.charName;
            hp = unit.hp;
            maxHP = unit.maxHP;
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
            MonsterDatabase.Data unit = new MonsterDatabase.Data(num);
            number = unit.number;
            monsterType = unit.monsterType;
            charName = unit.charName;
            hp = unit.hp;
            maxHP = unit.maxHP;
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
            basicSkillC = unit.basicSkillC;
            speialSkill = unit.speialSkill;
            rewardGold = unit.rewardGold;
            rewardSoul = unit.rewardSoul;
            rewardExp = unit.rewardExp;
            rewardCard = unit.rewardCard;
            rewardUnit = unit.rewardUnit;
            rewardRelic = unit.rewardRelic;
        }
    }

    public void StagePositionReset()
    {

    }

}