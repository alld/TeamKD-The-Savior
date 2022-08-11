using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpeningDialog : MonoBehaviour
{
    // BackGound ���� �Լ� ȣ���� ���� ����
    BackGround bg;

    // ��縦 �ѱ�� ���� ��ư
    public Button nextDialogButton;

    // �����׿��� ���� �ؽ�Ʈ ����Ʈ
    public TMP_Text[] dialogTextList;

    // ������
    public GameObject letter;
    public Text letterText;

    // ��翡�� ��Ÿ���� ĳ���͸� ����ϱ� ���� ī�޶�
    public Camera leftCamera;
    public Camera rightCamera;

    // ī�޶� ���� ������Ʈ ��ġ
    public Transform leftTr;
    public Transform rightTr;

    // ĳ���� ����Ʈ �̸�
    public enum CHARNAME { PLAYER, MATHER, SOLDIERA, SOLDIERB }
    // ���� ������ �����ϴ� ĳ���� ��
    private int maxChar = 4;
    // ī�޶� ���� ĳ���� ������Ʈ �迭 
    public GameObject[] character;

    //ĳ������ �ڵ�
    private const int player = 1000;
    private const int charName_Mother = 1001;
    private const int charName_SoldierA = 1002;
    private const int charName_SoldierB = 1003;
    // ���� ��� �ؽ�Ʈ�� ��ȣ
    // ������ �������� 0�� ���� 14�� ����.
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
        // ĳ���� �̸� �ؽ�Ʈ
        dialogTextList[1].text = TextManager.instance.ChangeLanguageText(charName_Mother);
        CharSetting(CHARNAME.MATHER, true, leftTr);
    }

    /// <summary>
    /// ĳ���� ������Ʈ�� ����
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
    /// ������ ������ ĳ���� �̸��� ��ȯ�ϴ� �ڵ�
    /// <br>�Ķ���� ���� �ش��ϴ� ĳ������ �̸��� ����ϰ�, ��� ������ �°� ĳ���͸� ��ġ�Ѵ�.</br>
    /// </summary>
    /// <param name="curTextNum">���� ��� ��ȣ</param>
    void NextTextChar(int curTextNum)
    {
        // ��翡 ���缭 background ��ȯ
        // ���� ��翡 ���缭 ĳ���� �̸� ��ȯ.
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
            // ���� �ؽ�Ʈ�� 6���� �Ѿ �� ������� ��ȭ�� �Ѿ�鼭 background�� ��ȯ��.
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
            // ���� �ؽ�Ź 8�� �Ѿ �� �÷��̾�� ������ ��ȭ�� �Ѿ�鼭 background�� ��ȯ��.
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
            // ���� �ؽ�Ʈ�� 12�϶� �������� ���.
            case 12:
                letter.SetActive(true);
                letterText.text = TextManager.instance.DialogChange(curTextNum);
                // 12��° �ؽ�Ʈ�� ������ ������ ����ֱ� ������ ĳ���� ���� ����д�.
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
                 * ������ ��簡 ��� ������ ���� ������ �̵�.
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
    /// ������ ������ ĳ������ ��ġ�� ����ش�.
    /// </summary>
    /// <param name="charName"> ������ ���� ĳ���� ��ȣ</param>
    /// <param name="isAct"> Ȱ��ȭ ���� </param>
    /// <param name="tr">��ġ </param>
    void CharSetting(CHARNAME charName, bool isAct, Transform tr)
    {
        character[(int)charName].SetActive(isAct);
        if (!isAct) return;
        character[(int)charName].transform.position = tr.position;
        character[(int)charName].transform.rotation = tr.rotation;
    }


    /// <summary>
    /// ������ ������ ĳ������ �̸��� ��縦 �����Ų��.
    /// </summary>
    private void OnClick_NextDialogBtn()
    {
        // ��ư Ŭ���ÿ� ���� �ؽ�Ʈ�� ����Ѵ�.
        curTextNum++;

        // ���콺�� Ŭ���� �� �ؽ�Ʈ ��ȯ.
        dialogTextList[0].text = TextManager.instance.DialogChange(curTextNum);

        NextTextChar(curTextNum);
    }

    /// <summary>
    /// �ɼǿ��� �� ����Ǿ��� ��, ȣ��Ǵ� �Լ�.
    /// </summary>
    public void ChangeLanguageDialog()
    {
        dialogTextList[0].text = TextManager.instance.DialogChange(curTextNum);
        NextTextChar(curTextNum);
    }
}
