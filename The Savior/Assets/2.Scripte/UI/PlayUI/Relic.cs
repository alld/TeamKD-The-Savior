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

    // ���� ���� ������ �˸��� �̹��� ��ġ
    private Transform[] setRelic = new Transform[5];
    [Header("���õ� ����")]
    public Image selectRelic;

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
    public void RelicSetting(Image copyImg, int num)
    {
        // �ٸ� �ڸ��� ������ ������ �ִٸ� �ı��Ѵ�.
        for(int i = 0; i < relicNum.Length; i++)
        {
            if(relicNum[i] == num)
            {
                relicNum[i] = 0;
                Destroy(relicTr[i].GetChild(0).gameObject);
            }
        }
        // �ش� �ڸ��� �̹� ������ ������ �ִٸ� �ı��ϰ� �����Ѵ�.
        if(relicTr[curRelicTr].childCount > 0)
        {
            Destroy(relicTr[curRelicTr].GetChild(0).gameObject);
        }
        relicNum[curRelicTr] = num;
        copyImg.transform.SetParent(relicTr[curRelicTr]);
        InitRectSize(copyImg);
       
        Destroy(copyImg.GetComponent<ViewRelic>());
        relicInventory.SetActive(false);
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
