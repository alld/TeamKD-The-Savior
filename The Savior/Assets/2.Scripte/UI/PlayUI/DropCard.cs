using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCard : MonoBehaviour, IDropHandler
{
    private Image img;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            img = ViewCard.dragItem.GetComponent<Image>();
            img.transform.SetParent(this.transform);
            img.rectTransform.sizeDelta = new Vector2(100, 100);
            img.transform.position = this.transform.position;

            Debug.Log("!!!!!");
        }
    }
}
