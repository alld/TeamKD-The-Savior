using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCard : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// 드래그한 카드가 해당 슬롯 위에서 드래그가 끝났을 경우 슬롯의 자식 객체로 넣는다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && ViewCard.dragItem != null)
        {
            ViewCard.dragItem.transform.SetParent(this.transform);
            ViewCard.dragItem.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
            ViewCard.dragItem.transform.position = this.transform.position;

            Debug.Log("카드 장착");
        }
    }
}
