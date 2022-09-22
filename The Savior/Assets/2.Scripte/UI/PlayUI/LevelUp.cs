using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private Button levelUpButton;
    [SerializeField] private Button closeButton;

    [SerializeField]private Slider addExpSlider;

    [SerializeField] private Image expBar;

    private float percentage = 0.01f;
    private float addExp;

    private int curExp;
    [SerializeField] private TMP_Text addExpText;

    private InfoCharacter info;

    void Start()
    {
        info = GameObject.Find("PUIManager").GetComponent<InfoCharacter>();
        curExp = GameManager.instance.charExp[info.currentCharacterNumber - 1].exp;

        levelUpButton = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        closeButton = transform.GetChild(1).GetChild(0).GetComponent<Button>();
        addExpSlider = transform.GetChild(2).GetComponent<Slider>();
        addExpText = transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>();
        expBar = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>();
        expBar.fillAmount = curExp * percentage;

        levelUpButton.onClick.AddListener(() => OnClick_ExpUpBtn());
        closeButton.onClick.AddListener(() => OnClick_CloseButton());
    }

    private void OnClick_ExpUpBtn()
    {
        
    }

    private void OnClick_CloseButton()
    {
        Destroy(this.gameObject);
    }
}
