using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSkill : MonoBehaviour
{
    public static CardSkill instance = null;
    CardDataBase.InfoCard card;
    private bool check;
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
        CardSkillSetting();

        return check;
    }
    
    private float skill_ActiveTime;
    private void CardSkillSetting()
    {
        CardSkillValueReset();
        if (card.cost > DungeonOS.instance.costDGP) // 코스트 검사
        {
            check = false;
            return;
        }
        switch (card.effectSortB)
        {
            case CardDataBase.InfoCard.EffectSortB.즉시:
                skill_ActiveTime = 0;
                break;
            case CardDataBase.InfoCard.EffectSortB.지속:
                skill_ActiveTime = -card.effectValue_floatD;
                break;
            case CardDataBase.InfoCard.EffectSortB.지연:
                skill_ActiveTime = card.effectValue_floatD;
                break;
            default:
                DungeonOS.instance.GameError("카드 스킬 : 분류값 (B)가 제대로 할당되지 않았습니다.");
                check = false;
                return;
        }

        switch (card.effectSortA)
        {
            case CardDataBase.InfoCard.EffectSortA.회복:
                StartCoroutine(CardSkill_Heal());
                break;
            case CardDataBase.InfoCard.EffectSortA.보호:
                break;
            case CardDataBase.InfoCard.EffectSortA.버프:
                break;
            case CardDataBase.InfoCard.EffectSortA.디버프:
                break;
            case CardDataBase.InfoCard.EffectSortA.공격:
                break;
            case CardDataBase.InfoCard.EffectSortA.특수:
                break;
            default:
                DungeonOS.instance.GameError("카드 스킬 : 분류값 (A)가 제대로 할당되지 않았습니다.");
                check = false;
                return;
        }
    }

    private void CardSkillValueReset()
    {
        check = true;
        skill_ActiveTime = 0;
    }

    private int AllyTargetCheck(CardDataBase.InfoCard.EffectSortD targetType)
    {
        switch (targetType)
        {
            case CardDataBase.InfoCard.EffectSortD.전체:
                break;
            case CardDataBase.InfoCard.EffectSortD.랜덤:
                return Random.Range(0, DungeonOS.instance.partyUnit.Count);
            case CardDataBase.InfoCard.EffectSortD.체력많음:
                break;
            case CardDataBase.InfoCard.EffectSortD.체력적음:
                break;
            case CardDataBase.InfoCard.EffectSortD.데미지높음:
                break;
            case CardDataBase.InfoCard.EffectSortD.데미지낮음:
                break;
            default:
                break;
        }
        return 0;
    }
    private int EnemyTargetCheck(CardDataBase.InfoCard.EffectSortD targetType)
    {
        switch (targetType)
        {
            case CardDataBase.InfoCard.EffectSortD.전체:
                break;
            case CardDataBase.InfoCard.EffectSortD.랜덤:
                return Random.Range(0, DungeonOS.instance.monsterGroup.Count);
            case CardDataBase.InfoCard.EffectSortD.체력많음:
                break;
            case CardDataBase.InfoCard.EffectSortD.체력적음:
                break;
            case CardDataBase.InfoCard.EffectSortD.데미지높음:
                break;
            case CardDataBase.InfoCard.EffectSortD.데미지낮음:
                break;
            default:
                break;
        }
        return 0;
    }

    IEnumerator CardSkill_Heal()
    {
        // (제작할것) if 로 지연시간 반영
        do {
            switch (card.effectSortC)
            {
                case CardDataBase.InfoCard.EffectSortC.아군단일:
                    DungeonOS.instance.partyUnit[AllyTargetCheck(card.effectSortD)].hP += card.effectValue_floatA;
                    break;
                case CardDataBase.InfoCard.EffectSortC.아군전체:
                    foreach (var item in DungeonOS.instance.partyUnit)
                    {
                        item.hP += card.effectValue_floatA;
                    }
                    break;
                case CardDataBase.InfoCard.EffectSortC.적단일:
                    DungeonOS.instance.monsterGroup[EnemyTargetCheck(card.effectSortD)].hP += card.effectValue_floatA;
                    break;
                case CardDataBase.InfoCard.EffectSortC.적전체:
                    foreach (var item in DungeonOS.instance.monsterGroup)
                    {
                        item.hP += card.effectValue_floatA;
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("카드 스킬 : 분류값 (C)가 제대로 할당되지 않았습니다.");
                    check = false;
                    break;
            }
        }
        while (check); //(제작할것) 조건으로 무한 반복인지 1회 사용인지 구분
        yield return null;
    }
}
