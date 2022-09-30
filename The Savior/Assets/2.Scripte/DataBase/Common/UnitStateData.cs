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
    public float defRange = 1;
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
    /// ���Ͱ� ���� �ñر� �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int speialSkill = 0;
    public int rewardGold = 0;
    public int rewardSoul = 0;
    public List<int> rewardCard = new List<int>();
    public List<int> rewardUnit = new List<int>();
    public List<int> rewardRelic = new List<int>();
    public GameObject unitObj;
    public GameObject unitPrefabs;

    #endregion
    #region ���� ����ġ
    // �Ʊ�
    /// <summary>
    /// ����ġ :: �߰� ���ݷ�
    /// </summary>
    public float Add_damage = 0;
    /// <summary>
    /// ����ġ :: ���� �߰� ���ݷ�
    /// </summary>
    public float Add_fianlDamage = 1;
    /// <summary>
    /// ����ġ :: ���ط� ���ҷ�
    /// </summary>
    public float Add_dropDamage = 1;
    /// <summary>
    /// ����ġ(�߰��ɷ�) :: ��ȣ�� ��ġ
    /// </summary>
    public float Current_protect = 0;
    /// <summary>
    /// ����ġ(�߰��ɷ�) :: ��ȣ�� �ִ� ��ġ
    /// </summary>
    public float Current_protectMax = 0;
    /// <summary>
    /// ����ġ :: ���� �ӵ�
    /// </summary>
    public float Add_attackSpeed = 1;
    /// <summary>
    /// ����ġ :: �̵� �ӵ�
    /// </summary>
    public float Add_moveSpeed = 1;
    /// <summary>
    /// ����ġ :: ����
    /// </summary>
    public float Add_defense = 0;
    /// <summary>
    /// ����ġ :: ���� ����(��Ÿ�)
    /// </summary>
    public float Add_attackRange = 1;
    /// <summary>
    /// ����ġ :: �Ӽ� ���� ���� (bool)
    /// </summary>
    public bool Add_attributeCheck = false;
    /// <summary>
    /// ����ġ :: ����� �Ӽ��� 
    /// </summary>
    public int Add_attribute = 0;
    /// <summary>
    /// ����ġ :: �Ӽ� �߰� ������
    /// </summary>
    public float[] Add_attributeVlaue = { 1, 1, 1, 1 };
    /// <summary>
    /// ����ġ :: ���� �ν� ����
    /// </summary>
    public float Add_priRange = 1;
    /// <summary>
    /// ����ġ :: ���� �켱��
    /// </summary>
    public int Add_priorities = 0;
    /// <summary>
    /// ����ġ :: ��ų ��ٿ�
    /// </summary>
    public float Add_skilcoldown = 1;
    /// <summary>
    /// ����ġ :: ���� ����
    /// </summary>
    public bool isinvincible = false;
    public List<BuffDataBase> Current_buff = new List<BuffDataBase>();
    //��
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
            unitPrefabs = unit.charObject;
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
            unitPrefabs = unit.charObject;
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
        if (playerUnit)
        {
            partySlotHPGauage = DungeonController.instance.partySlotHPGauage[partyNumber];
            unitHPGauage = Instantiate(Resources.Load<Image>("Unit/UI/HpGauageG"), canvas.transform);
        }
        else
        {
            unitHPGauage = Instantiate(Resources.Load<Image>("Unit/UI/HpGauageR"), canvas.transform);
        }
        HPUIMove();
    }

    public void StagePositionReset()
    {

    }


    public void HPUI()
    {
        if (!UISettingCheck) UISetting();
        float temp = hp / maxHP;
        unitHPGauage.fillAmount = temp;
        if (playerUnit) partySlotHPGauage.fillAmount = temp;
        if (hp <= 0) unitHPGauage.gameObject.SetActive(false);
        else if (hp > 1) unitHPGauage.gameObject.SetActive(true);
        // ��Ƽ���� ���� UI ����/ ���ʹ� ���� UI �׷� ���� 
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
    /* ü�°����� UI ����
     * �ǰ� UI�� ���� (������ �����ؽ�Ʈ)
     * 
     * �����ͺ��̽� ���� �� ����
     * ����ġ ���� Ȯ��
     * ������ ī�� ���� Ȯ��
     * ������ ���� ���� Ȯ��
     * ���� ��� ����� Ȯ��
     * 
     * 
     */

}

