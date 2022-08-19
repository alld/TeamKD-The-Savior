using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
        relicContentImage.gameObject.SetActive(!isContent);

        relicPositiveContentText.text = null;
        relicNegativeContentText.text = null;

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.instance.data.equipRelic[i] == 0) continue;
            
            // �̷ο� ȿ�� ��Ʈ��
            StringBuilder positiveStr = new StringBuilder();
            positiveStr.Append("<color=#ffdc73>");
            positiveStr.Append(textJson[GameManager.instance.data.equipRelic[i] - 1]["Name_Kr"].ToObject<string>());
            positiveStr.Append("</color>");
            positiveStr.Append(" : ");
            positiveStr.Append(positiveRelic[GameManager.instance.data.equipRelic[i] - 1]);
            positiveStr.Append("\n");

            positive[i] = positiveStr.ToString();

            // �طο� ȿ�� ��Ʈ��
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
