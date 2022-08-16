using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    //캐시 처리

    private void Start() // 상점에서도 같은 오브젝트를 사용하기때문에, Start사용시 예외처리 필수
    {
        unitNumber = GetComponent<UnitAI>().unitNumber;
        partyNumber = GetComponent<UnitAI>().partyNumber;
    }

    /// <summary>
    /// 이 컴포넌트를 가지고 있는 유닛의 공격 행동을 담당하는 함수
    /// </summary>
    public void OnAttack()
    {
        int tempAType = DungeonOS.instance.partyUnit[partyNumber].attackType;
        if(tempAType != 2)
        {
            // (작업 필요)근접 공격시 근접 무기에 달려있는 트리거상자 활성화
        }
        else
        {
            // (작업 필요)원거리 공격시 투사체 오브젝트 생성
        }
    }

    /// <summary>
    /// 이 컴포넌트를 가지고 있는 유닛이 피격될시 결과 처리 함수
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PHYSICS")) // 데미지 계산의 대상인지 검사
        {
            int Target = other.GetComponent<UnitMelee>().partyNumber; //(작업 필요)몬스터껄로 변경필요함
            float damage;
            damage = DungeonOS.instance.partyUnit[partyNumber].damage;
            OnDamage(damage, Target);
            // 투사체 및 트리거 박스 설정
            int tempAType = DungeonOS.instance.partyUnit[Target].attackType;
            if (tempAType != 2)
            {
                // (작업 필요)근접 공격 피격시 무기에 달려있는 트리거상자 비활성화
            }
            else
            {
                //(작업 필요) 원거리 공격시 투사체 비활성화 하던가 디스트로이 하던가..
            }

        }
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
        float temp_shield;
        float temp_damage = DamageEngine.instance.OnProtectCalculate(false, dmg, AttackerNumber, partyNumber, out temp_shield);
        DungeonOS.instance.weightAllyUnit[partyNumber].Current_protect -= temp_shield;
        DungeonOS.instance.partyUnit[partyNumber].hp -= temp_damage;
        if (DungeonOS.instance.partyUnit[partyNumber].hp < 0)
        {
            GetComponent<UnitAI>().AutoScheduler(2,UnitAI.AIPattern.Death);            
        }
    }
}
