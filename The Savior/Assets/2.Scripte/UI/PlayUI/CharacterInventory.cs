using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    [Header("캐릭터 인벤토리 관리")]
    public Transform charInventoryTr;

    private Image characterImg;

    private void Start()
    {
        for (int i = 0; i < GameManager.instance.data.haveCharacter.Count; i++)
        {
            for (int j = 0; j < GameManager.instance.data.haveCharacter.Count; j++)
            {
                if (charInventoryTr.GetChild(j).childCount == 0)
                {
                    characterImg = Resources.Load<Image>("Unit/Character_" + GameManager.instance.data.haveCharacter[i].ToString());
                    Debug.Log(characterImg);
                    characterImg = Instantiate(characterImg, charInventoryTr.GetChild(j).GetComponent<Transform>());
                    InitRect(characterImg);
                    break;
                }
            }
        }
    }

    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = Vector2.zero;
        img.rectTransform.offsetMin = Vector2.zero;
    }
}
