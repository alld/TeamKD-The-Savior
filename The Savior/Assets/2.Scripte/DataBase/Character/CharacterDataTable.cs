using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CharacterDataTable : MonoBehaviour
{
    private TextAsset jsonData;
    private string json;

    public int num;      // 캐릭터 번호

    public string charName; // 캐릭터 이름
    public float hp;
    public float maxHP;
    public float damage;
    public float attackSpeed;
    public float moveSpeed;
    public float defense;
    public int attackType;              // 캐릭터 속성
    public float attackRange;
    public int propertie;           // 속성  
    public float priRange;          // 공격 대상을 인식하는 범위
    public int priorities;          // 공격 인식 우선수치
    public int positionPer;         // 자리 배치
    public int unitClass;
    public int level;               // 캐릭터 등급
    public int exp;
    public bool partySet;           // 캐릭터 파티 참여 여부
    public bool islock;             // 캐릭터 해금 여부
    public bool isLive;             // 캐릭터 생존 여부
    public int basicSkill;          // 캐릭터 일반 스킬 인덱스 넘버
    public int speialSkill;         // 캐릭터 국극기 인덱스 넘버

    public Image icon;                // 캐릭터 아이콘
    public GameObject charObject;       // 캐릭터 오브젝트

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
