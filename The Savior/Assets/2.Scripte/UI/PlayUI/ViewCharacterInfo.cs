using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewCharacterInfo : MonoBehaviour, IPointerClickHandler
{
    private InfoCharacter info;
    private Image thisImg;

    public void OnPointerClick(PointerEventData eventData)
    {
        thisImg = GetComponent<Image>();
        info = GameObject.Find("PUIManager").GetComponent<InfoCharacter>();

        Image copyImg = Instantiate(thisImg);
        info.OnCharacterInfo(copyImg);
    }
}
