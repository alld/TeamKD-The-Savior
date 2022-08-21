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
    UnitInfo unitInfo;
    public GameObject TempThrown;

    private void Start()
    {
        unitInfo = GetComponent<UnitInfo>();
    }

    /// <summary>
    /// �� ������Ʈ�� ������ �ִ� ������ ���� �ൿ�� ����ϴ� �Լ�
    /// </summary>
    public void OnAttack()
    {
        int tempAType = GetComponent<UnitStateData>().attackType;
        if(tempAType != 2)
        {
            unitInfo.attackTriggerBox.SetActive(true);
            // (�۾� �ʿ�)���� ���ݽ� ���� ���⿡ �޷��ִ� Ʈ���Ż��� Ȱ��ȭ
        }
        else
        {
            TempThrown = Instantiate(Resources.Load<GameObject>("Unit/TempThrown"));
            TempThrown.transform.position = transform.position;
            TempThrown.GetComponent<UnitInfo>().partyNumber = partyNumber;
            TempThrown.GetComponent<UnitInfo>().unitNumber = unitNumber;
            TempThrown.GetComponent<UnitInfo>().playerUnit = unitInfo.playerUnit;
            TempThrown.GetComponent<UnitInfo>().thrown = true;
            TempThrown.GetComponent<ThrownObjMove>().thrownSpeed = 0.2f;
            TempThrown.GetComponent<ThrownObjMove>().targetPoint = GetComponent<UnitAI>().targetPoint;
            // (�۾� �ʿ�)���Ÿ� ���ݽ� ����ü ������Ʈ ����
            Debug.Log("������ ����");
        }
    }

    /// <summary>
    /// �� ������Ʈ�� ������ �ִ� ������ �ǰݵɽ� ��� ó�� �Լ�
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("������ �۵�");
        if (other.CompareTag("PHYSICS") && other.GetComponent<UnitInfo>().partyNumber == partyNumber && other.GetComponent<UnitInfo>().playerUnit != GetComponent<UnitInfo>().playerUnit) // ������ ����� ������� �˻�
        {
            other.GetComponent<UnitAI>().dele_attacked();
            int Target = other.GetComponent<UnitMelee>().partyNumber; //(�۾� �ʿ�)���Ͳ��� �����ʿ���
            float damage;
            damage = DungeonOS.instance.partyUnit[partyNumber].damage;
            OnDamage(damage, Target);
            // ����ü �� Ʈ���� �ڽ� ����
            int tempAType = DungeonOS.instance.partyUnit[Target].attackType;
            if (tempAType != 2)
            {
                other.GetComponent<UnitInfo>().attackTriggerBox.SetActive(false);
                // (�۾� �ʿ�)���� ���� �ǰݽ� ���⿡ �޷��ִ� Ʈ���Ż��� ��Ȱ��ȭ
            }
            else
            {
                Destroy(other.gameObject);
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
