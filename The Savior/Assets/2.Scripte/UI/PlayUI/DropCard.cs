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

            GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].preset[this.transform.GetSiblingIndex()] = ViewCard.dragItem.GetComponent<ViewCard>().num;
            GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].index = GameManager.instance.data.preset;
            switch (ViewCard.dragItem.GetComponent<ViewCard>().cardType)
            {
                case ViewCard.CARDTYPE.ġ��:
                    cardDeck.type_Heal++;
                    cardDeck.cardType[(int)ViewCard.CARDTYPE.ġ��].text = cardDeck.type_Heal.ToString();
                    break;
                case ViewCard.CARDTYPE.���:
                    cardDeck.type_Shield++;
                    cardDeck.cardType[(int)ViewCard.CARDTYPE.���].text = cardDeck.type_Shield.ToString();
                    break;
                case ViewCard.CARDTYPE.��ȭ:
                    cardDeck.type_Buff++;
                    cardDeck.cardType[(int)ViewCard.CARDTYPE.��ȭ].text = cardDeck.type_Buff.ToString();
                    break;
                case ViewCard.CARDTYPE.����:
                    cardDeck.type_Debuff++;
                    cardDeck.cardType[(int)ViewCard.CARDTYPE.����].text = cardDeck.type_Debuff.ToString();
                    break;
                case ViewCard.CARDTYPE.����:
                    cardDeck.type_Attack++;
                    cardDeck.cardType[(int)ViewCard.CARDTYPE.����].text = cardDeck.type_Attack.ToString();
                    break;
                default:
                    break;
            }
            GameManager.instance.PresetSave();
        }
    }
}
