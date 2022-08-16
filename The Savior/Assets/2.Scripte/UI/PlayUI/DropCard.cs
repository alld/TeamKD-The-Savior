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
