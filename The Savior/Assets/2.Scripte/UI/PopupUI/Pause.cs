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

        // �ð� ����.
        Time.timeScale = 0.0f;

        // OnClick ����
        buttons[(int)PauseButtons.ContinueButton].onClick.AddListener(delegate { OnClick_ContinueButton(); });
        buttons[(int)PauseButtons.MainSceneButton].onClick.AddListener(delegate { OnClick_MainSceneButton(); });
        buttons[(int)PauseButtons.OptionButton].onClick.AddListener(delegate { OnClick_OptionButton(); });
        buttons[(int)PauseButtons.ExitButton].onClick.AddListener(delegate { OnClick_ExitButton(); });
    }

    private void BindObjects()
    {
        Bind<TMP_Text>(typeof(PauseTexts));
        Bind<Button>(typeof(PauseButtons));
        Bind<Image>(typeof(PauseImanges));
    }

    private void SetObjects()
    {
        texts = Get<TMP_Text>(typeof(PauseTexts));
        buttons = Get<Button>(typeof(PauseButtons));
        images = Get<Image>(typeof(PauseImanges));
    }

    // ��Ӵٿ�� �� ��ȯ �Ǿ��� �� ��ȯ�� �� �¿� �ؽ�Ʈ�� �����մϴ�.
    private void LanguageChangeTexts()
    {
        // ��� ��ȯ ����.
    }

    private void OnClick_ContinueButton() { UIManager.instance.Hide(); Time.timeScale = 1.0f; }
    private void OnClick_MainSceneButton() { }
    private void OnClick_OptionButton() { UIManager.instance.Show("Option"); }
    private void OnClick_ExitButton() { Application.Quit(); UnityEditor.EditorApplication.isPlaying = false; }


}
