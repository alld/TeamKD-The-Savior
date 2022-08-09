using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;
public class BuyCardPack : MonoBehaviour
{
    private TextAsset jsonData;
    private string json;
    [Header("상점 카드팩 리스트 위치")]
    public Transform shopCardPackTr;

    [Header("카드팩 이미지 위치")]
    public GameObject unPack;
    public Transform shopCardPackImageTr;
    public Transform[] cardList;

    [Header("구매한 카드들의 위치")]
    public GameObject openPack;
    public Transform[] buyCardList;

    [Header("카드팩 이미지 리스트")]
    public Image[] cardPackImg;
    private Image[] cardImg;


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

    private void OnClick_SelectPackBtn(int idx)
    {
        for (int i = 0; i < 5; i++)
        {

        }

        Image pack = Instantiate(cardPackImg[(idx - 1)], shopCardPackImageTr);
        InitRect(pack);

        Debug.Log(idx + "번 카드팩");
        openPack.SetActive(false);
        unPack.SetActive(true);
    }

    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = Vector2.zero;
        img.rectTransform.offsetMin = Vector2.zero;
    }

    public void BuyCard()
    {
        unPack.SetActive(false);
        openPack.SetActive(true);
    }

}
