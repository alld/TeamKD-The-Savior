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
    public static MonsterDatabase instance = null;

    public class InfoMonster
    {
        #region ������ �⺻ ������
        /// <summary>
        /// ���� Ÿ�� ����
        /// </summary>
        public enum MonsterType { �Ϲ�, �߰�����, ��������}
        MonsterType monsterType;
        public int number;
        /// <summary>
        /// �����տ� �ִ� ĳ���� ������Ʈ, ��
        /// </summary>
        public GameObject objectMT;
        /// <summary>
        /// ���� �⺻ ������
        /// </summary>
        public Image iconMT;
        /// <summary>
        /// ���� �⺻ �̸�
        /// </summary>
        public string nameMT;
        /// <summary>
        /// ���� �⺻ ü��
        /// </summary>
        public float hPMT;
        /// <summary>
        /// ���� �⺻ ������
        /// </summary>
        public float damageMT;
        /// <summary>
        /// ���� �⺻ ���ݼӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeedMT;
        /// <summary>
        /// ���� �⺻ �̵��ӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeedMT;
        /// <summary>
        /// ���� �⺻ ����
        /// </summary>
        public float defenseMT;
        /// <summary>
        /// ���� �⺻ ����Ÿ��
        /// <br>1. �ٰŸ�</br>
        /// <br>2. ���Ÿ�</br>
        /// <br>3. ��Ŀ</br>
        /// </summary>
        public int attackTypeMT;
        /// <summary>
        /// ���� �⺻ ���� ����
        /// </summary>
        public float attackRangeMT;
        /// <summary>
        /// ���� �⺻ �Ӽ�
        /// <br>0. �� �Ӽ�</br>
        /// <br>1. �� �Ӽ�</br>
        /// <br>2. �� �Ӽ�</br>
        /// <br>3. Ǯ �Ӽ�</br>
        /// </summary>
        public int propertieMT;
        /// <summary>
        /// ���� �ν� ���� : ���� ����� �ν��ϴ� ����
        /// </summary>
        public float priRangeMT;
        /// <summary>
        /// ���� �켱�� : ���� �ν� �켱��ġ
        /// </summary>
        public int prioritiesMT;
        /// <summary>
        /// ���� �⺻ �ڸ� �켱�� : �ڸ� ��ġ
        /// </summary>
        public int positionPerMT;
        /// <summary>
        /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int basicskillMT1;
        /// <summary>
        /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int basicskillMT2;
        /// <summary>
        /// ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int basicskillMT3;
        /// <summary>
        /// ���Ͱ� ���� �ñر� �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int speialskillMT;
        public int rewardGold;
        public int rewardSoul;
        public int[] rewardCard;
        public int[] rewardUnit;
        public int[] rewardRelic;
        #endregion




        /// <summary>
        /// ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num) : ���� ������ �ѹ�����</br>
        /// </summary>
        /// <param name="num"></param>
        public InfoMonster(int num)
        {
            switch (num)
            {
                case 1: // �Ӽ��Ӽ� ���� ���� ����
                    number = num;
                    monsterType = MonsterType.�Ϲ�;
                    iconMT = null; // �̹��� ���� ����
                    nameMT = "�Ӽ��Ӽ� ĳ����"; // ���������� ���������� �ؽ�Ʈ ������� ����
                    hPMT = 100;
                    damageMT = 10.0f;
                    objectMT = Resources.Load<GameObject>("Unit/TestUnit");
                    attackSpeedMT = 1.0f;
                    moveSpeedMT = 1.0f;
                    defenseMT = 10.0f;
                    attackTypeMT = 1;
                    attackRangeMT = 10.0f;
                    propertieMT = 0;
                    priRangeMT = 10.0f;
                    prioritiesMT = 20;
                    positionPerMT = 30;
                    basicskillMT1 = 0;
                    basicskillMT2 = 0;
                    basicskillMT3 = 0;
                    speialskillMT = 0;
                    rewardGold = 1;
                    rewardSoul = 1;
                    rewardCard = null;
                    rewardUnit = null;
                    rewardRelic = null;
                    break;
                case 2: // snrn
                    number = num;
                    monsterType = MonsterType.�Ϲ�;
                    iconMT = null;
                    nameMT = null;
                    hPMT = 0;
                    damageMT = 0;
                    objectMT = null;
                    attackSpeedMT = 0;
                    moveSpeedMT = 0;
                    defenseMT = 0;
                    attackTypeMT = 0;
                    attackRangeMT = 0;
                    propertieMT = 0;
                    priRangeMT = 0;
                    prioritiesMT = 0;
                    positionPerMT = 0;
                    basicskillMT1 = 0;
                    basicskillMT2 = 0;
                    basicskillMT3 = 0;
                    speialskillMT = 0;
                    rewardGold = 1;
                    rewardSoul = 1;
                    rewardCard = null;
                    rewardUnit = null;
                    rewardRelic = null;
                    break;
                case 3:
                    number = num;
                    monsterType = MonsterType.�Ϲ�;
                    iconMT = null;
                    nameMT = null;
                    hPMT = 0;
                    damageMT = 0;
                    objectMT = null;
                    attackSpeedMT = 0;
                    moveSpeedMT = 0;
                    defenseMT = 0;
                    attackTypeMT = 0;
                    attackRangeMT = 0;
                    propertieMT = 0;
                    priRangeMT = 0;
                    prioritiesMT = 0;
                    positionPerMT = 0;
                    basicskillMT1 = 0;
                    basicskillMT2 = 0;
                    basicskillMT3 = 0;
                    speialskillMT = 0;
                    rewardGold = 1;
                    rewardSoul = 1;
                    rewardCard = null;
                    rewardUnit = null;
                    rewardRelic = null;
                    break;
                case 4:
                    number = num;
                    monsterType = MonsterType.�Ϲ�;
                    iconMT = null;
                    nameMT = null;
                    hPMT = 0;
                    damageMT = 0;
                    objectMT = null;
                    attackSpeedMT = 0;
                    moveSpeedMT = 0;
                    defenseMT = 0;
                    attackTypeMT = 0;
                    attackRangeMT = 0;
                    propertieMT = 0;
                    priRangeMT = 0;
                    prioritiesMT = 0;
                    positionPerMT = 0;
                    basicskillMT1 = 0;
                    basicskillMT2 = 0;
                    basicskillMT3 = 0;
                    speialskillMT = 0;
                    rewardGold = 1;
                    rewardSoul = 1;
                    rewardCard = null;
                    rewardUnit = null;
                    rewardRelic = null;
                    break;
                case 5:
                    number = num;
                    monsterType = MonsterType.�Ϲ�;
                    iconMT = null;
                    nameMT = null;
                    hPMT = 0;
                    damageMT = 0;
                    objectMT = null;
                    attackSpeedMT = 0;
                    moveSpeedMT = 0;
                    defenseMT = 0;
                    attackTypeMT = 0;
                    attackRangeMT = 0;
                    propertieMT = 0;
                    priRangeMT = 0;
                    prioritiesMT = 0;
                    positionPerMT = 0;
                    basicskillMT1 = 0;
                    basicskillMT2 = 0;
                    basicskillMT3 = 0;
                    speialskillMT = 0;
                    rewardGold = 1;
                    rewardSoul = 1;
                    rewardCard = null;
                    rewardUnit = null;
                    rewardRelic = null;
                    break;
                case 6:
                    number = num;
                    monsterType = MonsterType.�Ϲ�;
                    iconMT = null;
                    nameMT = null;
                    hPMT = 0;
                    damageMT = 0;
                    objectMT = null;
                    attackSpeedMT = 0;
                    moveSpeedMT = 0;
                    defenseMT = 0;
                    attackTypeMT = 0;
                    attackRangeMT = 0;
                    propertieMT = 0;
                    priRangeMT = 0;
                    prioritiesMT = 0;
                    positionPerMT = 0;
                    basicskillMT1 = 0;
                    basicskillMT2 = 0;
                    basicskillMT3 = 0;
                    speialskillMT = 0;
                    rewardGold = 1;
                    rewardSoul = 1;
                    rewardCard = null;
                    rewardUnit = null;
                    rewardRelic = null;
                    break;
                default:
                    break;
            }
        }
    }
    private void Awake()
    {
        instance = this;
        //foreach (var item in ImagefulSet)
        //{
        //    Imageful.Add(item);
        //}
    }
}
