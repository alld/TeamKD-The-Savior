using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class RelicInfo : MonoBehaviour
{
    private TextAsset textData;             // ������ ���� ������
    private TextAsset relicData;            // ������ ������ ��

    private Button relicContentButton;      // Ŭ�� �� ���� ���� Ȱ��ȭ.
    private bool isContent = false;         // ���� ������ â�� ����

    // ��Ʈ���� �迭�� �����Ͽ� �ؽ�Ʈ ��ü�� �ϳ��� + ����.
    private string[] positive = new string[5];  // ������ ������ �̷ο� ȿ��
    private string[] negative = new string[5];  // ������ ������ �طο� ȿ��

    // ��Ʈ���� ������ �� ����
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

        relicPositiveText.text = "[�̷ο� ȿ��]";
        relicNegativeText.text = "[�طο� ȿ��]";

        textJson = JArray.Parse(textData.text);
        dataJson = JArray.Parse(relicData.text);

        InitializeRelicData();
    }

    /// <summary>
    /// ���� ������â ��ư Ŭ��.
    /// </summary>
    private void OnClick_RelicInfoActiveBtn()
    {
        isContent = !isContent;
        relicContentImage.gameObject.SetActive(isContent);

        OnClick_RelicInfoBtn();
    }

    /// <summary>
    /// ���� ������ â�� �ؽ�Ʈ ���.
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
