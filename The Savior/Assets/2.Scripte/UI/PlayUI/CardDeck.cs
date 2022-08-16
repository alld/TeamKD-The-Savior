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
            for (int j = 0; j < GameManager.instance.card[i].haveCard; j++)
            {
                cardImg = Resources.Load<Image>("Card/Card_" + GameManager.instance.card[i].id);
                cardImg = Instantiate(cardImg, cardDeckTr);
            }
        }
        OnClick_PresetChangeBtn((GameManager.instance.data.presset -1));
    }

    public void OnClick_PresetChangeBtn(int n)
    {
        GameManager.instance.data.presset = (n + 1);
        presetName.text = GameManager.instance.data.presetName[n];

        for (int i = 0; i < equipTr.childCount; i++)
        {
            if (equipTr.GetChild(i).childCount != 0)
            {
                equipTr.GetChild(i).GetChild(0).transform.SetParent(cardDeckTr);
            }
        }
        for (int i = 0; i < equipTr.childCount; i++)
        {
            switch (n)
            {
                case 0:
                    if (GameManager.instance.data.equipCard1[i] != 0)
                    {
                        changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.data.equipCard1[i].ToString() + "(Clone)");
                        if (changePreset == null)
                        {
                            Debug.Log("Error");
                            continue;
                        }
                        changePreset.transform.SetParent(equipTr.GetChild(i).transform);
                        changePreset.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
                        changePreset.transform.position = equipTr.GetChild(i).transform.position;
                    }
                    break;
                case 1:
                    if (GameManager.instance.data.equipCard2[i] != 0)
                    {
                        changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.data.equipCard2[i].ToString() + "(Clone)");
                        if (changePreset == null)
                        {
                            Debug.Log("Error");
                            continue;
                        }
                        changePreset.transform.SetParent(equipTr.GetChild(i).transform);
                        changePreset.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
                        changePreset.transform.position = equipTr.GetChild(i).transform.position;
                    }
                    break;
                case 2:
                    if (GameManager.instance.data.equipCard3[i] != 0)
                    {
                        changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.data.equipCard3[i].ToString() + "(Clone)");
                        if (changePreset == null)
                        {
                            Debug.Log("Error");
                            continue;
                        }
                        changePreset.transform.SetParent(equipTr.GetChild(i).transform);
                        changePreset.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
                        changePreset.transform.position = equipTr.GetChild(i).transform.position;
                    }
                    break;
                case 3:
                    if (GameManager.instance.data.equipCard4[i] != 0)
                    {
                        changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.data.equipCard4[i].ToString() + "(Clone)");
                        if (changePreset == null)
                        {
                            Debug.Log("Error");
                            continue;
                        }
                        changePreset.transform.SetParent(equipTr.GetChild(i).transform);
                        changePreset.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
                        changePreset.transform.position = equipTr.GetChild(i).transform.position;
                    }
                    break;
                case 4:
                    if (GameManager.instance.data.equipCard5[i] != 0)
                    {
                        changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.data.equipCard5[i].ToString() + "(Clone)");
                        if (changePreset == null)
                        {
                            Debug.Log("Error");
                            continue;
                        }
                        changePreset.transform.SetParent(equipTr.GetChild(i).transform);
                        changePreset.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
                        changePreset.transform.position = equipTr.GetChild(i).transform.position;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
