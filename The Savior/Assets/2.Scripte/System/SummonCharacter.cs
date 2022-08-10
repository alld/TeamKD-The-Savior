using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonCharacter : MonoBehaviour
{

    // ĳ������ ��
    public int input = 6;
    // ����
    private int rnd;
    // ��ȯ�� ĳ������ ������Ʈ
    private GameObject character;
    // ��ȯ�� ĳ���Ͱ� ��ġ�� ������
    public Transform summonCharTr; // CharacterTransform

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
        rnd = Random.Range(1, input);
        character = Resources.Load<GameObject>("Unit/Character" + rnd.ToString());
        character = Instantiate(character, summonCharTr);

        GameManager.instance.data.haveCharacter.Add(character.GetComponent<SummonCharacterTest>().number);
        GameManager.instance.GameSave();
        Debug.Log(GameManager.instance.data.haveCharacter);
    }

    public void InitSummon()
    {
        if(summonCharTr.childCount > 0)
        {
            Destroy(summonCharTr.GetChild(0).gameObject);
        } 
    }
    
}
