using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSkill : MonoBehaviour
{
    public static CardSkill instance = null;
    CardDataBase.InfoCard card;
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// [기능] : 사용된 카드에 대한 정보를 갱신하고, 사용된 효과의 스킬를 적용시킴
    /// <br></br> [구조]
    /// <br></br><paramref name="cardNumber"/> : 데이터베이스에 접근하기위한 카드 넘버링
    /// </summary>
    /// <param name="cardNumber"></param>
    /// <returns></returns>
    public bool UseCard(int cardNumber)
    {
        card = new CardDataBase.InfoCard(cardNumber);

        return CardSkillSetting();
    }
    
    private bool CardSkillSetting()
    {
        float skill_ActiveTime = 0;

        if (card.cost > DungeonOS.instance.costDGP) // 코스트 검사
        {
            return false;
        }
        // 스킬의 지속상태의 설정값을 지정함
        switch (card.effectSortB)
        {
            case CardDataBase.InfoCard.EffectSortB.NOW:
                skill_ActiveTime = 0;
                break;
            case CardDataBase.InfoCard.EffectSortB.CONTINUE:
                skill_ActiveTime = -card.effectValue_floatD;
                break;
            case CardDataBase.InfoCard.EffectSortB.DELAY:
                skill_ActiveTime = card.effectValue_floatD;
                break;
            default:
                DungeonOS.instance.GameError("카드 스킬 : 분류값 (B)가 제대로 할당되지 않았습니다.");
                return false;
        }
        // 어떤 스킬 효과를 적용할지를 분류함
        switch (card.effectSortA)
        {
            case CardDataBase.InfoCard.EffectSortA.HEAL:
                StartCoroutine(CardSkill_Heal(skill_ActiveTime));
                break;
            case CardDataBase.InfoCard.EffectSortA.PROTECT:
                StartCoroutine(CardSkill_PROTECT(skill_ActiveTime));
                break;
            case CardDataBase.InfoCard.EffectSortA.BUFF:
                StartCoroutine(CardSkill_BUFF(skill_ActiveTime));
                break;
            case CardDataBase.InfoCard.EffectSortA.DEBUFF:
                break;
            case CardDataBase.InfoCard.EffectSortA.ATTACK:
                break;
            case CardDataBase.InfoCard.EffectSortA.SPEIAL:
                break;
            default:
                DungeonOS.instance.GameError("카드 스킬 : 분류값 (A)가 제대로 할당되지 않았습니다.");
                return false;
        }
        return true;
    }

    /// <summary>
    /// 효과 (아군)대상이 단일대상일 경우, 어떤 대상을 선택할지 지정함.
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    private int AllyTargetCheck(CardDataBase.InfoCard.EffectSortD targetType)
    {
        switch (targetType)
        {
            case CardDataBase.InfoCard.EffectSortD.TOTAL:
                break;
            case CardDataBase.InfoCard.EffectSortD.RANDOM:
                return Random.Range(0, DungeonOS.instance.partyUnit.Count);
            case CardDataBase.InfoCard.EffectSortD.HP_HIGH:
                break;
            case CardDataBase.InfoCard.EffectSortD.HP_LOW:
                break;
            case CardDataBase.InfoCard.EffectSortD.DAMAGE_HIGH:
                break;
            case CardDataBase.InfoCard.EffectSortD.DAMAGE_LOW:
                break;
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
    private int EnemyTargetCheck(CardDataBase.InfoCard.EffectSortD targetType)
    {
        switch (targetType)
        {
            case CardDataBase.InfoCard.EffectSortD.TOTAL:
                break;
            case CardDataBase.InfoCard.EffectSortD.RANDOM:
                return Random.Range(0, DungeonOS.instance.monsterGroup.Count);
            case CardDataBase.InfoCard.EffectSortD.HP_HIGH:
                break;
            case CardDataBase.InfoCard.EffectSortD.HP_LOW:
                break;
            case CardDataBase.InfoCard.EffectSortD.DAMAGE_HIGH:
                break;
            case CardDataBase.InfoCard.EffectSortD.DAMAGE_LOW:
                break;
            default:
                break;
        }
        return 0;
    }

    /// <summary>
    /// 회복 효과 적용
    /// </summary>
    /// <returns></returns>
    IEnumerator CardSkill_Heal(float skill_ActiveTime)
    {
        bool skill_Switch = true;
        if (skill_ActiveTime < 0)
        {
            skill_ActiveTime = -skill_ActiveTime;
            yield return new WaitForSeconds(skill_ActiveTime);
        }
        do {
            int temp;
            int count = 0;
            switch (card.effectSortC)
            {
                case CardDataBase.InfoCard.EffectSortC.ALLY:
                    temp = AllyTargetCheck(card.effectSortD);
                    DungeonOS.instance.partyUnit[temp].hp += card.effectValue_floatA;
                    if(DungeonOS.instance.partyUnit[temp].hp > DungeonOS.instance.partyUnit[temp].maxHP)
                    {
                        DungeonOS.instance.partyUnit[temp].hp = DungeonOS.instance.partyUnit[temp].maxHP;
                    }
                    break;
                case CardDataBase.InfoCard.EffectSortC.ALLIES:
                    foreach (var item in DungeonOS.instance.partyUnit)
                    {
                        item.hp += card.effectValue_floatA;
                        if (item.hp > item.maxHP)
                        {
                            item.hp = item.maxHP;
                        }
                    }
                    break;
                case CardDataBase.InfoCard.EffectSortC.ENEMY:
                    temp = EnemyTargetCheck(card.effectSortD);
                    DungeonOS.instance.monsterGroup[temp].hp += card.effectValue_floatA;
                    if (DungeonOS.instance.monsterGroup[temp].hp > DungeonOS.instance.monsterGroup[temp].maxHP)
                    {
                        DungeonOS.instance.monsterGroup[temp].hp = DungeonOS.instance.monsterGroup[temp].maxHP;
                    }
                    break;
                case CardDataBase.InfoCard.EffectSortC.ENEMIES:
                    foreach (var item in DungeonOS.instance.monsterGroup)
                    {
                        item.hp += card.effectValue_floatA;
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
        }
        while (skill_Switch);
    }

    /// <summary>
    /// 보호막 효과 적용
    /// </summary>
    /// <returns></returns>
    IEnumerator CardSkill_PROTECT(float skill_ActiveTime)
    {
        bool skill_Switch = true;
        if (skill_ActiveTime < 0)
        {
            skill_ActiveTime = -skill_ActiveTime;
            yield return new WaitForSeconds(skill_ActiveTime);
        }
        int temp;
        int count = 0;
        switch (card.effectSortC)
        {
            case CardDataBase.InfoCard.EffectSortC.ALLY:
                temp = AllyTargetCheck(card.effectSortD);
                DungeonOS.instance.weightAllyUnit[temp].Current_protect += card.effectValue_floatA;
                DungeonOS.instance.weightAllyUnit[temp].Current_protectMax += card.effectValue_floatA;
                break;
            case CardDataBase.InfoCard.EffectSortC.ALLIES:
                foreach (var item in DungeonOS.instance.weightAllyUnit)
                {
                    item.Current_protect += card.effectValue_floatA;
                    item.Current_protectMax += card.effectValue_floatA;
                }
                break;
            case CardDataBase.InfoCard.EffectSortC.ENEMY:
                temp = EnemyTargetCheck(card.effectSortD);
                DungeonOS.instance.weightEnemyGroup[temp].Current_protect += card.effectValue_floatA;
                DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax += card.effectValue_floatA;
                break;
            case CardDataBase.InfoCard.EffectSortC.ENEMIES:
                foreach (var item in DungeonOS.instance.weightEnemyGroup)
                {
                    item.Current_protect += card.effectValue_floatA;
                    item.Current_protectMax += card.effectValue_floatA;
                }
                break;
            default:
                DungeonOS.instance.GameError("카드 회복 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                break;
        }
        if (skill_ActiveTime <= 0) skill_Switch = false;
        do
        {
            if ((++count >= skill_ActiveTime) && (skill_ActiveTime > 0))
            {
                skill_Switch = false;
                switch (card.effectSortC)
                {
                    case CardDataBase.InfoCard.EffectSortC.ALLY:
                        temp = AllyTargetCheck(card.effectSortD);
                        DungeonOS.instance.weightAllyUnit[temp].Current_protectMax -= card.effectValue_floatA;
                        if (DungeonOS.instance.weightAllyUnit[temp].Current_protect >= DungeonOS.instance.weightAllyUnit[temp].Current_protectMax)
                        {
                            DungeonOS.instance.weightAllyUnit[temp].Current_protect = DungeonOS.instance.weightAllyUnit[temp].Current_protectMax;
                        }
                        break;
                    case CardDataBase.InfoCard.EffectSortC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            item.Current_protectMax -= card.effectValue_floatA;
                            if (item.Current_protect >= item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    case CardDataBase.InfoCard.EffectSortC.ENEMY:
                        temp = EnemyTargetCheck(card.effectSortD);
                        DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax -= card.effectValue_floatA;
                        if (DungeonOS.instance.weightEnemyGroup[temp].Current_protect >= DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax)
                        {
                            DungeonOS.instance.weightEnemyGroup[temp].Current_protect = DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax;
                        }
                        break;
                    case CardDataBase.InfoCard.EffectSortC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            item.Current_protectMax -= card.effectValue_floatA;
                            if (item.Current_protect >= item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 보호막 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
            }
            else yield return new WaitForSeconds(1f);
        }
        while (skill_Switch);
    }

    /// <summary>
    /// 버프 효과 적용
    /// </summary>
    /// <returns></returns>
    IEnumerator CardSkill_BUFF(float skill_ActiveTime)
    {
        bool skill_Switch = true;
        if (skill_ActiveTime < 0)
        {
            skill_ActiveTime = -skill_ActiveTime;
            yield return new WaitForSeconds(skill_ActiveTime);
        }
        int temp;
        int count = 0;
        switch (card.effectSortC)
        {
            case CardDataBase.InfoCard.EffectSortC.ALLY:
                temp = AllyTargetCheck(card.effectSortD);
                DungeonOS.instance.weightAllyUnit[temp].Current_protect += card.effectValue_floatA;
                DungeonOS.instance.weightAllyUnit[temp].Current_protectMax += card.effectValue_floatA;
                break;
            case CardDataBase.InfoCard.EffectSortC.ALLIES:
                foreach (var item in DungeonOS.instance.weightAllyUnit)
                {
                    item.Current_protect += card.effectValue_floatA;
                    item.Current_protectMax += card.effectValue_floatA;
                }
                break;
            case CardDataBase.InfoCard.EffectSortC.ENEMY:
                temp = EnemyTargetCheck(card.effectSortD);
                DungeonOS.instance.weightEnemyGroup[temp].Current_protect += card.effectValue_floatA;
                DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax += card.effectValue_floatA;
                break;
            case CardDataBase.InfoCard.EffectSortC.ENEMIES:
                foreach (var item in DungeonOS.instance.weightEnemyGroup)
                {
                    item.Current_protect += card.effectValue_floatA;
                    item.Current_protectMax += card.effectValue_floatA;
                }
                break;
            default:
                DungeonOS.instance.GameError("카드 회복 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                break;
        }
        if (skill_ActiveTime <= 0) skill_Switch = false;
        do
        {
            if ((++count >= skill_ActiveTime) && (skill_ActiveTime > 0))
            {
                skill_Switch = false;
                switch (card.effectSortC)
                {
                    case CardDataBase.InfoCard.EffectSortC.ALLY:
                        temp = AllyTargetCheck(card.effectSortD);
                        DungeonOS.instance.weightAllyUnit[temp].Current_protectMax -= card.effectValue_floatA;
                        if (DungeonOS.instance.weightAllyUnit[temp].Current_protect >= DungeonOS.instance.weightAllyUnit[temp].Current_protectMax)
                        {
                            DungeonOS.instance.weightAllyUnit[temp].Current_protect = DungeonOS.instance.weightAllyUnit[temp].Current_protectMax;
                        }
                        break;
                    case CardDataBase.InfoCard.EffectSortC.ALLIES:
                        foreach (var item in DungeonOS.instance.weightAllyUnit)
                        {
                            item.Current_protectMax -= card.effectValue_floatA;
                            if (item.Current_protect >= item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    case CardDataBase.InfoCard.EffectSortC.ENEMY:
                        temp = EnemyTargetCheck(card.effectSortD);
                        DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax -= card.effectValue_floatA;
                        if (DungeonOS.instance.weightEnemyGroup[temp].Current_protect >= DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax)
                        {
                            DungeonOS.instance.weightEnemyGroup[temp].Current_protect = DungeonOS.instance.weightEnemyGroup[temp].Current_protectMax;
                        }
                        break;
                    case CardDataBase.InfoCard.EffectSortC.ENEMIES:
                        foreach (var item in DungeonOS.instance.weightEnemyGroup)
                        {
                            item.Current_protectMax -= card.effectValue_floatA;
                            if (item.Current_protect >= item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("카드 보호막 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                        break;
                }
            }
            else yield return new WaitForSeconds(1f);
        }
        while (skill_Switch);
    }

}
