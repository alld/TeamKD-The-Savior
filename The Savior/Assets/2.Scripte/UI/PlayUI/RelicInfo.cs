using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class RelicInfo : MonoBehaviour
{
    private TextAsset textData;             // 유물의 문자 데이터
    private TextAsset relicData;            // 유물의 데이터 값

    private Button relicContentButton;      // 클릭 시 유물 정보 활성화.
    private bool isContent = false;         // 유물 상세정보 창의 상태

    // 스트링을 배열로 선언하여 텍스트 객체에 하나씩 + 해줌.
    private string[] positive = new string[5];  // 장착한 유물의 이로운 효과
    private string[] negative = new string[5];  // 장착한 유물의 해로운 효과

    // 스트링에 데이터 값 연결
    private string[] positiveRelic = new string[12];
    private string[] negativeRelic = new string[12];

    public ScrollRect relicContentImage;
    public TMP_Text relicPositiveText;
    public TMP_Text relicPositiveContentText;
    public TMP_Text relicNegativeText;
    public TMP_Text relicNegativeContentText;

    JArray textJson;
    JArray dataJson;

    void Start()
    {
        textData = Resources.Load<TextAsset>("RelicDB/RelicText");
        relicData = Resources.Load<TextAsset>("RelicDB/RelicData");
        relicContentButton = GetComponent<Button>();
        relicContentButton.onClick.AddListener(() => OnClick_RelicInfoActiveBtn());

        relicPositiveText.text = "[이로운 효과]";
        relicNegativeText.text = "[해로운 효과]";

        textJson = JArray.Parse(textData.text);
        dataJson = JArray.Parse(relicData.text);

        InitializeRelicData();
    }

    /// <summary>
    /// 유물 상세정보창 버튼 클릭.
    /// </summary>
    private void OnClick_RelicInfoActiveBtn()
    {
        isContent = !isContent;
        relicContentImage.gameObject.SetActive(isContent);

        OnClick_RelicInfoBtn();
    }

    /// <summary>
    /// 유물 상세정보 창에 텍스트 출력.
    /// </summary>
    public void OnClick_RelicInfoBtn()
    {
        relicContentImage.gameObject.SetActive(!isContent);

        relicPositiveContentText.text = null;
        relicNegativeContentText.text = null;

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.instance.data.equipRelic[i] == 0) continue;
            
            // 이로운 효과 스트링
            StringBuilder positiveStr = new StringBuilder();
            positiveStr.Append("<color=#ffdc73>");
            positiveStr.Append(textJson[GameManager.instance.data.equipRelic[i] - 1]["Name_Kr"].ToObject<string>());
            positiveStr.Append("</color>");
            positiveStr.Append(" : ");
            positiveStr.Append(positiveRelic[GameManager.instance.data.equipRelic[i] - 1]);
            positiveStr.Append("\n");

            positive[i] = positiveStr.ToString();

            // 해로운 효과 스트링
            StringBuilder negativeStr = new StringBuilder();
            negativeStr.Append("<color=#ffdc73>");
            negativeStr.Append(textJson[GameManager.instance.data.equipRelic[i] - 1]["Name_Kr"].ToObject<string>());
            negativeStr.Append("</color>");
            negativeStr.Append(" : ");
            negativeStr.Append(positiveRelic[GameManager.instance.data.equipRelic[i] - 1]);
            negativeStr.Append("\n");

            negative[i] = negativeStr.ToString();

            relicPositiveContentText.text += positive[i];
            relicNegativeContentText.text += negative[i];
        }

        relicContentImage.gameObject.SetActive(isContent);

    }

    private void InitializeRelicData()
    {
        for (int i = 0; i < textJson.Count; i++)
        {
            positiveRelic[i] = textJson[i]["Positive_Kr"].ToObject<string>().ToString();
            negativeRelic[i] = textJson[i]["Negative_Kr"].ToObject<string>().ToString();
        }
    }
}
