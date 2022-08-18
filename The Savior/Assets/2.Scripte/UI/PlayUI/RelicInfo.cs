using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicInfo : MonoBehaviour
{
    private Button relicContentButton;      // 클릭 시 유물 정보 활성화.
    private Image relicContentImage;        // 유물 정보 이미지
    private RectTransform relicContent;     // 유물 스크롤 뷰 content
    private TMP_Text relicContentText;      // 유물 정보 텍스트
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
