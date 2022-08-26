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

    public GameObject CardInventory;
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

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.instance.isSetting);
        //CardInventory.SetActive(true);
        // ���� ���۽� ������ ��ư ����
        for (int i = 0; i < presetButton.Length; i++)
        {
            int idx = i;
            presetButton[i].onClick.AddListener(() => OnClick_PresetChangeBtn(idx));
        }

        StartCoroutine(CardSettingInit());
        
        OnClick_PresetChangeBtn((GameManager.instance.data.preset - 1));

        //CardInventory.SetActive(false);
    }

    public IEnumerator DestroyCardDeck()
    {
        for (int i = 0; i < 5; i++)     // �������� ī���� Ÿ�� ������ �˷��ִ� �ؽ�Ʈ �ʱ�ȭ.
        {
            cardType[i].text = "0";
        }

        for(int i = 0; i < cardDeckTr.childCount; i++)
        {
            Destroy(cardDeckTr.GetChild(i).gameObject);
        }


        yield return null;
    }

    public IEnumerator CardSettingInit()
    {
        for (int i = 0; i < 5; i++)
        {
            cardType[i].text = "0";
        }

        // ���� ���۽� �����Ϳ� ����Ǿ��ִ� ī�带 �˻�.
        for (int i = 1; i <= GameManager.instance.maxCardCount; i++)
        {
            // �ش� ��ȣ�� ī�尡 �ִٸ�
            if (GameManager.instance.cardDic.ContainsKey(i))
            {
                // �ش� ī���� ������ŭ �ݺ��Ͽ� �κ��丮�� ����.
                for (int j = 1; j <= GameManager.instance.cardDic[i]; j++)
                {
                    cardImg = Resources.Load<Image>("Card/Card_" + i);
                    cardImg = Instantiate(cardImg, cardDeckTr);
                }
            }
        }
        yield return null;
    }

    public IEnumerator PresetInit(int n)
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
            if (GameManager.instance.cardPreset[n] == null) yield break;
            if (GameManager.instance.cardPreset[n].preset[i] == 0) continue;
            changePreset = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content/Card_" + GameManager.instance.cardPreset[n].preset[i].ToString() + "(Clone)");
            switch (changePreset.GetComponent<ViewCard>().cardType)
            {
                case ViewCard.CARDTYPE.ġ��:
                    type_Heal++;
                    cardType[(int)ViewCard.CARDTYPE.ġ��].text = type_Heal.ToString();
                    break;
                case ViewCard.CARDTYPE.���:
                    type_Shield++;
                    cardType[(int)ViewCard.CARDTYPE.���].text = type_Shield.ToString();
                    break;
                case ViewCard.CARDTYPE.��ȭ:
                    type_Buff++;
                    cardType[(int)ViewCard.CARDTYPE.��ȭ].text = type_Buff.ToString();
                    break;
                case ViewCard.CARDTYPE.����:
                    type_Debuff++;
                    cardType[(int)ViewCard.CARDTYPE.����].text = type_Debuff.ToString();
                    break;
                case ViewCard.CARDTYPE.����:
                    type_Attack++;
                    cardType[(int)ViewCard.CARDTYPE.����].text = type_Attack.ToString();
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
            changePreset.GetComponent<ViewCard>().isSet = true;
        }
        yield return null;
    }

    public void OnClick_PresetChangeBtn(int n)
    {
        StartCoroutine(PresetInit(n));
    }
}
