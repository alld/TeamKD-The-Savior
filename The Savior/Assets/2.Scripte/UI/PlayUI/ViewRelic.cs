using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ViewRelic : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // ���� ������ ���� ����
    private Relic relic;

    public bool isSelect = false;

    [Header("���� ����")]
    
    public Transform mousePoint;

    private GameObject info;

    // �κ��丮�� �ִ� ������ �����Ͽ� ���� â�� �־���.
    private Image thisImg;

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelect = true;
        Image copyImg = Instantiate(thisImg);
        switch (relic.isRelicSetting)
        {
            case true:
                relic.RelicSetting(copyImg);
                break;
            case false:
                Debug.Log("False!!!");
                break;
        }
        relic.isRelicSetting = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        info = relic.infoImage.gameObject;

        mousePoint.position = Input.mousePosition;

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
        relic = GameObject.Find("PUIManager").GetComponent<Relic>();
        thisImg = GetComponent<Image>();
        mousePoint = GameObject.Find("MousePoint").GetComponent<Transform>();
    }



    private void InitRectSize(Image img, TMP_Text text)
    {
        img.rectTransform.offsetMin = new Vector2(200, 0);
        img.rectTransform.offsetMax = new Vector2(300, 0);

        text.rectTransform.offsetMin = Vector2.zero;
        text.rectTransform.offsetMax = Vector2.zero;
    }
}
