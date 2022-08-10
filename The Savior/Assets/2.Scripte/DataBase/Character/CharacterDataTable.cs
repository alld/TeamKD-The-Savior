using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CharacterDataTable : MonoBehaviour
{
    private TextAsset jsonData;
    private string json;

    public int num;      // ĳ���� ��ȣ

    public string charName; // ĳ���� �̸�
    public float hp;
    public float maxHP;
    public float damage;
    public float attackSpeed;
    public float moveSpeed;
    public float defense;
    public int attackType;              // ĳ���� �Ӽ�
    public float attackRange;
    public int propertie;           // �Ӽ�  
    public float priRange;          // ���� ����� �ν��ϴ� ����
    public int priorities;          // ���� �ν� �켱��ġ
    public int positionPer;         // �ڸ� ��ġ
    public int unitClass;
    public int level;               // ĳ���� ���
    public int exp;
    public bool partySet;           // ĳ���� ��Ƽ ���� ����
    public bool islock;             // ĳ���� �ر� ����
    public bool isLive;             // ĳ���� ���� ����
    public int basicSkill;          // ĳ���� �Ϲ� ��ų �ε��� �ѹ�
    public int speialSkill;         // ĳ���� ���ر� �ε��� �ѹ�
    public int overlapValueA;       // ������ �ߺ� ���� (���)
    public int overlapValueB;       // ������ �ߺ� ���� (�ҿ�)

    public Image icon;                // ĳ���� ������
    public GameObject charObject;       // ĳ���� ������Ʈ

    public CharacterDataTable(int num)
    {
        jsonData = Resources.Load<TextAsset>("CharacterData");
        json = jsonData.text;

        var data = JSON.Parse(json);
        switch (GameManager.instance.data.Language)
        {
            case 0:
                charName = data[num]["Name_Kr"];
                break;
            case 1:
                charName = data[num]["Name_Eng"];
                break;
            default:
                break;
        }

        maxHP = data[num][""];
        damage = data[num][""];
        attackSpeed = data[num][""];
        moveSpeed = data[num][""];

    }
}
