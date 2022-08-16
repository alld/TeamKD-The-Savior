using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    /*
     * Json ���Ͽ��� ������ �����͸� �������
     * haveCard�� �� ��ŭ ī�带 �����Ͽ� ī�� �� �κ��丮�� �ִ´�.
     */
    public Button[] presetButton = new Button[5];   // ������ ��ư

    public Transform equipTr;       // ī�尡 �����Ǿ��ִ� ��ġ
    public Transform cardDeckTr;    // ī�� ���� ��ġ

    private Image cardImg;      // ���ҽ����� ������ ī��
    private GameObject changePreset;  // ������ ����ÿ� ����� ������Ʈ

    void Start()
    {
        for (int i = 0; i < presetButton.Length; i++)
        {
            int idx = i;
            presetButton[i].onClick.AddListener(() => OnClick_PresetChangeBtn(idx));
        }
        for (int i = 0; i < GameManager.instance.cardIdx - 1; i++)
        {
            for (int j = 0; j < GameManager.instance.card[i].haveCard; j++)
            {
                cardImg = Resources.Load<Image>("Card/Card_" + GameManager.instance.card[i].id);
                cardImg = Instantiate(cardImg, cardDeckTr);
            }
        }
    }

    public void OnClick_PresetChangeBtn(int n)
    {
        GameManager.instance.data.presset = (n + 1);

        for (int i = 0; i < equipTr.childCount; i++)
        {
            equipTr.GetChild(i).GetChild(0).transform.SetParent(cardDeckTr);
            if (GameManager.instance.data.equipCard[n, i] != 0)
            {
                changePreset = GameObject.Find("Card_" + GameManager.instance.data.equipCard[n, i].ToString());
                
            }
        }
    }
}
