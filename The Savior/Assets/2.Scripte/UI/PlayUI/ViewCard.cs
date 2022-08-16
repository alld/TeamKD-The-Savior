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
    private int equipIdx;

    private void Start()
    {
        moveTr = GameObject.Find("GameUI").transform;
        tr = GetComponent<Transform>();
        curTr = GameObject.Find("PUIManager").GetComponent<CardDeck>().cardDeckTr;
        nameText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardName").GetComponent<TMP_Text>();
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
            switch ((GameManager.instance.data.presset-1))
            {
                case 0:
                    GameManager.instance.data.equipCard1[equipIdx] = 0;
                    break;
                case 1:
                    GameManager.instance.data.equipCard2[equipIdx] = 0;
                    break;
                case 2:
                    GameManager.instance.data.equipCard3[equipIdx] = 0;
                    break;
                case 3:
                    GameManager.instance.data.equipCard4[equipIdx] = 0;
                    break;
                case 4:
                    GameManager.instance.data.equipCard5[equipIdx] = 0;
                    break;
                default:
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        curIdx = tr.GetSiblingIndex();
        equipIdx = this.transform.parent.GetSiblingIndex();
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
