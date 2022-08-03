using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    // ��ư
    public Button newGameButton;
    public Button closeWarnningButton;
    public Button newGameWarnningButton;

    // ����� �����Ͱ� ���� ��� ��µǴ� ���â.
    public GameObject warnningNewGame;

    void Start()
    {
        // �� ���� ��ư Ŭ���� ȣ��
        newGameButton.onClick.AddListener(() => OnClick_NewGameBtn());
        // ��� â �ݱ� ��ư Ŭ���� ȣ��
        closeWarnningButton.onClick.AddListener(() => OnClick_CloseWarnningBtn());
        // ��� â�� ���� �� ���� ��ư�� Ŭ���� ȣ��
        newGameWarnningButton.onClick.AddListener(() => OnClick_NewGameWarnningBtn());
    }


    /// <summary>
    /// �� ���� ��ư�� ���� �� �̹� ����� �����Ͱ� �ִٸ�, ���â�� Ȱ��ȭ��Ŵ.
    /// ����� �����Ͱ� ���ٸ�, �� ������ ������.
    /// </summary>
    private void OnClick_NewGameBtn()
    {
        if(GameManager.instance.data.gameProgress != -1)
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
    /// ���â�� �ݴ� �Լ�
    /// </summary>
    private void OnClick_CloseWarnningBtn()
    {
        warnningNewGame.SetActive(false);
    }

    /// <summary>
    /// ���â���� ���� ����.
    /// </summary>
    private void OnClick_NewGameWarnningBtn()
    {
        warnningNewGame.SetActive(false);
        GameManager.instance.SceneChange(1);
    }
}
