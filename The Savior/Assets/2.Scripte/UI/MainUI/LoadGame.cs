using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    // ��ư
    public Button loadGameButton;

    // ����� ������ ���� ��� �ҷ����� ��ư ��Ȱ��ȭ.
    public GameObject blindLoadButton;

    void Start()
    {
        // ����� �����Ͱ� ���� ���, ��ư�� ��Ȱ��ȭ ��Ų��.
        if (GameManager.instance.data.gameProgress != -1)
        {
            blindLoadButton.SetActive(false);
        }
        else
        {
            blindLoadButton.SetActive(true);
        }

        loadGameButton.onClick.AddListener(() => OnClick_LoadGameBtn());
    }
    /// <summary>
    /// ����� ���൵�� ȣ���Ͽ� �ش� ������ �̵��Ѵ�.
    /// </summary>
    private void OnClick_LoadGameBtn()
    {
        GameManager.instance.SceneChange(GameManager.instance.data.gameProgress);
    }

}
