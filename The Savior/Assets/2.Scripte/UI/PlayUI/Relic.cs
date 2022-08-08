using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Relic : MonoBehaviour
{
    private RelicData data;
    public Button[] relic = new Button[5];
    public GameObject relicInventory;

    // 유물 세팅 버튼을 사용하여 창을 열었는가?
    public bool isRelicSetting = false;

    // 유물 선택 됐음을 알리는 이미지 위치
    private Transform[] setRelic = new Transform[5];
    [Header("선택된 유물")]
    public Image selectRelic;

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
    public void RelicSetting(Image copyImg)
    {
        if(relicTr[curRelicTr].childCount > 1)
        {
            Destroy(relicTr[curRelicTr].GetChild(1).gameObject);
        }

        //
        // 이미 유물이 장착되어 있다면, 장착된 유물을 파괴하고
        // 클릭한 위치에 유물을 장착한다.
        // 버그가 있으므로 주석 처리하고 나중에 만들겠읍니다.
        //

        copyImg.transform.SetParent(relicTr[curRelicTr]);
        InitRectSize(copyImg);
        //Debug.Log(relicNum[curRelicTr]);
       
        Destroy(copyImg.GetComponent<ViewRelic>());
        relicInventory.SetActive(false);
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
