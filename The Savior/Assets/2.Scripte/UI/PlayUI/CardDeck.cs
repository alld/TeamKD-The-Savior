using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardDeck : MonoBehaviour
{
    /*
     * Json 파일에서 가져온 데이터를 기반으로
     * haveCard의 수 만큼 카드를 생성하여 카드 덱 인벤토리에 넣는다.
     */
    public Button[] presetButton = new Button[5];   // 프리셋 버튼

    public Transform equipTr;       // 카드가 장착되어있는 위치
    public Transform cardDeckTr;    // 카드 덱의 위치
    public TMP_Text presetName;

    private Image cardImg;      // 리소스에서 가져올 카드
    private GameObject changePreset;  // 프리셋 변경시에 사용할 오브젝트

    void Start()
    {
        // 게임 시작시 프리셋 버튼 연결
        for (int i = 0; i < presetButton.Length; i++)
        {
            int idx = i;
            presetButton[i].onClick.AddListener(() => OnClick_PresetChangeBtn(idx));
        }
        // 게임 시작시 데이터에 저장되어있는 카드를 인벤토리에 생성
        for (int i = 0; i < GameManager.instance.cardIdx - 1; i++)
        {
            if (GameManager.instance.card[i].haveCard != 0)
            {
                for (int j = 0; j < GameManager.instance.card[i].haveCard; j++)
                {
                    cardImg = Resources.Load<Image>("Card/Card_" + GameManager.instance.card[i].id);
                    Debug.Log(cardImg);
                    cardImg = Instantiate(cardImg, cardDeckTr);
                }
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
