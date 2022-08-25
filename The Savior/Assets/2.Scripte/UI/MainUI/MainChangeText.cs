using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainChangeText : MonoBehaviour
{
    [Header("언어 변경할 문자열")]
    // 언어 변경을 위한 배열
    public TMP_Text[] changeTextList;
    // 해당 씬에서는 0번 텍스트부터 변환함.
    private const int startChangeText = 0;

    public void TranslateLanguage()
    {
        for (int i = 0; i < changeTextList.Length; i++)
        {
            changeTextList[i].text = TextManager.instance.ChangeLanguageText(i + startChangeText);
        }
    }
}