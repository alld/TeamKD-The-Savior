using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningChangeText : MonoBehaviour
{
    public TMP_Text[] openingTextList;



    public void LanguageTranslate()
    {
        for (int i = 2; i <= 7; i++)
        {
            openingTextList[i].text = TextManager.instance.ChangeLanguageText(i + 4);
        }
    }
}
