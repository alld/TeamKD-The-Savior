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
            switch ((GameManager.instance.data.presset-1))
            {
                case 0:
                    GameManager.instance.data.equipCard1[this.transform.GetSiblingIndex()] = ViewCard.dragItem.GetComponent<ViewCard>().num;
                    break;
                case 1:
                    GameManager.instance.data.equipCard2[this.transform.GetSiblingIndex()] = ViewCard.dragItem.GetComponent<ViewCard>().num;
                    break;
                case 2:
                    GameManager.instance.data.equipCard3[this.transform.GetSiblingIndex()] = ViewCard.dragItem.GetComponent<ViewCard>().num;
                    break;
                case 3:
                    GameManager.instance.data.equipCard4[this.transform.GetSiblingIndex()] = ViewCard.dragItem.GetComponent<ViewCard>().num;
                    break;
                case 4:
                    GameManager.instance.data.equipCard5[this.transform.GetSiblingIndex()] = ViewCard.dragItem.GetComponent<ViewCard>().num;
                    break;
                default:
                    break;
            }
            
        }
    }
}
