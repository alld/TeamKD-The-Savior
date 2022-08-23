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

    public GameObject CardInventory;
    public Button[] presetButton = new Button[5];   // 프리셋 버튼

    public Transform equipTr;       // 카드가 장착되어있는 위치
    public Transform cardDeckTr;    // 카드 덱의 위치
    public TMP_Text presetName;

    private Image cardImg;      // 리소스에서 가져올 카드
    private GameObject changePreset;  // 프리셋 변경시에 사용할 오브젝트

    public TMP_Text[] cardType = new TMP_Text[5];
    public int type_Heal;
    public int type_Shield;
    public int type_Buff;
    public int type_Debuff;
    public int type_Attack;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.instance.isSetting);
        
        //CardInventory.SetActive(true);
        // 게임 시작시 프리셋 버튼 연결
        for (int i = 0; i < presetButton.Length; i++)
        {
            int idx = i;
            presetButton[i].onClick.AddListener(() => OnClick_PresetChangeBtn(idx));
        }
        // 게임 시작시 데이터에 저장되어있는 카드를 검색.
        for (int i = 1; i <= GameManager.instance.maxCardCount; i++)
        {
            // 해당 번호의 카드가 있다면
            if (GameManager.instance.cardDic.ContainsKey(i))
            {
                // 해당 카드의 개수만큼 반복하여 인벤토리에 생성.
                for (int j = 1; j <= GameManager.instance.cardDic[i]; j++)
                {
                    cardImg = Resources.Load<Image>("Card/Card_" + i);
                    cardImg = Instantiate(cardImg, cardDeckTr);
                }
            }
        }
        OnClick_PresetChangeBtn((GameManager.instance.data.preset - 1));
        //CardInventory.SetActive(false);
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
        // 이전 프리셋에 장착중인 카드 해제
        for (int i = 0; i < equipTr.childCount; i++)
        {
            if (equipTr.GetChild(i).childCount != 0)
            {
                equipTr.GetChild(i).GetChild(0).transform.SetParent(cardDeckTr);
            }
        }
        // 현재 프리셋의 데이터에 세팅된 카드 장착
        for (int i = 0; i < equipTr.childCount; i++)
        {
            if (GameManager.instance.cardPreset[n] == null) return;
            if (GameManager.instance.cardPreset[n].preset[i] == 0) continue;
            changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.cardPreset[n].preset[i].ToString() + "(Clone)");

            switch (changePreset.GetComponent<ViewCard>().cardType)
            {
                case ViewCard.CARDTYPE.치유:
                    type_Heal++;
                    break;
                case ViewCard.CARDTYPE.방어:
                    type_Shield++;
                    break;
                case ViewCard.CARDTYPE.강화:
                    type_Buff++;
                    break;
                case ViewCard.CARDTYPE.방해:
                    type_Debuff++;
                    break;
                case ViewCard.CARDTYPE.공격:
                    type_Attack++;
                    break;
                default:
                    break;
            }
            if (changePreset == null)
            {
                Debug.Log("Error : " + i + "번 인덱스");
                GameManager.instance.cardPreset[n].preset[i] = 0;
                continue;
            }
            changePreset.transform.SetParent(equipTr.GetChild(i).transform);
            changePreset.GetComponent<Image>().rectTransform.anchorMin = new Vector2(0, 0);
            changePreset.GetComponent<Image>().rectTransform.anchorMax = new Vector2(1, 1);
            changePreset.GetComponent<Image>().rectTransform.offsetMin = Vector2.zero;
            changePreset.GetComponent<Image>().rectTransform.offsetMax = Vector2.zero;
            changePreset.transform.position = equipTr.GetChild(i).transform.position;
            changePreset.GetComponent<ViewCard>().isSet = true;

            cardType[(int)ViewCard.CARDTYPE.치유].text = type_Heal.ToString();
            cardType[(int)ViewCard.CARDTYPE.방어].text = type_Shield.ToString();
            cardType[(int)ViewCard.CARDTYPE.강화].text = type_Buff.ToString();
            cardType[(int)ViewCard.CARDTYPE.방해].text = type_Debuff.ToString();
            cardType[(int)ViewCard.CARDTYPE.공격].text = type_Attack.ToString();
        }
    }
}
