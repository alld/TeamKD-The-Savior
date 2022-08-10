using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    //ȸ�� ����, ���� ����, �ڸ� �缱��
    //�ǰݽ� �Ǵ�, ���ݴ�� �缳�� (�ο���ִ� ���, �Ʊ��� �ǰݵ�)

    enum UnitState { COMMON, ATTACK, ATTACK_MOVE, MOVE, SKILL, DIE, SPEIAL_SKILL }
    UnitState unitState;
    /// <summary>
    /// ��� : �ΰ����� ������ �����ϱ����� �ѹ�, �ش� ������Ʈ���� �� ���� �������� ���سѹ��� �����
    /// <br></br>��� : DungeonOS.instance.partySlot[<paramref name="unitNumber"/>]
    /// </summary>
    public int unitNumber;


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

    /// <summary>
    /// ���� �ӵ�(�����), ��ų ��ٿ��� üũ�ϴ� �Լ�
    /// ���� �������� üũ 
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
    /// ������ �����ϰ� �ϴ� �Լ�
    /// </summary>
    public void Sstate_Attack()
    {
        unitState = UnitState.ATTACK;
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
