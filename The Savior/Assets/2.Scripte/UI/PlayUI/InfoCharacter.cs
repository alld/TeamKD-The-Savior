using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCharacter : MonoBehaviour
{
    // 버튼
    public Button closeInfoButton;
    public Button skillButton;
    public Button statusButton;
    public Button identityButton;

    // 해당 이미지
    public GameObject charInfo; // : CharacterInfo Image  
    public GameObject skillImg;
    public GameObject statusImg;
    public GameObject identityImg;

    //해당 창이 열려 있는가 ?
    private bool isOpen = false;

    void Start()
    {
        closeInfoButton.onClick.AddListener(() => OnClick_CloseInfoBtn());
        skillButton.onClick.AddListener(() => OnClick_SkillBtn());
        statusButton.onClick.AddListener(() => OnClick_StatusBtn());
        identityButton.onClick.AddListener(() => OnClick_IdentityBtn());
    }

    /// <summary>
    /// 인벤토리에서 해당 캐릭터를 클릭시 상세정보창이 활성화됨.
    /// </summary>
    public void OnCharacterInfo()
    {
        isOpen = true;
        charInfo.SetActive(isOpen);
    }

    private void OnClick_CloseInfoBtn()
    {
        isOpen = false;
        charInfo.SetActive(isOpen);
    }

    private void OnClick_SkillBtn()
    {
        skillImg.SetActive(true);
        statusImg.SetActive(false);
        identityImg.SetActive(false);
    }

    private void OnClick_StatusBtn()
    {
        statusImg.SetActive(true);

        skillImg.SetActive(false);
        identityImg.SetActive(false);
    }

    private void OnClick_IdentityBtn()
    {
        identityImg.SetActive(true);

        skillImg.SetActive(false);
        statusImg.SetActive(false);
    }
}
