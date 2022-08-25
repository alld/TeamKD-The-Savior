using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayOption : MonoBehaviour
{
    [Header("세팅 - 언어")]
    public TMP_Dropdown languageDropDown;
    private PlayLanguage playLanguage;

    [Header("세팅 - 전체 사운드")]
    public GameObject soundMuteImg;
    public Slider soundSlider;
    public TMP_Text soundText;

    [Header("세팅 - 배경 사운드")]
    public GameObject bgmMuteImg;
    public Slider bgmSlider;
    public TMP_Text bgmText;

    [Header("세팅 - 효과 사운드")]
    public GameObject sfxMuteImg;
    public Slider sfxSlider;
    public TMP_Text sfxText;


    IEnumerator Start()
    {
        playLanguage = GetComponent<PlayLanguage>();
        yield return new WaitUntil(() => playLanguage.isPlayLanguageSetting);

        // 세팅 - 언어
        languageDropDown.onValueChanged.AddListener(delegate { OnValueChanged_LanguageDropDown(); });

        // 세팅 - 사운드
        soundSlider.onValueChanged.AddListener(delegate { OnValueChanged_SoundSlider(); });
        soundSlider.onValueChanged.AddListener(delegate { OnValueChanged_ChangeSoundText(); });

        // 세팅 - 배경음
        bgmSlider.onValueChanged.AddListener(delegate { OnValueChanged_BGMSlider(); });
        bgmSlider.onValueChanged.AddListener(delegate { OnValueChanged_ChangeBGMText(); });

        // 세팅 - 효과음
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged_SFXSlider(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged_ChangeSFXText(); });


        UISetting();
    }

    /// <summary>
    /// 저장된 데이터에 따라 UI를 세팅해준다.
    /// </summary>
    private void UISetting()
    {
        OnValueChanged_ChangeSoundText();
        OnValueChanged_ChangeBGMText();
        OnValueChanged_ChangeSFXText();
        OnValueChanged_LanguageSetting();
    }

    /// <summary>
    /// 드롭다운을 통해 언어를 변경한다.
    /// 변경된 값을 저장한다.
    /// </summary>
    private void OnValueChanged_LanguageDropDown()
    {
        GameManager.instance.data.Language = languageDropDown.value;
        playLanguage.PlayLanguageChange(GameManager.instance.data.Language);

        if (GameManager.instance.currentlyScene == "WorldMap")
        {
            WorldMapLanguage world = GameObject.Find("WUIManager").GetComponent<WorldMapLanguage>();
            world.WorldLanguageChange(GameManager.instance.data.Language);
        }

        StartCoroutine(GameManager.instance.GameSave());
    }

    /// <summary>
    /// 저장된 데이터에 따라 언어값 변경.
    /// </summary>
    public void OnValueChanged_LanguageSetting()
    {
        languageDropDown.value = (int)GameManager.instance.data.Language;
    }


    /// <summary>
    /// 슬라이더를 이용하여 전체 사운드의 값을 변환시킨다.
    /// /// 변경된 값을 저장한다.
    /// </summary>
    private void OnValueChanged_SoundSlider()
    {
        GameManager.instance.data.Sound = (int)soundSlider.value;
        StartCoroutine(GameManager.instance.GameSave());
    }

    /// <summary>
    /// 슬라이더를 이용하여 배경 사운드의 값을 변환시킨다.
    /// /// 변경된 값을 저장한다.
    /// </summary>
    private void OnValueChanged_BGMSlider()
    {
        GameManager.instance.data.BGM = (int)bgmSlider.value;
        StartCoroutine(GameManager.instance.GameSave());
    }

    /// <summary>
    /// 슬라이더를 이용하여 효과 사운드의 값을 변환시킨다.
    /// /// 변경된 값을 저장한다.
    /// </summary>
    private void OnValueChanged_SFXSlider()
    {
        GameManager.instance.data.SFX = (int)sfxSlider.value;
        StartCoroutine(GameManager.instance.GameSave());
    }

    /// <summary>
    /// 저장된 데이터를 불러온다.
    /// 슬라이더 값에 따라 텍스트 변환.
    /// 값이 0일땐 뮤트 이미지 활성화.
    /// </summary>
    private void OnValueChanged_ChangeSoundText()
    {
        soundSlider.value = GameManager.instance.data.Sound;
        if (soundSlider.value == 0)
        {
            soundText.text = "";
            soundMuteImg.SetActive(true);
        }
        else
        {
            soundText.text = GameManager.instance.data.Sound.ToString();
            soundMuteImg.SetActive(false);
        }
    }

    /// <summary>
    /// 저장된 데이터를 불러온다.
    /// 슬라이더 값에 따라 텍스트 변환.
    /// 값이 0일땐 뮤트 이미지 활성화.
    /// </summary>
    private void OnValueChanged_ChangeBGMText()
    {
        bgmSlider.value = GameManager.instance.data.BGM;
        if (bgmSlider.value == 0)
        {
            bgmText.text = "";
            bgmMuteImg.SetActive(true);
        }
        else
        {
            bgmText.text = GameManager.instance.data.BGM.ToString();
            bgmMuteImg.SetActive(false);
        }
    }

    /// <summary>
    /// 저장된 데이터를 불러온다.
    /// 슬라이더 값에 따라 텍스트 변환.
    /// 값이 0일땐 뮤트 이미지 활성화.
    /// </summary>
    private void OnValueChanged_ChangeSFXText()
    {
        sfxSlider.value = GameManager.instance.data.SFX;
        if (sfxSlider.value == 0)
        {
            sfxText.text = "";
            sfxMuteImg.SetActive(true);
        }
        else
        {
            sfxText.text = GameManager.instance.data.SFX.ToString();
            sfxMuteImg.SetActive(false);
        }
    }
}
