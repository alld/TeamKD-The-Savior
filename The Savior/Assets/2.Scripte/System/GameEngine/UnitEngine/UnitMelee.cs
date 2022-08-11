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
    /// <br></br>��� : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    private int unitNumber;
    //ĳ�� ó��
    private DamageEngine dmgEngine;

    private void Start() // ���������� ���� ������Ʈ�� ����ϱ⶧����, Start���� ����ó�� �ʼ�
    {
        dmgEngine = DungeonOS.instance?.GetComponent<DamageEngine>();
       unitNumber = GetComponent<UnitAI>().unitNumber;
    }

    /// <summary>
    /// �� ������Ʈ�� ������ �ִ� ������ ���� �ൿ�� ����ϴ� �Լ�
    /// </summary>
    public void OnAttack()
    {
        int tempAType = DungeonOS.instance.partyUnit[unitNumber].attackType;
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
            int Target = other.GetComponent<UnitMelee>().unitNumber; //(�۾� �ʿ�)���Ͳ��� �����ʿ���
            float damage;
            damage = DungeonOS.instance.partyUnit[unitNumber].damage;
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
        float temp_damage = dmgEngine.OnProtectCalculate(false, dmg, AttackerNumber, unitNumber, out temp_shield);
        DungeonOS.instance.weightAllyUnit[unitNumber].Current_protect -= temp_shield;
        DungeonOS.instance.partyUnit[unitNumber].hp -= temp_damage;
        if (DungeonOS.instance.partyUnit[unitNumber].hp < 0)
        {
            GetComponent<UnitAI>().State_Die();            
        }
    }
}
