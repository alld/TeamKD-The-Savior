using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ ��꿡 �ʿ��� ������ �Է¹ް�, ������ ������� ��ȯ�ϴ� ����
/// </summary>
public class DamageEngine : MonoBehaviour
{
    float a_Damage, d_defense, addDamage_attribute; // �⺻ ��갪
    float add_attDamageA, add_attDamageD; // �Ӽ� ��ȭ��ġ
    int a_attribute, d_attribute, sum_attribute; // �Ӽ� ����

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
        if (playercheck) // �����ڰ� �÷��̾��� �������� Ȯ��
        {
            // �Ӽ� ��ȯ üũ �� ������ ����
            if (DungeonOS.instance.weightAlly.Add_attributeCheck) a_attribute = DungeonOS.instance.weightAlly.Add_attribute;
            else a_attribute = DungeonOS.instance.partyUnit[attacker].attribute;
            if (DungeonOS.instance.weightEnemy.Add_attributeCheck) d_attribute = DungeonOS.instance.weightEnemy.Add_attribute;
            else d_attribute = DungeonOS.instance.monsterGroup[defender].attribute;
            add_attDamageA = DungeonOS.instance.weightAlly.Add_attributeVlaue[a_attribute];
            add_attDamageD = DungeonOS.instance.weightEnemy.Add_attributeVlaue[d_attribute];


            dmg += DungeonOS.instance.weightAlly.Add_damage;
            d_defense = DungeonOS.instance.monsterGroup[defender].defense;
            d_defense += DungeonOS.instance.weightEnemy.Add_defense;
        }
        else 
        {
            // �Ӽ� ��ȯ üũ �� ������ ���� 
            if (DungeonOS.instance.weightAlly.Add_attributeCheck) d_attribute = DungeonOS.instance.weightAlly.Add_attribute;
            else d_attribute = DungeonOS.instance.partyUnit[defender].attribute;
            if (DungeonOS.instance.weightEnemy.Add_attributeCheck) a_attribute = DungeonOS.instance.weightEnemy.Add_attribute;
            else a_attribute = DungeonOS.instance.monsterGroup[attacker].attribute;
            add_attDamageD = DungeonOS.instance.weightAlly.Add_attributeVlaue[d_attribute];
            add_attDamageA = DungeonOS.instance.weightEnemy.Add_attributeVlaue[a_attribute];


            dmg += DungeonOS.instance.weightEnemy.Add_damage;
            d_defense = DungeonOS.instance.partyUnit[defender].defense;
            d_defense += DungeonOS.instance.weightAlly.Add_defense;
        }
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
        // ������ ���̳ʽ� ���� 
        if(a_Damage < 0) return 0;
        else return a_Damage;
    }
    /// <summary>
    /// ī�峪 ���� ȿ���� �������� ���� ���
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns></returns>
    public float OnDamageCalculate(bool playercheck, bool notUnitCheck, float dmg, int attacker, int defender)
    {
        if (playercheck) // �����ڰ� �÷��̾��� �������� Ȯ��
        {
            // �Ӽ� ��ȯ üũ �� ������ ����
            if (DungeonOS.instance.weightAlly.Add_attributeCheck) a_attribute = DungeonOS.instance.weightAlly.Add_attribute;
            else a_attribute = DungeonOS.instance.partyUnit[attacker].attribute;
            if (DungeonOS.instance.weightEnemy.Add_attributeCheck) d_attribute = DungeonOS.instance.weightEnemy.Add_attribute;
            else d_attribute = DungeonOS.instance.monsterGroup[defender].attribute;
            add_attDamageA = DungeonOS.instance.weightAlly.Add_attributeVlaue[a_attribute];
            add_attDamageD = DungeonOS.instance.weightEnemy.Add_attributeVlaue[d_attribute];


            dmg += DungeonOS.instance.weightAlly.Add_damage;
            d_defense = DungeonOS.instance.monsterGroup[defender].defense;
            d_defense += DungeonOS.instance.weightEnemy.Add_defense;
        }
        else
        {
            // �Ӽ� ��ȯ üũ �� ������ ���� 
            if (DungeonOS.instance.weightAlly.Add_attributeCheck) d_attribute = DungeonOS.instance.weightAlly.Add_attribute;
            else d_attribute = DungeonOS.instance.partyUnit[defender].attribute;
            if (DungeonOS.instance.weightEnemy.Add_attributeCheck) a_attribute = DungeonOS.instance.weightEnemy.Add_attribute;
            else a_attribute = DungeonOS.instance.monsterGroup[attacker].attribute;
            add_attDamageD = DungeonOS.instance.weightAlly.Add_attributeVlaue[d_attribute];
            add_attDamageA = DungeonOS.instance.weightEnemy.Add_attributeVlaue[a_attribute];


            dmg += DungeonOS.instance.weightEnemy.Add_damage;
            d_defense = DungeonOS.instance.partyUnit[defender].defense;
            d_defense += DungeonOS.instance.weightAlly.Add_defense;
        }
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
        // ������ ���̳ʽ� ���� 
        if (a_Damage < 0) return 0;
        else return a_Damage;
    }

}
