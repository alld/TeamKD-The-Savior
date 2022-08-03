using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCollect : MonoBehaviour
{
    // 버튼
    public Button collectButton;
    public Button closeCollectButton;

    // 업적창 이미지
    public GameObject collectImg;

    private void Start()
    {
        collectButton.onClick.AddListener(() => OnClick_CollectBtn());
        closeCollectButton.onClick.AddListener(() => OnClick_CloseCollectBtn());
    }


    /// <summary>
    ///  업적창 활성화
    /// </summary>
    private void OnClick_CollectBtn()
    {
        collectImg.SetActive(true);
    }

    /// <summary>
    /// 업적창 비활성화
    /// </summary>
    private void OnClick_CloseCollectBtn()
    {
        collectImg.SetActive(false);
    }
}
