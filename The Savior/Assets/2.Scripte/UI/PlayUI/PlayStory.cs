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
    private GameObject curTalker;


    public bool isNotStoryMode = false;
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

        switch (GameManager.instance.data.storyProgress)
        {
            case < 47:
                GameManager.instance.data.storyProgress = 0;
                break;
            case < 56:
                GameManager.instance.data.storyProgress = 46;
                break;
            case < 62:
                GameManager.instance.data.storyProgress = 55;
                break;
            case < 66:
                GameManager.instance.data.storyProgress = 63;
                break;
            case < 90:
                GameManager.instance.data.storyProgress = 67;
                break;
            case 91:
                GameManager.instance.data.storyProgress = 67;
                break;
            default:
                break;
        }

        Debug.Log(GameManager.instance.data.storyProgress);
    }

    public void OnDiaLog()
    {
        diaLog.SetActive(true);
        Story();
    }

    public void CloseDiaLog()
    {
        diaLog.SetActive(false);
    }

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
                talkerNameText.text = textData[GameManager.instance.data.storyProgress]["Talker"].ToString();

                talker[textData[GameManager.instance.data.storyProgress]["TalkerIndex"].ToObject<int>()].SetActive(true);

                break;
            default:
                break;
        }

    }

    /// <summary>
    /// ��ư Ŭ���� ���丮 ����.
    /// </summary>
    private void OnClick_NextStoryBtn()
    {

        // ���丮�� �����Ŵ.
        GameManager.instance.data.storyProgress++;

        switch (GameManager.instance.data.storyProgress)
        {
            // �ռ����� ��.
            case 1:
                CloseDiaLog();
                // �ռ� �����ϴ� �̹���
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_1").SetActive(true);
                Debug.Log("11");
                return;
                // �ռ� ��ȭ�� ������ ��ȸ�� ��.
            case 47:
                CloseDiaLog();
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_3").SetActive(true);
                return;
                // ��ȸ ��ȭ�� ������ ��ȯ...
            case 52:
                CloseDiaLog();

                return;
                // ��ȯ �� ��ȭ�� ������ �������� ��.
            case 56:
                CloseDiaLog();
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_4").SetActive(true);
                return;
                // �������� ��ȭ�� ����.
            case 62:
                CloseDiaLog();
                return;
                // �ռ������� ��ȭ�� ��� ����.
            case 67:
                CloseDiaLog();
                map = GameObject.Find("WUIManager").GetComponent<WorldMapCastle>();
                map.OnClick_OutCastleBtn();
                OnDiaLog();
                isNotStoryMode = true;
                return;
                // ���� ���������� ��ȭ�� ��� ������.
            case 90:
                CloseDiaLog();
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_5").SetActive(true);
                return;
            default:
                break;
        }


        Story();
    }
}
