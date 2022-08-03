using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterDatabase : MonoBehaviour
{
    /// <summary>
    /// ĳ������ �⺻ �������� �������ִ� ������ ���̽� 
    /// <br>Ch_Info�� ���� �ν��Ͻ��� ���� ����ؾ���. </br>
    /// <br>CH_Info(int) : int == �����ͺ��̽��� ĳ���� �����ѹ� </br>
    /// </summary>
    public static CharacterDatabase instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

    public class CH_Info
    {
        #region ĳ���� �⺻ ������
        /// <summary>
        /// ĳ���� �⺻ ������
        /// </summary>
        public Image iconCH;
        /// <summary>
        /// ĳ���� �⺻ �̸�
        /// </summary>
        public string nameCH;
        /// <summary>
        /// ĳ���� �⺻ ü��
        /// </summary>
        public float hPCH;
        /// <summary>
        /// ĳ���� �⺻ ������
        /// </summary>
        public float damageCH;
        /// <summary>
        /// �����տ� �ִ� ĳ���� ������Ʈ, ��
        /// </summary>
        public GameObject objectCH;
        /// <summary>
        /// ĳ���� �⺻ ���ݼӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeedCH;
        /// <summary>
        /// ĳ���� �⺻ �̵��ӵ�
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeedCH;
        /// <summary>
        /// ĳ���� �⺻ ����
        /// </summary>
        public float defenseCH;
        /// <summary>
        /// ĳ���� �⺻ ����Ÿ��
        /// <br>1. �ٰŸ�</br>
        /// <br>2. ���Ÿ�</br>
        /// <br>3. ��Ŀ</br>
        /// </summary>
        public int attackTypeCH;
        /// <summary>
        /// ĳ���� �⺻ ���� ����
        /// </summary>
        public float attackRangeCH;
        /// <summary>
        /// ĳ���� �⺻ �Ӽ�
        /// <br>0. �� �Ӽ�</br>
        /// <br>1. �� �Ӽ�</br>
        /// <br>2. �� �Ӽ�</br>
        /// <br>3. Ǯ �Ӽ�</br>
        /// </summary>
        public int propertieCH;
        /// <summary>
        /// ĳ���� �ν� ���� : ���� ����� �ν��ϴ� ����
        /// </summary>
        public float priRangeCH;
        /// <summary>
        /// ĳ���� �켱�� : ���� �ν� �켱��ġ
        /// </summary>
        public int prioritiesCH;
        /// <summary>
        /// ĳ���� �⺻ �ڸ� �켱�� : �ڸ� ��ġ
        /// </summary>
        public int positionPerCH;
        /// <summary>
        /// [saveData] ĳ���� Ŭ����
        /// </summary>
        public int classCH;
        /// <summary>
        /// [saveData] ĳ���� �⺻ ��� 
        /// <br>1~3 �ܰ�� ����</br>
        /// </summary>
        public int levelCH;
        /// <summary>
        /// [saveData] ĳ���� ���� �ҿ� 
        /// </summary>
        public int soulCH;
        /// <summary>
        /// ĳ���� ��Ƽ ���� ���� 
        /// <para>���̺� ���� ���� </para>
        /// </summary>
        public bool partySetCH;
        /// <summary>
        /// ĳ���� �ر� ���� 
        /// <br> ��������Ʈ �������� �����Ұ��̶�, �رݿ��� �������ʿ����.</br>
        /// </summary>
        public bool islockCH;
        /// <summary>
        /// ĳ���Ͱ� ���� �⺻ ��ų �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int basicskillCH;
        /// <summary>
        /// ĳ���Ͱ� ���� �ñر� �ε��� �ѹ�
        /// <br> 0 �� ��ų�� ���°� </br>
        /// </summary>
        public int speialskillCH;
        #endregion




        /// <summary>
        /// ĳ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
        /// <br>(int = num) : ĳ���� ������ �ѹ�����</br>
        /// </summary>
        /// <param name="num"></param>
        public CH_Info(int num)
        {
            switch (num)
            {
                case 1: // �Ӽ��Ӽ� ĳ���� ���� ����
                    iconCH = null; // �̹��� ���� ����
                    nameCH = "�Ӽ��Ӽ� ĳ����"; // ���������� ���������� �ؽ�Ʈ ������� ����
                    hPCH = 100;
                    damageCH = 10.0f;
                    objectCH = Resources.Load<GameObject>("Unit/TestUnit");
                    attackSpeedCH = 1.0f;
                    moveSpeedCH = 1.0f;
                    defenseCH = 10.0f;
                    attackTypeCH = 1;
                    attackRangeCH = 10.0f;
                    propertieCH = 0;
                    priRangeCH = 10.0f;
                    prioritiesCH = 20;
                    positionPerCH = 30;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 2: // snrn
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 3:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 4:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 5:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 6:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
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
