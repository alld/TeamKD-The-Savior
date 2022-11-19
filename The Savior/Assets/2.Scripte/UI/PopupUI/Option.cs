using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Option : UI_Popup
{
    // 이 스크립트에서 접근하려고 하는 UI들.

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

        // 각 버튼 / 드롭다운 / 슬라이더를 연결한다.
        dropdowns[(int)OptionDropdowns.Language_DropDown].onValueChanged.AddListener(delegate { onValueChanged_LanguageDropdown(); });
        buttons[(int)OptionButtons.CloseButton].onClick.AddListener(delegate { OnClick_CloseOption(); });

        for (int i = 0; i < sliders.Count; i++)
        {
            int idx = i;
            sliders[idx].onValueChanged.AddListener(delegate { OnValueChanged_SoundSlider(idx); });
            sliders[idx].onValueChanged.AddListener(delegate { OnValueChanged_SoundTextOrMuteImage(idx); });
        }

        // Init 함수가 호출될 때 옵션 창의 Sound Text / Mute Image가 저장된 값에 맞춰 세팅된다.
        for (int i = 0; i < sliders.Count; i++)
        {
            OnValueChanged_SoundTextOrMuteImage(i);
        }

        // 딤드 처리한 이미지
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

    // 드롭다운에서 언어가 변환 되었을 때 변환된 언어에 맞에 텍스트를 갱신합니다.
    private void LanguageChangeTexts()
    {
        // 언어 변환 로직.
        Debug.Log($"언어가 변환되었음 : {NewGameDataManager.instance.GetPlayerData().language}");
    }

    // 드롭다운을 통해 언어를 변환합니다.
    private void onValueChanged_LanguageDropdown()
    {
        PlayerData playerData = NewGameDataManager.instance.GetPlayerData();
        playerData.language = dropdowns[(int)OptionDropdowns.Language_DropDown].value;

        NewGameDataManager.instance.LanguageChange();
        NewGameDataManager.instance.SetPlayerData(playerData);
    }

    // 옵션 창의 Slider의 value 변환값을 playerData에 넣어줍니다.
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
                Debug.Log($"잘못 된 입력값 : OnValueChanged_SoundSlider - {idx}");
                return;
        }

        NewGameDataManager.instance.SetPlayerData(playerData);        
    }

    // 옵션 창의 사운드 value를 통해 value Text와 Mute Image를 출력합니다.
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
                Debug.Log($"잘못 된 입력값 : OnValueChanged_SoundTextOrMuteImage - {idx}");
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
