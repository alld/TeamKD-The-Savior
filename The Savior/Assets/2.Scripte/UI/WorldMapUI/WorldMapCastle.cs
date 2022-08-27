using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldMapCastle : MonoBehaviour
{
    SummonCharacter summon;
    BuyCardPack buy;
    [Header("��ư - ��")]
    public Button castleButton;
    public Button closeCastleButton;

    [Header("��ư - �� ����")]
    public Button shopButton;
    public Button shopBuyButton;
    public Button closeShopButton;
    public Button churchButton;
    public Button churchSummonButton;
    public Button closeChurchButton;
    public Button summonConfirmButton;
    public Button reSummonButton;
    public Button castle;       // �� ������ �ռ�

    //public Button kingsCastle;

    [Header("�� �̹���")]
    public GameObject castleImg;
    public GameObject churchImg;
    public GameObject shopImg;

    [Header("���� - �׷�Ÿ")]
    public Button grottaButton;

    // �ش� â�� Ȱ��ȭ �Ǿ��ִ°�?
    private bool isChurch = false;
    private bool isShop = false;

    private int summonPrice = 100;


    PlayStory story;


    // �ռ��� �����ϴ� �̹���.
    public GameObject[] storyBackGound;


    void Start()
    {
        castleButton.onClick.AddListener(() => OnClick_VisitCastleBtn());
        closeCastleButton.onClick.AddListener(() => OnClick_OutCastleBtn());
        churchButton.onClick.AddListener(() => OnClick_OnChurchBtn());
        shopButton.onClick.AddListener(() => OnClick_OnShopBtn());
        closeShopButton.onClick.AddListener(() => OnClick_CloseShopBtn());
        closeChurchButton.onClick.AddListener(() => OnClick_CloseChurchBtn());
        shopBuyButton.onClick.AddListener(() => OnClick_OnBuyBtn());
        churchSummonButton.onClick.AddListener(() => OnClick_OnSummonBtn());
        summonConfirmButton.onClick.AddListener(() => OnClick_ConfirmSummonBtn());
        reSummonButton.onClick.AddListener(() => OnClick_ReSummonBtn());
        castle.onClick.AddListener(() => OnClick_CastleBtn());
        grottaButton.onClick.AddListener(() => OnClick_GrottaBtn());

        story = GameObject.Find("PUIManager").GetComponent<PlayStory>();

        summon = GetComponent<SummonCharacter>();
        buy = GetComponent<BuyCardPack>();
    }

    // ���丮���� ���� Ŭ���� ������ ����Ű�� �̹��� ����.
    // ���� ���� Ȱ��ȭ.
    private void OnClick_GrottaBtn()
    {
        if (GameManager.instance.data.storyProgress == 92)
        {
            storyBackGound[4].SetActive(false);
            story.toolTip[5].SetActive(true);
            story.toolTip[6].SetActive(true);
        }
    }

    /// <summary>
    /// ����� ���� ���� ���� �Լ�.
    /// ���丮 �������ϰ�� �ش� �̺�Ʈ�� �߻�.
    /// �ռ��� ����Ű�� �̹����� Ȱ��ȭ�ϰ� ��ȭâ�� Ȱ��ȭ�Ѵ�.
    /// </summary>
    private void OnClick_CastleBtn()
    {
        if(GameManager.instance.data.storyProgress == 3)
        {
            storyBackGound[1].SetActive(false);
            story.OnDiaLog();
        }
    }

    /// <summary>
    /// ����ʿ��� ������ �̵��ϴ� ��ư
    /// ���丮 �������ϰ�� �ش� �̺�Ʈ �߻�.
    /// ����ʿ��� �ռ��� ����Ű�� �̹����� ��Ȱ��ȭ �ϰ�, ���� �������� �ռ��� ����Ű�� �̹����� Ȱ��ȭ�Ѵ�.
    /// </summary>
    public void OnClick_VisitCastleBtn()
    {
        castleImg.SetActive(true);
        if(GameManager.instance.data.storyProgress == 3)
        {
            // �ռ��� �����ϴ� �̹���.
            storyBackGound[0].SetActive(false);
            storyBackGound[1].SetActive(true);
        }
    }

    /// <summary>
    /// ������ ��������� ������ ��ư
    /// </summary>
    public void OnClick_OutCastleBtn()
    {
        castleImg.SetActive(false);
    }

    /// <summary>
    /// ����â Ȱ��ȭ ��ư
    /// </summary>
    private void OnClick_OnShopBtn()
    {
        isShop = !isShop;
        shopImg.SetActive(isShop);
        buy.OpenShop();

        // ���丮 ���൵�� 58�� �� �߻��ϴ� �̺�Ʈ
        // ��ȭâ�� Ȱ��ȭ�ϰ� ������ ����Ű�� �̹����� ��Ȱ��ȭ�Ѵ�.
        if(GameManager.instance.data.storyProgress == 58)
        {
            story.OnDiaLog();
            storyBackGound[3].SetActive(false);
        }
    }


    /// <summary>
    /// ����â�� ��Ȱ��ȭ �ϴ� ��ư.
    /// </summary>
    private void OnClick_CloseShopBtn()
    {
        buy.InitBuyCard();
        isShop = false;
        shopImg.SetActive(false);

        // ���丮 ���൵ 64�� �� �߻��ϴ� �̺�Ʈ
        // ��ȭâ�� Ȱ��ȭ �Ѵ�.
        if (GameManager.instance.data.storyProgress == 64)
        {
            story.OnDiaLog();
        }
    }

    /// <summary>
    /// ����â���� ���� ���� ��ư 
    /// </summary>
    private void OnClick_OnBuyBtn()
    {
        StartCoroutine(buy.BuyCard());
    }

    /// <summary>
    /// ��ȸâ Ȱ��ȭ ��ư
    /// </summary>
    private void OnClick_OnChurchBtn()
    {
        isChurch = !isChurch;
        churchImg.SetActive(true);
        summon.InitSummon();
        InitChurchButton();

        // ���丮 ���൵ 49�� �� �߻��ϴ� �̺�Ʈ.
        // ��ȸ�� ����Ű�� �̹����� ��Ȱ��ȭ�Ѵ�.
        if(GameManager.instance.data.storyProgress == 49)
        {
            story.OnDiaLog();
            storyBackGound[2].SetActive(false);
        }
    }

    /// <summary>
    /// ��ȸâ ��Ȱ��ȭ ��ư
    /// </summary>
    private void OnClick_CloseChurchBtn()
    {
        isChurch = false;
        churchImg.SetActive(false);

        if(GameManager.instance.data.storyProgress == 54)
        {
            story.OnDiaLog();
        }
    }

    /// <summary>
    /// ��ȸ���� ��ȯ ��ư
    /// </summary>
    private void OnClick_OnSummonBtn()
    {

        if(GameManager.instance.data.haveCharacter.Count >= 27)
        {
            Debug.Log("�κ��丮�� ĳ���Ͱ� ���� á���ϴ�.");
            return;
        }
        summon.SummonRandom(summonPrice);

        if (summon.ishave)      // ��� ĳ���͸� ������ ���� ��.
        {
            return;
        }

        churchSummonButton.gameObject.SetActive(false);
        summonConfirmButton.gameObject.SetActive(true);
        reSummonButton.gameObject.SetActive(true);
    }


    /// <summary>
    /// ĳ���� ��ȯ �� Ȯ�� ��ư Ŭ���� ȣ��
    /// </summary>
    private void OnClick_ConfirmSummonBtn()
    {
        summon.InitSummon();
        InitChurchButton();
    }

    /// <summary>
    /// ĳ���� ��ȯ �� �ѹ� �� ��ȯ�ϱ� ��ư Ŭ���� ȣ��
    /// </summary>
    private void OnClick_ReSummonBtn()
    {
        summon.InitSummon();
        OnClick_OnSummonBtn();
    }

    private void InitChurchButton()
    {
        churchSummonButton.gameObject.SetActive(true);
        summonConfirmButton.gameObject.SetActive(false); ;
        reSummonButton.gameObject.SetActive(false);
    }

}
