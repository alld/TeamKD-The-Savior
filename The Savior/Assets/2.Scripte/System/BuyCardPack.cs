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
    public Transform shopCardPackTr;

    private int maxCardPack = 5;
    private List<GameObject> cardPackList = new List<GameObject>();
    private TextMeshProUGUI packName;
    private TextMeshProUGUI packContant;
    private Button selectPackButton;

    private void Awake()
    {
        jsonData = Resources.Load<TextAsset>("CardShopData");
        json = jsonData.text;
    }


    private void Start()
    {
        var data = JSON.Parse(json);

        for (int i = 0; i < maxCardPack; i++)
        {
            cardPackList.Add(Resources.Load<GameObject>("PackItem"));
            packName = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[0];
            packContant = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[1];
            packName.text = i.ToString() + "번 카드팩";
            packContant.text = i.ToString() + "번 카드팩의 설명";
            Instantiate(cardPackList[i], shopCardPackTr);
            cardPackList[i].gameObject.name = "Pack_" + i;
        }


        selectPackButton.onClick.AddListener(() => OnClick_SelectPackBtn());
    }

    private void OnClick_SelectPackBtn()
    {

    }

}
