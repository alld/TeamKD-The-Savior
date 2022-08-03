using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayToolBar : MonoBehaviour
{
    // ��ư
    [Header("Button")]
    public Button closeRelicButton;
    public Button closeCardButton;
    public Button closePartyButton;
    public Button closeCollectButton;
    public Button relicButton;
    public Button cardButton;
    public Button partyButton;
    public Button collectButton;

    // �ش� â
    [Header("ActiveObject")]
    public GameObject relicImg;
    public GameObject cardImg;
    public GameObject partyImg;
    public GameObject collectImg;

    // ���� �ҿ��� ǥ�õ� �ؽ�Ʈ
    public TMP_Text souls;
    public TMP_Text golds;

    // �ش� â�� �����ִ°�?
    private bool isRelic = false;
    private bool isCard = false;
    private bool isParty = false;
    private bool isCollect = false;

    private void Start()
    {
        // ���� �κ��丮 On, Off
        relicButton.onClick.AddListener(() => OnClick_RelicBtn());
        closeRelicButton.onClick.AddListener(() => OnClick_RelicBtn());
        // ī�� �κ��丮 On, Off
        cardButton.onClick.AddListener(() => OnClick_CardBtn());
        closeCardButton.onClick.AddListener(() => OnClick_CardBtn());
        // ĳ���� �κ��丮 On, Off
        partyButton.onClick.AddListener(() => OnClick_PartyBtn());
        closePartyButton.onClick.AddListener(() => OnClick_PartyBtn());
        // ���� �κ��丮 On, Off
        collectButton.onClick.AddListener(() => OnClick_CollectBtn());
        closeCollectButton.onClick.AddListener(() => OnClick_CollectBtn());

        Gold();
    }

    public void Gold()
    {
        souls.text = GameManager.instance.data.souls.ToString();
        golds.text = GameManager.instance.data.golds.ToString();
    }

    /// <summary>
    /// ���� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_RelicBtn()
    {
        cardImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);

        isCard = false;
        isParty = false;
        isCollect = false;

        isRelic = !isRelic;
        relicImg.SetActive(isRelic);
    }

    /// <summary>
    /// ī�� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_CardBtn()
    {
        relicImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);

        isRelic = false;
        isParty = false;
        isCollect = false;

        isCard = !isCard;
        cardImg.SetActive(isCard);
    }

    /// <summary>
    /// ĳ���� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_PartyBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        collectImg.SetActive(false);

        isRelic = false;
        isCard = false;
        isCollect = false;

        isParty = !isParty;
        partyImg.SetActive(isParty);
    }

    /// <summary>
    /// ���� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_CollectBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        partyImg.SetActive(false);

        isRelic = false;
        isCard = false;
        isParty = false;

        isCollect = !isCollect;
        collectImg.SetActive(isCollect);
    }

}
