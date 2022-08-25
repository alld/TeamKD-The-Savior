using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class WorldMapLanguage : MonoBehaviour
{
    TextAsset textAsset;
    JArray jsonData;

    public TMP_Text[] worldText;

    public bool isWorldTextSetting = false;
    IEnumerator Start()
    {
        yield return StartCoroutine(WorldMapTextSetting());
        WorldLanguageChange(GameManager.instance.data.Language);
        isWorldTextSetting = true;
    }

    IEnumerator WorldMapTextSetting()
    {
        textAsset = Resources.Load<TextAsset>("TextDB/TextData");
        jsonData = JArray.Parse(textAsset.text);
        yield return null;
    }

    public void WorldLanguageChange(int language)
    {
        switch (language)
        {
            case 0:
                KorWorldText();
                break;
            case 1:
                EngWorldText();
                break;
            default:
                break;
        }
    }

    private void KorWorldText()
    {
        for (int i = 0; i < worldText.Length; i++)
        {
            worldText[i].text = jsonData[i]["worldKor"].ToString();
        }
    }

    private void EngWorldText()
    {
        for (int i = 0; i < worldText.Length; i++)
        {
            worldText[i].text = jsonData[i]["worldEng"].ToString();
        }
    }
}
