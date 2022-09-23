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


    private void Start()
    {
        unitAI = GetComponent<UnitAI>();
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
            default:
                SkillUse = false;
                break;

        }
        Debug.Log(SkillUse);
        return SkillUse;
    }

    

}
