using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 캐릭터 사망처리시 사용 변수 변경 (리스트 첨삭 불가능하기때문에 변경해줘야함)
 * 캐릭터 추가시 해당 컴포넌트에 접근하여 설정값을 잡아줌. 
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
    /// 공격 명령이 내려지면 일반 공격을 하는 기능 
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
    /// 공격 받게될 시 내려지는 기능
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
    /// 데미지 엔진에 필요한 값을 전달시킴
    /// </summary>
    private void OnDamage(float dmg, int TargetNumber)
    {
        dmgEngine.OnDamageTarget(0.1f);

    }
}
