using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*(작업 필요) 캐릭터 사망처리시 사용 변수 변경 (리스트 첨삭 불가능하기때문에 변경해줘야함)
 * 캐릭터 추가시 해당 컴포넌트에 접근하여 설정값을 잡아줌. 
 * unitNumber,
 * 
 * 
 * 
 * 
 * 
 */

/// <summary>
/// 기능 : 플레이어 유닛의 일반 공격 기능 컴포넌트
/// </summary>
public class UnitMelee : MonoBehaviour
{

    /// <summary>
    /// 기능 : 피격된 대상에대한 데이터에 접근하기위한 변수
    /// <br></br>방법 : DungeonOS.instance.partySlot[<paramref name="partyNumber"/>]
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
    /// 이 컴포넌트를 가지고 있는 유닛의 공격 행동을 담당하는 함수
    /// </summary>
    public void OnAttack()
    {
        int tempAType = GetComponent<UnitStateData>().attackType;
        if (tempAType != 2)
        {
            unitInfo.attackTriggerBox.SetActive(true);
            unitInfo.targetUnit = GetComponent<UnitAI>().targetObj;

            // (작업 필요)근접 공격시 근접 무기에 달려있는 트리거상자 활성화
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
            // (작업 필요)원거리 공격시 투사체 오브젝트 생성
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
    /// 이 컴포넌트를 가지고 있는 유닛이 피격될시 결과 처리 함수
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ParentInfo>(out parentInfo)) 
        {
            attackerObj = parentInfo.Info;
            attackerData = attackerObj.GetComponent<UnitStateData>();
            attackerInfo = attackerObj.GetComponent<UnitInfo>();
            if (!unitdata.isLive) return;
            if ((attackerInfo.playerUnit != unitInfo.playerUnit) && (attackerInfo.targetUnit.unitObj == unitdata.unitObj)) // 데미지 계산의 대상인지 검사
            {
                //Debug.Log("입구투사체");

                //GetComponent<UnitAI>().dele_attacked();
                int AttackerNumber = attackerInfo.partyNumber; //(작업 필요)몬스터껄로 변경필요함
                
                if (attackerInfo.thrown)
                {
                  //  Debug.Log("투사체");

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
    /// 투사체 전용 레이캐스트로 전달한 데미지 피해 
    /// </summary>
    /// <param name="other"></param>
    public bool ThrownTriggerCheck(Transform other)
    {
            attackerInfo = other.GetComponent<UnitInfo>();
            if (!unitdata.isLive) return false;
            if ((attackerInfo.playerUnit != unitInfo.playerUnit) && (attackerInfo.targetUnit.unitObj == unitdata.unitObj)) // 데미지 계산의 대상인지 검사
            {
                //GetComponent<UnitAI>().dele_attacked();
                int AttackerNumber = attackerInfo.partyNumber; //(작업 필요)몬스터껄로 변경필요함
                float damage = attackerInfo.damage;
                OnDamage(damage, AttackerNumber);
                return true;
            }
        return false;
    }

    /// <summary>
    /// 데미지 엔진을 실행시키는 함수 
    /// <br></br><paramref name="dmg"/> : 피격될시 가해진 기본 데미지량
    /// <br></br><paramref name="AttackerNumber"/> : 공격한 대상의 넘버링 
    /// <br></br>데이터베이스가 아닌 dungeonOS에서 별도 그룹값에 접근하기위한 넘버링
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
            //Debug.Log("계산전데미지" + dmg  + "계산후: " + temp_damage);
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
    /// 데미지 엔진을 실행시키는 함수 
    /// <br></br><paramref name="dmg"/> : 공격시 가해진 기본 데미지량
    /// <br></br><paramref name="defenerNumber"/> : 공격받은 대상의 넘버링 
    /// <br></br>데이터베이스가 아닌 dungeonOS에서 별도 그룹값에 접근하기위한 넘버링
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
