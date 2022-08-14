using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicSkill : MonoBehaviour
{
    public int relicEffectCount;
    public float tempSkillTime;
    public static RelicSkill instance = null;
    private WaitForSeconds delay_03s = new WaitForSeconds(0.3f);
    private WaitForSeconds delay_10s = new WaitForSeconds(1.0f);

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
            yield return delay_03s;
            if(relicEffectCount == 0)
            {
                StopAllCoroutines();
                break;
            }
        }
    }

    private void RelicSkillSetting(int relicNum)
    {
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
                    StartCoroutine(RelicPositiveSK_ABILITY(relicNum, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.ATTACK:
                    StartCoroutine(RelicPositiveSK_ATTACK(relicNum, tempSkillTime));
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
                    StartCoroutine(RelicNegativeSK_ABILITY(relicNum-5, tempSkillTime));
                    break;
                case RelicData.EffectTypeA.ATTACK:
                    StartCoroutine(RelicNegativeSK_ATTACK(relicNum-5, tempSkillTime));
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
            else yield return new WaitForSeconds(1f);
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
        relicEffectCount--;
    }

    IEnumerator RelicPositiveSK_PROTECT(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }

    IEnumerator RelicPositiveSK_ABILITY(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }

    IEnumerator RelicPositiveSK_ATTACK(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }


    IEnumerator RelicNegativeSK_HEAL(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }

    IEnumerator RelicNegativeSK_PROTECT(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }

    IEnumerator RelicNegativeSK_ABILITY(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }

    IEnumerator RelicNegativeSK_ATTACK(int relicNum, float skill_ActiveTime)
    {
        yield return null;
    }



}
