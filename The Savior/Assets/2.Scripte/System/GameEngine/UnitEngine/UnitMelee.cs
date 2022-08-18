using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*(�۾� �ʿ�) ĳ���� ���ó���� ��� ���� ���� (����Ʈ ÷�� �Ұ����ϱ⶧���� �����������)
 * ĳ���� �߰��� �ش� ������Ʈ�� �����Ͽ� �������� �����. 
 * unitNumber,
 * 
 * 
 * 
 * 
 * 
 */

/// <summary>
/// ��� : �÷��̾� ������ �Ϲ� ���� ��� ������Ʈ
/// </summary>
public class UnitMelee : MonoBehaviour
{
    /// <summary>
    /// ��� : �ǰݵ� ��󿡴��� �����Ϳ� �����ϱ����� ����
    /// <br></br>��� : DungeonOS.instance.partySlot[<paramref name="partyNumber"/>]
    /// </summary>
    public int unitNumber;
    public int partyNumber;

    /// <summary>
    /// �� ������Ʈ�� ������ �ִ� ������ ���� �ൿ�� ����ϴ� �Լ�
    /// </summary>
    public void OnAttack()
    {
        int tempAType = DungeonOS.instance.partyUnit[partyNumber].attackType;
        if(tempAType != 2)
        {
            // (�۾� �ʿ�)���� ���ݽ� ���� ���⿡ �޷��ִ� Ʈ���Ż��� Ȱ��ȭ
        }
        else
        {
            // (�۾� �ʿ�)���Ÿ� ���ݽ� ����ü ������Ʈ ����
        }
    }

    /// <summary>
    /// �� ������Ʈ�� ������ �ִ� ������ �ǰݵɽ� ��� ó�� �Լ�
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PHYSICS")) // ������ ����� ������� �˻�
        {
            int Target = other.GetComponent<UnitMelee>().partyNumber; //(�۾� �ʿ�)���Ͳ��� �����ʿ���
            float damage;
            damage = DungeonOS.instance.partyUnit[partyNumber].damage;
            OnDamage(damage, Target);
            // ����ü �� Ʈ���� �ڽ� ����
            int tempAType = DungeonOS.instance.partyUnit[Target].attackType;
            if (tempAType != 2)
            {
                // (�۾� �ʿ�)���� ���� �ǰݽ� ���⿡ �޷��ִ� Ʈ���Ż��� ��Ȱ��ȭ
            }
            else
            {
                //(�۾� �ʿ�) ���Ÿ� ���ݽ� ����ü ��Ȱ��ȭ �ϴ��� ��Ʈ���� �ϴ���..
            }

        }
    }

    /// <summary>
    /// ������ ������ �����Ű�� �Լ� 
    /// <br></br><paramref name="dmg"/> : �ǰݵɽ� ������ �⺻ ��������
    /// <br></br><paramref name="AttackerNumber"/> : ������ ����� �ѹ��� 
    /// <br></br>�����ͺ��̽��� �ƴ� dungeonOS���� ���� �׷찪�� �����ϱ����� �ѹ���
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="AttackerNumber"></param>
    private void OnDamage(float dmg, int AttackerNumber)
    {
        float temp_shield;
        float temp_damage = DamageEngine.instance.OnProtectCalculate(false, dmg, AttackerNumber, partyNumber, out temp_shield);
        DungeonOS.instance.monsterGroup[partyNumber].Current_protect -= temp_shield;
        DungeonOS.instance.partyUnit[partyNumber].hp -= temp_damage;
        if (DungeonOS.instance.partyUnit[partyNumber].hp < 0)
        {
            GetComponent<UnitAI>().AutoScheduler(2,UnitAI.AIPattern.Death);            
        }
    }
}
