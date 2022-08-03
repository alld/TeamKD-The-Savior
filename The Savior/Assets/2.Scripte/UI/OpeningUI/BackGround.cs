using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    // 오프닝 씬에서 사용될 비디오 플레이어
    public VideoPlayer openingVideo;
    // background 장면 변환마다 사용될 비디오 클립들
    public VideoClip[] videos;
    // video를 출력할 이미지
    public RawImage backGroundImage;

    void Start()
    {
        openingVideo.clip = videos[0];
        backGroundImage.transform.SetAsFirstSibling();
    }

    /// <summary>
    /// 오프닝 씬의 백그라운드 변경.
    /// </summary>
    /// <param name="idx">
    /// 백그라운드 번호
    /// </param>
    public void ChangeBackGround(int idx)
    {
        openingVideo.clip = videos[idx];
    }
}
