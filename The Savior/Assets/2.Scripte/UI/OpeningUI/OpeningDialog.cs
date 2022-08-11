using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpeningDialog : MonoBehaviour
{
    // BackGound 관련 함수 호출을 위한 선언
    BackGround bg;

    // 대사를 넘기기 위한 버튼
    public Button nextDialogButton;

    // 오프닝에서 쓰일 텍스트 리스트
    public TMP_Text[] dialogTextList;

    // 편지지
    public GameObject letter;
    public Text letterText;

    // 대사에서 나타나는 캐릭터를 출력하기 위한 카메라
    public Camera leftCamera;
    public Camera rightCamera;

    // 카메라가 찍을 오브젝트 위치
    public Transform leftTr;
    public Transform rightTr;

    // 캐릭터 리스트 이름
    public enum CHARNAME { PLAYER, MATHER, SOLDIERA, SOLDIERB }
    // 현재 씬에서 등장하는 캐릭터 수
    private int maxChar = 4;
    // 카메라가 찍을 캐릭터 오브젝트 배열 
    public GameObject[] character;

    //캐릭터의 코드
    private const int player = 1000;
    private const int charName_Mother = 1001;
    private const int charName_SoldierA = 1002;
    private const int charName_SoldierB = 1003;
    // 현재 대사 텍스트의 번호
    // 오프닝 씬에서는 0번 부터 14번 까지.
    private int curTextNum = 0;


    private void Start()
    {
        nextDialogButton.onClick.AddListener(() => OnClick_NextDialogBtn());
        nextDialogButton.transform.SetSiblingIndex(6);

        bg = GetComponent<BackGround>();

        character = new GameObject[maxChar];
        charInit();

        curTextNum = 0;
        dialogTextList[0].text = TextManager.instance.DialogChange(curTextNum);
        // 캐릭터 이름 텍스트
        dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
        CharSetting(CHARNAME.MATHER, true, leftTr);
    }

    /// <summary>
    /// 캐릭터 오브젝트를 연결
    /// </summary>
    private void charInit()
    {
        character[0] = GameObject.Find("Player");
        character[1] = GameObject.Find("Mother");
        character[2] = GameObject.Find("Soldier");
        character[3] = GameObject.Find("Soldier");

        foreach (var item in character)
        {
            item.SetActive(false);
        }
    }

    /// <summary>
    /// 오프닝 씬에서 캐릭터 이름을 변환하는 코드
    /// <br>파라미터 값에 해당하는 캐릭터의 이름을 출력하고, 대사 순서에 맞게 캐릭터를 배치한다.</br>
    /// </summary>
    /// <param name="curTextNum">현재 대사 번호</param>
    void NextTextChar(int curTextNum)
    {
        // 대사에 맞춰서 background 변환
        // 현재 대사에 맞춰서 캐릭터 이름 변환.
        switch (curTextNum)
        {
            case 0:
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
                break;
            case 1:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 2:
                CharSetting(CHARNAME.MATHER, true, leftTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
                break;
            case 3:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 4:
                CharSetting(CHARNAME.MATHER, true, leftTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
                break;
            case 5:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            // 현재 텍스트가 6으로 넘어갈 때 병사들의 대화로 넘어가면서 background가 변환됌.
            case 6:
                bg.ChangeBackGround(1);
                CharSetting(CHARNAME.SOLDIERA, true, rightTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierA);
                break;
            case 7:
                CharSetting(CHARNAME.SOLDIERA, false, rightTr);
                CharSetting(CHARNAME.SOLDIERB, true, rightTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierB);
                break;
            // 현재 텍스탁 8로 넘어갈 때 플레이어와 엄마의 대화로 넘어가면서 background가 변환됌.
            case 8:
                bg.ChangeBackGround(0);
                CharSetting(CHARNAME.MATHER, true, leftTr);
                CharSetting(CHARNAME.SOLDIERB, false, rightTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
                break;
            case 9:
                CharSetting(CHARNAME.MATHER, false, leftTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 10:
                CharSetting(CHARNAME.SOLDIERA, true, rightTr);
                CharSetting(CHARNAME.PLAYER, false, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierA);
                break;
            case 11:
                CharSetting(CHARNAME.SOLDIERB, true, rightTr);
                CharSetting(CHARNAME.SOLDIERA, false, rightTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_SoldierB);
                break;
            // 현재 텍스트가 12일때 편지지를 띄움.
            case 12:
                letter.SetActive(true);
                letterText.text = TextManager.instance.DialogChange(curTextNum);
                // 12번째 텍스트는 편지의 내용이 들어있기 때문에 캐릭터 대사는 비워둔다.
                dialogTextList[0].text = "";
                break;
            case 13:
                letter.SetActive(false);
                CharSetting(CHARNAME.SOLDIERB, false, rightTr);
                CharSetting(CHARNAME.PLAYER, true, leftTr);
                dialogTextList[1].text = TextManager.instance.ChangeLanguageText(player);
                break;
            case 14:
                /*
                 * 오프닝 대사가 모두 끝나면 다음 씬으로 이동.
                 */
                GameManager.instance.SceneChange(2);
                GameManager.instance.data.gameProgress = 2;
                GameManager.instance.data.CurrentScene = 2;
                GameManager.instance.GameSave();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 오프닝 씬에서 캐릭터의 위치를 잡아준다.
    /// </summary>
    /// <param name="charName"> 오프닝 씬의 캐릭터 번호</param>
    /// <param name="isAct"> 활성화 여부 </param>
    /// <param name="tr">위치 </param>
    void CharSetting(CHARNAME charName, bool isAct, Transform tr)
    {
        character[(int)charName].SetActive(isAct);
        if (!isAct) return;
        character[(int)charName].transform.position = tr.position;
        character[(int)charName].transform.rotation = tr.rotation;
    }


    /// <summary>
    /// 오프닝 씬에서 캐릭터의 이름과 대사를 변경시킨다.
    /// </summary>
    private void OnClick_NextDialogBtn()
    {
        // 버튼 클릭시에 다음 텍스트를 출력한다.
        curTextNum++;

        // 마우스를 클릭할 때 텍스트 변환.
        dialogTextList[0].text = TextManager.instance.DialogChange(curTextNum);

        NextTextChar(curTextNum);
    }

    /// <summary>
    /// 옵션에서 언어가 변경되었을 때, 호출되는 함수.
    /// </summary>
    public void ChangeLanguageDialog()
    {
        dialogTextList[0].text = TextManager.instance.DialogChange(curTextNum);
        NextTextChar(curTextNum);
    }
}
