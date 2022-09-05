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
    /// <param name="num"> :: 스킬 고유 번호</param>
    /// <param name="isPlayer"> :: 플레이어 유무</param>
    /// <param name="isPartyNumber"> :: 유닛의 던전OS 파티 넘버</param>
    /// <param name="cooldown"> :: 스킬의 쿨다운 (AI반환용) </param>
    public void OnSkill(int num,bool isPlayer,int isPartyNumber, out float cooldown)
    {
        // 임시용 적파티, 아군 파티 추가 해야함 
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
