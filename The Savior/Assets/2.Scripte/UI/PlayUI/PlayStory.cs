using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class PlayStory : MonoBehaviour
{

    // ���丮 ��� �ؽ�Ʈ ������
    private TextAsset textAsset;
    private JArray textData;

    // ��ȭâ
    [Header("��ȭâ")]
    public GameObject diaLog;

    // ���丮 ������ ���� ��ư
    [Header("���� ��� ��ư")]
    public Button diaLogButton;

    // ��ȭâ�� ��� �� �ؽ�Ʈ
    [Header("��� �ؽ�Ʈ")]
    public TMP_Text diaLogText;

    // ��ȭâ�� ��� �� ĳ������ �̸�
    [Header("���� ���ϴ� ����� �̸�")]
    public TMP_Text talkerNameText;

    // ���� ���ϰ� �ִ� 3D ������Ʈ�� ī�޶�� �Կ��ϱ� ���� ��ġ
    [Header("���� ���ϰ� �ִ� ����� ��ġ")]
    public Transform talkerTr;

    // ĳ���Ͱ� ���� �� �� �ش� ĳ���͸� ī�޶�� �Կ���.
    [Header("ĳ���� ������Ʈ")]
    public GameObject[] talker;

    // ���� ���ϰ� �ִ� ĳ����
    private int curTalker;

    // 0 : ž�� ���� 
    // 1 ~ 2 : ���� ����
    // 3 ~ 4 : ī�� ����
    // 5 ~ 6 : ���� ����
    [Header("���丮 ���� �� ��µǴ� ����")]
    public GameObject[] toolTip;

    WorldMapCastle map;

    void Start()
    {
        textAsset = Resources.Load<TextAsset>("TextDB/DiaLogData");
        textData = JArray.Parse(textAsset.text);

        diaLogButton.onClick.AddListener(() => OnClick_NextStoryBtn());

        for (int i = 0; i < talker.Length; i++)
        {
            talker[i].transform.position = talkerTr.position;
            talker[i].SetActive(false);
        }


        // ���� ���۽� ���丮 ���൵�� ���� ���� ������ ���
        // �ش� �̺�Ʈ�� ���ۺη� �ʱ�ȭ��Ų��.
        switch (GameManager.instance.data.storyProgress)
        {
            case < 3:
                GameManager.instance.data.storyProgress = 0;
                break;
            case < 49:
                GameManager.instance.data.storyProgress = 3;
                break;
            case < 54:
                GameManager.instance.data.storyProgress = 49;
                break;
            case < 58:
                GameManager.instance.data.storyProgress = 54;
                break;
            case < 64:
                GameManager.instance.data.storyProgress = 58;
                break;
            case < 69:
                GameManager.instance.data.storyProgress = 64;
                break;
            case < 92:
                GameManager.instance.data.storyProgress = 69;
                break;
            default:
                break;
        }

        Debug.Log(GameManager.instance.data.storyProgress);
    }

    // ��ȭâ�� Ȱ��ȭ�Ѵ�.
    public void OnDiaLog()
    {
        diaLog.SetActive(true);
        Story();
    }

    // ��ȭâ�� ��Ȱ��ȭ�Ѵ�.
    public void CloseDiaLog()
    {
        diaLog.SetActive(false);
        talker[curTalker].SetActive(false);
    }

    // ��ȭâ�� �����Ѵ�.
    // storyProgress�� ���缭 �ش� �ؽ�Ʈ�� ����Ѵ�.
    // ���� �� ���� ��µȴ�.
    public void Story()
    {

        if (GameManager.instance.data.storyProgress != 0)
        {
            // �� ���� ���� ĳ���� ��Ȱ��ȭ
            talker[textData[GameManager.instance.data.storyProgress - 1]["TalkerIndex"].ToObject<int>()].SetActive(false);
        }
        // ���� ���õ� ���� ��� ���
        switch (GameManager.instance.data.Language)
        {
            case 0:
                // ���� ���൵�� ���� ��� ���, ĳ���� �Կ�
                diaLogText.text = textData[GameManager.instance.data.storyProgress]["TextKor"].ToString();
                talkerNameText.text = textData[GameManager.instance.data.storyProgress]["Talker"].ToString();

                talker[textData[GameManager.instance.data.storyProgress]["TalkerIndex"].ToObject<int>()].SetActive(true);

                break;
            case 1:
                diaLogText.text = textData[GameManager.instance.data.storyProgress]["TextEng"].ToString();
                talkerNameText.text = textData[GameManager.instance.data.storyProgress]["Talker_Eng"].ToString();

                talker[textData[GameManager.instance.data.storyProgress]["TalkerIndex"].ToObject<int>()].SetActive(true);

                break;
            default:
                break;
        }

        curTalker = textData[GameManager.instance.data.storyProgress]["TalkerIndex"].ToObject<int>();
    }

    /// <summary>
    /// ��ư Ŭ���� ���丮 ����.
    /// </summary>
    private void OnClick_NextStoryBtn()
    {

        // ���丮�� �����Ŵ.
        GameManager.instance.data.storyProgress++;
        // ���� ��簡 ������ ��ȭâ�� �ݴ´�.
        switch (GameManager.instance.data.storyProgress)
        {
            // 0 ~ 2  �������� ������ ����� ����� ��µǴ� �ؽ�Ʈ
            // ��ư�� �ѹ� �� Ŭ���ϸ� storyProgress�� 3�� �Ǹ� ��ȭâ�� ������.
            // ����ʿ��� �ռ��� ����Ű�� �̹����� Ȱ��ȭ�Ѵ�.
            case 3:
                CloseDiaLog();
                // �ռ� �����ϴ� �̹���
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_1").SetActive(true);
                Debug.Log("11");
                return;
            // 3 ~ 48 �ռ� ����� ��µǴ� �ؽ�Ʈ.
            // ��ư�� �ѹ� �� Ŭ���� storyProgress�� 49�� �Ǹ� ��ȭâ�� ������.
            // ���� ������ ��ȸ�� ����Ű�� �̹����� Ȱ��ȭ�ȴ�.
            // ž �� ������ Ȱ��ȭ �ȴ�.
            case 49:
                CloseDiaLog();
                // ��ȸ�� �����ϴ� �̹���.
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_3").SetActive(true);
                // �ռ����� ������ Ȱ��ȭ �Ǵ� ����.
                toolTip[0].SetActive(true);
                return;

            // 49 ~ 53 ��ȸ ����� ��µǴ� �ؽ�Ʈ.
            // ��ư�� �ѹ� �� Ŭ���� storyProgress�� 54�� �Ǹ� ��ȭâ�� ������.
            // ���� ���� �̹����� Ȱ��ȭ �Ѵ�.
            case 54:
                CloseDiaLog();
                toolTip[1].SetActive(true);
                toolTip[2].SetActive(true);
                return;
            // 54 ~ 57  ��ȸ���� ����� ��µǴ� �ؽ�Ʈ.
            // ��ư�� �ѹ� �� Ŭ���� storyProgress�� 58�� �Ǹ� ��ȭâ�� ������.
            // ������ ����Ű�� �̹����� Ȱ��ȭ �ȴ�.
            case 58:
                CloseDiaLog();
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_4").SetActive(true);
                return;
            // 58 ~ 63  ���� ����� ��µǴ� �ؽ�Ʈ.
            // ��ư�� �ѹ� �� Ŭ���� storyProgress�� 64�� �Ǹ� ��ȭâ�� ������.
            // ���� ���� �̹����� Ȱ��ȭ �Ѵ�.
            case 64:
                CloseDiaLog();
                toolTip[3].SetActive(true);
                toolTip[4].SetActive(true);
                return;
            // 64 ~ 68 �������� ����� ������ �ؽ�Ʈ.
            // ��ư�� �ѹ� �� Ŭ���� storyProgress�� 69�� �Ǹ� ��ȭâ�� ���� ���� â�� ������.
            // ������ ����Ű�� �̹����� Ȱ��ȭ �ϰ� ���� ���丮 ��縦 ����Ѵ�.
            case 69:
                CloseDiaLog();
                map = GameObject.Find("WUIManager").GetComponent<WorldMapCastle>();
                map.OnClick_OutCastleBtn();

                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_5").SetActive(true);
                OnDiaLog();
                return;
            // 69 ~ 91 ���� ���� �� ��µǴ� �ؽ�Ʈ.
            // ��ư�� �ѹ� �� Ŭ���� storyProgress�� 92�� �Ǹ� ��ȭâ�� ������.
            case 92:
                CloseDiaLog(); 
                return;
            default:
                break;
        }


        Story();
    }
}
