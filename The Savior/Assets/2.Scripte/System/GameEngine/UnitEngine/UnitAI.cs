using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    //ȸ�� ����, ���� ����, �ڸ� �缱��
    //�ǰݽ� �Ǵ�, ���ݴ�� �缳�� (�ο���ִ� ���, �Ʊ��� �ǰݵ�)


    /// <summary>
    /// ������ �ڵ����� �Ϲݽ�ų�� ���� �Լ�
    /// </summary>
    public void State_Skil()
    {

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
       
    }


    /// <summary>
    /// ������ �����ϱ����� �̵��ϴ� �Լ�
    /// </summary>
    public void State_AttackMove()
    {
        
    }


    /// <summary>
    /// ������ �ڵ����� �����̰� �ϴ� �Լ�
    /// </summary>
    public void State_Move()
    {

        /* �̵���� ����
         * 
         * 
         * 
         * 
         * 
         */
    }

    /// <summary>
    /// ������ �׾����� ȣ���ϴ� �Լ�
    /// </summary>
    public void State_Die()
    {
        
    }
}
