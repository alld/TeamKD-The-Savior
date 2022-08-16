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
    public GameObject[] cardSetImg = new GameObject[5];

    public Transform cardDeckTr;    // ī�� ���� ��ġ
    private Image cardImg;      // ���ҽ����� ������ ī��

    void Start()
    {
        for (int i = 0; i < presetButton.Length; i++)
        {
            presetButton[i].onClick.AddListener(() => OnClick_PresetChangeBtn(i));
        }
        for (int i = 0; i < GameManager.instance.cardIdx-1; i++)
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

    }
}
