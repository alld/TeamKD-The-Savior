using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    // ������ ������ ���� ���� �÷��̾�
    public VideoPlayer openingVideo;
    // background ��� ��ȯ���� ���� ���� Ŭ����
    public VideoClip[] videos;
    // video�� ����� �̹���
    public RawImage backGroundImage;

    void Start()
    {
        openingVideo.clip = videos[0];
        backGroundImage.transform.SetAsFirstSibling();
    }

    /// <summary>
    /// ������ ���� ��׶��� ����.
    /// </summary>
    /// <param name="idx">
    /// ��׶��� ��ȣ
    /// </param>
    public void ChangeBackGround(int idx)
    {
        openingVideo.clip = videos[idx];
    }
}
