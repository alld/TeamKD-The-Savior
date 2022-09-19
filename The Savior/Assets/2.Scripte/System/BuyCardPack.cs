using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class BuyCardPack : MonoBehaviour
{
    [Header("상점 카드팩 리스트 위치")]
    public Transform shopCardPackTr;

    [Header("카드팩 이미지 위치")]
    public GameObject unPack;
    public Transform shopCardPackImageTr;
    public Transform[] cardList;

    [Header("구매한 카드들의 위치")]
    public GameObject openPack;
    public Transform[] buyCardList;
    public Button confirmButton;

    [Header("카드팩 이미지 리스트")]
    public Image[] cardPackImg;
    private Image[] cardImg = new Image[5];

    // instantiate로 생성하여 사용할 이미지
    private Image pack; // 팩 이미지
    private Image card; // 카드 이미지

    private Transform cardDectTr;


    private int maxCardPack = 3;
    private List<GameObject> cardPackList = new List<GameObject>();
    private GameObject thisObject;
    private TextMeshProUGUI packName;
    private TextMeshProUGUI packContant;
    private Button[] selectPackButton;
    private TextAsset cardPackData;
    private bool isBuy = false;
    private int curPack = 0;
    private int firstNum = 0;
    private int lastNum = 0;

    private void Start()
    {
        selectPackButton = new Button[maxCardPack];
        unPack.SetActive(true);
        OnClick_SelectPackBtn(1);
        confirmButton.onClick.AddListener(() => OnClick_ConfirmBtn());
        cardDectTr = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content").transform;
        cardPackData = Resources.Load<TextAsset>("CardDB/CardPackContent");

        JArray packData = JArray.Parse(cardPackData.text);

        // 상점 창 오픈 시에 호출됨.
        // 상점에 카드 팩이 생성된다.
        for (int i = 0; i < maxCardPack; i++)
        {
            int idx = i + 1;
            cardPackList.Add(Resources.Load<GameObject>("PackItem"));
            packName = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[0];
            packContant = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[1];

            packName.text = packData[i]["CardPack"].ToString();        // 카드팩 이름
            packContant.text = packData[i]["CardContent"].ToString();        // 카드팩 설명

            thisObject = Instantiate(cardPackList[i], shopCardPackTr);  // 프리팹에 있는 카드팩을 상점에 생성
            thisObject.gameObject.name = "Pack_" + i;                   // 생성한 오브젝트의 이름
            selectPackButton[i] = thisObject.GetComponentInChildren<Button>();  // 생성한 오브젝트에 버튼 연결
            selectPackButton[i].onClick.AddListener(() => OnClick_SelectPackBtn(idx));  // 생성한 오브젝트를 클릭시 호출되는 함수

        }
    }

    /// <summary>
    /// 카드를 사고 난 후 이전상태로 초기화.
    /// </summary>
    public void OpenShop()
    {
        unPack.SetActive(true);
        openPack.SetActive(false);
    }

    /// <summary>
    /// 생성된 카드팩을 파괴함.
    /// </summary>
    public void InitCardPack()
    {
        if (shopCardPackImageTr.childCount > 0) Destroy(shopCardPackImageTr.GetChild(0).gameObject);
        for (int i = 0; i < 5; i++)
        {
            if (cardList[i].transform.childCount > 0)
            {
                Destroy(cardList[i].GetChild(0).gameObject);
            }
        }
    }

    /// <summary>
    /// 카드 상점 리스트에서 카드팩을 누를 경우 해당 카드팩의 정보를 출력함.
    /// </summary>
    /// <param name="idx"></param>
    private void OnClick_SelectPackBtn(int idx)
    {
        curPack = idx;
        InitCardPack();
        switch (idx)
        {
            case 1:
                HealCardPack();
                firstNum = 1;
                lastNum = 8;
                break;
            case 2:
                AttackCardPack();
                firstNum = 16;
                lastNum = 24;
                break;
            case 3:
                ShieldCardPack();
                firstNum = 8;
                lastNum = 16;
                break;
            default:
                break;
        }

        pack = Instantiate(cardPackImg[(idx - 1)], shopCardPackImageTr);
        InitRect(pack);

        openPack.SetActive(false);
        unPack.SetActive(true);
    }

    private void HealCardPack()
    {
        for (int i = 0; i < 5; i++)
        {
            cardImg[i] = Resources.Load<Image>("Card/Card_" + (i + 1).ToString());
            card = Instantiate(cardImg[i], cardList[i]);
            Destroy(card.GetComponent<ViewCard>());     // 구매한 카드의 드래그를 막기 위함.
            InitRect(card);
        }
    }

    private void ShieldCardPack()
    {
        for (int i = 0; i < 5; i++)
        {
            cardImg[i] = Resources.Load<Image>("Card/Card_" + (i + 8).ToString());
            card = Instantiate(cardImg[i], cardList[i]);
            Destroy(card.GetComponent<ViewCard>());     // 구매한 카드의 드래그를 막기 위함.
            InitRect(card);
        }
    }
    private void AttackCardPack()
    {
        for (int i = 0; i < 5; i++)
        {
            cardImg[i] = Resources.Load<Image>("Card/Card_" + (i + 16).ToString());
            card = Instantiate(cardImg[i], cardList[i]);
            Destroy(card.GetComponent<ViewCard>());     // 구매한 카드의 드래그를 막기 위함.
            InitRect(card);
        }
    }

    /// <summary>
    /// 확인 버튼이 눌릴 경우 호출
    /// </summary>
    private void OnClick_ConfirmBtn()
    {
        DestroyCard();
        unPack.SetActive(true);
        openPack.SetActive(false);
    }

    /// <summary>
    /// 이미지의 사이즈를 부모 객체의 사이즈에 맞춤.
    /// </summary>
    /// <param name="img"></param>
    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = Vector2.zero;
        img.rectTransform.offsetMin = Vector2.zero;
    }

    /// <summary>
    /// 카드 구매시 호출되는 함수.
    /// </summary>
    public IEnumerator BuyCard()
    {
        unPack.SetActive(false);
        openPack.SetActive(true);
        isBuy = true;

        for (int i = 0; i < 5; i++)
        {
            int rnd = Random.Range(firstNum, lastNum);
            cardImg[i] = Resources.Load<Image>("Card/Card_" + (rnd).ToString());
            Instantiate(cardImg[i], cardDectTr);
            card = Instantiate(cardImg[i], buyCardList[i]);
            Destroy(card.GetComponent<ViewCard>());     // 구매한 카드의 드래그를 막기 위함.

            // 획득한 카드의 데이터 저장.
            if (GameManager.instance.cardDic.ContainsKey(rnd))
            {
                GameManager.instance.cardDic[rnd] += 1;
            }
            else if (!GameManager.instance.cardDic.ContainsKey(rnd))
            {
                GameManager.instance.cardDic.Add(rnd, 1);
            }
            yield return StartCoroutine(GameManager.instance.SaveOrReviseCardData(rnd, GameManager.instance.cardDic[rnd]));
        }
    }

    public void DestroyCard()
    {
        if (!isBuy) return;
        for (int i = 0; i < 5; i++)
        {
            Destroy(buyCardList[i].GetChild(0).gameObject);
        }
        isBuy = false;
    }
}