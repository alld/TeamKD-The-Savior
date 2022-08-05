using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOS : MonoBehaviour
{
    #region 환경 변수
    [Header("환경 변수")]
    public int dungeonNumber = 0;
    //private PlayUIManager PUIManager;
    private GameObject timerArrowDG;
    private GameObject[] timerlevelUI;
    private WaitForSeconds delay = new WaitForSeconds(0.1f);
    private DungeonController DungeonCtrl;
    public List<string> errorList;
    #endregion

    #region 던전 기본 데이터

    List<MonsterDatabase.InfoMonster> monsterBox = new List<MonsterDatabase.InfoMonster>();

    List<CharacterDatabase.InfoCharacter> stageSlotPlayerBottom;
    List<CharacterDatabase.InfoCharacter> stageSlotPlayerTop;
    List<CharacterDatabase.InfoCharacter> stageSlotPlayerMid;

    //던전 관련된 변수 : DG
    /// <summary>
    /// 던전이 가지고있는 모든 스테이지 그룹
    /// </summary>
    [Header("던전정보")]
    public GameObject[] stageGroupDG;
    /// <summary>
    /// 현재 던전이 사용중인 스테이지
    /// </summary>
    public GameObject slotStageDG;

    public GameObject playerStagePointGroup;
    public GameObject monsterStagePointGroup;
    private Transform[] playerStagePoint;
    private Transform[] monsterStagePoint;


    /// <summary>
    /// 각라운드가 가지고있는 정보
    /// <br>1. 일반</br>
    /// <br>2. 중간보스방</br>
    /// <br>3. 이벤트방</br>
    /// <br>4. 특수목적방</br>
    /// <br>5. 엘리트몹</br>
    /// <br>10. 보스방 </br>
    /// </summary>
    [Header("게임진행 변수")]
    public int[] roundInfoDG;
    /// <summary>
    /// 현재 라운드에 생존해있는 몬스터 그룹
    /// </summary>
    public List<MonsterDatabase.InfoMonster> monsterGroup = new List<MonsterDatabase.InfoMonster>();
    /// <summary>
    /// 플레이어 유닛 그룹
    /// </summary>
    public List<CharacterDatabase.InfoCharacter> characterGroup = new List<CharacterDatabase.InfoCharacter>(); // 오브젝트로 설정
    /// <summary>
    /// 게임 분기 확인 스테이지가 순서대로 들어있기때문에, 게임분기 컷팅시키는 변수
    /// </summary>
    public int checkCountDGGame;
    
    //던전안에서 플레이어관련된 변수 : DGP
    /// <summary>
    /// 현재 라운드 
    /// </summary>
    public int roundDGP;
    /// <summary>
    /// 현재 까지 얻은 누적골드
    /// </summary>
    public int accrueGoldDGP;
    /// <summary>
    /// 현재까지 얻은 누적 소울(개별로 얻은 소울 제외)
    /// </summary>
    public int accrueSoulDGP;
    /// <summary>
    /// 현재 보유한 코스트 (최대치 10)
    /// </summary>
    public int costDGP;
    /// <summary>
    /// 라운드 동안 지속된 시간 
    /// </summary>
    public float progressTimeDGP 
    {
        get { return progressTimeDGP; }
        set 
        { 
            progressTimeDGP = value;
            DungeonCtrl.gameTimerText.text = progressTimeDGP.ToString("F0");
        }
    }
    /// <summary>
    /// 시간 흐름에 따른 시간단계를 표시 
    /// <br>0. 초반</br>
    /// <br>1. 중반</br>
    /// <br>2. 후반</br>
    /// </summary>
    public bool timerOnDGP;
    public int timeLevelDGP;
    /// <summary>
    /// 남은 카드 (덱잔량)
    /// </summary>
    public int remainingCardDGP;
    /// <summary>
    /// 개별적으로 얻은 소울 정보
    /// </summary>
    public int[] eaGetSoulDGP;
    /// <summary>
    /// 파티 그룹의 사망 여부 판단
    /// <br>1. 살아있음</br>
    /// <br>2. 죽었음</br>
    /// <br>3. 특수상태(빈사등..)</br>
    /// </summary>
    public int[] eaIsDieDGP;
    /// <summary>
    /// [통계데이터] 개별 누적 피격량
    /// </summary>
    public float[] eaDamagedDGP;
    /// <summary>
    /// [통계데이터] 개별 누적 데미지량
    /// </summary>
    public float[] eaDamageDGP;
    /// <summary>
    /// [통계데이터] 개별 누적 킬수
    /// </summary>
    public float[] eaKillCountDGP;
    #endregion

    #region 전달받은 GameManager의 Data
    //캐릭터 정보 
    public CharacterDatabase.InfoCharacter[] partyUnit = 
        { 
            new CharacterDatabase.InfoCharacter(0),
            new CharacterDatabase.InfoCharacter(0),
            new CharacterDatabase.InfoCharacter(0),
            new CharacterDatabase.InfoCharacter(0)
        };
    //덱정보
    public List<int> useDeckDGP;
    //유물 정보
    //초회 보상 유무 진행상황변수와는 별개
    bool ClearkCheck;
    #endregion
    //카드기능 제외... 카드 데이터베이스를 만들어야함. 
    void Start()
    {
        #region 캐시처리 //합칠때 다시한번 설정해줘야함..
        DungeonCtrl = DungeonController.instance;
        DungeonDatabase.InfoDungeon infoDungeon = new DungeonDatabase.InfoDungeon(dungeonNumber);
        monsterBox = infoDungeon.dungeonMonsterBox;
        //PUIManager = GameObject.Find("PUIManaer").GetComponent<PlayUIManager>();
        //DGUI = PUIManager.DungeonUI;
        //DGtimerArrow = PUIManager.DungeonTimerArrow;
        //DGtimerlevel = PUIManager.DungeonTimerColor;

        playerStagePoint = playerStagePointGroup.GetComponentsInChildren<Transform>();
        monsterStagePoint = monsterStagePointGroup.GetComponentsInChildren<Transform>();
        #endregion
        //게임용 UI 활성화 
        DungeonCtrl.dungeonUI.SetActive(true);
        GameSetting();
    }

    #region 던전 UI 처리
    /// <summary>
    /// 라운드 종료후 페이드인 처리
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        bool check = true;
        DungeonCtrl.fadeObj.SetActive(true);
        float colorvalue = 0;
        while (check)
        {
            Color color = DungeonCtrl.fade.color;
            colorvalue += Time.deltaTime * 10;
            if (colorvalue < 1)
            {
                color.a = colorvalue;
                DungeonCtrl.fade.color = color;
            }
            else check = false;
            yield return delay;
        }
        DungeonCtrl.fade.color = new Color(0, 0, 0, 1);
        FadeOut();
    }
    /// <summary>
    /// 페이드인 처리후 페이드아웃 
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        bool check = true;
        float colorvalue = 1;
        while (check)
        {
            Color color = DungeonCtrl.fade.color;
            colorvalue -= Time.deltaTime * 10;
            if (colorvalue > 0)
            {
                color.a = colorvalue;
                DungeonCtrl.fade.color = color;
            }
            else check = false;
            yield return delay;
        }
        DungeonCtrl.fade.color = new Color(0, 0, 0, 0);
        DungeonCtrl.fadeObj.SetActive(false);
    }
    #endregion
    #region 던전 셋팅
    /// <summary>
    /// 게임이 시작하고 던전에서 기본값들을 셋팅할때 사용
    /// </summary>
    void GameSetting()
    {
        DeckShuffle();
        PlayerUnitCreate();
        ////스테이지 설정 한번 들어가야함. 
        StageReset(checkCountDGGame);
    }
    /// <summary>
    /// 스테이지가 변동되고 실행되는 기능
    /// 매개변수는 몇번째 스테이지를 진행할지 적용
    /// </summary>
    /// <param name="stageNum"></param>
    void StageReset(int stageNum)
    {
        DGTimerStart();
        PlayerUnitSetting();
    }

    /// <summary>
    /// 플레이어유닛 스테이지 자동배치
    /// </summary>
    void PlayerUnitSetting()
    {
        foreach (var item in partyUnit)
        {
            switch (item.attackTypeCH)
            {
                case 1:
                    if (stageSlotPlayerBottom.Count < 3)
                    {
                        stageSlotPlayerBottom.Add(item);
                    }
                    else
                    {
                        CharacterDatabase.InfoCharacter moveSlot = item;
                        CharacterDatabase.InfoCharacter tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 작은수치가 우선
                        for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
                        {
                            if (stageSlotPlayerBottom[i].positionPerCH > moveSlot.positionPerCH)
                            {
                                tempSlot = stageSlotPlayerBottom[i];
                                stageSlotPlayerBottom.RemoveAt(i);
                                stageSlotPlayerBottom.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }  
                        }
                        // 다음줄 검토 
                        if(stageSlotPlayerMid.Count < 3)
                        {
                            stageSlotPlayerMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPerCH > moveSlot.positionPerCH)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerTop.Count < 3)
                            {
                                stageSlotPlayerTop.Add(moveSlot);
                            }
                            else
                            {
                                GameError("유닛배치 : 초과된 유닛 발생");
                            }
                        }
                    }
                    break;
                case 2:
                    if (stageSlotPlayerMid.Count < 3)
                    {
                        stageSlotPlayerMid.Add(item);
                    }
                    else
                    {
                        CharacterDatabase.InfoCharacter moveSlot = item;
                        CharacterDatabase.InfoCharacter tempSlot = item;
                        // 수치가 낮은 경우 
                        if (item.positionPerCH >= 30)
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPerCH > moveSlot.positionPerCH)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerBottom.Count < 3)
                            {
                                stageSlotPlayerBottom.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
                                {
                                    if (stageSlotPlayerBottom[i].positionPerCH > moveSlot.positionPerCH)
                                    {
                                        tempSlot = stageSlotPlayerBottom[i];
                                        stageSlotPlayerBottom.RemoveAt(i);
                                        stageSlotPlayerBottom.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // 다음줄 검토 
                                if (stageSlotPlayerTop.Count < 3)
                                {
                                    stageSlotPlayerTop.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("유닛배치 : 초과된 유닛 발생");
                                }
                            }
                        }
                        else // 수치가 높은 경우 
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPerCH < moveSlot.positionPerCH)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerTop.Count < 3)
                            {
                                stageSlotPlayerTop.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotPlayerTop.Count; i++)
                                {
                                    if (stageSlotPlayerTop[i].positionPerCH < moveSlot.positionPerCH)
                                    {
                                        tempSlot = stageSlotPlayerTop[i];
                                        stageSlotPlayerTop.RemoveAt(i);
                                        stageSlotPlayerTop.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // 다음줄 검토 
                                if (stageSlotPlayerBottom.Count < 3)
                                {
                                    stageSlotPlayerBottom.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("유닛배치 : 초과된 유닛 발생");
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    if (stageSlotPlayerTop.Count < 3)
                    {
                        stageSlotPlayerTop.Add(item);
                    }
                    else
                    {
                        CharacterDatabase.InfoCharacter moveSlot = item;
                        CharacterDatabase.InfoCharacter tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 큰수치가 우선
                        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
                        {
                            if (stageSlotPlayerTop[i].positionPerCH < moveSlot.positionPerCH)
                            {
                                tempSlot = stageSlotPlayerTop[i];
                                stageSlotPlayerTop.RemoveAt(i);
                                stageSlotPlayerTop.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // 다음줄 검토 
                        if (stageSlotPlayerMid.Count < 3)
                        {
                            stageSlotPlayerMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPerCH < moveSlot.positionPerCH)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerBottom.Count < 3)
                            {
                                stageSlotPlayerBottom.Add(moveSlot);
                            }
                            else
                            {
                                GameError("유닛배치 : 초과된 유닛 발생");
                            }
                        }
                    }
                    break;
                default:
                    GameError("유닛배치 : 공격타입이 지정되지 않은 유닛이 존재함");
                    break;
            }
        }
        for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
        {
            stageSlotPlayerBottom[i].objectCH.transform.position = playerStagePoint[i + 1].position;
        }
        for (int i = 0; i < stageSlotPlayerMid.Count; i++)
        {
            stageSlotPlayerMid[i].objectCH.transform.position = playerStagePoint[i + 4].position;
        }
        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
        {
            stageSlotPlayerTop[i].objectCH.transform.position = playerStagePoint[i + 7].position;
        }
    }
    /// <summary>
    /// 플레이어 유닛 오브젝트화
    /// </summary>
    void PlayerUnitCreate()
    {
        foreach (var item in partyUnit)
        {
            item.objectCH = Instantiate(item.objectCH);
        }
    }
    /// <summary>
    /// 몬스터 유닛 오브젝트화
    /// </summary>
    void MonsterCreate()
    {
        
    }

    /// <summary>
    /// 게임 시작후, 휴식 타이밍에 덱 셔플 기능
    /// </summary>
    void DeckShuffle()
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < useDeckDGP.Count; i++)
        {
            int tempA = Random.Range(0, useDeckDGP.Count);
            tempList.Add(useDeckDGP[tempA]);
            useDeckDGP.RemoveAt(tempA);
        }
        useDeckDGP = tempList;
    }
    #endregion
    #region 던전 타이머 기능
    /// <summary>
    /// 타이머 시작 기능
    /// </summary>
    public void DGTimerStart() 
    {
        progressTimeDGP = 0;
        timerOnDGP = true;
        timeLevelDGP = 0;
        DungeonCtrl.gameTimerBG[0].fillAmount = 1;
        DungeonCtrl.gameTimerBG[1].fillAmount = 1;
        StartCoroutine(DGTimer());
    }
    /// <summary>
    /// 타이머 작동 기능
    /// 값이 변동되면 자동으로 텍스트가 반영됨
    /// </summary>
    /// <returns></returns>
    public IEnumerator DGTimer()
    {
        float cycleTime = 0f;
        while (timerOnDGP)
        {
            yield return null;
            cycleTime += Time.deltaTime;
            progressTimeDGP += Time.deltaTime;
            if (timeLevelDGP == 3)
            {
                DGTimerUIReset();
            }
            if (cycleTime >= 20)
            {
                cycleTime = 0f;
                switch (timeLevelDGP)
                {
                    case 0:
                        timeLevelDGP = 1;
                        DGTimerUIReset();
                        break;
                    case 1:
                        timeLevelDGP = 2;
                        DGTimerUIReset();
                        break;
                    default:
                        timeLevelDGP = 3;
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 외부에서 타이머 종료시 사용
    /// </summary>
    public void DGTimerEnd()
    {
        timerOnDGP = false;
    }
    /// <summary>
    /// 타이머의 회전하는 UI 확인 
    /// </summary>
    public void DGTimerUIReset()
    {
        DungeonCtrl.gameTimerBG[timeLevelDGP].fillAmount = (20 - (progressTimeDGP % 20)) * 0.05f;
    }
    #endregion
    #region 던전 종료
    /// <summary>
    /// 던전 종료시 작동 // 결산창에서 나가기버튼 클릭시 실행되는 함수
    /// </summary>
    void DungeonEnd()
    {
        DungeonCtrl.dungeonUI.SetActive(false);
        // 데이터 전달 
        // 세이브 1회 실행
    }
    #endregion
    #region 게임 에러 기록
    public void GameError(string str)
    {
        errorList.Add(str);
        foreach (var item in errorList)
        {
            Debug.Log(errorList);
        }
    }
    #endregion
}
