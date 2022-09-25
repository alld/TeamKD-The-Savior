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

    public bool SkillCheck(int num)
    {
        bool check = false;

        switch (num)
        {
            case 1:
                if (DungeonOS.instance.DieCount_Ally == 0)
                {
                    check = true;
                }
                break;
            case 2:
            case 3: // 도발 :: 주변 유닛 체크 확인 검토;
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
    /// <param name="num"> :: 스킬 고유 번호</param>
    /// <param name="cooldown"> :: 스킬의 쿨다운 (AI반환용) </param>
    public bool OnSkill(int num, out float cooldown)
    {
        //targetUnitN = targetUnitTempN; 대상 탐색을 여기서 해야함. 
        // 임시용 적파티, 아군 파티 추가 해야함 
        tempSkill = new SkillDatabase(num);
        unit = unitAI.unit;
        cooldown = tempSkill.cooldown; // 초기화
        switch (num)
        {
            case 0: 
                unitAI.AttackTargetSearch(out targetUnit);
                SkillUse = true;
                GameObject.Find("Model").transform.localPosition = Vector3.zero; // 애니메이션 반복실행시 뛰쳐나가는거 재조정
                break;
            case 1: // 부활 스킬
                if (SkillCheck(num))
                {
                    unitAI.AttackTargetSearch(out targetUnit);
                    SkillUse = true;
                    GameObject.Find("Model").transform.localPosition = Vector3.zero; // 애니메이션 반복실행시 뛰쳐나가는거 재조정
                }
                break;
            case 2: // 음유시인 스킬 
                break;
            case 3: // 탱커 스킬
                break;
            case 4: // 궁수 스킬 
                break;
            default:
                SkillUse = false;
                break;
        }
        Debug.Log(SkillUse);
        return SkillUse;
    }

    

}
