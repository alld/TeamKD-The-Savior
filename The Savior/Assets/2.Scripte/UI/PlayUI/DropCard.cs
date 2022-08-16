using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCard : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// �巡���� ī�尡 �ش� ���� ������ �巡�װ� ������ ��� ������ �ڽ� ��ü�� �ִ´�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && ViewCard.dragItem != null)
        {
            ViewCard.dragItem.transform.SetParent(this.transform);
            ViewCard.dragItem.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
            ViewCard.dragItem.transform.position = this.transform.position;

            Debug.Log("ī�� ����");
        }
    }
}
