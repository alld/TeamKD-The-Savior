using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private Button levelUpButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private Slider addExpSlider;

    [SerializeField] private Image expBar;

    private float percentage = 0.01f;
    private int addExp = 0;
    private int curLevel;
    private int requiredExp;
    private int[] needExp = new int[2];

    private int curExp;
    [SerializeField] private TMP_Text addExpText;

    private InfoCharacter info;
    private int characterNumber;    //ĳ������ ��ȣ�� 1������ ������. -> �迭�� ���� �� -1�ϱ�.

    PlayToolBar tool;

    void Start()
    {
        info = GameObject.Find("PUIManager").GetComponent<InfoCharacter>();
        tool = GameObject.Find("PUIManager").GetComponent<PlayToolBar>();

        levelUpButton = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        closeButton = transform.GetChild(1).GetChild(0).GetComponent<Button>();
        addExpSlider = transform.GetChild(2).GetComponent<Slider>();
        addExpText = transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>();
        expBar = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>();
        expBar.fillAmount = curExp * percentage;

        levelUpButton.onClick.AddListener(() => OnClick_ExpUpBtn());
        closeButton.onClick.AddListener(() => OnClick_CloseButton());


        characterNumber = info.currentCharacterNumber;
        curLevel = GameManager.instance.charExp[characterNumber - 1].level;
        curExp = GameManager.instance.charExp[characterNumber - 1].exp;


        // ���߿� ��� �ٲ��� ���� �ϵ��ڵ��߽��ϴ�.
        needExp[0] = 100;
        needExp[1] = 150;

        requiredExp = needExp[curLevel - 1] - curExp;
        addExpSlider.value = 0;

        addExpSlider.minValue = 0;
        addExpSlider.maxValue = requiredExp;
        addExpSlider.wholeNumbers = true;
        addExpSlider.onValueChanged.AddListener(OnValueChanged_RequiredExpSlider);

        addExpText.text = "0 / " + requiredExp.ToString();
    }

    private void OnClick_ExpUpBtn()
    {
        // 1. ���� ����ġ�� �ִ�ġ�� �ƴ϶�� ���� ����ġ�� ȹ�� ����ġ�� �����ش�.
        // 2. ���� ����ġ�� �ִ�ġ��� ������ �ø��� 0���� �ٲ��ش�.
        GameManager.instance.charExp[characterNumber - 1].exp += addExp;

        int overExp = 0;
        switch (GameManager.instance.charExp[characterNumber - 1].level)
        {
            case 1:
                if(GameManager.instance.charExp[characterNumber - 1].exp >= needExp[0])
                {
                    overExp = GameManager.instance.charExp[characterNumber - 1].exp - needExp[0];
                    GameManager.instance.charExp[characterNumber - 1].level++;
                    GameManager.instance.charExp[characterNumber - 1].exp = 0;
                }
                break;
            case 2:
                if (GameManager.instance.charExp[characterNumber - 1].exp >= needExp[0])
                {
                    overExp = GameManager.instance.charExp[characterNumber - 1].exp - needExp[1];
                    GameManager.instance.charExp[characterNumber - 1].level++;
                    GameManager.instance.charExp[characterNumber - 1].exp = 0;
                }
                break;
        }
        GameManager.instance.data.souls -= addExp;
        StartCoroutine(GameManager.instance.SaveCharExp(characterNumber));
        StartCoroutine( GameManager.instance.GameSave());
        StartCoroutine(tool.Gold());

        // ������ ��ư Ŭ���� ���� â ����
        info.LevelSystem();

        Destroy(this.gameObject);
    }

    private void OnValueChanged_RequiredExpSlider(float _value)
    {
        addExpText.text = _value.ToString() + " / " + requiredExp.ToString();
        addExp = (int)_value;
    }

    private void OnClick_CloseButton()
    {
        Destroy(this.gameObject);
    }
}
