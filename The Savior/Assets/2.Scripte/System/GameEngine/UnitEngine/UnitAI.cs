using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    //ȸ�� ����, ���� ����, �ڸ� �缱��
    //�ǰݽ� �Ǵ�, ���ݴ�� �缳�� (�ο���ִ� ���, �Ʊ��� �ǰݵ�)

    private UnitMelee unitMelee;
    enum UnitState { COMMON, ATTACK, ATTACK_MOVE, MOVE, SKILL, DIE, SPEIAL_SKILL }
    UnitState unitState;
    private WaitForSeconds stateCheckDelay = new WaitForSeconds(0.5f);
    private WaitForSeconds cooldownDelay = new WaitForSeconds(1.0f);
    /// <summary>
    /// ��� : �ΰ����� ������ �����ϱ����� �ѹ�, �ش� ������Ʈ���� �� ���� �������� ���سѹ��� �����
    /// <br></br>��� : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    public int unitNumber;
    public int partyNumber;

    // ���������� ���� ������Ʈ�� ����ϱ⶧����, Start���� ����ó�� �ʼ�
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
    /// ������ �ڵ����� �Ϲݽ�ų�� ���� �Լ�
    /// </summary>
    public void State_Skil��()
    {
        unitState = UnitState.SKILL;
    }

    /// <summary>
    /// ������ �ñر��� ���� �Լ�
    /// </summary>
    public void State_SpeialSkil��()
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
    /// ���� �ӵ�(�����), ��ų ��ٿ��� üũ�ϴ� �Լ�
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
    /// ������ �����ϰ� �ϴ� �Լ�
    /// </summary>
    public void Sstate_Attack()
    {
        unitState = UnitState.ATTACK;
        unitMelee.OnAttack();
    }


    /// <summary>
    /// ������ �����ϱ����� �̵��ϴ� �Լ�
    /// </summary>
    public void State_AttackMove()
    {
        unitState = UnitState.ATTACK_MOVE;
    }


    /// <summary>
    /// ������ �ڵ����� �����̰� �ϴ� �Լ�
    /// </summary>
    public void State_Move()
    {
        unitState = UnitState.MOVE;

    }

    /// <summary>
    /// ������ �׾����� ȣ���ϴ� �Լ�
    /// </summary>
    public void State_Die()
    {
        DungeonOS.instance.partyUnit[unitNumber].isLive = false;
        unitState = UnitState.DIE;
    }


    /// <summary>
    /// ��ǥ ��� ���� �Լ� 
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    private int EnemyTargetCheck(int A)
    {
        return 0;
    }
}
