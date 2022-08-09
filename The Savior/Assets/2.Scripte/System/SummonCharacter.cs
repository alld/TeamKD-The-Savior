using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCharacter : MonoBehaviour
{
    public int input = 11;

    private int rnd;

    /// <summary>
    /// ĳ���� ��ȯ ��ư�� ������ ȣ��Ǵ� �Լ�.
    /// ������ ���� ������ ������ ���� �� �ش� ��ȣ�� �´� ĳ���͸� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    public void SummonRandom(int price)
    {
        if (GameManager.instance.data.golds < price) 
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        } 
        rnd = Random.Range(1, input);
        GameManager.instance.data.golds -= price;

        SummonChar(rnd);
    }

    /// <summary>
    /// ĳ���� ��ȯ
    /// </summary>
    private void SummonChar(int n)
    {
        Debug.Log(n + "�� ĳ���� ��ȯ");
        
    }
}
