using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyCardPack : MonoBehaviour
{
    [Header("���� ī���� ����Ʈ ��ġ")]
    public Transform shopCardPackTr;

    [Header("ī���� �̹��� ��ġ")]
    public GameObject unPack;
    public Transform shopCardPackImageTr;
    public Transform[] cardList;

    [Header("������ ī����� ��ġ")]
    public GameObject openPack;
    public Transform[] buyCardList;
    public Button confirmButton;

    [Header("ī���� �̹��� ����Ʈ")]
    public Image[] cardPackImg;
    private Image[] cardImg = new Image[5];

    // instantiate�� �����Ͽ� ����� �̹���
    private Image pack; // �� �̹���
    private Image card; // ī�� �̹���

    private Transform cardDectTr;


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
        cardDectTr = GameObject.Find("GameUI/MainUI/DeckWindow/ContentBox/MyCard/Viewport/Content").transform;


        // ���� â ���� �ÿ� ȣ���.
        // ������ ī�� ���� �����ȴ�.
        for (int i = 0; i < maxCardPack; i++)
        {
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

    /// <summary>
    /// ī�带 ��� �� �� �������·� �ʱ�ȭ.
    /// </summary>
    public void OpenShop()
    {
        unPack.SetActive(true);
        openPack.SetActive(false);
    }

    /// <summary>
    /// ������ ī������ �ı���.
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
    /// ī�� ���� ����Ʈ���� ī������ ���� ��� �ش� ī������ ������ �����.
    /// </summary>
    /// <param name="idx"></param>
    private void OnClick_SelectPackBtn(int idx)
    {
        InitCardPack();
        for (int i = 0; i < 5; i++)
        {
            cardImg[i] = Resources.Load<Image>("Card/Card_"+ (i + 1).ToString());
            card = Instantiate(cardImg[i], cardList[i]);
            Destroy(card.GetComponent<ViewCard>());     // ������ ī���� �巡�׸� ���� ����.
            InitRect(card);
        }

        pack = Instantiate(cardPackImg[(idx - 1)], shopCardPackImageTr);
        InitRect(pack);

        openPack.SetActive(false);
        unPack.SetActive(true);
    }

    /// <summary>
    /// Ȯ�� ��ư�� ���� ��� ȣ��
    /// </summary>
    private void OnClick_ConfirmBtn()
    {
        DestroyCard();
        unPack.SetActive(true);
        openPack.SetActive(false);
    }

    /// <summary>
    /// �̹����� ����� �θ� ��ü�� ����� ����.
    /// </summary>
    /// <param name="img"></param>
    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = Vector2.zero;
        img.rectTransform.offsetMin = Vector2.zero;
    }

    /// <summary>
    /// ī�� ���Ž� ȣ��Ǵ� �Լ�.
    /// </summary>
    public IEnumerator BuyCard()
    {
        unPack.SetActive(false);
        openPack.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            int rnd = Random.Range(1, 24);
            cardImg[i] = Resources.Load<Image>("Card/Card_" + (rnd).ToString());
            Instantiate(cardImg[i], cardDectTr);
            card = Instantiate(cardImg[i], buyCardList[i]);
            Destroy(card.GetComponent<ViewCard>());     // ������ ī���� �巡�׸� ���� ����.

            // ȹ���� ī���� ������ ����.
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
        for (int i = 0; i < 5; i++)
        {
            Destroy(buyCardList[i].GetChild(0).gameObject);
        }
    }
}