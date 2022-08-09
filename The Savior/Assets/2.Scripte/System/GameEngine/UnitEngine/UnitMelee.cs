using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* ĳ���� ���ó���� ��� ���� ���� (����Ʈ ÷�� �Ұ����ϱ⶧���� �����������)
 * ĳ���� �߰��� �ش� ������Ʈ�� �����Ͽ� �������� �����. 
 * unitNumber,
 * 
 * 
 * 
 * 
 * 
 */


public class UnitMelee : MonoBehaviour
{
    public int unitNumber;
    private DamageEngine dmgEngine;
    
    private void Start()
    {
       dmgEngine = GetComponent<DamageEngine>();
    }

    /// <summary>
    /// ���� ����� �������� �Ϲ� ������ �ϴ� ��� 
    /// 
    /// </summary>
    public void OnAttack()
    {
        int tempAType = DungeonOS.instance.partyUnit[unitNumber].attackType;
        if(tempAType != 2)
        {
            
        }
        else if(tempAType == 2)
        {

        }
    
    }

    /// <summary>
    /// ���� �ްԵ� �� �������� ���
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PHYSICS"))
        {
            int Target = other.GetComponent<UnitMelee>().unitNumber;
            float damage;
            damage = DungeonOS.instance.partyUnit[unitNumber].meleDmg;
            OnDamage(damage, Target);
        }
    }

    /// <summary>
    /// ������ ������ �ʿ��� ���� ���޽�Ŵ
    /// </summary>
    private void OnDamage(float dmg, int TargetNumber)
    {
        dmgEngine.OnDamageTarget(0.1f);

    }
}
