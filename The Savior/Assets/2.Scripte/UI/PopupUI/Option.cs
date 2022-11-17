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
        TotalSound_Slider,
        BGMSound_Slider,
        SFXSound_Slider,
    }
    enum OptionDropdowns
    {
        Language_DropDown,
    }
    enum OptionButtons
    {
        CloseButton,
    }

    private List<TMP_Text> texts = new List<TMP_Text>();
    private List<Image> images = new List<Image>();
    private List<Slider> sliders = new List<Slider>();
    private List<TMP_Dropdown> dropdowns = new List<TMP_Dropdown>();
    private List<Button> buttons = new List<Button>();

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TMP_Text>(typeof(OptionTexts));
        Bind<Image>(typeof(OptionImages));
        Bind<Slider>(typeof(OptionSliders));
        Bind<TMP_Dropdown>(typeof(OptionDropdowns));
        Bind<Button>(typeof(OptionButtons));

        texts = Get<TMP_Text>();
        images = Get<Image>();
        sliders = Get<Slider>();
        dropdowns = Get<TMP_Dropdown>();
        buttons = Get<Button>();
    }
}
