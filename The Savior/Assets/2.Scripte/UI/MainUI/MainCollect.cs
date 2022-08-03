using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCollect : MonoBehaviour
{
    // ��ư
    public Button collectButton;
    public Button closeCollectButton;

    // ����â �̹���
    public GameObject collectImg;

    private void Start()
    {
        collectButton.onClick.AddListener(() => OnClick_CollectBtn());
        closeCollectButton.onClick.AddListener(() => OnClick_CloseCollectBtn());
    }


    /// <summary>
    ///  ����â Ȱ��ȭ
    /// </summary>
    private void OnClick_CollectBtn()
    {
        collectImg.SetActive(true);
    }

    /// <summary>
    /// ����â ��Ȱ��ȭ
    /// </summary>
    private void OnClick_CloseCollectBtn()
    {
        collectImg.SetActive(false);
    }
}
