using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainChangeText : MonoBehaviour
{
    [Header("��� ������ ���ڿ�")]
    // ��� ������ ���� �迭
    public TMP_Text[] changeTextList;
    // �ش� �������� 0�� �ؽ�Ʈ���� ��ȯ��.
    private const int startChangeText = 0;

    public void TranslateLanguage()
    {
        for (int i = 0; i < changeTextList.Length; i++)
        {
            changeTextList[i].text = TextManager.instance.ChangeLanguageText(i + startChangeText);
        }
    }
}