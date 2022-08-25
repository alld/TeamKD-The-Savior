using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropCard : MonoBehaviour, IDropHandler
{
    private CardDeck cardDeck;
    private void Start()
    {
        cardDeck = GameObject.Find("PUIManager").GetComponent<CardDeck>();
    }
    /// <summary>
    /// 드래그한 카드가 해당 슬롯 위에서 드래그가 끝났을 경우 슬롯의 자식 객체로 넣는다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && ViewCard.dragItem != null)
        {
            ViewCard.dragItem.transform.SetParent(this.transform);
            ViewCard.dragItem.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(80, 113);
            ViewCard.dragItem.transform.position = this.transform.position;

            GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].preset[this.transform.GetSiblingIndex()] = ViewCard.dragItem.GetComponent<ViewCard>().num;
            GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].index = GameManager.instance.data.preset;
            if (!ViewCard.dragItem.GetComponent<ViewCard>().isSet)
            {
                ViewCard.dragItem.GetComponent<ViewCard>().isSet = true;
                switch (ViewCard.dragItem.GetComponent<ViewCard>().cardType)
                {
                    case ViewCard.CARDTYPE.치유:
                        cardDeck.type_Heal++;
                        cardDeck.cardType[(int)ViewCard.CARDTYPE.치유].text = cardDeck.type_Heal.ToString();
                        break;
                    case ViewCard.CARDTYPE.방어:
                        cardDeck.type_Shield++;
                        cardDeck.cardType[(int)ViewCard.CARDTYPE.방어].text = cardDeck.type_Shield.ToString();
                        break;
                    case ViewCard.CARDTYPE.강화:
                        cardDeck.type_Buff++;
                        cardDeck.cardType[(int)ViewCard.CARDTYPE.강화].text = cardDeck.type_Buff.ToString();
                        break;
                    case ViewCard.CARDTYPE.방해:
                        cardDeck.type_Debuff++;
                        cardDeck.cardType[(int)ViewCard.CARDTYPE.방해].text = cardDeck.type_Debuff.ToString();
                        break;
                    case ViewCard.CARDTYPE.공격:
                        cardDeck.type_Attack++;
                        cardDeck.cardType[(int)ViewCard.CARDTYPE.공격].text = cardDeck.type_Attack.ToString();
                        break;
                    default:
                        break;
                }
            }
            StartCoroutine(GameManager.instance.PresetSave());
        }
    }
}
