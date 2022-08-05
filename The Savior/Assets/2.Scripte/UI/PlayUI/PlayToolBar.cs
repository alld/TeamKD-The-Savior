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

    [Header("���� ���� ������ ����")]
    Relic relic;

    private void Start()
    {
        // ���� �κ��丮 On, Off
        relicButton.onClick.AddListener(() => OnClick_RelicBtn());
        closeRelicButton.onClick.AddListener(() => OnClick_CloseRelicBtn());
        // ī�� �κ��丮 On, Off
        cardButton.onClick.AddListener(() => OnClick_CardBtn());
        closeCardButton.onClick.AddListener(() => OnClick_CloseCardBtn());
        // ĳ���� �κ��丮 On, Off
        partyButton.onClick.AddListener(() => OnClick_PartyBtn());
        closePartyButton.onClick.AddListener(() => OnClick_ClosePartyBtn());
        // ���� �κ��丮 On, Off
        collectButton.onClick.AddListener(() => OnClick_CollectBtn());
        closeCollectButton.onClick.AddListener(() => OnClick_CloseCollectBtn());

        // ���� ����� ������ ����
        relic = GameObject.Find("PUIManager").GetComponent<Relic>();

        Gold();
    }

    public void Gold()
    {
        souls.text = GameManager.instance.data.souls.ToString();
        golds.text = GameManager.instance.data.golds.ToString();
    }

    /// <summary>
    /// ���� �κ��丮�� On���ִ� �Լ�.
    /// </summary>
    private void OnClick_RelicBtn()
    {
        cardImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);

        relicImg.SetActive(true);
    }

    /// <summary>
    /// ī�� �κ��丮�� On���ִ� �Լ�.
    /// </summary>
    private void OnClick_CardBtn()
    {
        relicImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);

        cardImg.SetActive(true);
    }

    /// <summary>
    /// ĳ���� �κ��丮�� On���ִ� �Լ�.
    /// </summary>
    private void OnClick_PartyBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        collectImg.SetActive(false);

        partyImg.SetActive(true);
    }

    /// <summary>
    /// ���� �κ��丮�� On���ִ� �Լ�.
    /// </summary>
    private void OnClick_CollectBtn()
    {
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        partyImg.SetActive(false);

        collectImg.SetActive(true);
    }

    private void OnClick_CloseRelicBtn()
    {
        relic.isRelicSetting = false;
        relicImg.SetActive(false);
    }
    private void OnClick_CloseCardBtn()
    {
        cardImg.SetActive(false);
    }
    private void OnClick_ClosePartyBtn()
    {
        partyImg.SetActive(false);
    }
    private void OnClick_CloseCollectBtn()
    {
        collectImg.SetActive(false);
    }



}
