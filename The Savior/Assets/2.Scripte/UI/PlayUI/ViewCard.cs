using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ViewCard : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject dragItem = null;

    public int num;
    public TMP_Text nameText;

    private Transform tr;
    private Transform curTr;
    private Transform moveTr;
    private int curIdx;

    private void Start()
    {
        moveTr = GameObject.Find("GameUI").transform;
        tr = GetComponent<Transform>();
        curTr = this.gameObject.transform.parent;
        nameText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardName").GetComponent<TMP_Text>();
        Debug.Log(nameText.name);
    }

    /*
     * ī�带 �巡�� �ϸ� ī�尡 �̵��Ѵ�.
     * ��� �̺�Ʈ�� �Ͼ�� �ʾҴٸ� ���� �ڸ��� ���ư���.
     */
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
            Debug.Log("���� ����");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        curIdx = tr.GetSiblingIndex();
    }

    /// <summary>
    /// ī�带 Ŭ���ϸ� ī���� ������ ����Ѵ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        nameText.text = this.name;
    }

    private void InitImage(Image img)
    {
        img.rectTransform.sizeDelta = new Vector2(100, 100);
    }
}
