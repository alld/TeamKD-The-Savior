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
        // �κ��丮���� ĳ���� �̹����� Ŭ�� �Ǿ��� �� �ڱ� �ڽ��� �����´�.
        thisImg = GetComponent<Image>();
        manager = GameObject.Find("PUIManager").GetComponent<PartySettingManager>();

        if (manager.isPartySettingMode == true)
        {
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