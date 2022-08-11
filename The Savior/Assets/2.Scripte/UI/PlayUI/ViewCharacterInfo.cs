using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewCharacterInfo : MonoBehaviour, IPointerClickHandler
{
    private InfoCharacter info;
    private PartySettingManager manager;

    private Image thisImg;
    public int num;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 인벤토리에서 캐릭터 이미지가 클릭 되었을 때 자기 자신을 가져온다.
        thisImg = GetComponent<Image>();
        manager = GameObject.Find("PUIManager").GetComponent<PartySettingManager>();

        if (manager.isPartySettingMode == true)
        {
            Debug.Log("aaaa");
            manager.OnPartySetting(thisImg);
            manager.isPartySettingMode = false;
        }
        else
        {
            info = GameObject.Find("PUIManager").GetComponent<InfoCharacter>();
            info.OnCharacterInfo(thisImg, num);
        }        
    }
}
