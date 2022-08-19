using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Newtonsoft.Json.Linq;
using System.Text;

public class ViewRelic : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // ���� ������ ���� ����
    private Relic relic;

    public bool isSelect = false;

    private TextAsset textAsset;    // ���콺 ���ͽÿ� ������ ������ ���.
    private JArray jText;

    private string[] relicName = new string[12];
    private string[] relicCondition = new string[12];
    private string[] relicPositive = new string[12];
    private string[] relicNegative = new string[12];

    // ������ ��ȣ
    public int number = 0;

    [Header("���� ����")]
    private Transform mousePoint;

    private GameObject info;

    // �κ��丮�� �ִ� ������ �����Ͽ� ���� â�� �־���.
    private Image thisImg;

    // �κ��丮�� �ִ� ���� Ŭ���� ���� ����
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

    // �κ��丮�� ���� ���� ���콺 Ŀ���� �ø��� �̹��� ���
    public void OnPointerEnter(PointerEventData eventData)
    {

        info = relic.infoImage.gameObject;

        StringAdd(number);


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
        relic = GameObject.Find("PUIManager").GetComponent<Relic>();
        thisImg = GetComponent<Image>();
        mousePoint = GameObject.Find("MousePoint").GetComponent<Transform>();
        textAsset = Resources.Load<TextAsset>("RelicDB/RelicText");
        jText = JArray.Parse(textAsset.text);
        InitText();
    }

    private void InitText()
    {
        for (int i = 0; i < jText.Count; i++)
        {
            relicName[i] = jText[i]["Name_Kr"].ToObject<string>();
            relicCondition[i] = jText[i]["Condition_kor"].ToObject<string>();
            relicPositive[i] = jText[i]["Positive_Kr"].ToObject<string>();
            relicNegative[i] = jText[i]["Negative_Kr"].ToObject<string>();
        }
    }

    private void StringAdd(int n)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<color=#ffdc73>");
        str.Append(relicName[n-1]);
        str.Append("</color>");
        str.Append("\n");
        str.Append("<color=#ffdc73>");
        str.Append(relicCondition[n-1]);
        str.Append("</color>");
        str.Append("\n");
        str.Append("<color=#00ff00>");
        str.Append(relicPositive[n-1]);
        str.Append("</color>");
        str.Append("\n");
        str.Append("<color=#ff0000>");
        str.Append(relicNegative[n-1]);
        str.Append("</color>");

        relic.infoText.text = str.ToString();
    }


    private void InitRectSize(Image img, TMP_Text text)
    {
        img.rectTransform.offsetMin = new Vector2(150, 0);
        img.rectTransform.offsetMax = new Vector2(500, 0);

        text.rectTransform.offsetMin = Vector2.zero;
        text.rectTransform.offsetMax = Vector2.zero;
    }
}
