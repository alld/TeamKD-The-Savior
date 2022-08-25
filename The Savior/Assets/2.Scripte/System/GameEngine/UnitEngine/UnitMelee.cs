using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public ParentInfo parentInfo;

    private UnitStateData unitdata;
    private UnitStateData attackerData;
    private GameObject attackerObj;
    private UnitInfo attackerInfo;


    private void Start()
    {
        unitInfo = GetComponent<UnitInfo>();
        unitdata = GetComponent<UnitStateData>();
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
            unitInfo.targetUnit = GetComponent<UnitAI>().targetObj;

            // (�۾� �ʿ�)���� ���ݽ� ���� ���⿡ �޷��ִ� Ʈ���Ż��� Ȱ��ȭ
        }
        else
        {
            TempThrown = Instantiate(Resources.Load<GameObject>("Unit/TempThrown"));
            SceneManager.MoveGameObjectToScene(TempThrown, SceneManager.GetSceneByName(GameManager.instance.currentlyScene));
            TempThrown.transform.position = transform.position;
            UnitInfo tempInfo = TempThrown.GetComponent<UnitInfo>();
            tempInfo.partyNumber = partyNumber;
            tempInfo.unitNumber = unitNumber;
            tempInfo.playerUnit = unitInfo.playerUnit;
            tempInfo.targetUnit = GetComponent<UnitAI>().targetObj;
            tempInfo.damage = GetComponent<UnitStateData>().damage;
            tempInfo.thrown = true;
            TempThrown.GetComponent<ThrownObjMove>().thrownSpeed = 0.2f;
            TempThrown.GetComponent<ThrownObjMove>().targetPoint = GetComponent<UnitAI>().targetPoint;

            Destroy(tempInfo, 3.0f);
            // (�۾� �ʿ�)���Ÿ� ���ݽ� ����ü ������Ʈ ����
        }
    }

    /// <summary>
    /// �� ������Ʈ�� ������ �ִ� ������ �ǰݵɽ� ��� ó�� �Լ�
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ParentInfo>(out parentInfo)) 
        {
            attackerObj = parentInfo.Info;
            attackerData = attackerObj.GetComponent<UnitStateData>();
            attackerInfo = attackerObj.GetComponent<UnitInfo>();
            if (!unitdata.isLive) return;
            if ((attackerInfo.playerUnit != unitInfo.playerUnit) && (attackerInfo.targetUnit.unitObj == unitdata.unitObj)) // ������ ����� ������� �˻�
            {
                //Debug.Log("�Ա�����ü");

                //GetComponent<UnitAI>().dele_attacked();
                int AttackerNumber = attackerInfo.partyNumber; //(�۾� �ʿ�)���Ͳ��� �����ʿ���
                
                if (attackerInfo.thrown)
                {
                  //  Debug.Log("����ü");

                    float damage = attackerInfo.damage;
                    OnDamage(damage, AttackerNumber);
                    Destroy(attackerObj);
                }
                else 
                {
                    float damage = attackerData.damage;
                    OnDamage(damage, AttackerNumber);
                    parentInfo.Info.GetComponent<UnitInfo>().attackTriggerBox.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// ����ü ���� ����ĳ��Ʈ�� ������ ������ ���� 
    /// </summary>
    /// <param name="other"></param>
    public bool ThrownTriggerCheck(Transform other)
    {
            attackerInfo = other.GetComponent<UnitInfo>();
            if (!unitdata.isLive) return false;
            if ((attackerInfo.playerUnit != unitInfo.playerUnit) && (attackerInfo.targetUnit.unitObj == unitdata.unitObj)) // ������ ����� ������� �˻�
            {
                //GetComponent<UnitAI>().dele_attacked();
                int AttackerNumber = attackerInfo.partyNumber; //(�۾� �ʿ�)���Ͳ��� �����ʿ���
                float damage = attackerInfo.damage;
                OnDamage(damage, AttackerNumber);
                return true;
            }
        return false;
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
        if (unitdata.isLive)
        {
            float temp_shield;
            float temp_damage = DamageEngine.instance.OnProtectCalculate(false, dmg, AttackerNumber, partyNumber, out temp_shield);
            //Debug.Log("�����������" + dmg  + "�����: " + temp_damage);
            unitdata.Current_protect -= temp_shield;
            unitdata.hp -= temp_damage;
            if (unitdata.hp <= 0)
            {
                if (unitdata.playerUnit) DungeonOS.instance.DieCount_Ally++;
                else DungeonOS.instance.DieCount_Enemy++;
                GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
            }
        }
    }
}
