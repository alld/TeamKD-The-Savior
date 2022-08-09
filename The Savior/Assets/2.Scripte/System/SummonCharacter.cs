using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCharacter : MonoBehaviour
{
    public int input = 11;

    private int rnd;

    /// <summary>
    /// 캐릭터 소환 버튼을 누르면 호출되는 함수.
    /// 정해진 범위 내에서 난수를 만든 뒤 해당 번호에 맞는 캐릭터를 소환한다.
    /// 돈이 부족하면 팝업창을 활성화
    /// </summary>
    /// <returns></returns>
    public void SummonRandom(int price)
    {
        if (GameManager.instance.data.golds < price) 
        {
            Debug.Log("골드가 부족합니다.");
            return;
        } 
        rnd = Random.Range(1, input);
        GameManager.instance.data.golds -= price;

        SummonChar(rnd);
    }

    /// <summary>
    /// 캐릭터 소환
    /// </summary>
    private void SummonChar(int n)
    {
        Debug.Log(n + "번 캐릭터 소환");
        
    }
}
