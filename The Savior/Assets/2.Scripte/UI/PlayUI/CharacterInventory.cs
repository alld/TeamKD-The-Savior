using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    [Header("캐릭터 인벤토리 관리")]
    public Transform charInventoryTr;

    private Image characterImg;

    private void OnEnable()
    {
        StartCoroutine(CharInvenInit());    
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.instance.isSetting);
        StartCoroutine(CharInvenInit());
    }
    public IEnumerator CharInvenInit()
    {
        // 데이터가 가지고 있는 캐릭터 정보 검사
        for (int i = 0; i < GameManager.instance.data.haveCharacter.Count; i++)
        {
            for (int j = 0; j < GameManager.instance.data.haveCharacter.Count; j++)
            {
                // 0은 비어있는 인덱스
                if (GameManager.instance.data.haveCharacter[i] != 0)
                {
                    if (charInventoryTr.GetChild(j).childCount != 0)        // 인벤토리에 캐릭터가 배치되어있고.
                    {
                        //배치된 캐릭터가 현재 내가 가진 캐릭터가 맞다면
                        if (charInventoryTr.GetChild(j).GetChild(0).gameObject.GetComponent<ViewCharacterInfo>().num == GameManager.instance.data.haveCharacter[i])
                        {
                            break;
                        }
                    }
                    // 인벤토리에 남은 자리가 있는지 검사
                    if (charInventoryTr.GetChild(j).childCount == 0)
                    {
                        Debug.Log("초기화!!");
                        characterImg = Resources.Load<Image>("Unit/Image/Character_" + GameManager.instance.data.haveCharacter[i].ToString());
                        characterImg = Instantiate(characterImg, charInventoryTr.GetChild(j).GetComponent<Transform>());
                        InitRect(characterImg);
                        break;
                    }
                }
            }
        }
        yield return null;
    }

    public IEnumerator DestroyCharacterInventory()
    {
        int idx = 0;
        // 세팅되어있는 캐릭터를 모두 파괴 후 다시 생성.
        for (int i = 0; i < charInventoryTr.childCount; i++) idx++; // 현재 보유중인 캐릭터의 수 검사
        for (int i = 0; i < idx; i++)
        {
            // 현재 캐릭터 창에 캐릭터가 배치되어 있다면 파괴.
            if (charInventoryTr.GetChild(i).childCount != 0)
            {
                Destroy(charInventoryTr.GetChild(i).GetChild(0).gameObject);
            }

        }
        yield return null;
    }

    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = new Vector2(-5, -5);
        img.rectTransform.offsetMin = new Vector2(5, 5);
    }
}
