using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Option : UI_Popup
{
    // �� ��ũ��Ʈ���� �����Ϸ��� �ϴ� UI��.
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
        NewGameDataManager.instance.ObserberLanguage(LanguageChangeTexts);

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

        // �� ��ư / ��Ӵٿ� / �����̴��� �����Ѵ�.
        dropdowns[(int)OptionDropdowns.Language_DropDown].onValueChanged.AddListener(delegate { onValueChanged_LanguageDropdown(); });

        for (int i = 0; i < sliders.Count; i++)
        {
            int idx = i;
            sliders[idx].onValueChanged.AddListener(delegate { OnValueChanged_SoundSlider(idx); });
            sliders[idx].onValueChanged.AddListener(delegate { OnValueChanged_SoundTextOrMuteImage(idx); });
        }

        // Init �Լ��� ȣ��� �� �ɼ� â�� Sound Text / Mute Image�� ����� ���� ���� ���õȴ�.
        for (int i = 0; i < sliders.Count; i++)
        {
            OnValueChanged_SoundTextOrMuteImage(i);
        }
    }

    private void LanguageChangeTexts()
    {
        // ��� ��ȯ ����.
    }

    private void onValueChanged_LanguageDropdown()
    {
        PlayerData playerData = NewGameDataManager.instance.GetPlayerData();
        playerData.language = dropdowns[(int)OptionDropdowns.Language_DropDown].value;

        NewGameDataManager.instance.LanguageChange();
        NewGameDataManager.instance.SetPlayerData(playerData);
    }

    private void OnValueChanged_SoundSlider(int idx)
    {
        PlayerData playerData = NewGameDataManager.instance.GetPlayerData();

        switch (idx)
        {
            case (int)OptionSliders.TotalSound_Slider:
                playerData.sound = Mathf.RoundToInt(sliders[(int)OptionSliders.TotalSound_Slider].value);
                break;
            case (int)OptionSliders.BGMSound_Slider:
                playerData.bgm = Mathf.RoundToInt(sliders[(int)OptionSliders.BGMSound_Slider].value);
                break;
            case (int)OptionSliders.SFXSound_Slider:
                playerData.sfx = Mathf.RoundToInt(sliders[(int)OptionSliders.SFXSound_Slider].value);
                break;
            default:
                Debug.Log($"�߸� �� �Է°� : OnValueChanged_SoundSlider - {idx}");
                return;
        }

        NewGameDataManager.instance.SetPlayerData(playerData);
    }

    private void OnValueChanged_SoundTextOrMuteImage(int idx)
    {
        PlayerData playerData = NewGameDataManager.instance.GetPlayerData();
        Slider slider;
        string soundValueText;

        switch (idx)
        {
            case (int)OptionSliders.TotalSound_Slider:
                slider = sliders[(int)OptionSliders.TotalSound_Slider];
                slider.value = playerData.sound;
                soundValueText = playerData.sound.ToString();
                break;
            case (int)OptionSliders.BGMSound_Slider:
                slider = sliders[(int)OptionSliders.BGMSound_Slider];
                slider.value = playerData.bgm;
                soundValueText = playerData.bgm.ToString();
                break;
            case (int)OptionSliders.SFXSound_Slider:
                slider = sliders[(int)OptionSliders.SFXSound_Slider];
                slider.value = playerData.sfx;
                soundValueText = playerData.sfx.ToString();
                break;
            default:
                Debug.Log($"�߸� �� �Է°� : OnValueChanged_SoundTextOrMuteImage - {idx}");
                return;
        }

        if (slider.value == 0)
        {
            texts[idx].text = "";
            images[idx].gameObject.SetActive(true);
        }
        else
        {
            texts[idx].text = soundValueText;
            images[idx].gameObject.SetActive(false);
        }
    }
}
