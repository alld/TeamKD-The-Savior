using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 데미지 계산에 필요한 값들을 입력받고, 데미지 결과값을 반환하는 엔진
/// </summary>
public class DamageEngine : MonoBehaviour
{
    float a_Damage, d_defense, addDamage_attribute, add_DropDamage, add_finalDamage; // 기본 계산값
    float add_attDamageA, add_attDamageD; // 속성 강화수치
    int a_attribute, d_attribute, sum_attribute; // 속성 관련

    public static DamageEngine instance = null;

    private void Start()
    {
        instance = this;
    }


    /// <summary>
    /// 데미지와 공격대상과 피격대상을 입력하면 스텟치와 속성값을 비교해서 데미지 결과를 반영하는 기능.
    /// <br></br>
    /// <br></br><paramref name="playercheck"/> : 데이터를 참조해오는곳이 enemy, monsterGroup와 ally, partyUnit 이다르기때문에 구분이 필요함.
    /// <br></br><paramref name="dmg"/> : 데미지의 기본값
    /// <br></br><paramref name="attacker"/> : 공격한 대상의 스테이지 그룹 기준 넘버링
    /// <br></br><paramref name="defender"/> : 피격된 대상의 스테이지 그룹 기준 넘버링
    /// </summary>
    /// <param name="playercheck"> 플레이어가 공격자인지 체크하는 변수</param>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns></returns>
    public float OnDamageCalculate(bool playercheck, float dmg, int attacker, int defender)
    {
        if (playercheck) // 공격자가 플레이언지 몬스터인지 확인
        {
            if (DungeonOS.instance.monsterGroup[defender].isinvincible) return 0;
            // 속성 변환 체크 후 설정값 지정
            if (DungeonOS.instance.partyUnit[attacker].Add_attributeCheck) a_attribute = DungeonOS.instance.partyUnit[attacker].Add_attribute;
            else a_attribute = DungeonOS.instance.partyUnit[attacker].attribute;
            if (DungeonOS.instance.monsterGroup[defender].Add_attributeCheck) d_attribute = DungeonOS.instance.monsterGroup[defender].Add_attribute;
            else d_attribute = DungeonOS.instance.monsterGroup[defender].attribute;
            add_attDamageA = DungeonOS.instance.partyUnit[attacker].Add_attributeVlaue[a_attribute];
            add_attDamageD = DungeonOS.instance.monsterGroup[defender].Add_attributeVlaue[d_attribute];


            dmg += DungeonOS.instance.partyUnit[attacker].Add_damage;
            d_defense = DungeonOS.instance.monsterGroup[defender].defense;
            d_defense += DungeonOS.instance.monsterGroup[defender].Add_defense;
            add_finalDamage = DungeonOS.instance.partyUnit[attacker].Add_fianlDamage + DungeonOS.instance.partyUnit[attacker].Add_fianlDamage;
            add_DropDamage = DungeonOS.instance.monsterGroup[defender].Add_dropDamage + DungeonOS.instance.monsterGroup[defender].Add_dropDamage;
        }
        else
        {
            if (DungeonOS.instance.partyUnit[defender].isinvincible) return 0;
            // 속성 변환 체크 후 설정값 지정 
            if (DungeonOS.instance.partyUnit[defender].Add_attributeCheck) d_attribute = DungeonOS.instance.partyUnit[defender].Add_attribute;
            else d_attribute = DungeonOS.instance.partyUnit[defender].attribute;
            if (DungeonOS.instance.monsterGroup[attacker].Add_attributeCheck) a_attribute = DungeonOS.instance.monsterGroup[attacker].Add_attribute;
            else a_attribute = DungeonOS.instance.monsterGroup[attacker].attribute;
            add_attDamageD = DungeonOS.instance.partyUnit[defender].Add_attributeVlaue[d_attribute];
            add_attDamageA = DungeonOS.instance.monsterGroup[attacker].Add_attributeVlaue[a_attribute];


            dmg += DungeonOS.instance.monsterGroup[attacker].Add_damage;
            d_defense = DungeonOS.instance.partyUnit[defender].defense;
            d_defense += DungeonOS.instance.partyUnit[defender].Add_defense;

            add_finalDamage = DungeonOS.instance.monsterGroup[attacker].Add_fianlDamage + DungeonOS.instance.monsterGroup[attacker].Add_fianlDamage;
            add_DropDamage = DungeonOS.instance.partyUnit[defender].Add_dropDamage + DungeonOS.instance.partyUnit[defender].Add_dropDamage;
        }
        sum_attribute = (a_attribute * 10) + d_attribute;
        switch (sum_attribute) // 속성 데미지 비율 적용하는 기능
        {
            // 불1 물2 풀3 
            // 앞자리가 공격자의 속성, 뒷자리가 방어자의 속성
            case 0:
            case 11:
            case 22:
            case 33:
                addDamage_attribute = 1;
                break;
            case 13:
            case 21:
            case 32:
                addDamage_attribute = 1.5f + (add_attDamageA - add_attDamageD);
                break;
            case 12:
            case 23:
            case 31:
                addDamage_attribute = 0.5f + (add_attDamageA - add_attDamageD);
                break;
            default:
                DungeonOS.instance.GameError("데미지 엔진 : 없는 속성값이 설정되었습니다.");
                break;
        }
        // 최종 데미지 계산
        a_Damage = (dmg - d_defense) * addDamage_attribute;
        a_Damage = a_Damage * add_finalDamage * add_DropDamage;
        // 데미지 마이너스 방지 
        if (a_Damage < 0) return 0;
        else return a_Damage;
    }
    /// <summary>
    /// 보호막에도 피해를 줄경우 
    /// </summary>
    /// <param name="playercheck"></param>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <param name="shieldDamage"></param>
    /// <returns></returns>
    public float OnProtectCalculate(bool playercheck, float dmg, int attacker, int defender, out float shieldDamage)
    {
        float a_Damage = dmg;
        shieldDamage = 0;
        float temp_shield;
        if (playercheck) temp_shield = DungeonOS.instance.partyUnit[defender].Current_protect;
        else temp_shield = DungeonOS.instance.monsterGroup[defender].Current_protect;

        if (dmg <= temp_shield)
        {
            shieldDamage = dmg;
            a_Damage = 0;
        }
        else
        {
            shieldDamage = temp_shield;
            a_Damage = dmg - temp_shield;
        }

        return OnDamageCalculate(playercheck, a_Damage, attacker, defender);
    }
    /// <summary>
    /// 카드나 유물 효과로 데미지를 줬을 경우
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <returns></returns>
    public float OnDamageCalculate(bool playercheck, bool notUnitCheck, float dmg, int defender)
    {
        return 0.0f;
    }
    public float OnDamageCalculate(bool playercheck, bool notUnitCheck, float dmg, int defender, out float shieldDamage)
    {
        float a_Damage = dmg;
        shieldDamage = 0;
        float temp_shield;
        if (playercheck) temp_shield = DungeonOS.instance.partyUnit[defender].Current_protect;
        else temp_shield = DungeonOS.instance.monsterGroup[defender].Current_protect;

        if (dmg <= temp_shield)
        {
            shieldDamage = dmg;
            a_Damage = 0;
        }
        else
        {
            shieldDamage = temp_shield;
            a_Damage = dmg - temp_shield;
        }
        return OnDamageCalculate(playercheck, true, a_Damage, defender);
    }
}
