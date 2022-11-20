using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pause : UI_Popup
{
    enum PauseTexts
    {
        ContinueText,
        MainSceneText,
        OptionText,
        ExitText,
    }
    enum PauseButtons
    {
        ContinueButton,
        MainSceneButton,
        OptionButton,
        ExitButton,
    }
    enum PauseImanges
    {
        Dimmed,
    }

    private List<TMP_Text> texts = new List<TMP_Text>();
    private List<Button> buttons = new List<Button>();
    private List<Image> images = new List<Image>();

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        NewGameDataManager.instance.ObserberLanguage(LanguageChangeTexts);

        BindObjects();
        SetObjects();

        // 시간 정지.
        Time.timeScale = 0.0f;

        // OnClick 연결
        buttons[(int)PauseButtons.ContinueButton].onClick.AddListener(delegate { OnClick_ContinueButton(); });
        buttons[(int)PauseButtons.MainSceneButton].onClick.AddListener(delegate { OnClick_MainSceneButton(); });
        buttons[(int)PauseButtons.OptionButton].onClick.AddListener(delegate { OnClick_OptionButton(); });
        buttons[(int)PauseButtons.ExitButton].onClick.AddListener(delegate { OnClick_ExitButton(); });
    }

    protected override void BindObjects()
    {
        Bind<TMP_Text>(typeof(PauseTexts));
        Bind<Button>(typeof(PauseButtons));
        Bind<Image>(typeof(PauseImanges));
    }

    protected override void SetObjects()
    {
        texts = Get<TMP_Text>(typeof(PauseTexts));
        buttons = Get<Button>(typeof(PauseButtons));
        images = Get<Image>(typeof(PauseImanges));
    }

    // 드롭다운에서 언어가 변환 되었을 때 변환된 언어에 맞에 텍스트를 갱신합니다.
    private void LanguageChangeTexts()
    {
        // 언어 변환 로직.
    }

    private void OnClick_ContinueButton() { UIManager.instance.Hide(); Time.timeScale = 1.0f; }
    private void OnClick_MainSceneButton() { }
    private void OnClick_OptionButton() { UIManager.instance.Show("Option"); }
    private void OnClick_ExitButton() { Application.Quit(); UnityEditor.EditorApplication.isPlaying = false; }


}
