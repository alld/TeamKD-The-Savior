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

        StartCoroutine(Gold());
    }

    public IEnumerator Gold()
    { 
        souls.text = GameManager.instance.data.souls.ToString();
        golds.text = GameManager.instance.data.golds.ToString();
        yield return null;
    }

    /// <summary>
    /// ���� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_RelicBtn()
    {
        if (GameManager.instance.isDungeon)
        {
            return;
        }
        cardImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);
        if (!relicImg.activeSelf)
        {
            relicImg.SetActive(true);
        }
        else
        {
            relicImg.SetActive(false);
        }
    }

    /// <summary>
    /// ī�� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_CardBtn()
    {
        if (GameManager.instance.isDungeon)
        {
            return;
        }
        relicImg.SetActive(false);
        partyImg.SetActive(false);
        collectImg.SetActive(false);
        if (!cardImg.activeSelf)
        {
            cardImg.SetActive(true);
        }
        else
        {
            cardImg.SetActive(false);
        }
    }

    /// <summary>
    /// ĳ���� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_PartyBtn()
    {
        if (GameManager.instance.isDungeon)
        {
            return;
        }
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        collectImg.SetActive(false);
        if (!partyImg.activeSelf)
        {
            partyImg.SetActive(true);
        }
        else
        {
            partyImg.SetActive(false);
        }
    }

    /// <summary>
    /// ���� �κ��丮�� On, Off ���ִ� �Լ�.
    /// </summary>
    private void OnClick_CollectBtn()
    {
        if (GameManager.instance.isDungeon)
        {
            return;
        }
        relicImg.SetActive(false);
        cardImg.SetActive(false);
        partyImg.SetActive(false);
        if (!collectImg.activeSelf)
        {
            collectImg.SetActive(true);
        }
        else
        {
            collectImg.SetActive(false);
        }
    }

    private void OnClick_CloseRelicBtn()
    {
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
