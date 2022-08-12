using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Relic : MonoBehaviour
{
    public Button[] relic = new Button[5];
    public GameObject relicInventory;

    // ���� ���� ��ư�� ����Ͽ� â�� �����°�?
    public bool isRelicSetting = false;

    // �κ��丮�� ��� ������ �����Ͽ����� ����
    private bool isUseRelic = false;

    [Header("���õ� ����")]
    public Image selectRelic;
    public Transform[] selectTr = new Transform[5];

    [Header("������ ��ġ")]
    public Transform[] relicTr = new Transform[5];
    public int[] relicNum = new int[5];

    private int curRelicTr = 0;


    // �������� ���콺�� �ø��� ��Ÿ���� ����
    public Image infoImage;
    public TMP_Text infoText;

    void Start()
    {
        relic[0].onClick.AddListener(() => OnClick_RelicSettingBtn(0));
        relic[1].onClick.AddListener(() => OnClick_RelicSettingBtn(1));
        relic[2].onClick.AddListener(() => OnClick_RelicSettingBtn(2));
        relic[3].onClick.AddListener(() => OnClick_RelicSettingBtn(3));
        relic[4].onClick.AddListener(() => OnClick_RelicSettingBtn(4));

        infoImage = Instantiate(infoImage);
        infoText = Instantiate(infoText);
        infoText.transform.SetParent(infoImage.transform);
    }

    /// <summary>
    /// ���� �ڽ��� �ִ� ���� ��ư�� ���� ��� ���� â�� ����.
    /// �� ��ȣ�� �´� ��ġ�� ������ ������ �� �ְ� �Ѵ�.
    /// </summary>
    /// <param name="idx"></param>
    private void OnClick_RelicSettingBtn(int idx)
    {
        curRelicTr = idx;
        isRelicSetting = true;
        relicInventory.SetActive(true);
    }

    /// <summary>
    /// �κ��丮�� �ִ� ������ �����Ͽ� ���� ����ĭ���� �̵���Ű�� ����� �����Ѵ�.
    /// ���� �� â�� �ݴ´�.
    /// </summary>
    /// <param name="copyImg"></param>
    public void RelicSetting(Image relicImg, int num, Transform setRelicTr)
    {
        Image copyImg = Instantiate(relicImg);
        Image selectRelicImg = Instantiate(selectRelic);

        // ���� �ڸ��� ������ ������ �ִٸ� �ı��ϰ� �����͸� �ʱ�ȭ�Ѵ�.
        if (GameManager.instance.data.equipRelic[curRelicTr] == copyImg.GetComponent<ViewRelic>().number)
        {
            GameManager.instance.data.equipRelic[curRelicTr] = 0;
            relicNum[curRelicTr] = 0;
            Destroy(copyImg.gameObject);
            Destroy(selectRelicImg.gameObject);
            Destroy(relicTr[curRelicTr].GetChild(0).gameObject);
            Destroy(selectTr[curRelicTr].GetChild(1).gameObject);
            relicInventory.SetActive(false);
            return;
        }

        // �ٸ� �ڸ��� ������ ������ �ִٸ� �ı��Ѵ�.
        for (int i = 0; i < relicNum.Length; i++)
        {
            if (relicNum[i] == num)
            {
                relicNum[i] = 0;
                GameManager.instance.data.equipRelic[i] = 0;
                Destroy(relicTr[i].GetChild(0).gameObject);
                Destroy(selectTr[i].GetChild(1).gameObject);
            }
        }
        // �ش� �ڸ��� �̹� ������ ������ �ִٸ� �ı��ϰ� �����Ѵ�.
        if (relicNum[curRelicTr] != 0)
        {
            Destroy(relicTr[curRelicTr].GetChild(0).gameObject);
            Destroy(selectTr[curRelicTr].GetChild(1).gameObject);
        }
        relicNum[curRelicTr] = num;
        GameManager.instance.data.equipRelic[curRelicTr] = num;
        copyImg.transform.SetParent(relicTr[curRelicTr]);

        // �κ��丮�� ������ ������ ǥ����.
        selectTr[curRelicTr] = setRelicTr;
        selectRelicImg.transform.SetParent(selectTr[curRelicTr]);
        //Debug.Log(selectTr[curRelicTr]);

        InitRectSize(selectRelicImg);
        InitRectSize(copyImg);



        Destroy(copyImg.GetComponent<ViewRelic>());
        relicInventory.SetActive(false);
    }

    /// <summary>
    /// �κ��丮�� �ִ� ������ �����Ͽ� ���� ����ĭ���� �̵� ��Ų��.
    /// ��, 0������ �˻��Ͽ� ����ִ� �ڸ��� ������Ų��.
    /// �������� ������ Ŭ�� �� ��� ������Ų��.
    /// </summary>
    /// <param name="copyImg"></param>
    /// <param name="num"></param>
    public void StackRelicSetting(Image relicImg, int num, Transform setRelicTr)
    {
        for (int i = 0; i < relicNum.Length; i++)
        {
            // �������� ������ ������ �ִ��� �˻縦 �ϰ�
            // ���� ������ �ִٸ� ���� ����
            if (relicNum[i] == num)
            {
                relicNum[i] = 0;
                GameManager.instance.data.equipRelic[i] = 0;
                Destroy(relicTr[i].GetChild(0).gameObject);
                Destroy(selectTr[i].GetChild(1).gameObject);
                isUseRelic = true;
                break;
            }
        }
        if (!isUseRelic)
        {
            for (int i = 0; i < relicNum.Length; i++)
            {
                // �������� ������ ������ ���ٸ� ����.
                if (relicNum[i] == 0)
                {
                    Image selectRelicImg = Instantiate(selectRelic);
                    Image copyImg = Instantiate(relicImg);
                    relicNum[i] = num;
                    GameManager.instance.data.equipRelic[i] = num;
                    copyImg.transform.SetParent(relicTr[i]);
                    selectTr[i] = setRelicTr;
                    selectRelicImg.transform.SetParent(selectTr[i]);
                    InitRectSize(copyImg);
                    InitRectSize(selectRelicImg);

                    Destroy(copyImg.GetComponent<ViewRelic>());

                    break;
                }
            }
        }
        isUseRelic = false;
    }


    /// <summary>
    /// ��Ʈ��ġ�� ����� �̹����� ����� �θ� ��ü�� ����� ����.
    /// </summary>
    /// <param name="img"></param>
    private void InitRectSize(Image img)
    {
        img.rectTransform.offsetMin = Vector2.zero;
        img.rectTransform.offsetMax = Vector2.zero;
    }

}
