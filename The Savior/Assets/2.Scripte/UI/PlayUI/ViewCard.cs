using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;
public class ViewCard : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject dragItem = null;

    public int num;
    public TMP_Text nameText;

    private Transform tr;
    private Transform curTr;
    private Transform moveTr;
    private Image[] childImg;
    private TextAsset textAsset;
    private int curIdx;
    private int equipIdx;
    private JArray json;
    private void Start()
    {
        moveTr = GameObject.Find("GameUI").transform;
        tr = GetComponent<Transform>();
        curTr = GameObject.Find("PUIManager").GetComponent<CardDeck>().cardDeckTr;
        nameText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardName").GetComponent<TMP_Text>();
        textAsset = Resources.Load<TextAsset>("CardData");
        json = JArray.Parse(textAsset.text);
    }

    /*
     * 카드를 드래그 하면 카드가 이동한다.
     * 드롭 이벤트가 일어나지 않았다면 원래 자리로 돌아간다.
     */
    public void OnDrag(PointerEventData eventData)
    {
        tr.position = Input.mousePosition;
        tr.GetComponent<Image>().raycastTarget = false;
        childImg = tr.GetComponentsInChildren<Image>();
        for (int i = 0; i < childImg.Length; i++)
        {
            childImg[i].raycastTarget = false;
        }
        Debug.Log("드래그");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(moveTr);
        dragItem = this.gameObject;
        Debug.Log("드래그 하는 중");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 끝남");
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
    /// 카드를 클릭하면 카드의 정보를 출력한다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        nameText.text = json[num - 1]["Name_Kr"].ToString();
    }

    private void InitImage(Image img)
    {
        img.rectTransform.sizeDelta = new Vector2(100, 100);
    }
}
