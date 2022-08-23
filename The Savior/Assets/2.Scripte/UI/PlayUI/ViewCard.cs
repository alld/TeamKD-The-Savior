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

    public bool isSet = false;
    public enum CARDTYPE { ġ�� = 0, ��� = 1, ��ȭ = 2, ���� = 3, ���� = 4 }
    public CARDTYPE cardType;
    public TMP_Text nameText;
    public TMP_Text contentText;
    public TMP_Text typeText;

    private Transform tr;
    private Transform curTr;
    private Transform moveTr;
    private Transform actTr;
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
        actTr = GameObject.Find("PUIManager").GetComponent<CardDeck>().equipTr;
        nameText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardName").GetComponent<TMP_Text>();
        contentText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardContent").GetComponent<TMP_Text>();
        typeText = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/CardInfo/CardBoxImage/CardType").GetComponent<TMP_Text>();
        textAsset = Resources.Load<TextAsset>("CardData");
        json = JArray.Parse(textAsset.text);
        cardDeck = GameObject.Find("PUIManager").GetComponent<CardDeck>();
    }

    /*
     * ī�带 �巡�� �ϸ� ī�尡 �̵��Ѵ�.
     * ��� �̺�Ʈ�� �Ͼ�� �ʾҴٸ� ���� �ڸ��� ���ư���.
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
            //isSet = false;
            switch (cardType)
            {
                case CARDTYPE.ġ��:
                    cardDeck.type_Heal--;
                    cardDeck.cardType[(int)CARDTYPE.ġ��].text = cardDeck.type_Heal.ToString();
                    break;
                case CARDTYPE.���:
                    cardDeck.type_Shield--;
                    cardDeck.cardType[(int)CARDTYPE.���].text = cardDeck.type_Shield.ToString();
                    break;
                case CARDTYPE.��ȭ:
                    cardDeck.type_Buff--;
                    cardDeck.cardType[(int)CARDTYPE.��ȭ].text = cardDeck.type_Buff.ToString();
                    break;
                case CARDTYPE.����:
                    cardDeck.type_Debuff--;
                    cardDeck.cardType[(int)CARDTYPE.����].text = cardDeck.type_Debuff.ToString();
                    break;
                case CARDTYPE.����:
                    cardDeck.type_Attack--;
                    cardDeck.cardType[(int)CARDTYPE.����].text = cardDeck.type_Attack.ToString();
                    break;
                default:
                    break;
            }

            StartCoroutine(GameManager.instance.PresetSave());
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
        // �� Ŭ���� ī���� ������ ����Ѵ�.
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            nameText.text = json[num - 1]["Name_Kr"].ToString();
            contentText.text = json[num - 1]["Content_1_Kr"].ToString();
            typeText.text = "ī�� �Ӽ� : " + cardType.ToString();
        }
        // �� Ŭ���� ī�� Ż��
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // �κ��丮�� �� �ڸ��� �ִٸ� ī�� ����.
            if (!isSet)
            {
                for (int i = 0; i < actTr.childCount; i++)
                {
                    if (actTr.GetChild(i).childCount == 0)
                    {
                        isSet = true;
                        this.transform.SetParent(actTr.GetChild(i));
                        InitImage(this.GetComponent<Image>());
                        this.transform.position = actTr.GetChild(i).position;

                        GameManager.instance.cardPreset[GameManager.instance.data.preset - 1].preset[i] = this.num;
                        switch (this.cardType)
                        {
                            case CARDTYPE.ġ��:
                                cardDeck.type_Heal++;
                                cardDeck.cardType[(int)ViewCard.CARDTYPE.ġ��].text = cardDeck.type_Heal.ToString();
                                break;
                            case CARDTYPE.���:
                                cardDeck.type_Shield++;
                                cardDeck.cardType[(int)ViewCard.CARDTYPE.���].text = cardDeck.type_Shield.ToString();
                                break;
                            case CARDTYPE.��ȭ:
                                cardDeck.type_Buff++;
                                cardDeck.cardType[(int)ViewCard.CARDTYPE.��ȭ].text = cardDeck.type_Buff.ToString();
                                break;
                            case CARDTYPE.����:
                                cardDeck.type_Debuff++;
                                cardDeck.cardType[(int)ViewCard.CARDTYPE.����].text = cardDeck.type_Debuff.ToString();
                                break;
                            case CARDTYPE.����:
                                cardDeck.type_Attack++;
                                cardDeck.cardType[(int)ViewCard.CARDTYPE.����].text = cardDeck.type_Attack.ToString();
                                break;
                            default:
                                break;
                        }
                        StartCoroutine(GameManager.instance.PresetSave());
                        break;
                    }

                }
            }

            // �������� ī�� �����ϱ�.

        }
    }

    private void InitImage(Image img)
    {
        img.rectTransform.sizeDelta = new Vector2(100, 100);
    }
}
