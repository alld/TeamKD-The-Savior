using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    //회피 패턴, 도주 패턴, 자리 재선정
    //피격시 판단, 공격대상 재설정 (싸우고있는 대상, 아군이 피격됨)

    private UnitMelee unitMelee;
    enum UnitState { COMMON, ATTACK, ATTACK_MOVE, MOVE, SKILL, DIE, SPEIAL_SKILL }
    UnitState unitState;
    private WaitForSeconds stateCheckDelay = new WaitForSeconds(0.5f);
    private WaitForSeconds cooldownDelay = new WaitForSeconds(1.0f);
    /// <summary>
    /// 기능 : 인공지능 유닛을 구분하기위한 넘버, 해당 컴포넌트들을 이 값을 기준으로 기준넘버가 변경됨
    /// <br></br>방법 : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    public int unitNumber;

    // 상점에서도 같은 오브젝트를 사용하기때문에, Start사용시 예외처리 필수
    private void Start()
    {
        unitMelee = GetComponent<UnitMelee>();
    }

    public void OnStartAI()
    {
        unitState = UnitState.COMMON;
        
    }

    public void OnEndAI()
    {

    }

    /// <summary>
    /// 유닛이 자동으로 일반스킬을 쓰는 함수
    /// </summary>
    public void State_Skilㅣ()
    {
        unitState = UnitState.SKILL;
    }

    /// <summary>
    /// 유닛이 궁극기을 쓰는 함수
    /// </summary>
    public void State_SpeialSkilㅣ()
    {
        unitState = UnitState.SPEIAL_SKILL;
    }

    private IEnumerator StateCheck()
    {
        while (true)
        {
            yield return stateCheckDelay;
        }
    }


    /// <summary>
    /// 공격 속도(재공격), 스킬 쿨다운을 체크하는 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator CooldownCheck()
    {
        while (true)
        {

            yield return cooldownDelay;
        }
    }
    
    /// <summary>
    /// 유닛을 공격하게 하는 함수
    /// </summary>
    public void Sstate_Attack()
    {
        unitState = UnitState.ATTACK;
        unitMelee.OnAttack();
    }


    /// <summary>
    /// 유닛이 공격하기위해 이동하는 함수
    /// </summary>
    public void State_AttackMove()
    {
        unitState = UnitState.ATTACK_MOVE;
    }


    /// <summary>
    /// 유닛을 자동으로 움직이게 하는 함수
    /// </summary>
    public void State_Move()
    {
        unitState = UnitState.MOVE;

    }

    /// <summary>
    /// 유닛이 죽었을때 호출하는 함수
    /// </summary>
    public void State_Die()
    {
        DungeonOS.instance.partyUnit[unitNumber].isLive = false;
        unitState = UnitState.DIE;
    }


    /// <summary>
    /// 목표 대상 선정 함수 
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    private int EnemyTargetCheck(int A)
    {
        return 0;
    }
}
