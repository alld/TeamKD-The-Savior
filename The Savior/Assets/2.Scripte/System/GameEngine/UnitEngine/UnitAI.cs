using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    //회피 패턴, 도주 패턴, 자리 재선정
    //피격시 판단, 공격대상 재설정 (싸우고있는 대상, 아군이 피격됨)


    /// <summary>
    /// 유닛이 자동으로 일반스킬을 쓰는 함수
    /// </summary>
    public void State_Skilㅣ()
    {
        
    }

    /// <summary>
    /// 공격 속도(재공격), 스킬 쿨다운을 체크하는 함수
    /// 상태 변동시점 체크 
    /// </summary>
    /// <returns></returns>
    public IEnumerator CooldownCheck()
    {
        while (true)
        {

            yield return null;
            
        }
    }
    
    /// <summary>
    /// 유닛을 공격하게 하는 함수
    /// </summary>
    public void Sstate_Attack()
    {
       
    }


    /// <summary>
    /// 유닛이 공격하기위해 이동하는 함수
    /// </summary>
    public void State_AttackMove()
    {
        
    }


    /// <summary>
    /// 유닛을 자동으로 움직이게 하는 함수
    /// </summary>
    public void State_Move()
    {

    }

    /// <summary>
    /// 유닛이 죽었을때 호출하는 함수
    /// </summary>
    public void State_Die()
    {
        
    }
}
