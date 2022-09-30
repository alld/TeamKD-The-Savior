using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill : MonoBehaviour
{
    public bool SkillUse = false;
    public UnitStateData unit;
    public UnitStateData targetUnit;
    public UnitAI unitAI;
    SkillDatabase tempSkill;

    public WaitForSeconds delay_01 = new WaitForSeconds(0.1f);
    public WaitForSeconds delay_10 = new WaitForSeconds(1.0f);
    

    private void Start()
    {
        unitAI = GetComponent<UnitAI>();
    }

    public bool SkillCheck(int num)
    {
        bool check = false;

        switch (num)
        {
            case 1:
                if (DungeonOS.instance.DieCount_Ally == 0)
                {
                    if (unit.isLive)
                    {
                        check = true;
                    }
                    else check = false;
                }
                break;
            case 2:
            case 3: // ���� :: �ֺ� ���� üũ Ȯ�� ����;
            case 4:
                check = true;
                break;
            default:
                check = false;
                break;
        }

        return check;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"> :: ��ų ���� ��ȣ</param>
    /// <param name="cooldown"> :: ��ų�� ��ٿ� (AI��ȯ��) </param>
    public bool OnSkill(int num, out float cooldown)
    {
        //targetUnitN = targetUnitTempN; ��� Ž���� ���⼭ �ؾ���. 
        // �ӽÿ� ����Ƽ, �Ʊ� ��Ƽ �߰� �ؾ��� 
        tempSkill = new SkillDatabase(num);
        unit = unitAI.unit;
        cooldown = tempSkill.cooldown; // �ʱ�ȭ
        switch (num)
        {
            case 0: 
                unitAI.AttackTargetSearch(out targetUnit);
                SkillUse = true;
                GameObject.Find("Model").transform.localPosition = Vector3.zero; // �ִϸ��̼� �ݺ������ ���ĳ����°� ������
                break;
            case 1: // ��Ȱ ��ų
                if (SkillCheck(num))
                {
                    unitAI.AttackTargetSearch(out targetUnit);
                    SkillUse = true;
                    GameObject.Find("Model").transform.localPosition = Vector3.zero; // �ִϸ��̼� �ݺ������ ���ĳ����°� ������
                    StartCoroutine(Skill_Resurrection());
                }
                break;
            case 2: // �������� ��ų 
                break;
            case 3: // ��Ŀ ��ų
                break;
            case 4: // �ü� ��ų 
                break;
            default:
                SkillUse = false;
                break;
        }
        Debug.Log(SkillUse);
        return SkillUse;
    }

    

    IEnumerator Skill_Resurrection()
    {
        yield return delay_10;
        yield return delay_10;
        yield return delay_10;
        if (SkillCheck(1))
        {
            unitAI.Resurrection(0);
        }
    }

}
