using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;

public class MainLanguage : MonoBehaviour
{
    public TMP_Text[] mainText;

    public string version;

    private TextAsset textAsset;
    private JArray textData;

    public bool isMainLanguageSetting = false;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.instance.isSetting);
        textAsset = Resources.Load<TextAsset>("TextDB/TextData");
        yield return StartCoroutine(TextDataSetting());
        yield return StartCoroutine(MainLanguageChange(GameManager.instance.data.Language));
        isMainLanguageSetting = true;
    }

    public IEnumerator MainLanguageChange(int language)
    {
        switch (language)
        {
            case 0:
                KoreaMainTextSetting();
                break;
            case 1:
                EnglishMainTextSetting();
                break;
            default:
                //잘못된 값이 들어올 경우 한국어로 변환.
                GameManager.instance.data.Language = 0;
                break;
        }
        yield return null;
    }

    IEnumerator TextDataSetting()
    {
        textData = JArray.Parse(textAsset.text);
        yield return null;
    }
    private void KoreaMainTextSetting()
    {
        mainText[0].text = textData[0]["mainKr"].ToString() + version;
        for (int i = 1; i < mainText.Length; i++)
        {
            mainText[i].text = textData[i]["mainKr"].ToString();
        }
    }
    private void EnglishMainTextSetting()
    {
        mainText[0].text = textData[0]["mainEng"].ToString() + version;
        for (int i = 1; i < mainText.Length; i++)
        {
            mainText[i].text = textData[i]["mainEng"].ToString();
        }
    }
}
