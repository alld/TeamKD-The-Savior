using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ViewRelic : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // 유물 장착을 위한 연결
    private Relic relic;
    private RelicData relicData;

    public bool isSelect = false;

    // 유물의 번호
    private int number = 0;

    [Header("유물 정보")]
    private Transform mousePoint;

    private GameObject info;

    // 인벤토리에 있는 유물을 복사하여 유물 창에 넣어줌.
    private Image thisImg;

    public void OnPointerClick(PointerEventData eventData)
    {
        info.SetActive(false);
        isSelect = true;
        //Image copyImg = Instantiate(thisImg);
        switch (relic.isRelicSetting)
        {
            case true:
                relic.RelicSetting(thisImg, number, transform.parent);
                break;
            case false:
                relic.StackRelicSetting(thisImg, number, transform.parent);
                break;
        }
        relic.isRelicSetting = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        info = relic.infoImage.gameObject;

        mousePoint.position = this.gameObject.transform.position;

        relic.infoImage.transform.SetParent(mousePoint);
        InitRectSize(relic.infoImage, relic.infoText);
        info.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info.SetActive(false);
    }

    private void Start()
    {
        relicData = GetComponent<RelicData>();
        relic = GameObject.Find("PUIManager").GetComponent<Relic>();
        thisImg = GetComponent<Image>();
        mousePoint = GameObject.Find("MousePoint").GetComponent<Transform>();
        number = relicData.idx;
        Debug.Log(number);
    }



    private void InitRectSize(Image img, TMP_Text text)
    {
        img.rectTransform.offsetMin = new Vector2(150, 0);
        img.rectTransform.offsetMax = new Vector2(600, 0);

        text.rectTransform.offsetMin = Vector2.zero;
        text.rectTransform.offsetMax = Vector2.zero;
    }
}
