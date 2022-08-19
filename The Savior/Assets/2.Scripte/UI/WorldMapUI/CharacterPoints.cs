using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPoints : MonoBehaviour
{
    public Image myPointImg; // ���� �� ��ġ�� �˷��ִ� ��Ŀ �̹���

    public Button[] points;  // ����� �� ��ư Ŭ���� �ش� ��ġ�� ��Ŀ�� �̵�

    void Start()
    {
        myPointImg.gameObject.SetActive(true);
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
        myPointImg.rectTransform.anchorMax = new Vector2(0.67f, 1.2f);
        myPointImg.rectTransform.anchorMin = new Vector2(0.33f, 0.6f);
        myPointImg.rectTransform.offsetMax = Vector3.zero;
        myPointImg.rectTransform.offsetMin = Vector3.zero;
    }

}
