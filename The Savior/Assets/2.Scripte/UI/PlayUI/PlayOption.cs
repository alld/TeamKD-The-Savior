using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayOption : MonoBehaviour
{
    [Header("���� - ���")]
    public TMP_Dropdown languageDropDown;
    private PlayLanguage playLanguage;

    [Header("���� - ��ü ����")]
    public GameObject soundMuteImg;
    public Slider soundSlider;
    public TMP_Text soundText;

    [Header("���� - ��� ����")]
    public GameObject bgmMuteImg;
    public Slider bgmSlider;
    public TMP_Text bgmText;

    [Header("���� - ȿ�� ����")]
    public GameObject sfxMuteImg;
    public Slider sfxSlider;
    public TMP_Text sfxText;


    IEnumerator Start()
    {
        playLanguage = GetComponent<PlayLanguage>();
        yield return new WaitUntil(() => playLanguage.isPlayLanguageSetting);

        // ���� - ���
        languageDropDown.onValueChanged.AddListener(delegate { OnValueChanged_LanguageDropDown(); });

        // ���� - ����
        soundSlider.onValueChanged.AddListener(delegate { OnValueChanged_SoundSlider(); });
        soundSlider.onValueChanged.AddListener(delegate { OnValueChanged_ChangeSoundText(); });

        // ���� - �����
        bgmSlider.onValueChanged.AddListener(delegate { OnValueChanged_BGMSlider(); });
        bgmSlider.onValueChanged.AddListener(delegate { OnValueChanged_ChangeBGMText(); });

        // ���� - ȿ����
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged_SFXSlider(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged_ChangeSFXText(); });


        UISetting();
    }

    /// <summary>
    /// ����� �����Ϳ� ���� UI�� �������ش�.
    /// </summary>
    private void UISetting()
    {
        OnValueChanged_ChangeSoundText();
        OnValueChanged_ChangeBGMText();
        OnValueChanged_ChangeSFXText();
        OnValueChanged_LanguageSetting();
    }

    /// <summary>
    /// ��Ӵٿ��� ���� �� �����Ѵ�.
    /// ����� ���� �����Ѵ�.
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
    /// ����� �����Ϳ� ���� �� ����.
    /// </summary>
    public void OnValueChanged_LanguageSetting()
    {
        languageDropDown.value = (int)GameManager.instance.data.Language;
    }


    /// <summary>
    /// �����̴��� �̿��Ͽ� ��ü ������ ���� ��ȯ��Ų��.
    /// /// ����� ���� �����Ѵ�.
    /// </summary>
    private void OnValueChanged_SoundSlider()
    {
        GameManager.instance.data.Sound = (int)soundSlider.value;
        StartCoroutine(GameManager.instance.GameSave());
    }

    /// <summary>
    /// �����̴��� �̿��Ͽ� ��� ������ ���� ��ȯ��Ų��.
    /// /// ����� ���� �����Ѵ�.
    /// </summary>
    private void OnValueChanged_BGMSlider()
    {
        GameManager.instance.data.BGM = (int)bgmSlider.value;
        StartCoroutine(GameManager.instance.GameSave());
    }

    /// <summary>
    /// �����̴��� �̿��Ͽ� ȿ�� ������ ���� ��ȯ��Ų��.
    /// /// ����� ���� �����Ѵ�.
    /// </summary>
    private void OnValueChanged_SFXSlider()
    {
        GameManager.instance.data.SFX = (int)sfxSlider.value;
        StartCoroutine(GameManager.instance.GameSave());
    }

    /// <summary>
    /// ����� �����͸� �ҷ��´�.
    /// �����̴� ���� ���� �ؽ�Ʈ ��ȯ.
    /// ���� 0�϶� ��Ʈ �̹��� Ȱ��ȭ.
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
    /// ����� �����͸� �ҷ��´�.
    /// �����̴� ���� ���� �ؽ�Ʈ ��ȯ.
    /// ���� 0�϶� ��Ʈ �̹��� Ȱ��ȭ.
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
    /// ����� �����͸� �ҷ��´�.
    /// �����̴� ���� ���� �ؽ�Ʈ ��ȯ.
    /// ���� 0�϶� ��Ʈ �̹��� Ȱ��ȭ.
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