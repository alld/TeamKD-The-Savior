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
    /// [���] : ���� ī�忡 ���� ������ �����ϰ�, ���� ȿ���� ��ų�� �����Ŵ
    /// <br></br> [����]
    /// <br></br><paramref name="cardNumber"/> : �����ͺ��̽��� �����ϱ����� ī�� �ѹ���
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
        if (card.cost > DungeonOS.instance.costDGP) // �ڽ�Ʈ �˻�
        {
            check = false;
            return;
        }
        switch (card.effectSortB)
        {
            case CardDataBase.InfoCard.EffectSortB.���:
                skill_ActiveTime = 0;
                break;
            case CardDataBase.InfoCard.EffectSortB.����:
                skill_ActiveTime = -card.effectValue_floatD;
                break;
            case CardDataBase.InfoCard.EffectSortB.����:
                skill_ActiveTime = card.effectValue_floatD;
                break;
            default:
                DungeonOS.instance.GameError("ī�� ��ų : �з��� (B)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                check = false;
                return;
        }

        switch (card.effectSortA)
        {
            case CardDataBase.InfoCard.EffectSortA.ȸ��:
                StartCoroutine(CardSkill_Heal());
                break;
            case CardDataBase.InfoCard.EffectSortA.��ȣ:
                break;
            case CardDataBase.InfoCard.EffectSortA.����:
                break;
            case CardDataBase.InfoCard.EffectSortA.�����:
                break;
            case CardDataBase.InfoCard.EffectSortA.����:
                break;
            case CardDataBase.InfoCard.EffectSortA.Ư��:
                break;
            default:
                DungeonOS.instance.GameError("ī�� ��ų : �з��� (A)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
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
            case CardDataBase.InfoCard.EffectSortD.��ü:
                break;
            case CardDataBase.InfoCard.EffectSortD.����:
                return Random.Range(0, DungeonOS.instance.partyUnit.Count);
            case CardDataBase.InfoCard.EffectSortD.ü�¸���:
                break;
            case CardDataBase.InfoCard.EffectSortD.ü������:
                break;
            case CardDataBase.InfoCard.EffectSortD.����������:
                break;
            case CardDataBase.InfoCard.EffectSortD.����������:
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
            case CardDataBase.InfoCard.EffectSortD.��ü:
                break;
            case CardDataBase.InfoCard.EffectSortD.����:
                return Random.Range(0, DungeonOS.instance.monsterGroup.Count);
            case CardDataBase.InfoCard.EffectSortD.ü�¸���:
                break;
            case CardDataBase.InfoCard.EffectSortD.ü������:
                break;
            case CardDataBase.InfoCard.EffectSortD.����������:
                break;
            case CardDataBase.InfoCard.EffectSortD.����������:
                break;
            default:
                break;
        }
        return 0;
    }

    IEnumerator CardSkill_Heal()
    {
        // (�����Ұ�) if �� �����ð� �ݿ�
        do {
            switch (card.effectSortC)
            {
                case CardDataBase.InfoCard.EffectSortC.�Ʊ�����:
                    DungeonOS.instance.partyUnit[AllyTargetCheck(card.effectSortD)].hP += card.effectValue_floatA;
                    break;
                case CardDataBase.InfoCard.EffectSortC.�Ʊ���ü:
                    foreach (var item in DungeonOS.instance.partyUnit)
                    {
                        item.hP += card.effectValue_floatA;
                    }
                    break;
                case CardDataBase.InfoCard.EffectSortC.������:
                    DungeonOS.instance.monsterGroup[EnemyTargetCheck(card.effectSortD)].hP += card.effectValue_floatA;
                    break;
                case CardDataBase.InfoCard.EffectSortC.����ü:
                    foreach (var item in DungeonOS.instance.monsterGroup)
                    {
                        item.hP += card.effectValue_floatA;
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                    check = false;
                    break;
            }
        }
        while (check); //(�����Ұ�) �������� ���� �ݺ����� 1ȸ ������� ����
        yield return null;
    }
}
