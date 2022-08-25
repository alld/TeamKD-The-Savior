using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ ��꿡 �ʿ��� ������ �Է¹ް�, ������ ������� ��ȯ�ϴ� ����
/// </summary>
public class DamageEngine : MonoBehaviour
{
    float a_Damage, d_defense, addDamage_attribute, add_DropDamage, add_finalDamage; // �⺻ ��갪
    float add_attDamageA, add_attDamageD; // �Ӽ� ��ȭ��ġ
    int a_attribute, d_attribute, sum_attribute; // �Ӽ� ����
    List<UnitStateData> AllyUnit, EnemyUnit;



    public static DamageEngine instance = null;

    private void Start()
    {
        instance = this;
    }


    /// <summary>
    /// �������� ���ݴ��� �ǰݴ���� �Է��ϸ� ����ġ�� �Ӽ����� ���ؼ� ������ ����� �ݿ��ϴ� ���.
    /// <br></br>
    /// <br></br><paramref name="playercheck"/> : �����͸� �����ؿ��°��� enemy, monsterGroup�� ally, partyUnit �̴ٸ��⶧���� ������ �ʿ���.
    /// <br></br><paramref name="dmg"/> : �������� �⺻��
    /// <br></br><paramref name="attacker"/> : ������ ����� �������� �׷� ���� �ѹ���
    /// <br></br><paramref name="defender"/> : �ǰݵ� ����� �������� �׷� ���� �ѹ���
    /// </summary>
    /// <param name="playercheck"> �÷��̾ ���������� üũ�ϴ� ����</param>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns></returns>
    public float OnDamageCalculate(bool playercheck, float dmg, int attacker, int defender)
    {
        if (playercheck)
        {
            //AllyUnit = DungeonOS.instance.partyUnit;
            AllyUnit = DungeonOS.instance.partyUnit;
            EnemyUnit = DungeonOS.instance.monsterGroup;
        }
        else
        {
            //AllyUnit = DungeonOS.instance.monsterGroup;
            AllyUnit = DungeonOS.instance.partyUnit;
            EnemyUnit = DungeonOS.instance.partyUnit;
        }


        if (EnemyUnit[defender].isinvincible) return 0;
        // �Ӽ� ��ȯ üũ �� ������ ����
        if (AllyUnit[attacker].Add_attributeCheck) a_attribute = AllyUnit[attacker].Add_attribute;
        else a_attribute = AllyUnit[attacker].attribute;
        if (EnemyUnit[defender].Add_attributeCheck) d_attribute = EnemyUnit[defender].Add_attribute;
        else d_attribute = EnemyUnit[defender].attribute;
        add_attDamageA = AllyUnit[attacker].Add_attributeVlaue[a_attribute];
        add_attDamageD = EnemyUnit[defender].Add_attributeVlaue[d_attribute];


        dmg += AllyUnit[attacker].Add_damage;
        d_defense = EnemyUnit[defender].defense;
        d_defense += EnemyUnit[defender].Add_defense;
        add_finalDamage = AllyUnit[attacker].Add_fianlDamage + AllyUnit[attacker].Add_fianlDamage;
        add_DropDamage = EnemyUnit[defender].Add_dropDamage + EnemyUnit[defender].Add_dropDamage;


        sum_attribute = (a_attribute * 10) + d_attribute;
        switch (sum_attribute) // �Ӽ� ������ ���� �����ϴ� ���
        {
            // ��1 ��2 Ǯ3 
            // ���ڸ��� �������� �Ӽ�, ���ڸ��� ������� �Ӽ�
            case 0:
            case 11:
            case 22:
            case 33:
                addDamage_attribute = 1;
                break;
            case 13:
            case 21:
            case 32:
                addDamage_attribute = 1.5f + (add_attDamageA - add_attDamageD);
                break;
            case 12:
            case 23:
            case 31:
                addDamage_attribute = 0.5f + (add_attDamageA - add_attDamageD);
                break;
            default:
                DungeonOS.instance.GameError("������ ���� : ���� �Ӽ����� �����Ǿ����ϴ�.");
                break;
        }
        // ���� ������ ���
        a_Damage = (dmg - d_defense) * addDamage_attribute;
        a_Damage = a_Damage * add_finalDamage * add_DropDamage;
        // ������ ���̳ʽ� ���� 
        if (a_Damage < 0) return 0;
        else return a_Damage;
    }
    /// <summary>
    /// ��ȣ������ ���ظ� �ٰ�� 
    /// </summary>
    /// <param name="playercheck"></param>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <param name="shieldDamage"></param>
    /// <returns></returns>
    public float OnProtectCalculate(bool playercheck, float dmg, int attacker, int defender, out float shieldDamage)
    {
        float a_Damage = dmg;
        shieldDamage = 0;
        float temp_shield;
        Debug.Log(attacker + "aa" + defender + "dd");
        foreach (var item in DungeonOS.instance.monsterGroup)
        {
            Debug.Log("����" + item);
        }
        if (DungeonOS.instance.monsterGroup.Count == 0) return 0; // ������ ���� ó��
        if (playercheck) temp_shield = DungeonOS.instance.partyUnit[defender].Current_protect;
        else temp_shield = DungeonOS.instance.monsterGroup[defender].Current_protect;

        if (dmg <= temp_shield)
        {
            shieldDamage = dmg;
            a_Damage = 0;
        }
        else
        {
            shieldDamage = temp_shield;
            a_Damage = dmg - temp_shield;
        }

        return OnDamageCalculate(playercheck, a_Damage, attacker, defender);
    }
    /// <summary>
    /// ī�峪 ���� ȿ���� �������� ���� ���
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns></returns>
    public float OnDamageCalculate(bool playercheck, bool notUnitCheck, float dmg, int defender)
    {
        return 0.0f;
    }
    public float OnDamageCalculate(bool playercheck, bool notUnitCheck, float dmg, int defender, out float shieldDamage)
    {
        float a_Damage = dmg;
        shieldDamage = 0;
        float temp_shield;
        if (playercheck) temp_shield = DungeonOS.instance.partyUnit[defender].Current_protect;
        else temp_shield = DungeonOS.instance.monsterGroup[defender].Current_protect;

        if (dmg <= temp_shield)
        {
            shieldDamage = dmg;
            a_Damage = 0;
        }
        else
        {
            shieldDamage = temp_shield;
            a_Damage = dmg - temp_shield;
        }
        return OnDamageCalculate(playercheck, true, a_Damage, defender);
    }
}
