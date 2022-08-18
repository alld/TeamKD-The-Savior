using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RelicInfo : MonoBehaviour
{
    private Button relicContentButton;
    private Image relicContentImage;
    private TMP_Text relicContentText;

    void Start()
    {
        relicContentButton = GetComponent<Button>();
        relicContentImage = Resources.Load<Image>("Relic/RelicContentImage");
        relicContentText = Resources.Load<TMP_Text>("Relic/RelicContentText");
        relicContentButton.onClick.AddListener(() => OnClick_RelicInfoBtn());
    }

    private void OnClick_RelicInfoBtn()
    {

    }
}
