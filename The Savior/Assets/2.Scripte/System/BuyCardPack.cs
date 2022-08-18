using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


    private int maxCardPack = 5;
    private List<GameObject> cardPackList = new List<GameObject>();
    private GameObject thisObject;
    private TextMeshProUGUI packName;
    private TextMeshProUGUI packContant;
    private Button[] selectPackButton;

    private void Start()
    {
        selectPackButton = new Button[maxCardPack];
        unPack.SetActive(true);
        OnClick_SelectPackBtn(1);
        confirmButton.onClick.AddListener(() => OnClick_ConfirmBtn());

        for (int i = 0; i < maxCardPack; i++)
        {
            // 제이슨 데이터를 연결하여 사용.
            // 나중에 필요해지면 연결.
            //jsonData = Resources.Load<TextAsset>("CardPack");
            //json = jsonData.text;
            //var data = JSON.Parse(json);

            int idx = i + 1;
            cardPackList.Add(Resources.Load<GameObject>("PackItem"));
            packName = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[0];
            packContant = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[1];

            packName.text = idx.ToString() + "번 카드팩";        // 카드팩 이름
            packContant.text = "카드팩 설명";        // 카드팩 설명

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
        InitCardPack();
        InitBuyCard();
        for (int i = 0; i < 5; i++)
        {
            cardImg[i] = Resources.Load<Image>("Card/Card_"+ (i + 1).ToString());
            card = Instantiate(cardImg[i], cardList[i]);
            InitRect(card);
        }

        pack = Instantiate(cardPackImg[(idx - 1)], shopCardPackImageTr);
        InitRect(pack);

        openPack.SetActive(false);
        unPack.SetActive(true);
    }

    /// <summary>
    /// 확인 버튼이 눌릴 경우 호출
    /// </summary>
    private void OnClick_ConfirmBtn()
    {
        InitBuyCard();
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
    public void BuyCard()
    {
        ViewCard viewCard;
        unPack.SetActive(false);
        openPack.SetActive(true);
        // 난수를 생성해서 리스트에서 랜덤하게 가져오게 할 예정이지만
        // 현재 카드가 3장 밖에 없어서 그냥 넣겠읍니다.
        for (int i = 0; i < 5; i++)
        {
            int rnd = Random.Range(1, 24);
            cardImg[i] = Resources.Load<Image>("Card/Card_" + (rnd).ToString());
            card = Instantiate(cardImg[i], buyCardList[i]);
            viewCard = card.GetComponent<ViewCard>();
            GameManager.instance.card[rnd].haveCard++;
            Debug.Log(viewCard.cardType);
        }
        GameManager.instance.CardSave();
    }

    /// <summary>
    /// 카드 구매 후 확인 버튼을 누를 경우 혹은 상점이 열릴 경우 호출
    /// </summary>
    private void InitBuyCard()
    {
        for (int i = 0; i < 5; i++)
        {
            if (buyCardList[i].childCount > 0)
            {
                Destroy(buyCardList[i].GetChild(0).gameObject);
            }
        }
    }
}