using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Option : UI_Popup
{
    // �� ��ũ��Ʈ���� �����Ϸ��� �ϴ� UI��.

    enum SoundTexts
    {
        TotalSound_Text,
        BGMSound_Text,
        SFXSound_Text,
    }
    enum OptionTexts
    {
        Total_Text,
        BGM_Text,
        SFX_Text,
        OptionTitle_Text,
        Language_Text,
        Close_Text,
    }
    enum OptionImages
    {
        Total_Mute,
        BGM_Mute,
        SFX_Mute,
        Dimmed,
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

    private List<TMP_Text> soundTexts = new List<TMP_Text>();
    private List<TMP_Text> texts = new List<TMP_Text>();
    private List<Image> images = new List<Image>();
    private List<Slider> sliders = new List<Slider>();
    private List<TMP_Dropdown> dropdowns = new List<TMP_Dropdown>();
    private List<Button> buttons = new List<Button>();

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        NewGameDataManager.instance.ObserberLanguage(LanguageChangeTexts);

        BindObjects();
        SetObjects();

        // �� ��ư / ��Ӵٿ� / �����̴��� �����Ѵ�.
        dropdowns[(int)OptionDropdowns.Language_DropDown].onValueChanged.AddListener(delegate { onValueChanged_LanguageDropdown(); });
        buttons[(int)OptionButtons.CloseButton].onClick.AddListener(delegate { OnClick_CloseOption(); });

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

        // ���� ó���� �̹���
        images[(int)OptionImages.Dimmed].SetToScreenSize();
    }

    private void BindObjects()
    {
        Bind<TMP_Text>(typeof(SoundTexts));
        Bind<TMP_Text>(typeof(OptionTexts));
        Bind<Image>(typeof(OptionImages));
        Bind<Slider>(typeof(OptionSliders));
        Bind<TMP_Dropdown>(typeof(OptionDropdowns));
        Bind<Button>(typeof(OptionButtons));
    }

    private void SetObjects()
    {
        soundTexts = Get<TMP_Text>(typeof(SoundTexts));
        texts = Get<TMP_Text>(typeof(OptionTexts));
        images = Get<Image>(typeof(OptionImages));
        sliders = Get<Slider>(typeof(OptionSliders));
        dropdowns = Get<TMP_Dropdown>(typeof(OptionDropdowns));
        buttons = Get<Button>(typeof(OptionButtons));
    }

    // ��Ӵٿ�� �� ��ȯ �Ǿ��� �� ��ȯ�� �� �¿� �ؽ�Ʈ�� �����մϴ�.
    private void LanguageChangeTexts()
    {
        // ��� ��ȯ ����.
        Debug.Log($"�� ��ȯ�Ǿ��� : {NewGameDataManager.instance.GetPlayerData().language}");
    }

    // ��Ӵٿ��� ���� �� ��ȯ�մϴ�.
    private void onValueChanged_LanguageDropdown()
    {
        PlayerData playerData = NewGameDataManager.instance.GetPlayerData();
        playerData.language = dropdowns[(int)OptionDropdowns.Language_DropDown].value;

        NewGameDataManager.instance.LanguageChange();
        NewGameDataManager.instance.SetPlayerData(playerData);
    }

    // �ɼ� â�� Slider�� value ��ȯ���� playerData�� �־��ݴϴ�.
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

    // �ɼ� â�� ���� value�� ���� value Text�� Mute Image�� ����մϴ�.
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
            soundTexts[idx].text = "";
            images[idx].gameObject.SetActive(true);
        }
        else
        {
            soundTexts[idx].text = soundValueText;
            images[idx].gameObject.SetActive(false);
        }
    }

    private void OnClick_CloseOption()
    {
        NewGameDataManager.instance.SaveGameData();
        UIManager.instance.Hide();
    }
}
