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
        // 데이터가 가지고 있는 캐릭터 정보 검사
        for (int i = 0; i < GameManager.instance.data.haveCharacter.Count; i++)
        {
            for (int j = 0; j < GameManager.instance.data.haveCharacter.Count; j++)
            {
                // 0은 비어있는 인덱스
                if (GameManager.instance.data.haveCharacter[i] != 0)
                {
                    if (charInventoryTr.GetChild(j).childCount != 0)
                    {
                        if (charInventoryTr.GetChild(j).GetChild(0).gameObject.GetComponent<ViewCharacterInfo>().num == GameManager.instance.data.haveCharacter[i])
                        {
                            break;
                        }
                    }
                    // 인벤토리에 남은 자리가 있는지 검사
                    if (charInventoryTr.GetChild(j).childCount == 0)
                    {
                        characterImg = Resources.Load<Image>("Unit/Image/Character_" + GameManager.instance.data.haveCharacter[i].ToString());
                        characterImg = Instantiate(characterImg, charInventoryTr.GetChild(j).GetComponent<Transform>());
                        InitRect(characterImg);
                        break;
                    }
                }
            }
        }
    }

    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = new Vector2(-5, -5);
        img.rectTransform.offsetMin = new Vector2(5, 5);
    }
}
