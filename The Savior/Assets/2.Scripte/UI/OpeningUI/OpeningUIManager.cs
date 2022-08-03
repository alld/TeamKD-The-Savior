using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OpeningUIManager : MonoBehaviour
{

    #region UI ȯ�� ����
    // ��� ������ ���� �迭
    // UI_TextList�� 0���� ��ȭâ
    // UI_TextList�� 1���� ��ȭ���� ĳ������ �̸�
    public TMP_Text[] UI_TextList;
    //�������� ���� �ؽ�Ʈ
    public Text letterText;
    // ������ ������ ���� ���� �÷��̾�
    public VideoPlayer openingVideo;
    // background ��� ��ȯ���� ���� ���� Ŭ����
    public VideoClip[] videos;
    // video�� ����� �̹���
    public RawImage backGroundImage;
    // ��ȭ�� ������ ���� �̹���
    public GameObject letter;
    // �ɼ�â
    public GameObject option;
    // ��ȭ ���۽ÿ� Ȱ��ȭ �Ǵ� ��ư
    // ȭ���� Ŭ���� �� �ؽ�Ʈ�� �����Ų��.
    public Button dialogButton;
    // �ɼ� ��ư
    public Button optionButton;


    //�ɼ�â ���� ����
    private bool isOption = false;


    // TextManager���� �����׿��� ���� �ؽ�Ʈ�� 30������ �����Ѵ�.
    // �ɼ�â�� �������� ���̹Ƿ� ������.
    // 7 ~ 11����
    private const int UI_fixedvalue = 7;
    // �����׿��� �����ϴ� ĳ������ �̸� �ؽ�Ʈ �ѹ���
    private const int player = 1000;
    private const int charName_Mother = 1001;
    private const int charName_SoldierA = 1002;
    private const int charName_SoldierB = 1003;
    // ���� ��� �ؽ�Ʈ�� ��ȣ
    // ������ �������� 0�� ���� �����Ѵ�.
    private int curTextNum = 0;
    #endregion

    // �ɼ� 
    #region UI_OPTION

    public TMP_Dropdown UI_languageDrop;
    public Slider UI_soundSlider;
    public Slider UI_BGMSlider;
    public Slider UI_SFXSlider;

    public TMP_Text UI_Option;
    public TMP_Text UI_soundText;
    public TMP_Text UI_BGMText;
    public TMP_Text UI_SFXText;
    //public TMP_Text UI_LanguageText;

    public GameObject UI_muteImageSound;
    public GameObject UI_muteImageBGM;
    public GameObject UI_muteImageSFX;

    public void OnOptionClick()
    {
        isOption = !isOption;
        option.SetActive(isOption);
        if (isOption) option.transform.SetAsLastSibling();
    }

    // �ɼ��� ������� �ٲ𶧸��� ȣ��
    // ���嵥������ Language���� ������Ŵ. 
    public void OnChangeLanguage()
    {
        GameManager.instance.data.Language = UI_languageDrop.value;
        UIReset();
    }

    //�ɼǰ�(�������)�� �����ɰ�� ���嵥������ �ɼǰ��� ������Ŵ.
    public void OnChangeSound()
    {
        GameManager.instance.data.Sound = (int)UI_soundSlider.value;
        UIReset();
    }
    public void OnChangeBGM()
    {
        GameManager.instance.data.BGM = (int)UI_BGMSlider.value;
        UIReset();
    }
    public void OnChangeSFX()
    {
        GameManager.instance.data.SFX = (int)UI_SFXSlider.value;
        UIReset();
    }

    //�ɼ��� ���尪�� �����ɰ�� ������ ��ġ���� ������Ŵ
    //���Ե����͸� �ҷ����� ���¶��, �ɼǰ��� ���ӵ����Ϳ� ��ġ��Ŵ.
    public void OnOpSetSound()
    {
        UI_soundSlider.value = GameManager.instance.data.Sound;
        if (UI_soundSlider.value == 0)
        {
            UI_soundText.text = "";
            UI_muteImageSound.SetActive(true);
        }
        else
        {
            UI_soundText.text = GameManager.instance.data.Sound.ToString();
            UI_muteImageSound.SetActive(false);
        }
    }
    public void OnOpSetBGM()
    {
        UI_BGMSlider.value = GameManager.instance.data.BGM;
        if (UI_BGMSlider.value == 0)
        {
            UI_BGMText.text = "";
            UI_muteImageBGM.SetActive(true);
        }
        else
        {
            UI_BGMText.text = GameManager.instance.data.BGM.ToString();
            UI_muteImageBGM.SetActive(false);
        }
    }
    public void OnOpSetSFX()
    {
        UI_SFXSlider.value = GameManager.instance.data.SFX;
        if (UI_SFXSlider.value == 0)
        {
            UI_SFXText.text = "";
            UI_muteImageSFX.SetActive(true);
        }
        else
        {
            UI_SFXText.text = GameManager.instance.data.SFX.ToString();
            UI_muteImageSFX.SetActive(false);
        }
    }
    public void OnOpSetLanguage()
    {
        UI_languageDrop.value = (int)GameManager.instance.data.Language;
    }

    #endregion

    // ī�޶�
    #region Camera

    /*
     * Camera�� Solid Color�� �����߽��ϴ�.
     */
    // ��ȭâ ���� ĳ���͸� ���� ī�޶�
    public Camera leftCamera;
    public Camera rightCamera;
    // ī�޶� ���� ������Ʈ ��ġ
    public Transform leftTr;
    public Transform rightTr;

    // ĳ���� ����Ʈ �̸�
    public enum CHARNAME { PLAYER, MATHER, SOLDIERA, SOLDIERB }
    private int maxChar = 4;
    // ī�޶� ���� ĳ���� ������Ʈ �迭 
    public GameObject[] character;

    // ĳ���͸� �迭�� �־��ְ� ��Ȱ��ȭ ��Ų��.
    void CharObjSetting()
    {
        character[(int)CHARNAME.PLAYER] = GameObject.Find("Player");
        character[(int)CHARNAME.MATHER] = GameObject.Find("Mather");
        character[(int)CHARNAME.SOLDIERA] = GameObject.Find("SoldierA");
        character[(int)CHARNAME.SOLDIERB] = GameObject.Find("SoldierB");

        foreach (var item in character)
        {
            item.SetActive(false);
        }
    }

    // ĳ������ ��ġ�� ����ְ�, Ȱ��ȭ, ��Ȱ��ȭ ��Ų��.
    void CharSetting(CHARNAME charName, bool isAct, Transform tr)
    {
        character[(int)charName].SetActive(isAct);
        if (!isAct) return;
        character[(int)charName].transform.position = tr.position;
        character[(int)charName].transform.rotation = tr.rotation;
    }
    #endregion

    // ���� �ؽ�Ʈ�� ��ȣ�� ���� ĳ������ �̸��� ����� ��ȯ�Ѵ�,
    #region ĳ���� ���
    void NextTextChar()
    {
        // ��翡 ���缭 background ��ȯ
        // ���� ��翡 ���缭 ĳ���� �̸� ��ȯ.
        switch (curTextNum)
        {
            case 1:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 2:
                CharSetting(CHARNAME.MATHER, true, leftTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
                break;
            case 3:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 4:
                CharSetting(CHARNAME.MATHER, true, leftTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
                break;
            case 5:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            // ���� �ؽ�Ʈ�� 6���� �Ѿ �� ������� ��ȭ�� �Ѿ�鼭 background�� ��ȯ��.
            case 6:
                openingVideo.clip = videos[1];
                CharSetting(CHARNAME.SOLDIERA, true, rightTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierA);
                break;
            case 7:
                CharSetting(CHARNAME.SOLDIERA, false, rightTr);
                CharSetting(CHARNAME.SOLDIERB, true, rightTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierB);
                break;
            // ���� �ؽ�Ź 8�� �Ѿ �� �÷��̾�� ������ ��ȭ�� �Ѿ�鼭 background�� ��ȯ��.
            case 8:
                openingVideo.clip = videos[0];
                CharSetting(CHARNAME.MATHER, true, leftTr);
                CharSetting(CHARNAME.SOLDIERB, false, rightTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
                break;
            case 9:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 10:
                CharSetting(CHARNAME.SOLDIERA, true, rightTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierA);
                break;
            case 11:
                CharSetting(CHARNAME.SOLDIERB, true, rightTr);
                CharSetting(CHARNAME.SOLDIERA, false, rightTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierB);
                break;
            // ���� �ؽ�Ʈ�� 12�϶� �������� ���.
            case 12:
                letter.SetActive(true);
                letterText.text = TextManager.instance.DialogChange(curTextNum);
                // 12��° �ؽ�Ʈ�� ������ ������ ����ֱ� ������ ĳ���� ���� ����д�.
                UI_TextList[0].text = "";
                break;
            case 13:
                letter.SetActive(false);
                CharSetting(CHARNAME.SOLDIERB, false, rightTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                UI_TextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 14:
                /*
                 * ������ ��簡 ��� ������ ���� ������ �̵�.
                 */
                GameManager.instance.SceneChange(2);
                GameManager.instance.data.CurrentScene = 2;
                GameManager.instance.GameSave();
                break;
            default:
                break;
        }
    }

    // ��ư Ŭ���� ���� �ؽ�Ʈ ������ ��ȯ�ϰ�, NextTextChar() �Լ��� ȣ���Ѵ�.
    public void OnNextDialogButton()
    {
        // ��ư Ŭ���ÿ� ���� �ؽ�Ʈ�� ����Ѵ�.
        curTextNum++;

        // ���콺�� Ŭ���� �� �ؽ�Ʈ ��ȯ.
        UI_TextList[0].text = TextManager.instance.DialogChange(curTextNum);

        NextTextChar();
    }
    #endregion

    // �ʱ�ȭ
    #region ���� ����
    private void Awake()
    {
        GameManager.instance.data.gameProgress = 0;
        GameManager.instance.GameSave();
        UIReset();
    }
    private void Start()
    {
        character = new GameObject[maxChar];
        // �� ���۽ÿ� ���ΰ��� �� ��� ������ ������.
        openingVideo.clip = videos[0];
        // ��ȭ �ؽ�Ʈ �ʱ�ȭ
        curTextNum = 0;
        UI_TextList[0].text = TextManager.instance.DialogChange(curTextNum);
        // ĳ���� �̸� �ؽ�Ʈ
        UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
        backGroundImage.transform.SetAsFirstSibling();
        dialogButton.transform.SetSiblingIndex(4);
        optionButton.transform.SetAsLastSibling();
        
        CharObjSetting();

        // �� ���۽� ���� ĳ���� Ȱ��ȭ.
        CharSetting(CHARNAME.MATHER, true, leftTr);
    }
    #endregion

    // ��� ����
    #region UI ȯ�� ����
    /* UI ���� �ʱ�ȭ �Լ� 
     * ���� : ���� ������ UI��� �������� �������Ҷ� ȣ��Ǵ� �Լ�
     * 
     * - ��� �ؽ�Ʈ ��� ����
     * 
     * 
     */
    private void UIReset()
    {
        LanguageTranslate();
        UI_TextList[0].text = TextManager.instance.DialogChange(curTextNum);
        NextTextChar();

        OnOpSetSound();
        OnOpSetBGM();
        OnOpSetSFX();
        OnOpSetLanguage();
    }

    /* UI �ؽ�Ʈ ��� ���� �Լ�
     * ���� : �����׾��� ���� ��� �ؽ�Ʈ�� �����Ͽ�, ������ ���� �°� �����Ű�� �Լ�
     * 
     * �� ���۽ÿ� �����ؾ� �ϴ� �ؽ�Ʈ���� �����Ų��.
     * 
     * @UI_TextList : �ش� ���� �������ִ� ��� textUI
     * @UI_fixedvalue : �ش���� �Ҵ�� text ��ȣ�� ù��°��
     * 
     */
    public void LanguageTranslate()
    {
        for (int i = 2; i <= 7; i++)
        {
            UI_TextList[i].text = TextManager.instance.ChangeLanguageText(i+4);
        }
    }
    #endregion
}
