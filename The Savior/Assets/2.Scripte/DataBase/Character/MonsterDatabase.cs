using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDatabase : MonoBehaviour
{
    /// <summary>
    /// ������ �⺻ �������� �������ִ� ������ ���̽� 
    /// <br>InfoMT�� ���� �ν��Ͻ��� ���� ����ؾ���. </br>
    /// <br>InfoMT(int) : int == �����ͺ��̽��� ĳ���� �����ѹ� </br>
    /// </summary>

    #region ������ �⺻ ������
    /// <summary>
    /// ���� Ÿ�� ����
    /// </summary>
    public enum MonsterType { MONSTER, BOSS, LASTBOSS }
    public MonsterType monsterType;
    public int number;
    /// <summary>
    /// �����տ� �ִ� ĳ���� ������Ʈ, ��
    /// </summary>
    public GameObject charObject;
    /// <summary>
    /// ���� �⺻ ������
    /// </summary>
    public Image icon;
    /// <summary>
    /// ���� �⺻ �̸�
    /// </summary>
    public string charName;
    /// <summary>
    /// ���� �⺻ ü��
    /// </summary>
    public float hp;
    /// <summary>
    /// ���� �ִ� ü��
    /// </summary>
    public float maxHP;
    /// <summary>
    /// ���� �⺻ ������
    /// </summary>
    public float damage;
    /// <summary>
    /// ���� �⺻ ���ݼӵ�
    /// <br>1.0f = 100%</br>
    /// </summary>
    public float attackSpeed;
    /// <summary>
    /// ���� �⺻ �̵��ӵ�
    /// <br>1.0f = 100%</br>
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// ���� �⺻ ����
    /// </summary>
    public float defense;
    /// <summary>
    /// ���� �⺻ ����Ÿ��
    /// <br>1. �ٰŸ�</br>
    /// <br>2. ���Ÿ�</br>
    /// <br>3. ��Ŀ</br>
    /// </summary>
    public int attackType;
    /// <summary>
    /// ���� �⺻ ���� ����
    /// </summary>
    public float attackRange;
    /// <summary>
    /// ���� �⺻ �Ӽ�
    /// <br>0. �� �Ӽ�</br>
    /// <br>1. �� �Ӽ�</br>
    /// <br>2. �� �Ӽ�</br>
    /// <br>3. Ǯ �Ӽ�</br>
    /// </summary>
    public int attribute;
    /// <summary>
    /// ���� �ν� ���� : ���� ����� �ν��ϴ� ����
    /// </summary>
    public float priRange;
    /// <summary>
    /// ���� �켱�� : ���� �ν� �켱��ġ
    /// </summary>
    public int priorities;
    /// <summary>
    /// ���� �⺻ �ڸ� �켱�� : �ڸ� ��ġ
    /// </summary>
    public int positionPri;
    /// <summary>
    /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int basicSkillA;
    /// <summary>
    /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int basicSkillB;
    /// <summary>
    /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int basicSkillC;
    /// <summary>
    /// ���Ͱ� ���� �ñر� �ε��� �ѹ�
    /// <br> 0 �� ��ų�� ���°� </br>
    /// </summary>
    public int speialSkill;
    public int rewardGold;
    public int rewardSoul;
    public int rewardExp;
    public int[] rewardCard;
    public int[] rewardUnit;
    public int[] rewardRelic;
    #endregion

    /// <summary>
    /// ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
    /// <br>(int = num) : ���� ������ �ѹ�����</br>
    /// </summary>
    /// <param name="num"></param>
    public MonsterDatabase(int num)
    {
        switch (num)
        {
            case 1: // �Ӽ��Ӽ� ���� ���� ����
                number = num;
                monsterType = MonsterType.MONSTER;
                icon = null; // �̹��� ���� ����
                charName = "�Ӽ��Ӽ� ĳ����"; // ���������� ���������� �ؽ�Ʈ ������� ����
                hp = 100;
                damage = 10.0f;
                charObject = Resources.Load<GameObject>("Unit/TestUnit");
                attackSpeed = 1.0f;
                moveSpeed = 1.0f;
                defense = 10.0f;
                attackType = 1;
                attackRange = 10.0f;
                attribute = 0;
                priRange = 10.0f;
                priorities = 20;
                positionPri = 30;
                basicSkillA = 0;
                basicSkillB = 0;
                basicSkillC = 0;
                speialSkill = 0;
                rewardGold = 1;
                rewardSoul = 1;
                rewardExp = 0;
                rewardCard = null;
                rewardUnit = null;
                rewardRelic = null;
                break;
        }
    }

}
