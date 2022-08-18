using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicInfo : MonoBehaviour
{
    private Button relicContentButton;      // Ŭ�� �� ���� ���� Ȱ��ȭ.
    private Image relicContentImage;        // ���� ���� �̹���
    private RectTransform relicContent;     // ���� ��ũ�� �� content
    private TMP_Text relicContentText;      // ���� ���� �ؽ�Ʈ
    private ScrollRect scrRect;
    private Scrollbar scrbar;

    private bool isContent = false;


    void Start()
    {
        relicContentButton = GetComponent<Button>();
        relicContentImage = Resources.Load<Image>("Relic/RelicContentImage");
        relicContentImage = Instantiate<Image>(relicContentImage, this.transform.parent);
        relicContentText = Resources.Load<TMP_Text>("Relic/RelicContentText");
        scrbar = Resources.Load<Scrollbar>("Relic/RelicContentScrollbar");
        scrbar = Instantiate<Scrollbar>(scrbar, relicContentImage.transform);
        scrRect = relicContentImage.GetComponent<ScrollRect>();
        scrRect.viewport = relicContentImage.transform.GetChild(0).GetComponent<RectTransform>();
        scrRect.content = scrRect.viewport.transform.GetChild(0).GetComponent<RectTransform>();
        relicContentText = Instantiate<TMP_Text>(relicContentText, scrRect.content.transform);
        scrRect.verticalScrollbar = scrbar;
        relicContentImage.GetComponent<ScrollRect>().inertia = false;
        relicContentImage.GetComponent<ScrollRect>().scrollSensitivity = 0;
        relicContentImage.GetComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        relicContentImage.gameObject.SetActive(isContent);
        relicContentButton.onClick.AddListener(() => OnClick_RelicInfoBtn());
    }

    private void OnClick_RelicInfoBtn()
    {
        isContent = !isContent;
        relicContentImage.gameObject.SetActive(isContent);
    }
}
