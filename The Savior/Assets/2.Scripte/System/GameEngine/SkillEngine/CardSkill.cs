using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSkill : MonoBehaviour
{
    public static CardSkill instance = null;
    CardDataBase.Data card;
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
        card = new CardDataBase.Data(cardNumber);
        return CardSkillSetting();
    }
    
    private bool CardSkillSetting()
    {

        float skill_ActiveTime = 0;

        if (card.cost > DungeonOS.instance.costDGP) // �ڽ�Ʈ �˻�
        {
            return false;
        }
        DungeonOS.instance.costDGP -= card.cost;
        DungeonOS.instance.HandUIReset();
        // ��ų�� ���ӻ����� �������� ������
        switch (card.effectTypeB)
        {
            case CardDataBase.Data.EffectTypeB.NOW:
                skill_ActiveTime = 0;
                break;
            case CardDataBase.Data.EffectTypeB.CONTINUE:
                skill_ActiveTime = -card.effectValue_floatD;
                break;
            case CardDataBase.Data.EffectTypeB.DELAY:
                skill_ActiveTime = card.effectValue_floatD;
                break;
            default:
                DungeonOS.instance.GameError("ī�� ��ų : �з��� (B)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                return false;
        }
        
        
        string buffCount = card.buffCount.ToString();
        // ���� ȿ�� �����Ͽ� ��з� �۾� (���� ���� üũ�� ������ ���� ��ȯ)
        switch (card.effectTypeA)
        {
            case CardDataBase.Data.EffectTypeA.HEAL:
                if (card.effectValue_bool && buffCount.Length > 3) card.effectTypeA = CardDataBase.Data.EffectTypeA.BUFF;
                break;
            case CardDataBase.Data.EffectTypeA.PROTECT:
                if (card.effectValue_bool && buffCount.Length > 3) card.effectTypeA = CardDataBase.Data.EffectTypeA.BUFF;
                break;
            case CardDataBase.Data.EffectTypeA.BUFF:
                break;
            case CardDataBase.Data.EffectTypeA.DEBUFF:
                break;
            case CardDataBase.Data.EffectTypeA.DAMAGE:
                if (card.effectValue_bool && buffCount.Length > 3) card.effectTypeA = CardDataBase.Data.EffectTypeA.DEBUFF;
                break;
            case CardDataBase.Data.EffectTypeA.SPEIAL:
                break;
            default:
                break;
        }
        // � ��ų ȿ���� ���������� �з���
        switch (card.effectTypeA)
        {
            case CardDataBase.Data.EffectTypeA.HEAL:
                StartCoroutine(CardSkill_Heal(skill_ActiveTime));
                Debug.Log("���� üũ1");
                break;
            case CardDataBase.Data.EffectTypeA.PROTECT:
                StartCoroutine(CardSkill_PROTECT(skill_ActiveTime));
                Debug.Log("���� üũ2");
                break;
            case CardDataBase.Data.EffectTypeA.BUFF:
                StartCoroutine(CardSkill_BUFF(skill_ActiveTime, buffCount));
                Debug.Log("���� üũ3");
                break;
            case CardDataBase.Data.EffectTypeA.DEBUFF:
                StartCoroutine(CardSkill_DEBUFF(skill_ActiveTime, buffCount));
                Debug.Log("���� üũ4");

                break;
            case CardDataBase.Data.EffectTypeA.DAMAGE:
                StartCoroutine(CardSkill_DAMAGE(skill_ActiveTime));
                Debug.Log("���� üũ5");

                break;
            case CardDataBase.Data.EffectTypeA.SPEIAL:
                break;
            default:
                DungeonOS.instance.GameError("ī�� ��ų : �з��� (A)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                return false;
        }

        return true;
    }

    /// <summary>
    /// ȿ�� (�Ʊ�)����� ���ϴ���� ���, � ����� �������� ������.
    /// 
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    private int AllyTargetCheck(CardDataBase.Data.EffectTypeD targetType)
    {
        int temp = 0;
        float tempValue = 0;
        switch (targetType)
        {
            case CardDataBase.Data.EffectTypeD.TOTAL:
                break;
            case CardDataBase.Data.EffectTypeD.RANDOM:
                return Random.Range(0, DungeonOS.instance.partyUnit.Count);
            case CardDataBase.Data.EffectTypeD.HP_HIGH:
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
            case CardDataBase.Data.EffectTypeD.HP_LOW:
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
            case CardDataBase.Data.EffectTypeD.DAMAGE_HIGH:
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
            case CardDataBase.Data.EffectTypeD.DAMAGE_LOW:
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
    /// ȿ�� (��)����� ���ϴ���� ��� � ����� �������� ������.
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    private int EnemyTargetCheck(CardDataBase.Data.EffectTypeD targetType)
    {
        int temp = 0;
        float tempValue = 0;
        switch (targetType)
        {
            case CardDataBase.Data.EffectTypeD.TOTAL:
                break;
            case CardDataBase.Data.EffectTypeD.RANDOM:
                return Random.Range(0, DungeonOS.instance.monsterGroup.Count);
            case CardDataBase.Data.EffectTypeD.HP_HIGH:
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
            case CardDataBase.Data.EffectTypeD.HP_LOW:
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
            case CardDataBase.Data.EffectTypeD.DAMAGE_HIGH:
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
            case CardDataBase.Data.EffectTypeD.DAMAGE_LOW:
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

    /// <summary>
    /// ȸ�� ȿ�� ����
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
            switch (card.effectTypeC)
            {
                case CardDataBase.Data.EffectTypeC.ALLY:
                    Debug.Log(card.effectValue);

                    temp = AllyTargetCheck(card.effectTypeD);
                    DungeonOS.instance.partyUnit[temp].hp += card.effectValue;
                    if(DungeonOS.instance.partyUnit[temp].hp > DungeonOS.instance.partyUnit[temp].maxHP)
                    {
                        DungeonOS.instance.partyUnit[temp].hp = DungeonOS.instance.partyUnit[temp].maxHP;
                    }
                    break;
                case CardDataBase.Data.EffectTypeC.ALLIES:
                    foreach (var item in DungeonOS.instance.partyUnit)
                    {
                        Debug.Log(card.effectValue);

                        item.hp += card.effectValue;
                        if (item.hp > item.maxHP)
                        {
                            item.hp = item.maxHP;
                        }
                    }
                    break;
                case CardDataBase.Data.EffectTypeC.ENEMY:
                    Debug.Log(card.effectValue);

                    temp = EnemyTargetCheck(card.effectTypeD);
                    DungeonOS.instance.monsterGroup[temp].hp += card.effectValue;
                    if (DungeonOS.instance.monsterGroup[temp].hp > DungeonOS.instance.monsterGroup[temp].maxHP)
                    {
                        DungeonOS.instance.monsterGroup[temp].hp = DungeonOS.instance.monsterGroup[temp].maxHP;
                    }
                    break;
                case CardDataBase.Data.EffectTypeC.ENEMIES:
                    foreach (var item in DungeonOS.instance.monsterGroup)
                    {
                        Debug.Log(card.effectValue);
                        item.hp += card.effectValue;
                        if (item.hp > item.maxHP)
                        {
                            item.hp = item.maxHP;
                        }
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return new WaitForSeconds(1f);
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
    }

    /// <summary>
    /// ��ȣ�� ȿ�� ����
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
        switch (card.effectTypeC)
        {
            case CardDataBase.Data.EffectTypeC.ALLY:
                temp = AllyTargetCheck(card.effectTypeD);
                DungeonOS.instance.partyUnit[temp].Current_protect += card.effectValue;
                DungeonOS.instance.partyUnit[temp].Current_protectMax += card.effectValue;
                break;
            case CardDataBase.Data.EffectTypeC.ALLIES:
                foreach (var item in DungeonOS.instance.partyUnit)
                {
                    item.Current_protect += card.effectValue;
                    item.Current_protectMax += card.effectValue;
                }
                break;
            case CardDataBase.Data.EffectTypeC.ENEMY:
                temp = EnemyTargetCheck(card.effectTypeD);
                DungeonOS.instance.monsterGroup[temp].Current_protect += card.effectValue;
                DungeonOS.instance.monsterGroup[temp].Current_protectMax += card.effectValue;
                break;
            case CardDataBase.Data.EffectTypeC.ENEMIES:
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    item.Current_protect += card.effectValue;
                    item.Current_protectMax += card.effectValue;
                }
                break;
            default:
                DungeonOS.instance.GameError("ī�� ȸ�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                break;
        }
        if (skill_ActiveTime <= 0) skill_Switch = false;
        do
        {
            if ((++count >= skill_ActiveTime) && (skill_ActiveTime > 0))
            {
                skill_Switch = false;
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        temp = AllyTargetCheck(card.effectTypeD);
                        DungeonOS.instance.partyUnit[temp].Current_protectMax -= card.effectValue;
                        if (DungeonOS.instance.partyUnit[temp].Current_protect >= DungeonOS.instance.partyUnit[temp].Current_protectMax)
                        {
                            DungeonOS.instance.partyUnit[temp].Current_protect = DungeonOS.instance.partyUnit[temp].Current_protectMax;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            item.Current_protectMax -= card.effectValue;
                            if (item.Current_protect >= item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        temp = EnemyTargetCheck(card.effectTypeD);
                        DungeonOS.instance.monsterGroup[temp].Current_protectMax -= card.effectValue;
                        if (DungeonOS.instance.monsterGroup[temp].Current_protect >= DungeonOS.instance.monsterGroup[temp].Current_protectMax)
                        {
                            DungeonOS.instance.monsterGroup[temp].Current_protect = DungeonOS.instance.monsterGroup[temp].Current_protectMax;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            item.Current_protectMax -= card.effectValue;
                            if (item.Current_protect >= item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ȣ�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
            }
            else yield return new WaitForSeconds(1f);
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
    }

    /// <summary>
    /// ���� ȿ�� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator CardSkill_BUFF(float skill_ActiveTime, string buffCount)
    {
        bool skill_Switch = true;
        List<byte> skill_Count = new List<byte>();
        byte buffSkill = (byte)buffCount[0];
        for (int i = 0; i < buffCount.Length; i++)
        {
            switch (i)
            {
                case 0:
                    buffSkill = (byte)buffCount[i];
                    break;
                case 2:
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                    skill_Count.Add((byte)(buffCount[i] + buffCount[i + 1]));
                    break;
                default:
                    break;
            }
        }

        if (skill_ActiveTime < 0)
        {
            skill_ActiveTime = -skill_ActiveTime;
            yield return new WaitForSeconds(skill_ActiveTime);
        }
        int allynum, enemynum;

        int count = 0;


        if (skill_ActiveTime <= 0) skill_Switch = false;
        BuffDataBase newbuff = new BuffDataBase(card.buffindex);
        allynum = AllyTargetCheck(card.effectTypeD);
        enemynum = EnemyTargetCheck(card.effectTypeD);
        switch (card.effectTypeC)
        {
            case CardDataBase.Data.EffectTypeC.ALLY:
                DungeonOS.instance.partyUnit[allynum].Current_buff.Add(newbuff);
                break;
            case CardDataBase.Data.EffectTypeC.ALLIES:
                foreach (var item in DungeonOS.instance.partyUnit)
                {
                    item.Current_buff.Add(newbuff);
                }
                break;
            case CardDataBase.Data.EffectTypeC.ENEMY:
                DungeonOS.instance.monsterGroup[enemynum].Current_buff.Add(newbuff);
                break;
            case CardDataBase.Data.EffectTypeC.ENEMIES:
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    item.Current_buff.Add(newbuff);
                }
                break;
        }

        switch (buffSkill)
        {
            case 0: // �ݺ� ȿ��

                while (skill_Switch)
                {
                    for (int i = 0; i < skill_Count.Count; i++)
                    {
                        BuffEffect_Add(skill_Count[i], allynum, enemynum, newbuff.ID);
                    }
                    yield return new WaitForSeconds(1.0f);
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    BuffEffect_Subtract(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
                }
                break;
            case 1: // ��� ȿ��

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    BuffEffect_Add(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                while (skill_Switch)
                {
                    yield return new WaitForSeconds(1.0f);
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }


                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    BuffEffect_Subtract(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
                }
                break;
            case 2: // ���� ȿ��
                for (int i = 0; i < skill_Count.Count; i++)
                {
                    BuffEffect_Add(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                while (skill_Switch)
                {
                    yield return new WaitForSeconds(2.5f);
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    BuffEffect_Subtract(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
                }

                break;
            case 3: // ���� �ο�(ȿ�� ����)

                while (skill_Switch)
                {
                    yield return new WaitForSeconds(1.0f);
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
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
            else yield return new WaitForSeconds(1f);
        }
        while (skill_Switch);
    }

    /// <summary>
    /// ����� ȿ�� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator CardSkill_DEBUFF(float skill_ActiveTime, string buffCount)
    {
        bool skill_Switch = true;
        List<byte> skill_Count = new List<byte>();
        byte buffSkill = (byte)buffCount[0];
        for (int i = 0; i < buffCount.Length; i++)
        {
            switch (i)
            {
                case 0:
                    buffSkill = (byte)buffCount[i];
                    break;
                case 2:
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                    skill_Count.Add((byte)(buffCount[i] + buffCount[i + 1]));
                    break;
                default:
                    break;
            }
        }

        if (skill_ActiveTime < 0)
        {
            skill_ActiveTime = -skill_ActiveTime;
            yield return new WaitForSeconds(skill_ActiveTime);
        }
        int allynum, enemynum;

        int count = 0;


        if (skill_ActiveTime <= 0) skill_Switch = false;
        BuffDataBase newbuff = new BuffDataBase(card.buffindex);
        allynum = AllyTargetCheck(card.effectTypeD);
        enemynum = EnemyTargetCheck(card.effectTypeD);
        switch (card.effectTypeC)
        {
            case CardDataBase.Data.EffectTypeC.ALLY:
                DungeonOS.instance.partyUnit[allynum].Current_buff.Add(newbuff);
                break;
            case CardDataBase.Data.EffectTypeC.ALLIES:
                foreach (var item in DungeonOS.instance.partyUnit)
                {
                    item.Current_buff.Add(newbuff);
                }
                break;
            case CardDataBase.Data.EffectTypeC.ENEMY:
                DungeonOS.instance.monsterGroup[enemynum].Current_buff.Add(newbuff);
                break;
            case CardDataBase.Data.EffectTypeC.ENEMIES:
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    item.Current_buff.Add(newbuff);
                }
                break;
        }

        switch (buffSkill)
        {
            case 0: // �ݺ� ȿ��

                while (skill_Switch)
                {
                    for (int i = 0; i < skill_Count.Count; i++)
                    {
                        BuffEffect_Subtract(skill_Count[i], allynum, enemynum, newbuff.ID);
                    }
                    yield return new WaitForSeconds(1.0f);
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    BuffEffect_Add(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
                }
                break;
            case 1: // ��� ȿ��

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    BuffEffect_Subtract(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                while (skill_Switch)
                {
                    yield return new WaitForSeconds(1.0f);
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }


                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    BuffEffect_Add(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
                }
                break;
            case 2: // ���� ȿ��
                for (int i = 0; i < skill_Count.Count; i++)
                {
                    BuffEffect_Subtract(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                while (skill_Switch)
                {
                    yield return new WaitForSeconds(2.5f);
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }

                for (int i = 0; i < skill_Count.Count; i++)
                {
                    if (skill_Count[i] == 1 || skill_Count[i] == 14) break;
                    BuffEffect_Add(skill_Count[i], allynum, enemynum, newbuff.ID);
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
                }

                break;
            case 3: // ���� �ο�(ȿ�� ����)

                while (skill_Switch)
                {
                    yield return new WaitForSeconds(1.0f);
                    if (count++ > skill_ActiveTime) skill_Switch = false;
                    if (!DungeonOS.instance.isRoundPlaying) break;
                }
                foreach (var item in DungeonOS.instance.monsterGroup)
                {
                    if (item.Current_buff.Exists((x => x == newbuff))) item.Current_buff.Remove(newbuff);
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
            else yield return new WaitForSeconds(1f);
        }
        while (skill_Switch);
    }

    /// <summary>
    /// ������ ȿ�� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator CardSkill_DAMAGE(float skill_ActiveTime)
    {
        bool skill_Switch = true;
        if (skill_ActiveTime < 0)
        {
            skill_ActiveTime = -skill_ActiveTime;
            yield return new WaitForSeconds(skill_ActiveTime);
        }
        do
        {
            int temp;
            int count = 0;
            float temp_shieldDamage, temp_damage;
            switch (card.effectTypeC)
            {
                case CardDataBase.Data.EffectTypeC.ALLY:
                    temp = EnemyTargetCheck(card.effectTypeD);
                    temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, card.effectValue, temp, out temp_shieldDamage);
                    DungeonOS.instance.partyUnit[temp].Current_protect -= temp_shieldDamage;
                    DungeonOS.instance.partyUnit[temp].hp -= temp_damage;
                    if (DungeonOS.instance.partyUnit[temp].hp < 0)
                    {
                        DungeonOS.instance.partyUnit[temp].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                    }
                    break;
                case CardDataBase.Data.EffectTypeC.ALLIES:
                    for (int i = 0; i < DungeonOS.instance.partyUnit.Count; i++)
                    {
                        temp = EnemyTargetCheck(card.effectTypeD);
                        temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, card.effectValue, i, out temp_shieldDamage);
                        DungeonOS.instance.partyUnit[i].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.partyUnit[i].hp -= temp_damage;
                        if (DungeonOS.instance.partyUnit[i].hp < 0)
                        {
                            DungeonOS.instance.partyUnit[i].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                        }
                    }
                    break;
                case CardDataBase.Data.EffectTypeC.ENEMY:
                    temp = EnemyTargetCheck(card.effectTypeD);
                    temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, card.effectValue, temp, out temp_shieldDamage);
                    DungeonOS.instance.monsterGroup[temp].Current_protect -= temp_shieldDamage;
                    DungeonOS.instance.monsterGroup[temp].hp -= temp_damage;
                    if (DungeonOS.instance.monsterGroup[temp].hp < 0)
                    {
                        DungeonOS.instance.monsterGroup[temp].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                    }
                    break;
                case CardDataBase.Data.EffectTypeC.ENEMIES:
                    for (int i = 0; i < DungeonOS.instance.monsterGroup.Count; i++)
                    {
                        temp = EnemyTargetCheck(card.effectTypeD);
                        temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, card.effectValue, i, out temp_shieldDamage);
                        DungeonOS.instance.monsterGroup[i].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.monsterGroup[i].hp -= temp_damage;
                        if (DungeonOS.instance.monsterGroup[i].hp < 0)
                        {
                            DungeonOS.instance.monsterGroup[i].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                        }
                    }
                    break;
                default:
                    DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                    break;
            }
            if (++count >= skill_ActiveTime) skill_Switch = false;
            else yield return new WaitForSeconds(1f);
            if (!DungeonOS.instance.isRoundPlaying) break;

        }
        while (skill_Switch);
    }


    void BuffEffect_Add(byte num, int allynum, int enemynum, int buffID)
    {
        float temp_shieldDamage, temp_damage;
        switch (num)
        {
            case 0: // �ִ�ü�� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;

                        DungeonOS.instance.partyUnit[allynum].maxHP += card.effectValue;
                        DungeonOS.instance.partyUnit[allynum].hp += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit.Select((value, index) => new { value, index }))
                        {
                            if (DungeonOS.instance.partyUnit[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.value.maxHP += card.effectValue;
                            item.value.hp += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[enemynum].Current_buff.Exists(x => x.ID != buffID)) break;

                        DungeonOS.instance.monsterGroup[enemynum].maxHP += card.effectValue;
                        DungeonOS.instance.monsterGroup[enemynum].hp += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup.Select((value, index) => new { value, index }))
                        {
                            if (DungeonOS.instance.monsterGroup[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.value.maxHP += card.effectValue;
                            item.value.hp += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ȸ�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 1: // ü�� ȸ�� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;

                        DungeonOS.instance.partyUnit[allynum].hp += card.effectValue;
                        if (DungeonOS.instance.partyUnit[allynum].hp > DungeonOS.instance.partyUnit[allynum].maxHP)
                        {
                            DungeonOS.instance.partyUnit[allynum].hp = DungeonOS.instance.partyUnit[allynum].maxHP;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit.Select((value, index) => new { value, index}))
                        {
                            if (DungeonOS.instance.partyUnit[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.value.hp += card.effectValue;
                            if (item.value.hp > item.value.maxHP)
                            {
                                item.value.hp = item.value.maxHP;
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[enemynum].Current_buff.Exists(x => x.ID != buffID)) break;

                        DungeonOS.instance.monsterGroup[enemynum].hp += card.effectValue;
                        if (DungeonOS.instance.monsterGroup[enemynum].hp > DungeonOS.instance.monsterGroup[enemynum].maxHP)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].hp = DungeonOS.instance.monsterGroup[enemynum].maxHP;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup.Select((value, index) => new { value, index }))
                        {
                            if (DungeonOS.instance.monsterGroup[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.value.hp += card.effectValue;
                            if (item.value.hp > item.value.maxHP)
                            {
                                item.value.hp = item.value.maxHP;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 2: // ������ ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;

                        DungeonOS.instance.partyUnit[allynum].Add_damage += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_damage += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        
                        DungeonOS.instance.monsterGroup[enemynum].Add_damage += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:

                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_damage += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 3: // ���� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_defense += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_defense += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_defense += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_defense += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 4: // ���� �ӵ� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attackSpeed += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attackSpeed += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attackSpeed += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attackSpeed += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 5: // �̵� �ӵ� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_moveSpeed += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_moveSpeed += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_moveSpeed += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_moveSpeed += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 6: // ���� �켱��
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_priorities += (int)card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_priorities += (int)card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_priorities += (int)card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_priorities += (int)card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 7: // ���� ��Ÿ� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attackRange += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attackRange += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attackRange += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attackRange += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 8: // �Ӽ� �߰� ������(��)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[1] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attributeVlaue[1] += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[1] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[1] += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 9: // �Ӽ� �߰� ������(��)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[2] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attributeVlaue[2] += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[2] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[2] += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 10: // �Ӽ� �߰� ������(Ǯ)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[3] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attributeVlaue[3] += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[3] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[3] += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 11: // �Ӽ� �߰� ������(��)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[0] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attributeVlaue[0] += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[0] += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[0] += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 12: // �޴� ���ط� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_dropDamage += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_dropDamage += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_dropDamage += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_dropDamage += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 13: // ��ȣ�� �ο� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Current_protect += card.effectValue;
                        DungeonOS.instance.partyUnit[allynum].Current_protectMax += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Current_protect += card.effectValue;
                            item.Current_protectMax += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Current_protect += card.effectValue;
                        DungeonOS.instance.monsterGroup[enemynum].Current_protectMax += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Current_protect += card.effectValue;
                            item.Current_protectMax += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 14: // ��ȣ�� ȸ�� / �ܿ��� ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Current_protect += card.effectValue;
                        if (DungeonOS.instance.partyUnit[allynum].Current_protect > DungeonOS.instance.partyUnit[allynum].Current_protectMax)
                        {
                            DungeonOS.instance.partyUnit[allynum].Current_protect = DungeonOS.instance.partyUnit[allynum].Current_protectMax;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Current_protect += card.effectValue;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Current_protect += card.effectValue;
                        if (DungeonOS.instance.monsterGroup[enemynum].Current_protect > DungeonOS.instance.monsterGroup[enemynum].Current_protectMax)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].Current_protect = DungeonOS.instance.monsterGroup[enemynum].Current_protectMax;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Current_protect += card.effectValue;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 15: // ���������� ���� (ũ��Ƽ��) / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_fianlDamage += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_fianlDamage += card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_fianlDamage += card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_fianlDamage += card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 16: // ���� 
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].isinvincible = true;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.isinvincible = true;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].isinvincible = true;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.isinvincible = true;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 17: // ������ �ο�
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, card.effectValue, allynum, out temp_shieldDamage);
                        DungeonOS.instance.partyUnit[allynum].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.partyUnit[allynum].hp -= temp_damage;
                        if (DungeonOS.instance.partyUnit[allynum].hp < 0)
                        {
                            DungeonOS.instance.partyUnit[allynum].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        for (int i = 0; i < DungeonOS.instance.partyUnit.Count; i++)
                        {
                            if (DungeonOS.instance.partyUnit[i].Current_buff.Exists(x => x.ID != buffID)) continue;

                            temp_damage = DamageEngine.instance.OnDamageCalculate(false, true, card.effectValue, i, out temp_shieldDamage);
                            DungeonOS.instance.partyUnit[i].Current_protect -= temp_shieldDamage;
                            DungeonOS.instance.partyUnit[i].hp -= temp_damage;
                            if (DungeonOS.instance.partyUnit[i].hp < 0)
                            {
                                DungeonOS.instance.partyUnit[i].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, card.effectValue, enemynum, out temp_shieldDamage);
                        DungeonOS.instance.monsterGroup[enemynum].Current_protect -= temp_shieldDamage;
                        DungeonOS.instance.monsterGroup[enemynum].hp -= temp_damage;
                        if (DungeonOS.instance.monsterGroup[enemynum].hp < 0)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        for (int i = 0; i < DungeonOS.instance.monsterGroup.Count; i++)
                        {
                            if (DungeonOS.instance.monsterGroup[i].Current_buff.Exists(x => x.ID != buffID)) continue;
                            temp_damage = DamageEngine.instance.OnDamageCalculate(true, true, card.effectValue, i, out temp_shieldDamage);
                            DungeonOS.instance.monsterGroup[i].Current_protect -= temp_shieldDamage;
                            DungeonOS.instance.monsterGroup[i].hp -= temp_damage;
                            if (DungeonOS.instance.monsterGroup[i].hp < 0)
                            {
                                DungeonOS.instance.monsterGroup[i].GetComponent<UnitAI>().AutoScheduler(2, UnitAI.AIPattern.Death);
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            default:
                break;
        }
    }
    void BuffEffect_Subtract(byte num, int allynum, int enemynum, int buffID)
    {
        switch (num)
        {
            case 0: // �ִ�ü�� ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].maxHP -= card.effectValue;
                        if (DungeonOS.instance.partyUnit[allynum].maxHP < 1)
                        {
                            DungeonOS.instance.partyUnit[allynum].maxHP = 1;
                        }
                        if (DungeonOS.instance.partyUnit[allynum].hp > DungeonOS.instance.partyUnit[allynum].maxHP)
                        {
                            DungeonOS.instance.partyUnit[allynum].hp = DungeonOS.instance.partyUnit[allynum].maxHP;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit.Select((value, index)=> new {value, index}))
                        {
                            if (DungeonOS.instance.partyUnit[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.value.maxHP -= card.effectValue;
                            if (item.value.maxHP < 1)
                            {
                                item.value.maxHP = 1;
                            }
                            if (item.value.hp > item.value.maxHP)
                            {
                                item.value.hp = item.value.maxHP;
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].maxHP -= card.effectValue;
                        if (DungeonOS.instance.monsterGroup[enemynum].maxHP < 1)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].maxHP = 1;
                        }
                        if (DungeonOS.instance.monsterGroup[enemynum].hp > DungeonOS.instance.monsterGroup[enemynum].maxHP)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].hp = DungeonOS.instance.monsterGroup[enemynum].maxHP;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup.Select((value, index) => new { value, index}))
                        {
                            if (DungeonOS.instance.monsterGroup[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.value.maxHP -= card.effectValue;
                            if (item.value.maxHP < 1)
                            {
                                item.value.maxHP = 1;
                            }
                            if (item.value.hp > item.value.maxHP)
                            {
                                item.value.hp = item.value.maxHP;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ȸ�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 1: // ü�� ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].hp -= card.effectValue;
                        if (DungeonOS.instance.partyUnit[allynum].hp < 1)
                        {
                            DungeonOS.instance.partyUnit[allynum].hp = 1;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit.Select((value, index) => new { value, index}))
                        {
                            if (DungeonOS.instance.partyUnit[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.value.hp -= card.effectValue;
                            if (item.value.hp < 1)
                            {
                                item.value.hp = 1;
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].hp -= card.effectValue;
                        if (DungeonOS.instance.monsterGroup[enemynum].hp < 1)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].hp = 1;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup.Select((value, index) => new { value, index }))
                        {
                            if (DungeonOS.instance.monsterGroup[item.index].Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.value.hp -= card.effectValue;
                            if (item.value.hp < 1)
                            {
                                item.value.hp = 1;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 2: // ������ ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_damage -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_damage -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_damage -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_damage -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 3: // ���� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_defense -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_defense -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_defense -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_defense -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 4: // ���� �ӵ� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attackSpeed -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attackSpeed -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attackSpeed -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attackSpeed -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 5: // �̵� �ӵ� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_moveSpeed -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_moveSpeed -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_moveSpeed -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_moveSpeed -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 6: // ���� �켱��
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_priorities -= (int)card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_priorities -= (int)card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_priorities -= (int)card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_priorities -= (int)card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 7: // ���� ��Ÿ� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attackRange -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;

                            item.Add_attackRange -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attackRange -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attackRange -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 8: // �Ӽ� �߰� ������(��)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[1] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[1] -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:

                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[1] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[1] -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 9: // �Ӽ� �߰� ������(��)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[2] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[2] -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[2] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[2] -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 10: // �Ӽ� �߰� ������(Ǯ)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[3] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[3] -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[3] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[3] -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 11: // �Ӽ� �߰� ������(��)
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_attributeVlaue[0] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[0] -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_attributeVlaue[0] -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_attributeVlaue[0] -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 12: // �޴� ���ط� ���� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_dropDamage -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_dropDamage -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_dropDamage -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_dropDamage -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 13: // ��ȣ�� �ο� / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Current_protectMax -= card.effectValue;
                        if (DungeonOS.instance.partyUnit[allynum].Current_protect > DungeonOS.instance.partyUnit[allynum].Current_protectMax)
                        {
                            DungeonOS.instance.partyUnit[allynum].Current_protect = DungeonOS.instance.partyUnit[allynum].Current_protectMax;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Current_protectMax -= card.effectValue;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Current_protectMax -= card.effectValue;
                        if (DungeonOS.instance.monsterGroup[enemynum].Current_protect > DungeonOS.instance.monsterGroup[enemynum].Current_protectMax)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].Current_protect = DungeonOS.instance.monsterGroup[enemynum].Current_protectMax;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Current_protectMax -= card.effectValue;
                            if (item.Current_protect > item.Current_protectMax)
                            {
                                item.Current_protect = item.Current_protectMax;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 14: // ��ȣ�� ȸ�� / �ܿ��� ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Current_protect -= card.effectValue;
                        if (DungeonOS.instance.partyUnit[allynum].Current_protect < 0)
                        {
                            DungeonOS.instance.partyUnit[allynum].Current_protect = 0;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Current_protect -= card.effectValue;
                            if (item.Current_protect < 0)
                            {
                                item.Current_protect = 0;
                            }
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Current_protect -= card.effectValue;
                        if (DungeonOS.instance.monsterGroup[enemynum].Current_protect < 0)
                        {
                            DungeonOS.instance.monsterGroup[enemynum].Current_protect = 0;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Current_protect -= card.effectValue;
                            if (item.Current_protect < 0)
                            {
                                item.Current_protect = 0;
                            }
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 15: // ���������� ���� (ũ��Ƽ��) / ����
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].Add_fianlDamage -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_fianlDamage -= card.effectValue;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].Add_fianlDamage -= card.effectValue;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.Add_fianlDamage -= card.effectValue;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 16: // ���� 
                switch (card.effectTypeC)
                {
                    case CardDataBase.Data.EffectTypeC.ALLY:
                        if (DungeonOS.instance.partyUnit[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.partyUnit[allynum].isinvincible = false;
                        break;
                    case CardDataBase.Data.EffectTypeC.ALLIES:
                        foreach (var item in DungeonOS.instance.partyUnit)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.isinvincible = false;
                        }
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMY:
                        if (DungeonOS.instance.monsterGroup[allynum].Current_buff.Exists(x => x.ID != buffID)) break;
                        DungeonOS.instance.monsterGroup[enemynum].isinvincible = false;
                        break;
                    case CardDataBase.Data.EffectTypeC.ENEMIES:
                        foreach (var item in DungeonOS.instance.monsterGroup)
                        {
                            if (item.Current_buff.Exists(x => x.ID != buffID)) continue;
                            item.isinvincible = false;
                        }
                        break;
                    default:
                        DungeonOS.instance.GameError("ī�� ��ų : �з��� (C)�� ����� �Ҵ���� �ʾҽ��ϴ�.");
                        break;
                }
                break;
            case 17: // ������ �ο�
                // ���� �Ұ�
                break;
            default:
                break;
        }
    }

}