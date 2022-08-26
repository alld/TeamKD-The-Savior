using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class PlayStory : MonoBehaviour
{

    // 스토리 대사 텍스트 데이터
    private TextAsset textAsset;
    private JArray textData;

    // 대화창
    [Header("대화창")]
    public GameObject diaLog;

    // 스토리 진행을 위한 버튼
    [Header("다음 대사 버튼")]
    public Button diaLogButton;

    // 대화창에 출력 될 텍스트
    [Header("대사 텍스트")]
    public TMP_Text diaLogText;

    // 대화창에 출력 될 캐릭터의 이름
    [Header("현재 말하는 사람의 이름")]
    public TMP_Text talkerNameText;

    // 현재 말하고 있는 3D 오브젝트를 카메라로 촬영하기 위한 위치
    [Header("현재 말하고 있는 사람의 위치")]
    public Transform talkerTr;

    // 캐릭터가 말을 할 때 해당 캐릭터를 카메라로 촬영함.
    [Header("캐릭터 오브젝트")]
    public GameObject[] talker;

    // 현재 말하고 있는 캐릭터
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
            // 이 전에 말한 캐릭터 비활성화
            talker[textData[GameManager.instance.data.storyProgress - 1]["TalkerIndex"].ToObject<int>()].SetActive(false);
        }
        // 현재 세팅된 언어로 대사 출력
        switch (GameManager.instance.data.Language)
        {
            case 0:
                // 현재 진행도에 맞춰 대사 출력, 캐릭터 촬영
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
    /// 버튼 클릭시 스토리 진행.
    /// </summary>
    private void OnClick_NextStoryBtn()
    {

        // 스토리를 진행시킴.
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
            default:
                break;
        }


        Story();
    }
}
