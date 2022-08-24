using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;

public class PlayLanguage : MonoBehaviour
{
    public TMP_Text[] playText;

    private TextAsset textAsset;
    private JArray textData;

    public bool isPlayLanguageSetting = false;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.instance.isSetting);
        textAsset = Resources.Load<TextAsset>("TextDB/TextData");

        yield return StartCoroutine(PlayTextSetting());

        isPlayLanguageSetting = true;
    }

    IEnumerator PlayTextSetting()
    {
        textData = JArray.Parse(textAsset.text);
        yield return null;
    }

    public IEnumerator PlayLanguageChange(int language)
    {
        switch (language)
        {
            case 0:
                KoreaPlayTextSetting();
                break;
            case 1:
                EnglishPlayTextSetting();
                break;
            default:
                break;
        }
        yield return null;
    }

    private void KoreaPlayTextSetting()
    {
        for (int i = 0; i < playText.Length; i++)
        {
            playText[i].text = textData[i]["playKor"].ToString();
        }
    }

    private void EnglishPlayTextSetting()
    {
        for (int i = 0; i < playText.Length; i++)
        {
            playText[i].text = textData[i]["playEng"].ToString();
        }
    }


}
