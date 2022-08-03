using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OpeningUIManager : MonoBehaviour
{

    #region UI 환경 변수
    // 언어 변경을 위한 배열
    // UI_TextList의 0번은 대화창
    // UI_TextList의 1번은 대화중인 캐릭터의 이름
    public TMP_Text[] UI_TextList;
    //편지지에 사용될 텍스트
    public Text letterText;
    // 오프닝 씬에서 사용될 비디오 플레이어
    public VideoPlayer openingVideo;
    // background 장면 변환마다 사용될 비디오 클립들
    public VideoClip[] videos;
    // video를 출력할 이미지
    public RawImage backGroundImage;
    // 대화중 나오는 편지 이미지
    public GameObject letter;
    // 옵션창
    public GameObject option;
    // 대화 시작시에 활성화 되는 버튼
    // 화면을 클릭할 때 텍스트를 변경시킨다.
    public Button dialogButton;
    // 옵션 버튼
    public Button optionButton;


    //옵션창 오픈 여부
    private bool isOption = false;


    // TextManager에서 오프닝에서 쓰일 텍스트는 30번부터 시작한다.
    // 옵션창의 언어를변경할 것이므로 조정함.
    // 7 ~ 11까지
    private const int UI_fixedvalue = 7;
    // 오프닝에서 등장하는 캐릭터의 이름 텍스트 넘버링
    private const int player = 1000;
    private const int charName_Mother = 1001;
    private const int charName_SoldierA = 1002;
    private const int charName_SoldierB = 1003;
    // 현재 대사 텍스트의 번호
    // 오프닝 씬에서는 0번 부터 시작한다.
    private int curTextNum = 0;
    #endregion

    // 옵션 
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

    // 옵션의 언어기능이 바뀔때마다 호출
    // 저장데이터의 Language값을 연동시킴. 
    public void OnChangeLanguage()
    {
        GameManager.instance.data.Language = UI_languageDrop.value;
        UIReset();
    }

    //옵션값(사운드관련)이 변동될경우 저장데이터의 옵션값을 연동시킴.
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

    //옵션중 사운드값이 변동될경우 우측에 수치값을 연동시킴
    //게입데이터만 불러와진 상태라면, 옵션값을 게임데이터와 일치시킴.
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

    // 카메라
    #region Camera

    /*
     * Camera는 Solid Color로 설정했습니다.
     */
    // 대화창 왼쪽 캐릭터를 찍을 카메라
    public Camera leftCamera;
    public Camera rightCamera;
    // 카메라가 찍을 오브젝트 위치
    public Transform leftTr;
    public Transform rightTr;

    // 캐릭터 리스트 이름
    public enum CHARNAME { PLAYER, MATHER, SOLDIERA, SOLDIERB }
    private int maxChar = 4;
    // 카메라가 찍을 캐릭터 오브젝트 배열 
    public GameObject[] character;

    // 캐릭터를 배열에 넣어주고 비활성화 시킨다.
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

    // 캐릭터의 위치를 잡아주고, 활성화, 비활성화 시킨다.
    void CharSetting(CHARNAME charName, bool isAct, Transform tr)
    {
        character[(int)charName].SetActive(isAct);
        if (!isAct) return;
        character[(int)charName].transform.position = tr.position;
        character[(int)charName].transform.rotation = tr.rotation;
    }
    #endregion

    // 현재 텍스트의 번호에 맞춰 캐릭터의 이름과 배경을 변환한다,
    #region 캐릭터 대사
    void NextTextChar()
    {
        // 대사에 맞춰서 background 변환
        // 현재 대사에 맞춰서 캐릭터 이름 변환.
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
            // 현재 텍스트가 6으로 넘어갈 때 병사들의 대화로 넘어가면서 background가 변환됌.
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
            // 현재 텍스탁 8로 넘어갈 때 플레이어와 엄마의 대화로 넘어가면서 background가 변환됌.
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
            // 현재 텍스트가 12일때 편지지를 띄움.
            case 12:
                letter.SetActive(true);
                letterText.text = TextManager.instance.DialogChange(curTextNum);
                // 12번째 텍스트는 편지의 내용이 들어있기 때문에 캐릭터 대사는 비워둔다.
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
                 * 오프닝 대사가 모두 끝나면 다음 씬으로 이동.
                 */
                GameManager.instance.SceneChange(2);
                GameManager.instance.data.CurrentScene = 2;
                GameManager.instance.GameSave();
                break;
            default:
                break;
        }
    }

    // 버튼 클릭시 현재 텍스트 정보를 변환하고, NextTextChar() 함수를 호출한다.
    public void OnNextDialogButton()
    {
        // 버튼 클릭시에 다음 텍스트를 출력한다.
        curTextNum++;

        // 마우스를 클릭할 때 텍스트 변환.
        UI_TextList[0].text = TextManager.instance.DialogChange(curTextNum);

        NextTextChar();
    }
    #endregion

    // 초기화
    #region 게임 시작
    private void Awake()
    {
        GameManager.instance.data.gameProgress = 0;
        GameManager.instance.GameSave();
        UIReset();
    }
    private void Start()
    {
        character = new GameObject[maxChar];
        // 씬 시작시에 주인공의 집 배경 영상을 시작함.
        openingVideo.clip = videos[0];
        // 대화 텍스트 초기화
        curTextNum = 0;
        UI_TextList[0].text = TextManager.instance.DialogChange(curTextNum);
        // 캐릭터 이름 텍스트
        UI_TextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
        backGroundImage.transform.SetAsFirstSibling();
        dialogButton.transform.SetSiblingIndex(4);
        optionButton.transform.SetAsLastSibling();
        
        CharObjSetting();

        // 씬 시작시 엄마 캐릭터 활성화.
        CharSetting(CHARNAME.MATHER, true, leftTr);
    }
    #endregion

    // 언어 변경
    #region UI 환경 관련
    /* UI 설정 초기화 함수 
     * 역할 : 메인 씬에서 UI기능 설정들을 재정립할때 호출되는 함수
     * 
     * - 모든 텍스트 언어 설정
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

    /* UI 텍스트 언어 변경 함수
     * 역할 : 오프닝씬에 사용된 모든 텍스트에 접근하여, 설정된 언어값에 맞게 변경시키는 함수
     * 
     * 씬 시작시에 변경해야 하는 텍스트들을 변경시킨다.
     * 
     * @UI_TextList : 해당 씬이 가지고있는 모든 textUI
     * @UI_fixedvalue : 해당씬에 할당된 text 번호중 첫번째값
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
