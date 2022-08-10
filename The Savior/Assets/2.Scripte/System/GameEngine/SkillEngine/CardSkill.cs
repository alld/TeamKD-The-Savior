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
        // ī��ȿ���� �⺻���� ���½�Ŵ (���� ��Ȱ���ϱ⶧���� �������ʿ�)
        CardSkillValueReset();
        if (card.cost > DungeonOS.instance.costDGP) // �ڽ�Ʈ �˻�
        {
            check = false;
            return;
        }
        // ��ų�� ���ӻ����� �������� ������
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
                DungeonOS.instance.GameError("ī�� ��ų : �з��� (B)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                check = false;
                return;
        }
        // � ��ų ȿ���� ���������� �з���
        switch (card.effectSortA)
        {
            case CardDataBase.InfoCard.EffectSortA.HEAL:
                StartCoroutine(CardSkill_Heal());
                break;
            case CardDataBase.InfoCard.EffectSortA.PROTECT:
                break;
            case CardDataBase.InfoCard.EffectSortA.BUFF:
                break;
            case CardDataBase.InfoCard.EffectSortA.DEBUFF:
                break;
            case CardDataBase.InfoCard.EffectSortA.ATTACK:
                break;
            case CardDataBase.InfoCard.EffectSortA.SPEIAL:
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

    IEnumerator CardSkill_Heal()
    {
        bool skill_Switch = true;
        if(skill_ActiveTime < 0) yield return new WaitForSeconds(-skill_ActiveTime);
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
                    DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                    check = false;
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return new WaitForSeconds(1f);
        }
        while (skill_Switch);
    }


}
