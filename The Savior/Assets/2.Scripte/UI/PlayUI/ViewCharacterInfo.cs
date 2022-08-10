using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewCharacterInfo : MonoBehaviour, IPointerClickHandler
{
    private InfoCharacter info;
    private Image thisImg;
    public int num;


    public void OnPointerClick(PointerEventData eventData)
    {
        thisImg = GetComponent<Image>();
        info = GameObject.Find("PUIManager").GetComponent<InfoCharacter>();

        info.OnCharacterInfo(thisImg, num);

    }
}
