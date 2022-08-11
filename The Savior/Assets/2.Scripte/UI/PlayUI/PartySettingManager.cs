using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySettingManager : MonoBehaviour
{
    public Transform[] partyTr;
    public GameObject characterInventory;

    public bool isPartySettingMode = false;

    private int idx;

    public void OnPartySettingMode(int index)
    {
        characterInventory.SetActive(true);
        isPartySettingMode = true;
        idx = index;
    }

    public void OnPartySetting(Image img)
    {
        // 현재 장착된 캐릭터가 없다면...
        if (partyTr[idx].childCount == 0)
        {
            Image copy = Instantiate(img, partyTr[idx]);
            //for(int i = 0; i < 4; i++)
            //{
            //    if(GameManager.instance.data.equipCharacter[i] == copy.GetComponent<ViewCharacterInfo>().num)
            //    {
            //        GameManager.instance.data.equipCharacter[i] = 0;
            //        Destroy(partyTr[i].GetChild(0).gameObject);
            //    }
            //}
            GameManager.instance.data.equipCharacter[idx] = copy.GetComponent<ViewCharacterInfo>().num;
            Destroy(copy.GetComponent<ViewCharacterInfo>());
        }
        // 현재 장착된 캐릭터가 있다면...
        else if (partyTr[idx].childCount > 0)
        {
            Destroy(partyTr[idx].GetChild(0).gameObject);
            Image copy = Instantiate(img, partyTr[idx]);
            //for (int i = 0; i < 4; i++)
            //{
            //    if (GameManager.instance.data.equipCharacter[i] == copy.GetComponent<ViewCharacterInfo>().num)
            //    {
            //        GameManager.instance.data.equipCharacter[i] = 0;
            //        Destroy(partyTr[i].GetChild(0).gameObject);
            //    }
            //}
            GameManager.instance.data.equipCharacter[idx] = copy.GetComponent<ViewCharacterInfo>().num;
            Destroy(copy.GetComponent<ViewCharacterInfo>());
        }
        characterInventory.SetActive(false);
    }
}
