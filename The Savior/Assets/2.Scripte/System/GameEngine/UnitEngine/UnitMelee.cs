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
    private UnitStateData targetUnitData;

    public GameObject TextPre;


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
        if (tempAType != 2)
        {
            unitInfo.attackTriggerBox.SetActive(true);
            unitInfo.targetUnit = GetComponent<UnitAI>().targetObj;

            // (�۾� �ʿ�)���� ���ݽ� ���� ���⿡ �޷��ִ� Ʈ���Ż��� Ȱ��ȭ
        }
        else
        {
            TempThrown = Instantiate(Resources.Load<GameObject>("Unit/TempThrown"));
            Instantiate(unitInfo.AttackEffect, TempThrown.transform);
            SceneManager.MoveGameObjectToScene(TempThrown, SceneManager.GetSceneByName(GameManager.instance.currentlyScene));
            TempThrown.transform.position = transform.position;
            UnitInfo tempInfo = TempThrown.GetComponent<UnitInfo>();
            tempInfo.partyNumber = partyNumber;
            tempInfo.unitNumber = unitNumber;
            tempInfo.playerUnit = unitInfo.playerUnit;
            tempInfo.targetUnit = GetComponent<UnitAI>().targetObj;
            tempInfo.damage = GetComponent<UnitStateData>().damage;
            tempInfo.thrown = true;
            TempThrown.GetComponent<ThrownObjMove>().thrownSpeed = 0.08f;
            TempThrown.GetComponent<ThrownObjMove>().targetPoint = GetComponent<UnitAI>().targetPoint;
            //else TempThrown.GetComponent<ThrownObjMove>().targetPoint = GetComponent<UnitAI>().targetPoint;
            StartCoroutine(DamageTransmission(TempThrown));
            // (�۾� �ʿ�)���Ÿ� ���ݽ� ����ü ������Ʈ ����
        }
    }

    IEnumerator DamageTransmission(GameObject obj)
    {
        targetUnitData = GetComponent<UnitAI>().targetObj;
        if (targetUnitData != null)
        {
            yield return new WaitForSeconds(Vector3.Distance(transform.position, targetUnitData.unitObj.transform.position) * 0.02f);
        }
        if (targetUnitData != null)
        {
            OnDamaged(unitdata.damage, targetUnitData.partyNumber);
        }
        Destroy(obj);
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
            GameObject textobj;
            float temp_shield;
            float temp_damage = DamageEngine.instance.OnProtectCalculate(unitdata.playerUnit, dmg, AttackerNumber, partyNumber, out temp_shield);
            //Debug.Log("�����������" + dmg  + "�����: " + temp_damage);
            unitdata.Current_protect -= temp_shield;
            unitdata.hp -= temp_damage;
            textobj = Instantiate(Resources.Load<GameObject>("Unit/UI/FloatText"));
            textobj.transform.position = transform.position + Vector3.up;
            textobj.GetComponent<TextMesh>().text = temp_damage.ToString();
            if (unitdata.hp <= 0)
            {
                GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                foreach (var item in DungeonOS.instance.partyUnit)
                {
                    item.GetComponent<UnitAI>().AITargetLiveCheck();
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    item.GetComponent<UnitAI>().AITargetLiveCheck();
                }
            }
        }
    }


    /// <summary>
    /// ������ ������ �����Ű�� �Լ� 
    /// <br></br><paramref name="dmg"/> : ���ݽ� ������ �⺻ ��������
    /// <br></br><paramref name="defenerNumber"/> : ���ݹ��� ����� �ѹ��� 
    /// <br></br>�����ͺ��̽��� �ƴ� dungeonOS���� ���� �׷찪�� �����ϱ����� �ѹ���
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="defenerNumber"></param>
    private void OnDamaged(float dmg, int defenerNumber)
    {
        UnitStateData targetData;
        if (GetComponent<UnitAI>().targetObj == null) return;
        if(GetComponent<UnitAI>().targetObj.playerUnit)
        {
            targetData = DungeonOS.instance.partyUnit[defenerNumber];
        }
        else
        {
            targetData = DungeonOS.instance.monsterGroup[defenerNumber];
        }
        if (targetData.isLive)
        {
            GameObject textobj;
            float temp_shield;
            float temp_damage = DamageEngine.instance.OnProtectCalculate(targetData.playerUnit, dmg, partyNumber, defenerNumber, out temp_shield);
            targetData.Current_protect -= temp_shield;
            targetData.hp -= temp_damage;
            textobj = Instantiate(Resources.Load<GameObject>("Unit/UI/FloatText"));
            textobj.transform.position = targetData.unitObj.transform.position + Vector3.up;
            textobj.GetComponent<TextMesh>().text = temp_damage.ToString();

            if (targetData.hp <= 0)
            {
                targetData.GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                foreach (var item in DungeonOS.instance.partyUnit)
                {
                    item.GetComponent<UnitAI>().AITargetLiveCheck();
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    item.GetComponent<UnitAI>().AITargetLiveCheck();
                }
            }
        }
    }
}
