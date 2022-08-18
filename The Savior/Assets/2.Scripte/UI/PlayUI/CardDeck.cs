using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardDeck : MonoBehaviour
{
    /*
     * Json ���Ͽ��� ������ �����͸� �������
     * haveCard�� �� ��ŭ ī�带 �����Ͽ� ī�� �� �κ��丮�� �ִ´�.
     */
    public Button[] presetButton = new Button[5];   // ������ ��ư

    public Transform equipTr;       // ī�尡 �����Ǿ��ִ� ��ġ
    public Transform cardDeckTr;    // ī�� ���� ��ġ
    public TMP_Text presetName;

    private Image cardImg;      // ���ҽ����� ������ ī��
    private GameObject changePreset;  // ������ ����ÿ� ����� ������Ʈ

    public TMP_Text[] cardType = new TMP_Text[5];
    public int type_Heal;
    public int type_Shield;
    public int type_Buff;
    public int type_Debuff;
    public int type_Attack;

    void Start()
    {
        // ���� ���۽� ������ ��ư ����
        for (int i = 0; i < presetButton.Length; i++)
        {
            int idx = i;
            presetButton[i].onClick.AddListener(() => OnClick_PresetChangeBtn(idx));
        }
        // ���� ���۽� �����Ϳ� ����Ǿ��ִ� ī�带 �κ��丮�� ����
        for (int i = 0; i < GameManager.instance.cardIdx - 1; i++)
        {
            if (GameManager.instance.card[i].haveCard != 0)
            {
                for (int j = 0; j < GameManager.instance.card[i].haveCard; j++)
                {
                    cardImg = Resources.Load<Image>("Card/Card_" + GameManager.instance.card[i].id);
                    cardImg = Instantiate(cardImg, cardDeckTr);
                }
            }
        }
        OnClick_PresetChangeBtn((GameManager.instance.data.preset -1));
    }

    public void OnClick_PresetChangeBtn(int n)
    {
        GameManager.instance.data.preset = (n + 1);
        presetName.text = GameManager.instance.data.presetName[n];
        type_Heal = 0;
        type_Shield = 0;
        type_Buff = 0;
        type_Debuff = 0;
        type_Attack = 0;
        // ���� �����¿� �������� ī�� ����
        for (int i = 0; i < equipTr.childCount; i++)
        {
            if (equipTr.GetChild(i).childCount != 0)
            {
                equipTr.GetChild(i).GetChild(0).transform.SetParent(cardDeckTr);
            }
        }
        // ���� �������� �����Ϳ� ���õ� ī�� ����
        for (int i = 0; i < equipTr.childCount; i++)
        {
            if (GameManager.instance.cardPreset[n].preset[i] == 0) continue;
            changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.cardPreset[n].preset[i].ToString() + "(Clone)");
            switch (changePreset.GetComponent<ViewCard>().cardType)
            {
                case ViewCard.CARDTYPE.ġ��:
                    type_Heal++;
                    break;
                case ViewCard.CARDTYPE.���:
                    type_Shield++;
                    break;
                case ViewCard.CARDTYPE.��ȭ:
                    type_Buff++;
                    break;
                case ViewCard.CARDTYPE.����:
                    type_Debuff++;
                    break;
                case ViewCard.CARDTYPE.����:
                    type_Attack++;
                    break;
                default:
                    break;
            }
            if (changePreset == null)
            {              
                Debug.Log("Error : " + i + "�� �ε���");
                GameManager.instance.cardPreset[n].preset[i] = 0;
                continue;
            }
            changePreset.transform.SetParent(equipTr.GetChild(i).transform);
            changePreset.GetComponent<Image>().rectTransform.anchorMin = new Vector2(0, 0);
            changePreset.GetComponent<Image>().rectTransform.anchorMax = new Vector2(1, 1);
            changePreset.GetComponent<Image>().rectTransform.offsetMin = Vector2.zero;
            changePreset.GetComponent<Image>().rectTransform.offsetMax = Vector2.zero;
            changePreset.transform.position = equipTr.GetChild(i).transform.position;
        }
    }
}
