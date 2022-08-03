using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    // 버튼
    public Button loadGameButton;

    // 저장된 게임이 없을 경우 불러오기 버튼 비활성화.
    public GameObject blindLoadButton;

    void Start()
    {
        // 저장된 데이터가 없을 경우, 버튼을 비활성화 시킨다.
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
    /// 저장된 진행도를 호출하여 해당 씬으로 이동한다.
    /// </summary>
    private void OnClick_LoadGameBtn()
    {
        GameManager.instance.SceneChange(GameManager.instance.data.gameProgress);
    }

}
