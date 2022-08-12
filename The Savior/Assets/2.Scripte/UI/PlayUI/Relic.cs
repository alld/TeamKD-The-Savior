using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Relic : MonoBehaviour
{
    public Button[] relic = new Button[5];
    public GameObject relicInventory;

    // 유물 세팅 버튼을 사용하여 창을 열었는가?
    public bool isRelicSetting = false;

    // 인벤토리를 열어서 유물을 장착하였는지 여부
    private bool isUseRelic = false;

    [Header("선택된 유물")]
    public Image selectRelic;
    public Transform[] selectTr = new Transform[5];

    [Header("유물의 위치")]
    public Transform[] relicTr = new Transform[5];
    public int[] relicNum = new int[5];

    private int curRelicTr = 0;


    // 유물위에 마우스를 올리면 나타나는 툴팁
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
    /// 유물 박스에 있는 유물 버튼을 누를 경우 유물 창을 열고.
    /// 각 번호에 맞는 위치에 유물을 세팅할 수 있게 한다.
    /// </summary>
    /// <param name="idx"></param>
    private void OnClick_RelicSettingBtn(int idx)
    {
        curRelicTr = idx;
        isRelicSetting = true;
        relicInventory.SetActive(true);
    }

    /// <summary>
    /// 인벤토리에 있는 유물을 복사하여 유물 장착칸으로 이동시키고 사이즈를 조정한다.
    /// 세팅 후 창을 닫는다.
    /// </summary>
    /// <param name="copyImg"></param>
    public void RelicSetting(Image relicImg, int num, Transform setRelicTr)
    {
        Image copyImg = Instantiate(relicImg);
        Image selectRelicImg = Instantiate(selectRelic);

        // 현재 자리에 동일한 유물이 있다면 파괴하고 데이터를 초기화한다.
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

        // 다른 자리에 동일한 유물이 있다면 파괴한다.
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
        // 해당 자리에 이미 장착된 유물이 있다면 파괴하고 장착한다.
        if (relicNum[curRelicTr] != 0)
        {
            Destroy(relicTr[curRelicTr].GetChild(0).gameObject);
            Destroy(selectTr[curRelicTr].GetChild(1).gameObject);
        }
        relicNum[curRelicTr] = num;
        GameManager.instance.data.equipRelic[curRelicTr] = num;
        copyImg.transform.SetParent(relicTr[curRelicTr]);

        // 인벤토리에 장착된 유물을 표시함.
        selectTr[curRelicTr] = setRelicTr;
        selectRelicImg.transform.SetParent(selectTr[curRelicTr]);
        //Debug.Log(selectTr[curRelicTr]);

        InitRectSize(selectRelicImg);
        InitRectSize(copyImg);



        Destroy(copyImg.GetComponent<ViewRelic>());
        relicInventory.SetActive(false);
    }

    /// <summary>
    /// 인벤토리에 있는 유물을 복사하여 유물 장착칸으로 이동 시킨다.
    /// 단, 0번부터 검사하여 비어있는 자리에 장착시킨다.
    /// 장착중인 유물을 클릭 할 경우 해제시킨다.
    /// </summary>
    /// <param name="copyImg"></param>
    /// <param name="num"></param>
    public void StackRelicSetting(Image relicImg, int num, Transform setRelicTr)
    {
        for (int i = 0; i < relicNum.Length; i++)
        {
            // 장착중인 동일한 유물이 있는지 검사를 하고
            // 동인 유물이 있다면 장착 해제
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
                // 장착중인 동일한 유물이 없다면 장착.
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
    /// 스트레치가 적용된 이미지의 사이즈를 부모 객체의 사이즈에 맞춤.
    /// </summary>
    /// <param name="img"></param>
    private void InitRectSize(Image img)
    {
        img.rectTransform.offsetMin = Vector2.zero;
        img.rectTransform.offsetMax = Vector2.zero;
    }

}
