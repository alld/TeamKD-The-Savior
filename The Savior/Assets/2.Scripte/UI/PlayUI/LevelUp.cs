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
    private int characterNumber;    //캐릭터의 번호는 1번부터 시작함. -> 배열에 넣을 땐 -1하기.

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


        // 나중에 어떻게 바뀔지 몰라 하드코딩했습니다.
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
        // 1. 현재 경험치가 최대치가 아니라면 현재 경험치에 획득 경험치를 더해준다.
        // 2. 현재 경험치가 최대치라면 레벨을 올리고 0으로 바꿔준다.
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

        // 레벨업 버튼 클릭시 인포 창 수정
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
