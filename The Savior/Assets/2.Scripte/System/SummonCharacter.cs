using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonCharacter : MonoBehaviour
{

    // 캐릭터의 수
    public int input = 5;
    // 난수
    private int rnd;
    private int stop;
    // 소환될 캐릭터의 오브젝트
    private GameObject character;
    private Image charImg;
    // 소환된 캐릭터가 배치될 포지션
    public Transform summonCharTr; // CharacterTransform
    private Transform charInventoryTr;

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
        stop = DuplicateSummon();
        if(stop == 1)
        {
            return;
        }

        GameManager.instance.data.golds -= price;

        character = Resources.Load<GameObject>("Unit/Character" + rnd.ToString());
        character = Instantiate(character, summonCharTr);
        
        // 캐릭터 소유 유무 저장
        GameManager.instance.data.haveCharacter.Add(character.GetComponent<UnitAI>().unitNumber);
        //Debug.Log(character.GetComponent<UnitAI>().unitNumber);
        GameManager.instance.GameSave();
    }

    public int DuplicateSummon()
    {
        int max = 0;
        // 캐릭터 생성
        rnd = Random.Range(1, input);


        for (int i = 0; i < GameManager.instance.data.haveCharacter.Count; i++)
        {
            if (GameManager.instance.data.haveCharacter[i] == rnd)
            {
                if (GameManager.instance.data.haveCharacter[i] == 0)
                {
                    DuplicateSummon();
                }
                else
                {
                    Debug.Log("모든 캐릭터를 소지하여 더 이상 뽑기를 할 수 없습니다.");
                    max = 1;
                    break;
                }
            }
        }
        return max;
    }

    public void InitSummon()
    {
        if (summonCharTr.childCount > 0)
        {
            Destroy(summonCharTr.GetChild(0).gameObject);
        }
    }

}
