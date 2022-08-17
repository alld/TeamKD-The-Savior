using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPoints : MonoBehaviour
{
    public Image myPointImg; // 현재 내 위치를 알려주는 마커 이미지

    public Button[] points;  // 월드맵 내 버튼 클릭시 해당 위치로 마커를 이동

    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            int idx = i;
            points[idx].onClick.AddListener(() => OnClick_MarkerPointBtn(idx));
        }
        OnClick_MarkerPointBtn(GameManager.instance.data.myPoint);
    }

    private void OnClick_MarkerPointBtn(int n)
    {
        GameManager.instance.data.myPoint = n;
        myPointImg.transform.SetParent(points[n].transform);
        myPointImg.rectTransform.anchorMax = new Vector2(0.65f, 1.0f);
        myPointImg.rectTransform.anchorMin = new Vector2(0.35f, 0.7f);
        myPointImg.rectTransform.offsetMax = Vector3.zero;
        myPointImg.rectTransform.offsetMin = Vector3.zero;
    }

}
