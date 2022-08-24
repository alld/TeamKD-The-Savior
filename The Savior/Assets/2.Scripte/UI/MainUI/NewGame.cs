using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    // 버튼
    public Button newGameButton;
    public Button closeWarnningButton;
    public Button newGameWarnningButton;

    // 저장된 데이터가 있을 경우 출력되는 경고창.
    public GameObject warnningNewGame;

    void Start()
    {
        // 새 게임 버튼 클릭시 호출
        newGameButton.onClick.AddListener(() => OnClick_NewGameBtn());
        // 경고 창 닫기 버튼 클릭시 호출
        closeWarnningButton.onClick.AddListener(() => OnClick_CloseWarnningBtn());
        // 경고 창을 통해 새 게임 버튼을 클릭시 호출
        newGameWarnningButton.onClick.AddListener(() => OnClick_NewGameWarnningBtn());
    }


    /// <summary>
    /// 새 게임 버튼을 누를 때 이미 저장된 데이터가 있다면, 경고창을 활성화시킴.
    /// 저장된 데이터가 없다면, 새 게임을 시작함.
    /// </summary>
    private void OnClick_NewGameBtn()
    {
        if (GameManager.instance.data.gameProgress != -1)
        {
            warnningNewGame.SetActive(true);
        }
        else
        {
            GameManager.instance.SceneChange(1);
            GameManager.instance.data.gameProgress = 1;
        }
    }

    /// <summary>
    /// 경고창을 닫는 함수
    /// </summary>
    private void OnClick_CloseWarnningBtn()
    {
        warnningNewGame.SetActive(false);
    }

    /// <summary>
    /// 경고창에서 게임 시작.
    /// </summary>
    private void OnClick_NewGameWarnningBtn()
    {
        StartCoroutine(NewStart());
    }

    private IEnumerator NewStart()
    {
        warnningNewGame.SetActive(false);
        // 새 게임 시작시 데이터 초기화
        yield return StartCoroutine(GameManager.instance.GameReset());
        GameManager.instance.SceneChange(1);
    }
}
