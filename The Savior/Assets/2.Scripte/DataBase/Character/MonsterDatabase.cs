using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;


public class MonsterDatabase : MonoBehaviour
{

    private TextAsset jsonData;
    private string json;
    private TextAsset jsonTextData;
    private string jsonText;
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
    /// ���� �켱�� : ��� �ν� ����
    /// </summary>
    public float defRange;
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
    public List<int> rewardCard = new List<int>();
    public List<int> rewardUnit = new List<int>();
    public List<int> rewardRelic = new List<int>();
    #endregion

    /// <summary>
    /// ������ �⺻���� �����������ؼ��� �Ű������� (int)�� �־������.
    /// <br>(int = num) : ���� ������ �ѹ�����</br>
    /// </summary>
    /// <param name="num"></param>
    public MonsterDatabase(int num)
    {
        jsonData = Resources.Load<TextAsset>("MonsterData");
        json = jsonData.text;
        jsonTextData = Resources.Load<TextAsset>("MonsterTextData");
        jsonText = jsonTextData.text;
        var textdata = JSON.Parse(jsonText);
        var data = JSON.Parse(json);
        switch (GameManager.instance.data.Language)
        {
            case 0:
                charName = textdata[num]["Name_Kr"];
                break;
            case 1:
                charName = textdata[num]["Name_Eng"];
                break;
        }
        number = num;
        maxHP = data[num]["Hp_Total"];
        hp = maxHP;
        int temp = data[num][""];
        monsterType = (MonsterType)temp;
        charObject = Resources.Load<GameObject>("");
        damage = data[num]["Total_Damage"];
        attackSpeed = data[num]["Chr_AtkSpeed"];
        moveSpeed = data[num]["Chr_MS"];
        defense = data[num]["Chr_DF"];
        attackType = data[num]["Attack_Type"];
        attackRange = data[num]["Chr_AtkRange"];
        attribute = data[num]["Attribute"];
        priRange = data[num]["Atk_Know_Range"];
        defRange = data[num]["Def_Know_Range"];
        priorities = data[num]["Attack_Priority"];
        positionPri = data[num]["Place_Priority"];
        basicSkillA = data[num][""];
        basicSkillB = data[num][""];
        basicSkillC = data[num][""];
        speialSkill = data[num][""];
        rewardGold = data[num][""];
        rewardSoul = data[num][""];
        rewardExp = data[num][""];
        temp = data[num][""];
        string tempString = temp.ToString();
        if (tempString.Length < 1)
        {
            for (int i = 0; i < tempString.Length / 2; i++)
            {
                rewardCard.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
            }
        }
        temp = data[num][""];
        tempString = temp.ToString();
        if (tempString.Length < 1)
        {
            for (int i = 0; i < tempString.Length; i++)
            {
                rewardUnit.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
            }
        }
        temp = data[num][""];
        tempString = temp.ToString();
        if (tempString.Length < 1)
        {
            for (int i = 0; i < tempString.Length; i++)
            {
                rewardRelic.Add((int)(tempString[(i * 2) + 1] + tempString[(i * 2)]));
            }
        }
    }
}
