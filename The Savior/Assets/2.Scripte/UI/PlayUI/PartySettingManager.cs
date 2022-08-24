using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySettingManager : MonoBehaviour
{
    public Transform[] partyTr;             // ĳ���� ��â�� ��ġ.
    public GameObject characterInventory;   // ĳ���� �κ��丮

    // ���� ���۽ÿ� �̹� ��ġ�Ǿ��ִ� ĳ���͵��� �����͸� ������ ��ġ �� �̹���
    private Image character;

    // ��â�� ���� ĳ���� �κ��丮�� Ȱ��ȭ ���״°�?
    public bool isPartySettingMode = false;

    // ���� ������ ��Ƽ ��â�� ��ȣ. 0 ~ 3
    private int idx;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.instance.isSetting);
        StartCoroutine(PartySettingInit());
    }

    public IEnumerator PartySettingInit()
    {
        // ��Ƽ â�� ���� ��ŭ �ݺ� �Ͽ� �˻�.
        for (int i = 0; i < GameManager.instance.data.equipCharacter.Length; i++)
        {
            // ������ �ʱ�ȭ �� ������ ���� �Ǿ��ִ� ĳ������ �̹��� �ı� �ؾ���.
            if(partyTr[i].childCount != 0)
            {
                Destroy(partyTr[i].GetChild(0).gameObject);
            }

            //Debug.Log(GameManager.instance.data.haveCharacter[i]);
            //Debug.Log(GameManager.instance.data.equipCharacter[i]);
            // ���� ĳ���͸� ���� ���� �����Ͱ� ���� ��.
            if (GameManager.instance.data.equipCharacter[i] != 0)
            {
                character = Resources.Load<Image>("Unit/Image/Character_" + GameManager.instance.data.equipCharacter[i].ToString());
                character = Instantiate(character, partyTr[i]);
                InitRect(character);
                Destroy(character.GetComponent<ViewCharacterInfo>());
            }
        }
        yield return null;
    }


    // ��Ƽ ��â�� ���� �� ȣ��Ǵ� �Լ�, �ش� ��â�� �ε����� �ް� ��Ƽ ���� ���� �κ��丮�� �����Ѵ�.
    public void OnPartySettingMode(int index)
    {
        characterInventory.SetActive(true);
        isPartySettingMode = true;
        idx = index;
    }

    // ��Ƽ ���� ���� �κ��丮�� ���� ���� �� ĳ���͸� Ŭ���ϸ� ȣ��Ǵ� �Լ�.
    public void OnPartySetting(Image img)
    {
        Image copy = Instantiate(img);

        // �̹� ��ġ�� ĳ���Ͱ� ���� ĳ������ ��� 
        // ��ġ�� ĳ���͸� �ı��ϰ�, ����� �����͸� 0���� �ٲ۴�.
        if (GameManager.instance.data.equipCharacter[idx] == copy.GetComponent<ViewCharacterInfo>().num)
        {
            GameManager.instance.data.equipCharacter[idx] = 0;
            Destroy(copy.gameObject);
            Destroy(partyTr[idx].GetChild(0).gameObject);
            characterInventory.SetActive(false);
            return;
        }


        // �̹� �ش� ĳ���Ͱ� �����Ǿ� �ִٸ� �����Ǿ� �ִ� ĳ���͸� �ı��ϰ� �����Ϳ� ����� ���� 0���� �ٲ۴�.
        for (int i = 0; i < GameManager.instance.data.equipCharacter.Length; i++)
        {

            if (GameManager.instance.data.equipCharacter[i] == copy.GetComponent<ViewCharacterInfo>().num)
            {
                GameManager.instance.data.equipCharacter[i] = 0;
                Destroy(partyTr[i].GetChild(0).gameObject);
                break;
            }
        }

        
        // ���� ������ ĳ���Ͱ� ���ٸ�...
        // �ش� �ε����� ĳ���͸� ��ġ�ϰ� �����͸� �����Ѵ�.
        if (partyTr[idx].childCount == 0)
        {
            copy.transform.SetParent(partyTr[idx]);
            InitRect(copy);
            GameManager.instance.data.equipCharacter[idx] = copy.GetComponent<ViewCharacterInfo>().num;
            Destroy(copy.GetComponent<ViewCharacterInfo>());
        }
        // ���� ������ ĳ���Ͱ� �ִٸ�...
        // �ش� �ε����� ��ġ�� ĳ���͸� �ı��ϰ� �ش� ��ġ�� ĳ���͸� �����Ѵ�.
        else if (partyTr[idx].childCount > 0)
        {
            Destroy(partyTr[idx].GetChild(0).gameObject);
            copy.transform.SetParent(partyTr[idx]);
            InitRect(copy);
            GameManager.instance.data.equipCharacter[idx] = copy.GetComponent<ViewCharacterInfo>().num;
            Destroy(copy.GetComponent<ViewCharacterInfo>());
        }
        characterInventory.SetActive(false);
        //GameManager.instance.GameSave();
    }
    //
    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = Vector2.zero;
        img.rectTransform.offsetMin = Vector2.zero;
    }
}