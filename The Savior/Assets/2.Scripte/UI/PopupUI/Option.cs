using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Option : UI_Base
{
    // 이 스크립트에서 접근하려고 하는 UI들.
    enum OptionTexts
    {
        OptionTitle_Text,
        Total_Text,
        BGM_Text,
        SFX_Text,
        TotalSound_Text,
        BGMSound_Text,
        SFXSound_Text,
        Language_Text,
        Close_Text,
    }
    enum OptionImages
    {
        Total_Mute,
        BGM_Mute,
        SFX_Mute,
    }
    enum OptionSliders
    {

    }
    enum OptionDropDowns
    {

    }
    enum OptionButtons
    {

    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TMP_Text>(typeof(OptionTexts));
        Bind<Image>(typeof(OptionImages));
        Bind<Slider>(typeof(OptionSliders));
        Bind<Dropdown>(typeof(OptionDropDowns));
        Bind<Button>(typeof(OptionButtons));

        TMP_Text optionTitle = GetText((int)OptionTexts.OptionTitle_Text);

        Debug.Log(optionTitle.gameObject.name);
    }
}
