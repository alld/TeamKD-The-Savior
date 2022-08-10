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
    
    private void Start()
    {
       dmgEngine = DungeonOS.instance.GetComponent<DamageEngine>();
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
    /// <br></br><paramref name="TargetNumber"/> : ������ ����� �ѹ��� 
    /// <br></br>�����ͺ��̽��� �ƴ� dungeonOS���� ���� �׷찪�� �����ϱ����� �ѹ���
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="TargetNumber"></param>
    private void OnDamage(float dmg, int TargetNumber)
    {
        DungeonOS.instance.partyUnit[unitNumber].hp -= dmgEngine.OnDamageCalculate(false, dmg, TargetNumber, unitNumber);
        if (DungeonOS.instance.partyUnit[unitNumber].hp < 0)
        {
            GetComponent<UnitAI>().State_Die();            
        }
    }
}
