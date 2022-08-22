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
    private CardDeck cardDeck;

    public int num;
    public enum CARDTYPE { 치유 = 0, 방어 = 1, 강화 = 2, 방해 = 3, 공격 = 4 }
    public CARDTYPE cardType;
    public TMP_Text nameText;
    public TMP_Text contentText;
    public TMP_Text typeText;

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
        contentText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardContent").GetComponent<TMP_Text>();
        typeText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardType").GetComponent<TMP_Text>();
        textAsset = Resources.Load<TextAsset>("CardData");
        json = JArray.Parse(textAsset.text);
        cardDeck = GameObject.Find("PUIManager").GetComponent<CardDeck>();
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
            GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].preset[equipIdx] = 0;
            GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].index = GameManager.instance.data.preset;

            switch (cardType)
            {
                case CARDTYPE.치유:
                    cardDeck.type_Heal--;
                    cardDeck.cardType[(int)CARDTYPE.치유].text = cardDeck.type_Heal.ToString();
                    break;
                case CARDTYPE.방어:
                    cardDeck.type_Shield--;
                    cardDeck.cardType[(int)CARDTYPE.방어].text = cardDeck.type_Shield.ToString();
                    break;
                case CARDTYPE.강화:
                    cardDeck.type_Buff--;
                    cardDeck.cardType[(int)CARDTYPE.강화].text = cardDeck.type_Buff.ToString();
                    break;
                case CARDTYPE.방해:
                    cardDeck.type_Debuff--;
                    cardDeck.cardType[(int)CARDTYPE.방해].text = cardDeck.type_Debuff.ToString();
                    break;
                case CARDTYPE.공격:
                    cardDeck.type_Attack--;
                    cardDeck.cardType[(int)CARDTYPE.공격].text = cardDeck.type_Attack.ToString();
                    break;
                default:
                    break;
            }

            GameManager.instance.PresetSave();
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
        contentText.text = json[num - 1]["Content_1_Kr"].ToString();
        typeText.text = "카드 속성 : " + cardType.ToString();
    }

    private void InitImage(Image img)
    {
        img.rectTransform.sizeDelta = new Vector2(100, 100);
    }
}
