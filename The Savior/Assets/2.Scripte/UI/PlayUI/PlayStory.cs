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
        Debug.Log(GameManager.instance.data.storyProgress);
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
            case 1:
                CloseDiaLog();
                return;
            case 47:
                CloseDiaLog();
                return;
            case 52:
                CloseDiaLog();
                return;
            case 56:
                CloseDiaLog();
                return;
            case 62:
                CloseDiaLog();
                return;
            case 67:
                CloseDiaLog();
                WorldMapCastle map = GameObject.Find("WUIManager").GetComponent<WorldMapCastle>();
                map.OnClick_OutCastleBtn();
                OnDiaLog();
                return;
            case 90:
                CloseDiaLog();
                return;
            default:
                break;
        }


        Story();
    }
}
