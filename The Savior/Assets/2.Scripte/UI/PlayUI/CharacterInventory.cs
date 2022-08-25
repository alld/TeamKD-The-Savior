using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    [Header("ĳ���� �κ��丮 ����")]
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
        // �����Ͱ� ������ �ִ� ĳ���� ���� �˻�
        for (int i = 0; i < GameManager.instance.data.haveCharacter.Count; i++)
        {
            for (int j = 0; j < GameManager.instance.data.haveCharacter.Count; j++)
            {
                // 0�� ����ִ� �ε���
                if (GameManager.instance.data.haveCharacter[i] != 0)
                {
                    if (charInventoryTr.GetChild(j).childCount != 0)        // �κ��丮�� ĳ���Ͱ� ��ġ�Ǿ��ְ�.
                    {
                        //��ġ�� ĳ���Ͱ� ���� ���� ���� ĳ���Ͱ� �´ٸ�
                        if (charInventoryTr.GetChild(j).GetChild(0).gameObject.GetComponent<ViewCharacterInfo>().num == GameManager.instance.data.haveCharacter[i])
                        {
                            break;
                        }
                    }
                    // �κ��丮�� ���� �ڸ��� �ִ��� �˻�
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
        yield return null;
    }

    public IEnumerator DestroyCharacterInventory()
    {
        int idx = 0;
        // ���õǾ��ִ� ĳ���͸� ��� �ı� �� �ٽ� ����.
        for (int i = 0; i < charInventoryTr.childCount; i++) idx++; // ���� �������� ĳ������ �� �˻�
        for (int i = 0; i < idx; i++)
        {
            // ���� ĳ���� â�� ĳ���Ͱ� ��ġ�Ǿ� �ִٸ� �ı�.
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
