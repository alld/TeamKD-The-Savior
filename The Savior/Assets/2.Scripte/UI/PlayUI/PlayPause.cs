using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPause : MonoBehaviour
{
    // ��ư
    public Button pauseButton;
    public Button continueButton;
    public Button mainTitleButton;
    public Button optionButton;
    public Button exitButton;
    public Button closeOptionButton;

    // �ش� â
    public GameObject pauseImg;
    public GameObject optionImg;

    // �ش� â�� �����ִ°�?
    private bool isPause = false;
    private bool isOption = false;

    void Start()
    {
        // pause ��ư
        pauseButton.onClick.AddListener(() => OnClick_PauseBtn());
        // ����ϱ� ��ư
        continueButton.onClick.AddListener(() => OnClick_PauseBtn());
        // ���� Ÿ��Ʋ ��ư
        mainTitleButton.onClick.AddListener(() => OnClick_MainTitleBtn());
        // �ɼ� ��ư
        optionButton.onClick.AddListener(() => OnClick_OptionBtn());
        closeOptionButton.onClick.AddListener(() => OnClick_OptionBtn());
        // ���� ��ư
        exitButton.onClick.AddListener(() => OnClick_ExitBtn());
    }

    /// <summary>
    /// Pause��ư�� ���� ��� �ش� â�� Ȱ��ȭ �Ѵ�.
    /// ��ư�� �ѹ� �� �����ų� ����ϱ� ��ư�� ���� ��� ��Ȱ��ȭ �Ѵ�.
    /// �ش� â�� Ȱ��ȭ �� ��� ���� �� �ð��� �����.
    /// </summary>
    private void OnClick_PauseBtn()
    {
        isPause = !isPause;
        pauseImg.SetActive(isPause);
        pauseImg.transform.SetAsLastSibling();
        if (GameManager.instance.dungeonOS != null) Time.timeScale = (isPause == true) ? 0.0f : 1.0f; // ������ �������̸� ���� �����ϴ� ��ũ��Ʈ�� �Ҵ�� ������ null, �Ҵ����̸� ������ �Ͻ�������Ŵ. 
    }

    /// <summary>
    /// ���� Ÿ��Ʋ�� �̵��ϴ� �Լ�.
    /// </summary>
    private void OnClick_MainTitleBtn()
    {
        //Time.timeScale = 1.0f;
        isPause = false;
        pauseImg.SetActive(isPause);
        GameManager.instance.SceneChange(0);
    }
    /// <summary>
    /// �ɼ� â�� Ȱ��ȭ �Ѵ�.
    /// �ѹ� �� ���� ��� ��Ȱ��ȭ �ǰ�, close ��ư�� ���� ��쿡�� ��Ȱ��ȭ�ȴ�.
    /// </summary>
    private void OnClick_OptionBtn()
    {
        isOption = !isOption;
        optionImg.SetActive(isOption);
    }

    /// <summary>
    /// ������ �����Ѵ�.
    /// </summary>
    private void OnClick_ExitBtn()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
