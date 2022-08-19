using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
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
    private string[] positiveRelic = new string[5];
    private string[] negativeRelic = new string[5];

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
        relicPositiveContentText.text = null;
        relicNegativeContentText.text = null;

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.instance.data.equipRelic[i] == 0) continue;
            positive[i] = "<color=#ffdc73>" + textJson[GameManager.instance.data.equipRelic[i] - 1]["Name_Kr"].ToObject<string>()+ "</color>" + " : " + positiveRelic[GameManager.instance.data.equipRelic[i] - 1] + "\n";
            negative[i] = "<color=#ffdc73>" + textJson[GameManager.instance.data.equipRelic[i] - 1]["Name_Kr"].ToObject<string>()+ "</color>" + " : " + negativeRelic[GameManager.instance.data.equipRelic[i] - 1] + "\n";
            Debug.Log(GameManager.instance.data.equipRelic[i] - 1);
            relicPositiveContentText.text += positive[i];
            relicNegativeContentText.text += negative[i];
        } 
    }

    private void InitializeRelicData()
    {
        positiveRelic[0] = string.Format(textJson[0]["Positive_Kr"].ToObject<string>(), dataJson[0]["effectValue"].ToObject<float>());
        negativeRelic[0] = string.Format(textJson[0]["Negative_Kr"].ToObject<string>(), dataJson[0]["negEffectValue"].ToObject<float>());

        positiveRelic[1] = string.Format(textJson[1]["Positive_Kr"].ToObject<string>(), dataJson[1]["effectValue"].ToObject<float>());
        negativeRelic[1] = string.Format(textJson[1]["Negative_Kr"].ToObject<string>(), dataJson[1]["negEffectValue"].ToObject<float>());

        positiveRelic[2] = string.Format(textJson[2]["Positive_Kr"].ToObject<string>(), dataJson[2]["effectValue"].ToObject<float>());
        negativeRelic[2] = string.Format(textJson[2]["Negative_Kr"].ToObject<string>(), dataJson[2]["negEffectValue"].ToObject<float>());

        positiveRelic[3] = string.Format(textJson[3]["Positive_Kr"].ToObject<string>(), dataJson[3]["effectValue"].ToObject<float>(), dataJson[3]["effectDataB1"].ToObject<float>());
        negativeRelic[3] = string.Format(textJson[3]["Negative_Kr"].ToObject<string>(), dataJson[3]["negEffectValue"].ToObject<float>());

        positiveRelic[4] = string.Format(textJson[4]["Positive_Kr"].ToObject<string>(), dataJson[4]["effectValue"].ToObject<float>());
        negativeRelic[4] = string.Format(textJson[4]["Negative_Kr"].ToObject<string>(), dataJson[4]["negEffectValue"].ToObject<float>());
    }
}
