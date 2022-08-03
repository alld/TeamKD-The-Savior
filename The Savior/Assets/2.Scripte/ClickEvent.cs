using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerClickHandler
{
    InfoCharacter info;

    public void OnPointerClick(PointerEventData eventData)
    {
        info = GameObject.Find("PUIManager").GetComponent<InfoCharacter>();
        info.OnCharacterInfo();
    }

}
