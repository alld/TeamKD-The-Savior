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
    [Header("���� ī���� ����Ʈ ��ġ")]
    public Transform shopCardPackTr;

    [Header("ī���� �̹��� ��ġ")]
    public GameObject unPack;
    public Transform shopCardPackImageTr;
    public Transform[] cardList;

    [Header("������ ī����� ��ġ")]
    public GameObject openPack;
    public Transform[] buyCardList;

    [Header("ī���� �̹��� ����Ʈ")]
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
            // ���̽� �����͸� �����Ͽ� ���.
            // ���߿� �ʿ������� ����.
            //jsonData = Resources.Load<TextAsset>("CardPack");
            //json = jsonData.text;
            //var data = JSON.Parse(json);

            int idx = i + 1;
            cardPackList.Add(Resources.Load<GameObject>("PackItem"));
            packName = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[0];
            packContant = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[1];

            packName.text = idx.ToString() + "�� ī����";        // ī���� �̸�
            packContant.text = "ī���� ����";        // ī���� ����

            thisObject = Instantiate(cardPackList[i], shopCardPackTr);  // �����տ� �ִ� ī������ ������ ����
            thisObject.gameObject.name = "Pack_" + i;                   // ������ ������Ʈ�� �̸�
            selectPackButton[i] = thisObject.GetComponentInChildren<Button>();  // ������ ������Ʈ�� ��ư ����
            selectPackButton[i].onClick.AddListener(() => OnClick_SelectPackBtn(idx));  // ������ ������Ʈ�� Ŭ���� ȣ��Ǵ� �Լ�

        }
    }

    private void OnClick_SelectPackBtn(int idx)
    {
        for (int i = 0; i < 5; i++)
        {

        }

        Image pack = Instantiate(cardPackImg[(idx - 1)], shopCardPackImageTr);
        InitRect(pack);

        Debug.Log(idx + "�� ī����");
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
