using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewCard : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject dragItem = null;

    public int num;

    private Transform tr;
    private Transform curTr;
    private Transform moveTr;
    private Transform cardDeckTr;
    private int curIdx;



    private void Start()
    {
        cardDeckTr = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/DeckSetting_Active/SsttingBoxImage").transform;
        moveTr = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/DeckSetting_Active").transform;
        tr = GetComponent<Transform>();
        curTr = this.gameObject.transform.parent;
        curIdx = tr.transform.GetSiblingIndex();
    }

    public void OnDrag(PointerEventData eventData)
    {
        tr.position = Input.mousePosition;
        tr.GetComponent<Image>().raycastTarget = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(moveTr);
        dragItem = this.gameObject;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragItem = null;
        tr.GetComponent<Image>().raycastTarget = true;
        if (tr.parent == moveTr)
        {
            tr.SetParent(curTr);
            tr.SetSiblingIndex(curIdx);
            Debug.Log("ÀåÂø ½ÇÆÐ");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    private void InitImage(Image img)
    {
        img.rectTransform.sizeDelta = new Vector2(100, 100);
    }

}
