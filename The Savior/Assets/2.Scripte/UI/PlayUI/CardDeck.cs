using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    /*
     * Json 파일에서 가져온 데이터를 기반으로
     * haveCard의 수 만큼 카드를 생성하여 카드 덱 인벤토리에 넣는다.
     */
    public Button[] presetButton = new Button[5];   // 프리셋 버튼

    public Transform equipTr;       // 카드가 장착되어있는 위치
    public Transform cardDeckTr;    // 카드 덱의 위치

    private Image cardImg;      // 리소스에서 가져올 카드
    private GameObject changePreset;  // 프리셋 변경시에 사용할 오브젝트

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
