using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill : MonoBehaviour
{
    public static UnitSkill instance = null;
    SkillDatabase tempSkill;
    void Awake()
    {
        instance = this;
    }


    public float Re_Colldown()
    {
        return tempSkill.cooldown;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"> :: ��ų ���� ��ȣ</param>
    /// <param name="isPlayer"> :: �÷��̾� ����</param>
    /// <param name="isPartyNumber"> :: ������ ����OS ��Ƽ �ѹ�</param>
    /// <param name="cooldown"> :: ��ų�� ��ٿ� (AI��ȯ��) </param>
    public void OnSkill(int num,bool isPlayer,int isPartyNumber, out float cooldown)
    {
        // �ӽÿ� ����Ƽ, �Ʊ� ��Ƽ �߰� �ؾ��� 
        tempSkill = new SkillDatabase(num);
        cooldown = Re_Colldown();
        switch (num)
        {
            case 0:
                break;
            default:
                break;

        }
    }


}
