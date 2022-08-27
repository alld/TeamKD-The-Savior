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
    private int curTalker;

    // 0 : 탑바 툴팁 
    // 1 ~ 2 : 영웅 툴팁
    // 3 ~ 4 : 카드 툴팁
    // 5 ~ 6 : 던전 툴팁
    [Header("스토리 진행 중 출력되는 툴팁")]
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


        // 게임 시작시 스토리 진행도가 일정 구간 이하일 경우
        // 해당 이벤트의 시작부로 초기화시킨다.
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

    // 대화창을 활성화한다.
    public void OnDiaLog()
    {
        diaLog.SetActive(true);
        Story();
    }

    // 대화창을 비활성화한다.
    public void CloseDiaLog()
    {
        diaLog.SetActive(false);
        talker[curTalker].SetActive(false);
    }

    // 대화창을 관리한다.
    // storyProgress에 맞춰서 해당 텍스트를 출력한다.
    // 현재 언어에 맞춰 출력된다.
    public void Story()
    {

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
                talkerNameText.text = textData[GameManager.instance.data.storyProgress]["Talker_Eng"].ToString();

                talker[textData[GameManager.instance.data.storyProgress]["TalkerIndex"].ToObject<int>()].SetActive(true);

                break;
            default:
                break;
        }

        curTalker = textData[GameManager.instance.data.storyProgress]["TalkerIndex"].ToObject<int>();
    }

    /// <summary>
    /// 버튼 클릭시 스토리 진행.
    /// </summary>
    private void OnClick_NextStoryBtn()
    {

        // 스토리를 진행시킴.
        GameManager.instance.data.storyProgress++;
        // 일정 대사가 나오면 대화창을 닫는다.
        switch (GameManager.instance.data.storyProgress)
        {
            // 0 ~ 2  오프닝이 끝나고 월드맵 입장시 출력되는 텍스트
            // 버튼을 한번 더 클릭하면 storyProgress가 3이 되며 대화창이 닫힌다.
            // 월드맵에서 왕성을 가르키는 이미지를 활성화한다.
            case 3:
                CloseDiaLog();
                // 왕성 강조하는 이미지
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_1").SetActive(true);
                Debug.Log("11");
                return;
            // 3 ~ 48 왕성 입장시 출력되는 텍스트.
            // 버튼을 한번 더 클릭시 storyProgress가 49가 되며 대화창이 닫힌다.
            // 영지 내에서 교회를 가르키는 이미지가 활성화된다.
            // 탑 바 툴팁이 활성화 된다.
            case 49:
                CloseDiaLog();
                // 교회를 강조하는 이미지.
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_3").SetActive(true);
                // 왕성에서 나오고 활성화 되는 툴팁.
                toolTip[0].SetActive(true);
                return;

            // 49 ~ 53 교회 입장시 출력되는 텍스트.
            // 버튼을 한번 더 클릭시 storyProgress가 54가 되며 대화창이 닫힌다.
            // 영웅 툴팁 이미지를 활성화 한다.
            case 54:
                CloseDiaLog();
                toolTip[1].SetActive(true);
                toolTip[2].SetActive(true);
                return;
            // 54 ~ 57  교회에서 퇴장시 출력되는 텍스트.
            // 버튼을 한번 더 클릭시 storyProgress가 58이 되며 대화창이 닫힌다.
            // 상점을 가르키는 이미지가 활성화 된다.
            case 58:
                CloseDiaLog();
                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_4").SetActive(true);
                return;
            // 58 ~ 63  상점 입장시 출력되는 텍스트.
            // 버튼을 한번 더 클릭시 storyProgress가 64가 되며 대화창이 닫힌다.
            // 상점 툴팁 이미지를 활성화 한다.
            case 64:
                CloseDiaLog();
                toolTip[3].SetActive(true);
                toolTip[4].SetActive(true);
                return;
            // 64 ~ 68 상점에서 퇴장시 출력퇴는 텍스트.
            // 버튼을 한번 더 클릭시 storyProgress가 69가 되며 대화창과 마을 지도 창이 닫힌다.
            // 던전을 가르키는 이미지를 활성화 하고 다음 스토리 대사를 출력한다.
            case 69:
                CloseDiaLog();
                map = GameObject.Find("WUIManager").GetComponent<WorldMapCastle>();
                map.OnClick_OutCastleBtn();

                GameObject.Find("Basic UI/StoryBackGround/StoryBackGround_5").SetActive(true);
                OnDiaLog();
                return;
            // 69 ~ 91 던전 입장 전 출력되는 텍스트.
            // 버튼을 한번 더 클릭시 storyProgress가 92가 되며 대화창이 닫힌다.
            case 92:
                CloseDiaLog(); 
                return;
            default:
                break;
        }


        Story();
    }
}
