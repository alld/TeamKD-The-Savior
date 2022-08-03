using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPause : MonoBehaviour
{
    // 버튼
    public Button pauseButton;
    public Button continueButton;
    public Button mainTitleButton;
    public Button optionButton;
    public Button exitButton;
    public Button closeOptionButton;

    // 해당 창
    public GameObject pauseImg;
    public GameObject optionImg;

    // 해당 창이 열려있는가?
    private bool isPause = false;
    private bool isOption = false;

    void Start()
    {
        // pause 버튼
        pauseButton.onClick.AddListener(() => OnClick_PauseBtn());
        // 계속하기 버튼
        continueButton.onClick.AddListener(() => OnClick_PauseBtn());
        // 메인 타이틀 버튼
        mainTitleButton.onClick.AddListener(() => OnClick_MainTitleBtn());
        // 옵션 버튼
        optionButton.onClick.AddListener(() => OnClick_OptionBtn());
        closeOptionButton.onClick.AddListener(() => OnClick_OptionBtn());
        // 종료 버튼
        exitButton.onClick.AddListener(() => OnClick_ExitBtn());
    }

    /// <summary>
    /// Pause버튼을 누를 경우 해당 창을 활성화 한다.
    /// 버튼을 한번 더 누르거나 계쏙하기 버튼을 누를 경우 비활성화 한다.
    /// 해당 창이 활성화 된 경우 게임 내 시간을 멈춘다.
    /// </summary>
    private void OnClick_PauseBtn()
    {
        isPause = !isPause;
        pauseImg.SetActive(isPause);
        //Time.timeScale = (isPause == true) ? 0.0f : 1.0f;
    }

    /// <summary>
    /// 메인 타이틀로 이동하는 함수.
    /// </summary>
    private void OnClick_MainTitleBtn()
    {
        //Time.timeScale = 1.0f;
        isPause = false;
        pauseImg.SetActive(isPause);
        GameManager.instance.SceneChange(0);
    }
    /// <summary>
    /// 옵션 창을 활성화 한다.
    /// 한번 더 누를 경우 비활성화 되고, close 버튼을 누를 경우에도 비활성화된다.
    /// </summary>
    private void OnClick_OptionBtn()
    {
        isOption = !isOption;
        optionImg.SetActive(isOption);
    }

    /// <summary>
    /// 게임을 종료한다.
    /// </summary>
    private void OnClick_ExitBtn()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
