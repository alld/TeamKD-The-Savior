using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicSkill : MonoBehaviour
{
    public int relicEffectCount;
    public float tempSkillTime;
    public static RelicSkill instance = null;
    private WaitForSeconds delay_03 = new WaitForSeconds(0.3f);
    private WaitForSeconds delay_10 = new WaitForSeconds(1.0f);

    public List<int> relicStart_ALWAY = new List<int>();
    public List<int> relicStart_FIRST = new List<int>();
    public List<int> relicStart_MIDDLE = new List<int>();
    public List<int> relicStart_HALF = new List<int>();
    public List<int> relicStart_LAST = new List<int>();


    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DungeonOS.instance.dele_TimeMIDDLE += RelicStart_MIDDLE;
        DungeonOS.instance.dele_TimeHALF += RelicStart_HALF;
    }

    private void OnDestroy()
    {
        DungeonOS.instance.dele_TimeMIDDLE -= RelicStart_MIDDLE;
        DungeonOS.instance.dele_TimeHALF -= RelicStart_HALF;
    }

    void RelicSkillReset()
    {
        relicEffectCount = 0;
        tempSkillTime = 0;
        relicStart_ALWAY.Clear();
        relicStart_FIRST.Clear();
        relicStart_HALF.Clear();
        relicStart_LAST.Clear();
        relicStart_MIDDLE.Clear();
    }
    /// <summary>
    /// 처음 시작할때 시작 시점에 맞쳐서 던전 OS의 유물값을 분류하는 기능
    /// </summary>
    public void UseRelic()
    {
        RelicSkillReset();
        for (int i = 0; i < DungeonOS.instance.equipRelic.Count; i++)
        {
            if (DungeonOS.instance.equipRelic[i].number != 0)
            {
                switch (DungeonOS.instance.equipRelic[i].effectTypeB)
                {
                    case RelicData.EffectTypeB.ALWAY:
                        relicStart_ALWAY.Add(i);
                        break;
                    case RelicData.EffectTypeB.FIRST:
                        relicStart_FIRST.Add(i);
                        break;
                    case RelicData.EffectTypeB.MIDDLE:
                        relicStart_MIDDLE.Add(i);
                        break;
                    case RelicData.EffectTypeB.HALF:
                        relicStart_HALF.Add(i);
                        break;
                    case RelicData.EffectTypeB.LAST:
                        relicStart_LAST.Add(i);
                        break;
                    default:
                        break;
                }
                switch (DungeonOS.instance.equipRelic[i].negEffectTypeB)
                {
                    case RelicData.EffectTypeB.ALWAY:
                        relicStart_ALWAY.Add(i+5);
                        break;
                    case RelicData.EffectTypeB.FIRST:
                        relicStart_FIRST.Add(i+5);
                        break;
                    case RelicData.EffectTypeB.MIDDLE:
                        relicStart_MIDDLE.Add(i+5);
                        break;
                    case RelicData.EffectTypeB.HALF:
                        relicStart_HALF.Add(i+5);
                        break;
                    case RelicData.EffectTypeB.LAST:
                        relicStart_LAST.Add(i+5);
                        break;
                    default:
                        break;
                }
            }
        }
        RelicStart_FIRST();
        RelicStart_ALWAY();
    }

    /// <summary>
    /// 시점 :: 게임 진행동안 항상 작동되는 유물
    /// </summary>
    private void RelicStart_ALWAY()
    {
        foreach (var item in relicStart_ALWAY)
        {
            RelicSkillSetting(item);
            relicEffectCount++;
        }
    }
    /// <summary>
    /// 시점 :: 최조 게임 시작할때 작동되는 유물
    /// </summary>
    private void RelicStart_FIRST()
    {
        foreach (var item in relicStart_FIRST)
        {
            RelicSkillSetting(item);
            relicEffectCount++;
        }
    }

    /// <summary>
    /// 시점 :: 중반에 돌입하면 작동되는 유물
    /// </summary>
    private void RelicStart_MIDDLE()
    {
        foreach (var item in relicStart_MIDDLE)
        {
            RelicSkillSetting(item);
            relicEffectCount++;
        }
    }

    /// <summary>
    /// 시점 :: 후반에 돌입하면 작동하는 유물
    /// </summary>
    private void RelicStart_HALF()
    {
        foreach (var item in relicStart_HALF)
        {
            RelicSkillSetting(item);
            relicEffectCount++;
        }
    }

    /// <summary>
    /// 시점 :: 라운드가 종료되면 작동하는 유물
    /// </summary>
    private void RelicStart_LAST()
    {
        foreach (var item in relicStart_LAST)
        {
            RelicSkillSetting(item);
            relicEffectCount++;
        }
    }


    public void StopRelic()
    {
        StartCoroutine(RelicSkillAllStopCheck());
    }

    IEnumerator RelicSkillAllStopCheck()
    {
        while (true)
        {
            yield return delay_03;
            if(relicEffectCount == 0)
            {
                StopAllCoroutines();
                break;
            }
        }
    }

    private void RelicSkillSetting(int relicNum)
    {
        string effectCount = DungeonOS.instance.equipRelic[relicNum].effectCount.ToString();
        if (relicNum < 5)
        {
            if (DungeonOS.instance.equipRelic[relicNum].loopEffect) tempSkillTime = DungeonOS.instance.equipRelic[relicNum].effectDataB4;
            // 어떤 스킬 효과를 적용할지를 분류함
            switch (DungeonOS.instance.equipRelic[relicNum].effectTypeA)
            {
                case RelicData.EffectTypeA.HEAL:
                    StartCoroutine(RelicPositiveSK_HEAL(relicNum, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.PROTECT:
                    StartCoroutine(RelicPositiveSK_PROTECT(relicNum, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.ABILITY:
                    StartCoroutine(RelicPositiveSK_ABILITY(relicNum, tempSkillTime, effectCount));
                    break;
                case RelicData.EffectTypeA.DAMAGE:
                    StartCoroutine(RelicPositiveSK_DAMAGE(relicNum, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.SPEIAL:
                    break;
                default:
                    DungeonOS.instance.GameError("유물 스킬 : 분류값 (A)가 제대로 할당되지 않았습니다.");
                    break;
            }
        }
        else
        {
            if (DungeonOS.instance.equipRelic[relicNum].negLoopEffect) tempSkillTime = DungeonOS.instance.equipRelic[relicNum].negEffectDataB4;
            switch (DungeonOS.instance.equipRelic[relicNum-5].negEffectTypeA)
            {
                case RelicData.EffectTypeA.HEAL:
                    StartCoroutine(RelicNegativeSK_HEAL(relicNum-5, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.PROTECT:
                    StartCoroutine(RelicNegativeSK_PROTECT(relicNum-5, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.ABILITY:
                    StartCoroutine(RelicNegativeSK_ABILITY(relicNum-5, tempSkillTime, effectCount));
                    break;
                case RelicData.EffectTypeA.DAMAGE:
                    StartCoroutine(RelicNegativeSK_DAMAGE(relicNum-5, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.SPEIAL:
                    break;
                default:
                    DungeonOS.instance.GameError("유물 스킬 : 분류값 (A)가 제대로 할당되지 않았습니다.");
                    break;
            }
        }
    }

    /// <summary>
    /// 효과 (아군)대상이 단일대상일 경우, 어떤 대상을 선택할지 지정함.
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    private int AllyTargetCheck(RelicData.EffectTypeD targetType)
    {
        int temp = 0;
        float tempValue = 0;
        switch (targetType)
        {
            case RelicData.EffectTypeD.TOTAL:
                break;
            case RelicData.EffectTypeD.RANDOM:
                return Random.Range(0, DungeonOS.instance.partyUnit.Count);
            case RelicData.EffectTypeD.HP_HIGH:
                tempValue = DungeonOS.instance.partyUnit[0].hp;
                for (int i = 1; i < DungeonOS.instance.partyUnit.Count; i++)
                {
                    if (DungeonOS.instance.partyUnit[i].hp > tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.partyUnit[i].hp;
                    }
                }
                return temp;
            case RelicData.EffectTypeD.HP_LOW:
                tempValue = DungeonOS.instance.partyUnit[0].hp;
                for (int i = 1; i < DungeonOS.instance.partyUnit.Count; i++)
                {
                    if (DungeonOS.instance.partyUnit[i].hp < tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.partyUnit[i].hp;
                    }
                }
                return temp;
            case RelicData.EffectTypeD.DAMAGE_HIGH:
                tempValue = DungeonOS.instance.partyUnit[0].damage;
                for (int i = 1; i < DungeonOS.instance.partyUnit.Count; i++)
                {
                    if (DungeonOS.instance.partyUnit[i].damage > tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.partyUnit[i].damage;
                    }
                }
                return temp;
            case RelicData.EffectTypeD.DAMAGE_LOW:
                tempValue = DungeonOS.instance.partyUnit[0].damage;
                for (int i = 1; i < DungeonOS.instance.partyUnit.Count; i++)
                {
                    if (DungeonOS.instance.partyUnit[i].damage < tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.partyUnit[i].damage;
                    }
                }
                return temp;
            default:
                break;
        }
        return 0;
    }

    /// <summary>
    /// 효과 (적)대상이 단일대상일 경우 어떤 대상을 선택할지 지정함.
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    private int EnemyTargetCheck(RelicData.EffectTypeD targetType)
    {
        int temp = 0;
        float tempValue = 0;
        switch (targetType)
        {
            case RelicData.EffectTypeD.TOTAL:
                break;
            case RelicData.EffectTypeD.RANDOM:
                return Random.Range(0, DungeonOS.instance.monsterGroup.Count);
            case RelicData.EffectTypeD.HP_HIGH:
                tempValue = DungeonOS.instance.monsterGroup[0].hp;
                for (int i = 1; i < DungeonOS.instance.monsterGroup.Count; i++)
                {
                    if (DungeonOS.instance.monsterGroup[i].hp > tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.monsterGroup[i].hp;
                    }
                }
                return temp;
            case RelicData.EffectTypeD.HP_LOW:
                tempValue = DungeonOS.instance.monsterGroup[0].hp;
                for (int i = 1; i < DungeonOS.instance.monsterGroup.Count; i++)
                {
                    if (DungeonOS.instance.monsterGroup[i].hp < tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.monsterGroup[i].hp;
                    }
                }
                return temp;
            case RelicData.EffectTypeD.DAMAGE_HIGH:
                tempValue = DungeonOS.instance.monsterGroup[0].damage;
                for (int i = 1; i < DungeonOS.instance.monsterGroup.Count; i++)
                {
                    if (DungeonOS.instance.monsterGroup[i].damage > tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.monsterGroup[i].damage;
                    }
                }
                return temp;
            case RelicData.EffectTypeD.DAMAGE_LOW:
                tempValue = DungeonOS.instance.monsterGroup[0].damage;
                for (int i = 1; i < DungeonOS.instance.monsterGroup.Count; i++)
                {
                    if (DungeonOS.instance.monsterGroup[i].damage < tempValue)
                    {
                        temp = i;
                        tempValue = DungeonOS.instance.monsterGroup[i].damage;
                    }
                }
                return temp;
            default:
                break;
        }
        return 0;
    }



    IEnumerator RelicPositiveSK_HEAL(int relicNum, float skill_ActiveTime)
    {
        bool skill_Switch = DungeonOS.instance.equipRelic[relicNum].loopEffect;
        do
        {
            int temp;
            int count = 0;
            switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
            {
                case RelicData.EffectTypeC.ALLY:
                    temp = AllyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                    DungeonOS.instance.partyUnit[temp].hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    if (DungeonOS.instance.partyUnit[temp].hp > DungeonOS.instance.partyUnit[temp].maxHP)
                    {
                        DungeonOS.instance.partyUnit[temp].hp = DungeonOS.instance.partyUnit[temp].maxHP;
                    }
                    break;
                case RelicData.EffectTypeC.ALLIES:
                    foreach (var item in DungeonOS.instance.partyUnit)
                    {
                        item.hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (item.hp > item.maxHP)
                        {
                            item.hp = item.maxHP;
                        }
                    }
                    break;
                case RelicData.EffectTypeC.ENEMY:
                    temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                    DungeonOS.instance.monsterGroup[temp].hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    if (DungeonOS.instance.monsterGroup[temp].hp > DungeonOS.instance.monsterGroup[temp].maxHP)
                    {
                        DungeonOS.instance.monsterGroup[temp].hp = DungeonOS.instance.monsterGroup[temp].maxHP;
                    }
                    break;
                case RelicData.EffectTypeC.ENEMIES:
                    foreach (var item in DungeonOS.instance.monsterGroup)
                    {
                        item.hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (item.hp > item.maxHP)
                        {
                            item.hp = item.maxHP;
                        }
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return delay_10;
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
        relicEffectCount--;
    }

    IEnumerator RelicPositiveSK_PROTECT(int relicNum, float skill_ActiveTime)
    {
        yield return null; bool skill_Switch = DungeonOS.instance.equipRelic[relicNum].loopEffect;
        do
        {
            int temp;
            int count = 0;
            switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
            {
                case RelicData.EffectTypeC.ALLY:
                    temp = AllyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                    DungeonOS.instance.weightAllyUnit[temp].Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    DungeonOS.instance.weightAllyUnit[temp].Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    break;
                case RelicData.EffectTypeC.ALLIES:
                    foreach (var item in DungeonOS.instance.weightAllyUnit)
                    {
                        item.Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        item.Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    }
                    break;
                case RelicData.EffectTypeC.ENEMY:
                    temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                    DungeonOS.instance.weightEnemyGroup[temp].Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    break;
                case RelicData.EffectTypeC.ENEMIES:
                    foreach (var item in DungeonOS.instance.weightEnemyGroup)
                    {
                        item.Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        item.Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return delay_10;
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
        relicEffectCount--;
    }

    IEnumerator RelicPositiveSK_ABILITY(int relicNum, float skill_ActiveTime, string effectCount)
    {
        bool skill_Switch = true;
        List<byte> skill_Count = new List<byte>();
        byte abilitySkill = (byte)effectCount[0];
        for (int i = 0; i < effectCount.Length; i++)
        {
            switch (i)
            {
                case 0:
                    abilitySkill = (byte)effectCount[i];
                    break;
                case 2:
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                    skill_Count.Add((byte)(effectCount[i] + effectCount[i + 1]));
                    break;
                default:
                    break;
            }
        }

        int allynum, enemynum;
        int count = 0;


        if (skill_ActiveTime <= 0) skill_Switch = false;
        allynum = AllyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
        enemynum = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);

        switch (abilitySkill)
        {
            case 0: // 반복 효과

                while (skill_Switch)
                {
                    for (int i = 0; i < skill_Count.Count; i++)
                    {
                        if (skill_Count[i] == 1 || skill_Count[i] == 14) DungeonOS.instance.equipRelic[relicNum].EffectCycle++;
                        AbilityEffect_Add(skill_Count[i], relicNum, allynum, enemynum);
                    }
                    yield return delay_10;
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }
                for (int j = 0; j < DungeonOS.instance.equipRelic[relicNum].EffectCycle; j++)
                {
                    for (int i = 0; i < skill_Count.Count; i++)
                    {
                        if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                        AbilityEffect_Subtract(skill_Count[i], relicNum, allynum, enemynum);
                    }
                }
                DungeonOS.instance.equipRelic[relicNum].EffectCycle = 0;
                break;
            case 1: // 즉시 효과

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    AbilityEffect_Add(skill_Count[i], relicNum, allynum, enemynum);
                }
                while (skill_Switch)
                {
                    yield return delay_10;
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    AbilityEffect_Subtract(skill_Count[i], relicNum, allynum, enemynum);
                }

                break;
            case 2: // 지속 효과
                for (int i = 0; i < skill_Count.Count; i++)
                {
                    AbilityEffect_Add(skill_Count[i], relicNum, allynum, enemynum);
                }
                while (skill_Switch)
                {
                    yield return delay_10;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    AbilityEffect_Subtract(skill_Count[i], relicNum, allynum, enemynum);
                }

                break;
            default:
                break;
        }
        do
        {
            if ((++count >= skill_ActiveTime) && (skill_ActiveTime > 0))
            {
                skill_Switch = false;

            }
            else yield return delay_10;
        }
        while (skill_Switch);
        relicEffectCount--;
    }

    IEnumerator RelicPositiveSK_DAMAGE(int relicNum, float skill_ActiveTime)
    {
        yield return null; bool skill_Switch = DungeonOS.instance.equipRelic[relicNum].loopEffect;
        do
        {
            int temp;
            int count = 0;
            float temp_shieldDamage, temp_damage;
            switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
            {
                case RelicData.EffectTypeC.ALLY:
                    temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                    temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, temp, out temp_shieldDamage);
                    DungeonOS.instance.weightAllyUnit[temp].Current_protect -= temp_shieldDamage;
                    DungeonOS.instance.partyUnit[temp].hp -= temp_damage;
                    if (DungeonOS.instance.partyUnit[temp].hp < 0)
                    {
                        DungeonOS.instance.partyUnit[temp].charObject.GetComponent<UnitAI>().Action_Die();
                    }
                    break;
                case RelicData.EffectTypeC.ALLIES:
                    for (int i = 0; i < DungeonOS.instance.partyUnit.Count; i++)
                    {
                        temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                        temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, i, out temp_shieldDamage);
                        DungeonOS.instance.weightAllyUnit[i].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.partyUnit[i].hp -= temp_damage;
                        if (DungeonOS.instance.partyUnit[i].hp < 0)
                        {
                            DungeonOS.instance.partyUnit[i].charObject.GetComponent<UnitAI>().Action_Die();
                        }
                    }
                    break;
                case RelicData.EffectTypeC.ENEMY:
                    temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                    temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, temp, out temp_shieldDamage);
                    DungeonOS.instance.weightEnemyGroup[temp].Current_protect -= temp_shieldDamage;
                    DungeonOS.instance.monsterGroup[temp].hp -= temp_damage;
                    if (DungeonOS.instance.monsterGroup[temp].hp < 0)
                    {
                        DungeonOS.instance.monsterGroup[temp].charObject.GetComponent<UnitAI>().Action_Die();
                    }
                    break;
                case RelicData.EffectTypeC.ENEMIES:
                    for (int i = 0; i < DungeonOS.instance.monsterGroup.Count; i++)
                    {
                        temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].effectTypeD);
                        temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, i, out temp_shieldDamage);
                        DungeonOS.instance.weightEnemyGroup[i].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.monsterGroup[i].hp -= temp_damage;
                        if (DungeonOS.instance.monsterGroup[i].hp < 0)
                        {
                            DungeonOS.instance.monsterGroup[i].charObject.GetComponent<UnitAI>().Action_Die();
                        }
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return delay_10;
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
        relicEffectCount--;
    }


    IEnumerator RelicNegativeSK_HEAL(int relicNum, float skill_ActiveTime)
    {
        bool skill_Switch = DungeonOS.instance.equipRelic[relicNum].negLoopEffect;
        do
        {
            int temp;
            int count = 0;
            switch (DungeonOS.instance.equipRelic[relicNum].negEffectTypeC)
            {
                case RelicData.EffectTypeC.ALLY:
                    temp = AllyTargetCheck(DungeonOS.instance.equipRelic[relicNum].negEffectTypeD);
                    DungeonOS.instance.partyUnit[temp].hp -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                    if (DungeonOS.instance.partyUnit[temp].hp < 1)
                    {
                        DungeonOS.instance.partyUnit[temp].hp = 1;
                    }
                    break;
                case RelicData.EffectTypeC.ALLIES:
                    foreach (var item in DungeonOS.instance.partyUnit)
                    {
                        item.hp -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                        if (item.hp < 1)
                        {
                            item.hp = 1;
                        }
                    }
                    break;
                case RelicData.EffectTypeC.ENEMY:
                    temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].negEffectTypeD);
                    DungeonOS.instance.monsterGroup[temp].hp -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                    if (DungeonOS.instance.monsterGroup[temp].hp < 1)
                    {
                        DungeonOS.instance.monsterGroup[temp].hp = 1;
                    }
                    break;
                case RelicData.EffectTypeC.ENEMIES:
                    foreach (var item in DungeonOS.instance.monsterGroup)
                    {
                        item.hp -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                        if (item.hp < 1)
                        {
                            item.hp = 1;
                        }
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return delay_10;
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
        relicEffectCount--;
    }

    IEnumerator RelicNegativeSK_PROTECT(int relicNum, float skill_ActiveTime)
    {
        yield return null; bool skill_Switch = DungeonOS.instance.equipRelic[relicNum].negLoopEffect;
        do
        {
            int temp;
            int count = 0;
            switch (DungeonOS.instance.equipRelic[relicNum].negEffectTypeC)
            {
                case RelicData.EffectTypeC.ALLY:
                    temp = AllyTargetCheck(DungeonOS.instance.equipRelic[relicNum].negEffectTypeD);
                    DungeonOS.instance.weightAllyUnit[temp].Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                    if(DungeonOS.instance.weightAllyUnit[temp].Current_protectMax < 0)
                    {
                        DungeonOS.instance.weightAllyUnit[temp].Current_protectMax = 0;
                    }
                    if (DungeonOS.instance.weightAllyUnit[temp].Current_protect > DungeonOS.instance.weightAllyUnit[temp].Current_protectMax)
                    {
                        DungeonOS.instance.weightAllyUnit[temp].Current_protect = DungeonOS.instance.weightAllyUnit[temp].Current_protectMax;
                    }
                    break;
                case RelicData.EffectTypeC.ALLIES:
                    foreach (var item in DungeonOS.instance.weightAllyUnit)
                    {
                        item.Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                        if(item.Current_protectMax < 0)
                        {
                            item.Current_protectMax = 0;
                        }
                        if(item.Current_protect > item.Current_protectMax)
                        {
                            item.Current_protect = item.Current_protectMax;
                        }
                    }
                    break;
                case RelicData.EffectTypeC.ENEMY:
                    temp = EnemyTargetCheck(DungeonOS.instance.equipRelic[relicNum].negEffectTypeD);
                    DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                    if (DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax < 0)
                    {
                        DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax = 0;
                    }
                    if (DungeonOS.instance.weightEnemyGroup[temp].Current_protect > DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax)
                    {
                        DungeonOS.instance.weightEnemyGroup[temp].Current_protect = DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax;
                    }
                    break;
                case RelicData.EffectTypeC.ENEMIES:
                    foreach (var item in DungeonOS.instance.weightEnemyGroup)
                    {
                        item.Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].negEffectDataB1;
                        if (item.Current_protectMax < 0)
                        {
                            item.Current_protectMax = 0;
                        }
                        if (item.Current_protect > item.Current_protectMax)
                        {
                            item.Current_protect = item.Current_protectMax;
                        }
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return delay_10;
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
        relicEffectCount--;
    }

    IEnumerator RelicNegativeSK_ABILITY(int relicNum, float skill_ActiveTime, string effectCount)
    {
        yield return null;
    }

    IEnumerator RelicNegativeSK_DAMAGE(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }

    void AbilityEffect_Add(byte skill_Count, int relicNum, int allynum, int enemynum)
    {
        float temp_shieldDamage, temp_damage;
        switch (skill_Count)
        {
            case 0: // 최대체력 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        

                        DungeonOS.instance.partyUnit[allynum].maxHP += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        DungeonOS.instance.partyUnit[allynum].hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {

                            item.maxHP += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            item.hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:


                        DungeonOS.instance.monsterGroup[enemynum].maxHP += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        DungeonOS.instance.monsterGroup[enemynum].hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {

                            item.maxHP += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            item.hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 회복 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 1: // 체력 회복 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        

                        DungeonOS.instance.partyUnit[allynum].hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.partyUnit[allynum].hp > DungeonOS.instance.partyUnit[allynum].maxHP)
                        {
                            DungeonOS.instance.partyUnit[allynum].hp = DungeonOS.instance.partyUnit[allynum].maxHP;
                        }
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {


                            item.hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.hp > item.maxHP)
                            {
                                item.hp = item.maxHP;
                            }
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:


                        DungeonOS.instance.monsterGroup[enemynum].hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.monsterGroup[enemynum].hp > DungeonOS.instance.monsterGroup[enemynum].maxHP)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].hp = DungeonOS.instance.monsterGroup[enemynum].maxHP;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {


                            item.hp += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.hp > item.maxHP)
                            {
                                item.hp = item.maxHP;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 2: // 데미지 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        

                        DungeonOS.instance.weightAllyUnit[allynum].Add_damage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Add_damage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:


                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_damage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:

                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            

                            item.Add_damage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 3: // 방어력 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_defense += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_defense += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_defense += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_defense += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 4: // 공격 속도 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attackSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            item.Add_attackSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attackSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            item.Add_attackSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 5: // 이동 속도 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        DungeonOS.instance.weightAllyUnit[allynum].Add_moveSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            item.Add_moveSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_moveSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_moveSpeed += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 6: // 공격 우선도
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_priorities += (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_priorities += (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_priorities += (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_priorities += (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 7: // 공격 사거리 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attackRange += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_attackRange += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attackRange += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attackRange += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 8: // 속성 추가 데미지(불)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[1] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_attributeVlaue[1] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[1] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[1] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 9: // 속성 추가 데미지(물)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[2] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_attributeVlaue[2] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[2] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[2] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 10: // 속성 추가 데미지(풀)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[3] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_attributeVlaue[3] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[3] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[3] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 11: // 속성 추가 데미지(무)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[0] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_attributeVlaue[0] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[0] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[0] += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 12: // 받는 피해량 감소 / 증가
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_dropDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_dropDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_dropDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_dropDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 13: // 보호막 부여 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        DungeonOS.instance.weightAllyUnit[allynum].Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            item.Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        DungeonOS.instance.weightEnemyGroup[enemynum].Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            item.Current_protectMax += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 14: // 보호막 회복 / 잔여량 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.weightAllyUnit[allynum].Current_protect > DungeonOS.instance.weightAllyUnit[allynum].Current_protectMax)
                        {
                            DungeonOS.instance.weightAllyUnit[allynum].Current_protect = DungeonOS.instance.weightAllyUnit[allynum].Current_protectMax;
                        }
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect > DungeonOS.instance.weightEnemyGroup[enemynum].Current_protectMax)
                        {
                            DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect = DungeonOS.instance.weightEnemyGroup[enemynum].Current_protectMax;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Current_protect += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 15: // 최종데미지 증가 (크리티컬) / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_fianlDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_fianlDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_fianlDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_fianlDamage += DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 16: // 무적 
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].isinvincible = true;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.isinvincible = true;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].isinvincible = true;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.isinvincible = true;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 17: // 데미지 부여
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, allynum, out temp_shieldDamage);
                        DungeonOS.instance.weightAllyUnit[allynum].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.partyUnit[allynum].hp -= temp_damage;
                        if (DungeonOS.instance.partyUnit[allynum].hp < 0)
                        {
                            DungeonOS.instance.partyUnit[allynum].charObject.GetComponent<UnitAI>().Action_Die();
                        }
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        for (int i = 0; i < DungeonOS.instance.partyUnit.Count; i++)
                        {


                            temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, i, out temp_shieldDamage);
                            DungeonOS.instance.weightAllyUnit[i].Current_protect -= temp_shieldDamage;
                            DungeonOS.instance.partyUnit[i].hp -= temp_damage;
                            if (DungeonOS.instance.partyUnit[i].hp < 0)
                            {
                                DungeonOS.instance.partyUnit[i].charObject.GetComponent<UnitAI>().Action_Die();
                            }
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, enemynum, out temp_shieldDamage);
                        DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.monsterGroup[enemynum].hp -= temp_damage;
                        if (DungeonOS.instance.monsterGroup[enemynum].hp < 0)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].charObject.GetComponent<UnitAI>().Action_Die();
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        for (int i = 0; i < DungeonOS.instance.monsterGroup.Count; i++)
                        {
                            temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, DungeonOS.instance.equipRelic[relicNum].effectDataB1, i, out temp_shieldDamage);
                            DungeonOS.instance.weightEnemyGroup[i].Current_protect -= temp_shieldDamage;
                            DungeonOS.instance.monsterGroup[i].hp -= temp_damage;
                            if (DungeonOS.instance.monsterGroup[i].hp < 0)
                            {
                                DungeonOS.instance.monsterGroup[i].charObject.GetComponent<UnitAI>().Action_Die();
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            default:
                break;
        }
    }
    void AbilityEffect_Subtract(byte skill_Count, int relicNum, int allynum, int enemynum)
    {
        switch (skill_Count)
        {
            case 0: // 최대체력 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.partyUnit[allynum].maxHP -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.partyUnit[allynum].maxHP < 1)
                        {
                            DungeonOS.instance.partyUnit[allynum].maxHP = 1;
                        }
                        if (DungeonOS.instance.partyUnit[allynum].hp > DungeonOS.instance.partyUnit[allynum].maxHP)
                        {
                            DungeonOS.instance.partyUnit[allynum].hp = DungeonOS.instance.partyUnit[allynum].maxHP;
                        }
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {


                            item.maxHP -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.maxHP < 1)
                            {
                                item.maxHP = 1;
                            }
                            if (item.hp > item.maxHP)
                            {
                                item.hp = item.maxHP;
                            }
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.monsterGroup[enemynum].maxHP -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.monsterGroup[enemynum].maxHP < 1)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].maxHP = 1;
                        }
                        if (DungeonOS.instance.monsterGroup[enemynum].hp > DungeonOS.instance.monsterGroup[enemynum].maxHP)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].hp = DungeonOS.instance.monsterGroup[enemynum].maxHP;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {

                            item.maxHP -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.maxHP < 1)
                            {
                                item.maxHP = 1;
                            }
                            if (item.hp > item.maxHP)
                            {
                                item.hp = item.maxHP;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 회복 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 1: // 체력 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.partyUnit[allynum].hp -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.partyUnit[allynum].hp < 1)
                        {
                            DungeonOS.instance.partyUnit[allynum].hp = 1;
                        }
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {


                            item.hp -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.hp < 1)
                            {
                                item.hp = 1;
                            }
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.monsterGroup[enemynum].hp -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.monsterGroup[enemynum].hp < 1)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].hp = 1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {

                            item.hp -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.hp < 1)
                            {
                                item.hp = 1;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 2: // 데미지 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_damage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_damage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_damage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_damage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 3: // 방어력 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_defense -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_defense -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_defense -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_defense -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 4: // 공격 속도 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attackSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_attackSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attackSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attackSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 5: // 이동 속도 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_moveSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_moveSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_moveSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_moveSpeed -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 6: // 공격 우선도
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_priorities -= (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_priorities -= (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_priorities -= (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_priorities -= (int)DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 7: // 공격 사거리 증가 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attackRange -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            

                            item.Add_attackRange -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attackRange -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attackRange -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 8: // 속성 추가 데미지(불)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[1] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Add_attributeVlaue[1] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:


                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[1] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[1] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 9: // 속성 추가 데미지(물)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[2] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Add_attributeVlaue[2] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[2] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[2] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 10: // 속성 추가 데미지(풀)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[3] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Add_attributeVlaue[3] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[3] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[3] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 11: // 속성 추가 데미지(무)
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_attributeVlaue[0] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Add_attributeVlaue[0] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_attributeVlaue[0] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_attributeVlaue[0] -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 12: // 받는 피해량 감소 / 증가
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_dropDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Add_dropDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_dropDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_dropDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 13: // 보호막 부여 / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.weightAllyUnit[allynum].Current_protect > DungeonOS.instance.weightAllyUnit[allynum].Current_protectMax)
                        {
                            DungeonOS.instance.weightAllyUnit[allynum].Current_protect = DungeonOS.instance.weightAllyUnit[allynum].Current_protectMax;
                        }
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect > DungeonOS.instance.weightEnemyGroup[enemynum].Current_protectMax)
                        {
                            DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect = DungeonOS.instance.weightEnemyGroup[enemynum].Current_protectMax;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Current_protectMax -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 14: // 보호막 회복 / 잔여량 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Current_protect -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.weightAllyUnit[allynum].Current_protect < 0)
                        {
                            DungeonOS.instance.weightAllyUnit[allynum].Current_protect = 0;
                        }
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Current_protect -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.Current_protect < 0)
                            {
                                item.Current_protect = 0;
                            }
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        if (DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect < 0)
                        {
                            DungeonOS.instance.weightEnemyGroup[enemynum].Current_protect = 0;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Current_protect -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                            if (item.Current_protect < 0)
                            {
                                item.Current_protect = 0;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 15: // 최종데미지 증가 (크리티컬) / 감소
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].Add_fianlDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.Add_fianlDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].Add_fianlDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.Add_fianlDamage -= DungeonOS.instance.equipRelic[relicNum].effectDataB1;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 16: // 무적 
                switch (DungeonOS.instance.equipRelic[relicNum].effectTypeC)
                {
                    case RelicData.EffectTypeC.ALLY:
                        
                        DungeonOS.instance.weightAllyUnit[allynum].isinvincible = false;
                        break;
                    case RelicData.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            
                            item.isinvincible = false;
                        }
                        break;
                    case RelicData.EffectTypeC.ENEMY:

                        DungeonOS.instance.weightEnemyGroup[enemynum].isinvincible = false;
                        break;
                    case RelicData.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            
                            item.isinvincible = false;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
                break;
            case 17: // 데미지 부여
                     // 적용 불가
                break;
            default:
                break;
        }
    }

}
