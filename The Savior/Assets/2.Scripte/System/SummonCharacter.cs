using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonCharacter : MonoBehaviour
{

    // ĳ������ ��
    public int input = 5;
    // ����
    private int rnd;
    // ��ȯ�� ĳ������ ������Ʈ
    private GameObject character;
    private Image charImg;
    // ��ȯ�� ĳ���Ͱ� ��ġ�� ������
    public Transform summonCharTr; // CharacterTransform
    private Transform charInventoryTr;

    /// <summary>
    /// ĳ���� ��ȯ ��ư�� ������ ȣ��Ǵ� �Լ�.
    /// ������ ���� ������ ������ ���� �� �ش� ��ȣ�� �´� ĳ���͸� ��ȯ�Ѵ�.
    /// ���� �����ϸ� �˾�â�� Ȱ��ȭ
    /// </summary>
    /// <returns></returns>
    public void SummonRandom(int price)
    {
        //if (GameManager.instance.data.golds < price) 
        //{
        //    Debug.Log("��尡 �����մϴ�.");
        //    return;
        //} 
        //GameManager.instance.data.golds -= price;

        // ĳ���� ����
        rnd = Random.Range(1, input);
        character = Resources.Load<GameObject>("Unit/Character" + rnd.ToString());
        character = Instantiate(character, summonCharTr);
        
        // ĳ���� ���� ���� ����
        GameManager.instance.data.haveCharacter.Add(character.GetComponent<UnitAI>().unitNumber);
        //Debug.Log(character.GetComponent<UnitAI>().unitNumber);
        GameManager.instance.GameSave();

        // ĳ������ ���� ���� ����
        GameManager.instance.SaveExp(GameManager.instance.data.haveCharacter.Count);
        GameManager.instance.CharacterDataSave(GameManager.instance.data.haveCharacter.Count - 1);
    }

    public void InitSummon()
    {
        if (summonCharTr.childCount > 0)
        {
            Destroy(summonCharTr.GetChild(0).gameObject);
        }
    }

}
