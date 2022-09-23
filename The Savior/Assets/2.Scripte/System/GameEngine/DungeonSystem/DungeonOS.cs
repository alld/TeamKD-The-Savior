using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DungeonOS : MonoBehaviour
{
    public static DungeonOS instance = null;
    #region 환경 변수
    [Header("환경 변수")]
    public int dungeonNumber = 0;
    //private PlayUIManager PUIManager;
    private GameObject timerArrowDG;
    private GameObject[] timerlevelUI;
    WaitForSeconds delay_10 = new WaitForSeconds(1.0f);
    WaitForSeconds delay_01 = new WaitForSeconds(0.1f);
    WaitForSeconds delay_001 = new WaitForSeconds(0.01f);
    WaitForSeconds delay_03 = new WaitForSeconds(0.3f);
    private DungeonController DungeonCtrl;
    public List<string> errorList;
    private PlayerInput playerInput;
    private InputActionMap playerMap;
    private InputAction clickAction;

    // 씬을 관리하는 스크립트를 만들었습니다. 게임 매니저에 있던 currentScene은 이쪽으로 옮겨졌습니다.
    // Start에서 초기화할게요.
    private SceneLoad sceneLoader;      

    private IEnumerator Timer;
    private int tempRoundNumber;

    #endregion
    #region 던전 기본 데이터
    public Transform UnitGroupTr;
    public Transform MonsterGroupTr;

    List<int> rewardHeroBox = new List<int>();
    List<int> rewardCardBox = new List<int>();
    List<int> rewardRelicBox = new List<int>();

    List<UnitStateData> stageSlotPlayerBottom = new List<UnitStateData>();
    List<UnitStateData> stageSlotPlayerTop = new List<UnitStateData>();
    List<UnitStateData> stageSlotPlayerMid = new List<UnitStateData>();

    List<UnitStateData> stageSlotMonsterBottom = new List<UnitStateData>();
    List<UnitStateData> stageSlotMonsterTop = new List<UnitStateData>();
    List<UnitStateData> stageSlotMonsterMid = new List<UnitStateData>();


    public delegate void StateCheck();
    public StateCheck dele_stateCheck; // 몬스터, 캐릭터 변화 체크 이벤트 // 몬스터 이벤트에 아직안넣음
    public StateCheck dele_TimeALWAY, dele_TimeFIRST, dele_TimeMIDDLE, dele_TimeHALF, dele_TimeLAST;
    public StateCheck dele_RoundStart, dele_RoundEnd;
    //던전 관련된 변수 : DG
    /// <summary>
    /// 던전이 가지고있는 모든 스테이지 그룹
    /// </summary>
    [Header("던전정보")]
    public GameObject[] stagePrefabGroupDG;
    public GameObject[] stageGroupDG;
    /// <summary>
    /// 특정 스테이지가 몇번째 프리팹을 가지는지에대한 정보
    /// </summary>
    public int[] stageIndexDG;
    /// <summary>
    /// 현재 던전이 사용중인 스테이지
    /// </summary>
    public GameObject slotStageDG;

    public GameObject playerStagePointGroup;
    public GameObject monsterStagePointGroup;
    private Transform[] playerStagePoint;
    public Transform[] monsterStagePoint;
    public DungeonData dungeonData;
    public bool[] handSlot = { false, false, false };
    /// <summary>
    /// 각라운드가 가지고있는 정보
    /// <br>1. 일반</br>
    /// <br>2. 중간보스방</br>
    /// <br>3. 이벤트방</br>
    /// <br>4. 특수목적방</br>
    /// <br>5. 엘리트몹</br>
    /// <br>6. 분기 존재</br>
    /// <br>10. 보스방 </br>
    /// </summary>
    [Header("게임진행 변수")]
    public int[] roundInfoDG;
    /// <summary>
    /// 현재 라운드에 생존해있는 몬스터 그룹
    /// </summary>
    public List<UnitStateData> monsterGroup;// = new List<UnitStateData>();
    /// <summary>
    /// 플레이어 유닛 그룹
    /// </summary>
    public List<UnitStateData> characterGroup = new List<UnitStateData>(); // 오브젝트로 설정
    /// <summary>
    /// 게임 분기 확인 스테이지가 순서대로 들어있기때문에, 게임분기 컷팅시키는 변수
    /// </summary>
    public int checkCountDGGame;
    public bool resurrectable;

    //던전안에서 플레이어관련된 변수 : DGP
    /// <summary>
    /// 현재 라운드 
    /// </summary>
    public int roundDGP;
    /// <summary>
    /// 현재 라운드 진행 여부
    /// </summary>
    public bool ISRoundPlaying;
    public bool isRoundPlaying
    {
        get { return ISRoundPlaying; }
        set
        {
            ISRoundPlaying = value;
            if (value)
            {
                dele_RoundStart();
                DGTimerStart();
            }
            else
            {
                dele_RoundEnd();
                DGTimerEnd();
            }
        }
    }
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
    private int CostDGP;
    public int costDGP
    {
        get { return CostDGP; }
        set
        {
            CostDGP = value;
            HandUIReset();
        }
    }
    /// <summary>
    /// 라운드 동안 지속된 시간 
    /// </summary>
    private float ProgressTimeDGP;
    public float progressTimeDGP
    {
        get { return ProgressTimeDGP; }
        set
        {
            ProgressTimeDGP = value;
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
    private int TimeLevelDGP;
    public int timeLevelDGP
    {
        get { return TimeLevelDGP; }
        set
        {
            TimeLevelDGP = value;
            switch (value)
            {
                case 1:
                    dele_TimeMIDDLE();
                    break;
                case 2:
                    dele_TimeHALF();
                    break;
                default:
                    break;
            }
        }
    }
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
    /// </summary>
    public int DieCount_Ally;
    public int DieCount_Enemy;
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

    public List<CardDataBase.Data> handCard = new List<CardDataBase.Data>();
    #endregion
    #region 전달받은 GameManager의 Data
    //캐릭터 정보 
    public List<UnitStateData> partyUnit;// = new List<UnitStateData>();
    public List<RelicData.Data> equipRelic = new List<RelicData.Data>();
    //덱정보
    public List<int> useDeckDGP = new List<int>();
    //유물 정보
    //초회 보상 유무 진행상황변수와는 별개
    bool ClearkCheck;
    #endregion


    private void Awake()
    {
        dungeonData = GetComponent<DungeonData>();
        dungeonData.DataSetting(dungeonNumber);
        instance = this;

    }
    //카드기능 제외... 카드 데이터베이스를 만들어야함. 
    void Start()
    {
        #region 캐시처리 //합칠때 다시한번 설정해줘야함..
        DungeonCtrl = DungeonController.instance;
        playerStagePoint = playerStagePointGroup.GetComponentsInChildren<Transform>();
        monsterStagePoint = monsterStagePointGroup.GetComponentsInChildren<Transform>();

        playerInput = GetComponent<PlayerInput>();
        playerMap = playerInput.actions.FindActionMap("Player");
        clickAction = playerMap.FindAction("Click");

        Timer = DGTimer();
        //mouseMoveAction.performed += ctx =>
        //{
        //    mousePoint = ctx.ReadValue<Vector2>();
        //};

        clickAction.performed += ctx =>
        {
            OnStageSelect();
        };

        #endregion

        GameManager.instance.dungeonOS = this;
        GameManager.instance.dungeonPlayTime = 0;
        //게임용 UI 활성화 
        DungeonCtrl.dungeonUI.SetActive(true);
        sceneLoader = GameObject.Find("GameManager").GetComponent<SceneLoad>();
        GameSetting();

    }

    #region 던전 이벤트(기능) // 주석처리 미흡
    public void OnStateCheck()
    {
        if (dele_stateCheck != null) dele_stateCheck();
        if (monsterGroup.Count <= DieCount_Enemy)
        {
            OnRoundVictory();
        }
        else if (partyUnit.Count <= DieCount_Ally)
        {
            OnDungeonFailed();
        }
    }

    public void OnRoundVictory()
    {
        DungeonCtrl.roundTextInfo.SetActive(true);
        DungeonCtrl.roundText.text = "Round Clear";
        isRoundPlaying = false;
        if (roundDGP == 10 || roundDGP == 20)
        {
            OnDungeonAllClear();
            return;
        }
        if (roundDGP % 10 == 5) OnRest();
        StageSelectButtonSet();
    }

    void OnDungeonAllClear()
    {
        DungeonCtrl.RewardWindow.SetActive(true);
        //보상 UI 처리(결과창)
    }

    void OnDungeonFailed()
    {
        //보상 UI 처리(결과창)
    }

    void NextRound(int num)
    {
        //isRoundPlaying = true;
        if (++roundDGP % 10 == 5) tempRoundNumber = 5;
        else if (roundDGP % 10 != 0)
        {
            if (num + 1 != roundDGP)
            {
                //StageReset(num);
                tempRoundNumber = num;
            }
            else
            {
                tempRoundNumber = roundDGP;
                //StageReset(roundDGP);
            }
        }
        else
        {
            tempRoundNumber = 10;
            //StageReset(10);
        }
        HandRefill();
        StartCoroutine(FadeIn());
    }
    // 마우스 입력 버튼 아직 안했음
    void OnStageSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("StagePoint")))
        {
            if (hit.collider.CompareTag("STAGEPOINT"))
            {
                int temp = hit.collider.GetComponent<PointInfo>().pointNumber;
                if (temp == 0)
                {
                    NextRound(roundDGP);
                }
                else
                {
                    if (roundDGP > 10) roundDGP -= 10;
                    else roundDGP += 10;
                    NextRound(roundDGP);
                }
            }
        }
    }

    void OnRest() //휴식
    {
        foreach (var item in partyUnit)
        {
            if (!item.isLive) resurrectable = true;
            item.hp = item.maxHP;
        }
        HandReset();
    }

    void SelectResurrection(int partySlotN)
    {
        if (resurrectable)
        {
            if (partyUnit != null & !partyUnit[partySlotN].isLive)
            {
                resurrectable = false;
                partyUnit[partySlotN].isLive = true;
                partyUnit[partySlotN].hp = partyUnit[partySlotN].maxHP;
                // 캐릭터 상태기능 전환 필요함
            }
        }
    }
    void SelectResurrection(int partySlotN, float recov)
    {
        if (resurrectable)
        {
            if (partyUnit != null & !partyUnit[partySlotN].isLive)
            {
                resurrectable = false;
                partyUnit[partySlotN].isLive = true;
                partyUnit[partySlotN].hp = recov;
                // 캐릭터 상태기능 전환 필요함
            }
        }
    }
    // 부활을 사용하지 않을 경우 경고창 출력; 
    #endregion
    #region 던전 UI 처리 // 주석기능 미흡
    void StageSelectButtonSet()
    {
        slotStageDG.GetComponent<StageInfo>().StageSelectGroup.SetActive(true);
        //GameObject.Find("StageSelectGroup").SetActive(true);
        // 버튼 클릭하게해서 NextRound 실행시킴 
    }
    public void HandUIReset()
    {
        int temp = roundDGP % 10;
        DungeonCtrl.gameRoundbarArrow.transform.SetParent(DungeonCtrl.gameRoundbarPoint[temp - 1].transform);

        DungeonCtrl.gameRoundbarArrow.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        DungeonCtrl.gameRoundbarArrow.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        DungeonCtrl.playerCostGauage.fillAmount = (float)costDGP / 10f;
        DungeonCtrl.playerExpectationsGauage.fillAmount = DungeonCtrl.playerCostGauage.fillAmount;
        DungeonCtrl.playerLackCost.fillAmount = DungeonCtrl.playerCostGauage.fillAmount;
        DungeonCtrl.gameCostNumber.text = costDGP.ToString();
    }


    // 외부에서 체력 변동시 해당값을 호출할것 
    public void PartyUIReset()
    {
        for (int i = 0; i < partyUnit.Count; i++)
        {
            if (partyUnit[i] != null)
            {
                DungeonCtrl.partySlotHPGauage[i].fillAmount = partyUnit[i].hp / partyUnit[i].maxHP;
            }
        }
    }

    #region 페이드인/아웃
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
            yield return delay_001;
        }
        DungeonCtrl.fade.color = new Color(0, 0, 0, 1);
        yield return delay_03;
        StartCoroutine(FadeOut());
        StageReset(tempRoundNumber);
        DungeonCtrl.roundText.text = " ";
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
            colorvalue -= Time.deltaTime * 1;
            if (colorvalue > 0)
            {
                color.a = colorvalue;
                DungeonCtrl.fade.color = color;
            }
            else check = false;
            yield return delay_001;
        }
        DungeonCtrl.fade.color = new Color(0, 0, 0, 0);
        DungeonCtrl.fadeObj.SetActive(false);

        yield return delay_10;
        DungeonCtrl.roundText.text = "3";
        yield return delay_10;
        DungeonCtrl.roundText.text = "2";
        yield return delay_10;
        DungeonCtrl.roundText.text = "1";
        yield return delay_10;
        DungeonCtrl.roundText.text = "Start";
        isRoundPlaying = true;
        yield return delay_10;
        yield return delay_10;
        DungeonCtrl.roundTextInfo.SetActive(false);
    }
    #endregion
    #endregion
    #region 던전 셋팅 // 주석기능 미흡
    /// <summary>
    /// 게임이 시작하고 던전에서 기본값들을 셋팅할때 사용
    /// </summary>
    void GameSetting()
    {
        roundInfoDG = dungeonData.stageDataInfo; // 해당 기능 기준으로 검사 % 나머지연산 교체
        stageIndexDG = dungeonData.stageDataIndex;
        foreach (var item in GameManager.instance.data.equipRelic)
        {
            if (item != 0) equipRelic.Add(new RelicData.Data(item));
        }

        foreach (var item in stagePrefabGroupDG.Select((value, index) => new { value, index }))
        {
            stageGroupDG[item.index] = Instantiate(item.value);
            stageGroupDG[item.index].SetActive(false);
            SceneManager.MoveGameObjectToScene(stageGroupDG[item.index], SceneManager.GetSceneByName(sceneLoader._currentlyScene)); // sceneLoader에서 현재 씬 이름을 가져옵니다.
        }
        roundDGP = 1;
        HandReset();
        PlayerUnitCreate();
        ////스테이지 설정 한번 들어가야함. 
        StageReset(roundDGP);

        //StageReset(checkCountDGGame); // 컷팅넘버 구조 재검토
    }
    /// <summary>
    /// 스테이지가 변동되고 실행되는 기능
    /// 매개변수는 몇번째 스테이지를 진행할지 적용
    /// </summary>
    /// <param name="stageNum"></param>
    void StageReset(int stageNum)
    {
        if (roundDGP != 1)
        {
            int temp = monsterGroup.Count;
            for (int i = temp - 1; i >= 0; i--)
            {
                monsterGroup[i].SelfDestory();
            }
            monsterGroup.Clear();
        }
        if (slotStageDG != null) slotStageDG.SetActive(false);
        slotStageDG = stageGroupDG[stageIndexDG[stageNum]];
        slotStageDG.SetActive(true);
        Camera.main.transform.position = slotStageDG.GetComponentInChildren<Camera>().transform.position;
        Camera.main.transform.rotation = slotStageDG.GetComponentInChildren<Camera>().transform.rotation;
        PlayerUnitSetting();
        MonsterCreate();
        MonsterSetting();
        MonsterBossCreate();
        //isRoundPlaying = true;
        if (stageNum == 1)
        {
            isRoundPlaying = true;
        }
        slotStageDG.GetComponent<StageInfo>().StageSelectGroup.SetActive(false);
    }

    /// <summary>
    /// 플레이어유닛 스테이지 자동배치
    /// </summary>
    void PlayerUnitSetting()
    {
        foreach (var item in partyUnit)
        {
            switch (item.attackType)
            {
                case 1:
                    if (stageSlotPlayerBottom.Count < 2)
                    {
                        stageSlotPlayerBottom.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 작은수치가 우선
                        for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
                        {
                            if (stageSlotPlayerBottom[i].positionPri > moveSlot.positionPri)
                            {
                                tempSlot = stageSlotPlayerBottom[i];
                                stageSlotPlayerBottom.RemoveAt(i);
                                stageSlotPlayerBottom.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // 다음줄 검토 
                        if (stageSlotPlayerMid.Count < 2)
                        {
                            stageSlotPlayerMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerTop.Count < 2)
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
                    if (stageSlotPlayerMid.Count < 2)
                    {
                        stageSlotPlayerMid.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        // 수치가 낮은 경우 
                        if (item.positionPri >= 30)
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerBottom.Count < 2)
                            {
                                stageSlotPlayerBottom.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotPlayerBottom.Count; i++)
                                {
                                    if (stageSlotPlayerBottom[i].positionPri > moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotPlayerBottom[i];
                                        stageSlotPlayerBottom.RemoveAt(i);
                                        stageSlotPlayerBottom.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // 다음줄 검토 
                                if (stageSlotPlayerTop.Count < 2)
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
                                if (stageSlotPlayerMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerTop.Count < 2)
                            {
                                stageSlotPlayerTop.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotPlayerTop.Count; i++)
                                {
                                    if (stageSlotPlayerTop[i].positionPri < moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotPlayerTop[i];
                                        stageSlotPlayerTop.RemoveAt(i);
                                        stageSlotPlayerTop.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // 다음줄 검토 
                                if (stageSlotPlayerBottom.Count < 2)
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
                    if (stageSlotPlayerTop.Count < 2)
                    {
                        stageSlotPlayerTop.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 큰수치가 우선
                        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
                        {
                            if (stageSlotPlayerTop[i].positionPri < moveSlot.positionPri)
                            {
                                tempSlot = stageSlotPlayerTop[i];
                                stageSlotPlayerTop.RemoveAt(i);
                                stageSlotPlayerTop.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // 다음줄 검토 
                        if (stageSlotPlayerMid.Count < 2)
                        {
                            stageSlotPlayerMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotPlayerMid.Count; i++)
                            {
                                if (stageSlotPlayerMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotPlayerMid[i];
                                    stageSlotPlayerMid.RemoveAt(i);
                                    stageSlotPlayerMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotPlayerBottom.Count < 2)
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
            stageSlotPlayerBottom[i].gameObject.transform.position = playerStagePoint[i + 1].position;
            stageSlotPlayerBottom[i].HPUIMove();
        }
        for (int i = 0; i < stageSlotPlayerMid.Count; i++)
        {
            stageSlotPlayerMid[i].gameObject.transform.position = playerStagePoint[i + 3].position;
            stageSlotPlayerMid[i].HPUIMove();
        }
        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
        {
            stageSlotPlayerTop[i].gameObject.transform.position = playerStagePoint[i + 5].position;
            stageSlotPlayerTop[i].HPUIMove();
        }



        stageSlotPlayerBottom.Clear();
        stageSlotPlayerMid.Clear();
        stageSlotPlayerTop.Clear();
    }
    /// <summary>
    /// 플레이어 유닛 오브젝트화
    /// </summary>
    void PlayerUnitCreate()
    {
        UnitInfo tempUnitInfo;
        GameObject tempUnit;
        foreach (var item in GameManager.instance.data.equipCharacter.Select((value, index) => new { value, index }))
        {
            if (item.value != 0)
            {
                tempUnit = Instantiate(new CharacterDatabase.Data(item.value).charObject);
                SceneManager.MoveGameObjectToScene(tempUnit, SceneManager.GetSceneByName(sceneLoader._currentlyScene));// 씬 관련 변경했읍니다.
                partyUnit.Add(tempUnit.AddComponent<UnitStateData>());
                partyUnit[partyUnit.Count - 1].unitObj = tempUnit;
                partyUnit[partyUnit.Count - 1].DataSetting(true, item.value);
                partyUnit[partyUnit.Count - 1].gameObject.AddComponent<UnitMelee>();
                partyUnit[partyUnit.Count - 1].gameObject.AddComponent<UnitAI>();
                tempUnitInfo = partyUnit[partyUnit.Count - 1].GetComponent<UnitInfo>();
                tempUnitInfo.changeUnitNumber = item.value;
                tempUnitInfo.changePartyNumber = partyUnit.Count - 1;
                partyUnit[partyUnit.Count - 1].isLive = true;
                partyUnit[partyUnit.Count - 1].UISetting();
                partyUnit[partyUnit.Count - 1].gameObject.AddComponent<UnitSkill>();

            }
        }
    }

    /// <summary>
    /// 몬스터 스테이지 자동배치
    /// </summary>
    void MonsterSetting()
    {
        foreach (var item in monsterGroup)
        {
            switch (item.attackType)
            {
                case 1:
                    if (stageSlotMonsterBottom.Count < 4)
                    {
                        stageSlotMonsterBottom.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 작은수치가 우선
                        for (int i = 0; i < stageSlotMonsterBottom.Count; i++)
                        {
                            if (stageSlotMonsterBottom[i].positionPri > moveSlot.positionPri)
                            {
                                tempSlot = stageSlotMonsterBottom[i];
                                stageSlotMonsterBottom.RemoveAt(i);
                                stageSlotMonsterBottom.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // 다음줄 검토 
                        if (stageSlotMonsterMid.Count < 4)
                        {
                            stageSlotMonsterMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotMonsterTop.Count < 4)
                            {
                                stageSlotMonsterTop.Add(moveSlot);
                            }
                            else
                            {
                                GameError("몬스터배치 : 초과된 몬스터 발생");
                            }
                        }
                    }
                    break;
                case 2:
                    if (stageSlotMonsterMid.Count < 4)
                    {
                        stageSlotMonsterMid.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        // 수치가 낮은 경우 
                        if (item.attackType >= 30)
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri > moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotMonsterBottom.Count < 4)
                            {
                                stageSlotMonsterBottom.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotMonsterBottom.Count; i++)
                                {
                                    if (stageSlotMonsterBottom[i].positionPri > moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotMonsterBottom[i];
                                        stageSlotMonsterBottom.RemoveAt(i);
                                        stageSlotMonsterBottom.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // 다음줄 검토 
                                if (stageSlotMonsterTop.Count < 4)
                                {
                                    stageSlotMonsterTop.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("몬스터배치 : 초과된 몬스터 발생");
                                }
                            }
                        }
                        else // 수치가 높은 경우 
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotMonsterTop.Count < 3)
                            {
                                stageSlotMonsterTop.Add(moveSlot);
                            }
                            else
                            {
                                for (int i = 0; i < stageSlotMonsterTop.Count; i++)
                                {
                                    if (stageSlotMonsterTop[i].positionPri < moveSlot.positionPri)
                                    {
                                        tempSlot = stageSlotMonsterTop[i];
                                        stageSlotMonsterTop.RemoveAt(i);
                                        stageSlotMonsterTop.Insert(i, moveSlot);
                                        moveSlot = tempSlot;
                                    }
                                }
                                // 다음줄 검토 
                                if (stageSlotMonsterBottom.Count < 3)
                                {
                                    stageSlotMonsterBottom.Add(moveSlot);
                                }
                                else
                                {
                                    GameError("몬스터 배치 : 초과된 몬스터 발생");
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    if (stageSlotMonsterTop.Count < 4)
                    {
                        stageSlotMonsterTop.Add(item);
                    }
                    else
                    {
                        UnitStateData moveSlot = item;
                        UnitStateData tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 큰수치가 우선
                        for (int i = 0; i < stageSlotMonsterTop.Count; i++)
                        {
                            if (stageSlotMonsterTop[i].positionPri < moveSlot.positionPri)
                            {
                                tempSlot = stageSlotMonsterTop[i];
                                stageSlotMonsterTop.RemoveAt(i);
                                stageSlotMonsterTop.Insert(i, moveSlot);
                                moveSlot = tempSlot;
                            }
                        }
                        // 다음줄 검토 
                        if (stageSlotMonsterMid.Count < 4)
                        {
                            stageSlotMonsterMid.Add(moveSlot);
                        }
                        else
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPri < moveSlot.positionPri)
                                {
                                    tempSlot = stageSlotMonsterMid[i];
                                    stageSlotMonsterMid.RemoveAt(i);
                                    stageSlotMonsterMid.Insert(i, moveSlot);
                                    moveSlot = tempSlot;
                                }
                            }
                            // 다음줄 검토 
                            if (stageSlotMonsterBottom.Count < 4)
                            {
                                stageSlotMonsterBottom.Add(moveSlot);
                            }
                            else
                            {
                                GameError("몬스터 배치 : 초과된 몬스터 발생");
                            }
                        }
                    }
                    break;
                default:
                    GameError("몬스터 배치 : 공격타입이 지정되지 않은 몬스터가 존재함");
                    break;
            }
        }
        for (int i = 0; i < stageSlotMonsterBottom.Count; i++)
        {
            stageSlotMonsterBottom[i].gameObject.transform.position = monsterStagePoint[i + 2].position;
            stageSlotMonsterBottom[i].HPUIMove();
        }
        for (int i = 0; i < stageSlotMonsterMid.Count; i++)
        {
            stageSlotMonsterMid[i].gameObject.transform.position = monsterStagePoint[i + 6].position;
            stageSlotMonsterMid[i].HPUIMove();
        }
        for (int i = 0; i < stageSlotMonsterTop.Count; i++)
        {
            stageSlotMonsterTop[i].gameObject.transform.position = monsterStagePoint[i + 10].position;
            stageSlotMonsterTop[i].HPUIMove();
        }

        stageSlotMonsterBottom.Clear();
        stageSlotMonsterMid.Clear();
        stageSlotMonsterTop.Clear();
    }


    /// <summary>
    /// 몬스터 유닛 오브젝트화
    /// </summary>
    void MonsterCreate()
    {
        DieCount_Enemy = 0;
        GameObject tempMonster;
        UnitInfo tempUnitInfo;
        for (int i = 0; i < dungeonData.monsterBoxCount[roundDGP]; i++)
        {

            int tempint = Random.Range(dungeonData.monsterBoxMin[roundDGP], dungeonData.monsterBoxMax[roundDGP]);
            tempMonster = Instantiate(dungeonData.dungeonMonsterBox[tempint].charObject);
            SceneManager.MoveGameObjectToScene(tempMonster, SceneManager.GetSceneByName(sceneLoader._currentlyScene));//변경했읍니다.
            monsterGroup.Add(tempMonster.AddComponent<UnitStateData>());
            monsterGroup[monsterGroup.Count - 1].unitObj = tempMonster;
            monsterGroup[monsterGroup.Count - 1].DataSetting(false, dungeonData.dungeonMonsterBox[tempint].number);
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitMelee>();
            tempUnitInfo = monsterGroup[monsterGroup.Count - 1].GetComponent<UnitInfo>();
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitAI>();
            tempUnitInfo.changeUnitNumber = monsterGroup[monsterGroup.Count - 1].number;
            tempUnitInfo.changePartyNumber = monsterGroup.Count - 1;
            monsterGroup[monsterGroup.Count - 1].isLive = true;
            monsterGroup[monsterGroup.Count - 1].gameObject.AddComponent<UnitSkill>();
        }
    }

    void MonsterBossCreate()
    {
        DieCount_Enemy = 0;
        GameObject tempMonster;
        UnitInfo tempUnitInfo;
        int tempMonsterGroupindex, tempbossNumber;
        if (roundDGP % 10 == 5)
        {
            tempbossNumber = 1;
            tempMonster = Instantiate(dungeonData.dungeonMonsterBox[tempbossNumber].charObject);
            SceneManager.MoveGameObjectToScene(tempMonster, SceneManager.GetSceneByName(SceneLoad.currentlyScene));
            monsterGroup.Add(tempMonster.AddComponent<UnitStateData>());
            tempMonsterGroupindex = monsterGroup.Count - 1;
            monsterGroup[tempMonsterGroupindex].unitObj = tempMonster;
            monsterGroup[tempMonsterGroupindex].DataSetting(false, dungeonData.dungeonMonsterBox[tempbossNumber].number);
            monsterGroup[tempMonsterGroupindex].gameObject.AddComponent<UnitMelee>();
            monsterGroup[tempMonsterGroupindex].gameObject.AddComponent<UnitAI>();
            tempUnitInfo = monsterGroup[tempMonsterGroupindex].GetComponent<UnitInfo>();
            tempUnitInfo.changeUnitNumber = monsterGroup[tempMonsterGroupindex].number;
            tempUnitInfo.changePartyNumber = tempMonsterGroupindex;
            monsterGroup[tempMonsterGroupindex].transform.position = monsterStagePoint[1].position;
            monsterGroup[tempMonsterGroupindex].transform.rotation = monsterStagePoint[1].rotation;
            monsterGroup[tempMonsterGroupindex].isLive = true;
            monsterGroup[tempMonsterGroupindex].gameObject.AddComponent<UnitSkill>();
        }
        else if (roundDGP % 10 == 0)
        {
            tempbossNumber = 0;
            tempMonster = Instantiate(dungeonData.dungeonMonsterBox[tempbossNumber].charObject);
            SceneManager.MoveGameObjectToScene(tempMonster, SceneManager.GetSceneByName(SceneLoad.currentlyScene));
            monsterGroup.Add(tempMonster.AddComponent<UnitStateData>());
            tempMonsterGroupindex = monsterGroup.Count - 1;
            monsterGroup[tempMonsterGroupindex].unitObj = tempMonster;
            monsterGroup[tempMonsterGroupindex].DataSetting(false, dungeonData.dungeonMonsterBox[tempbossNumber].number);
            monsterGroup[tempMonsterGroupindex].gameObject.AddComponent<UnitMelee>();
            monsterGroup[tempMonsterGroupindex].gameObject.AddComponent<UnitAI>();
            tempUnitInfo = monsterGroup[tempMonsterGroupindex].GetComponent<UnitInfo>();
            tempUnitInfo.changeUnitNumber = monsterGroup[tempMonsterGroupindex].number;
            tempUnitInfo.changePartyNumber = tempMonsterGroupindex;
            monsterGroup[tempMonsterGroupindex].transform.position = monsterStagePoint[1].position;
            monsterGroup[tempMonsterGroupindex].transform.rotation = monsterStagePoint[1].rotation;
            monsterGroup[tempMonsterGroupindex].isLive = true;
            monsterGroup[tempMonsterGroupindex].gameObject.AddComponent<UnitSkill>();

        }
    }

        /// <summary>
        /// 게임 시작후, 휴식 타이밍에 덱 셔플 기능
        /// </summary>
        public void DeckShuffle()
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

    public void HandReset()
    {
        switch (GameManager.instance.data.preset)    // 프리셋 저장 수정 하면서 조금 고쳤습니다.
        {
            case 1:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[0].preset);
                break;
            case 2:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[1].preset);
                break;
            case 3:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[2].preset);
                break;
            case 4:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[3].preset);
                break;
            case 5:
                useDeckDGP.AddRange(GameManager.instance.cardPreset[4].preset);
                break;
        }
        useDeckDGP.RemoveAll(x => x == 0);

        DeckShuffle();
        handCard.Clear();

        HandRefill();
    }

    public void HandRefill()
    {
        CardEvent CasheCardEvent;
        for (int i = handCard.Count; i < 3; i++)
        {
            if (useDeckDGP.Count != 0)
            {
                CardDataBase.Data card = new CardDataBase.Data(useDeckDGP[0]);
                handCard.Add(card);
                if (!handSlot[0])
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[0].transform);
                    CasheCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                    CasheCardEvent.card_handnumber = 0;
                    handSlot[0] = true;
                    CasheCardEvent.cost = card.cost;
                }
                else if (!handSlot[1])
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[1].transform);
                    CasheCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                    CasheCardEvent.card_handnumber = 1;
                    handSlot[1] = true;
                    CasheCardEvent.cost = card.cost;
                }
                else
                {
                    card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[2].transform);
                    CasheCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                    CasheCardEvent.card_handnumber = 2;
                    handSlot[2] = true;
                    CasheCardEvent.cost = card.cost;
                }
                CasheCardEvent.card_number = card.number;
                useDeckDGP.RemoveAt(0);
                remainingCardDGP++;
            }
            else
            {
                //카드 없음
                return;
            }
        }
        HandUIReset();
    }

    public void HandDraw()
    {
        if (useDeckDGP.Count != 0)
        {
            CardDataBase.Data card = new CardDataBase.Data(useDeckDGP[0] + 1);
            CardEvent tempCardEvent;
            handCard.Add(card);
            if (handSlot[0])
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[0].transform);
                tempCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                tempCardEvent.card_handnumber = 0;
                handSlot[0] = true;
                tempCardEvent.cost = card.cost;
            }
            else if (handSlot[1])
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[1].transform);
                tempCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                tempCardEvent.card_handnumber = 1;
                handSlot[1] = true;
                tempCardEvent.cost = card.cost;
            }
            else
            {
                card.Icon = Instantiate(Resources.Load<Image>("Card/Card_" + card.number), DungeonCtrl.cardSlot[2].transform);
                tempCardEvent = card.Icon.gameObject.AddComponent<CardEvent>();
                tempCardEvent.card_handnumber = 2;
                handSlot[2] = true;
                tempCardEvent.cost = card.cost;
            }
            tempCardEvent.card_number = card.number;
            useDeckDGP.RemoveAt(0);
            remainingCardDGP++;
        }
        else
        {
            //카드 없음
            return;
        }
        HandUIReset();
    }

    #endregion 
    #region 던전 타이머 기능 // 주석기능 미흡
    /// <summary>
    /// 타이머 시작 기능
    /// </summary>
    public void DGTimerStart()
    {
        costDGP = 3;
        progressTimeDGP = 0;
        timerOnDGP = true;
        timeLevelDGP = 0;
        foreach (var item in DungeonCtrl.gameTimerBG)
        {
            item.fillAmount = 1;
        }
        //DungeonCtrl.gameTimerBG[0].fillAmount = 1;
        //DungeonCtrl.gameTimerBG[1].fillAmount = 1;
        StartCoroutine(Timer);
    }
    /// <summary>
    /// 타이머 작동 기능
    /// 값이 변동되면 자동으로 텍스트가 반영됨
    /// </summary>
    /// <returns></returns>
    public IEnumerator DGTimer()
    {
        yield return delay_01;
        float cycleTime = 0f;
        while (timerOnDGP)
        {
            cycleTime += 0.1f;
            progressTimeDGP += 0.1f;
            if (timeLevelDGP != 3)
            {
                DGTimerUIReset();
            }
            if (cycleTime >= 20)
            {
                cycleTime = 0f;
                HandRefill();
                if (costDGP <= 7) costDGP += 3;
                else costDGP = 10;
                switch (timeLevelDGP)
                {
                    case 0:
                        timeLevelDGP = 1;
                        DungeonCtrl.gameTimerBG[0].fillAmount = 0;
                        break;
                    case 1:
                        timeLevelDGP = 2;
                        break;
                    default:
                        timeLevelDGP = 3;
                        break;
                }
            }
            yield return delay_01;
        }
    }
    /// <summary>
    /// 외부에서 타이머 종료시 사용
    /// </summary>
    public void DGTimerEnd()
    {
        StopCoroutine(Timer);
        timerOnDGP = false;
    }
    /// <summary>
    /// <b>타이머의 회전하는 UI 확인</b>
    ///
    /// </summary>
    public void DGTimerUIReset()
    {
        DungeonCtrl.gameTimerBG[timeLevelDGP].fillAmount = (20 - (progressTimeDGP % 20)) * 0.05f;
    }
    #endregion
    #region 던전 종료
    /// <summary>
    /// 던전 종료시 작동 
    /// <br><b>시점 : </b></br>결산창에서 나가기버튼 클릭시 실행되는 함수
    /// <br></br>게임이 종료됬을시 던전 셋팅값을 되돌리고 던전내에서 변동된 값을
    /// 게임매니저와 동기화시킴 
    /// <br></br>
    /// <br><b>구조 : </b></br> 게임매니저가 가지고있는 인스턴스에 접근하여
    /// DungeonOS가 가지고있는 일부값을 넣어줌. 
    /// </summary>
    void DungeonEnd()
    {
        //DungeonCtrl.dungeonUI.SetActive(false);
        //GameManager.instance.dungeonOS = null;
        //// 데이터 전달 
        //// 세이브 1회 실행

        //for (int i = 0; i < 4; i++)
        //{
        //    if (GameManager.instance.partySlot[i] != null)
        //    {
        //        //GameManager.instance.partySlot[i].exp = partyUnit[i].exp;
        //    }
        //}

        //if (rewardCardBox.Count != 0)
        //{
        //    foreach (var item in rewardCardBox)
        //    {
        //        if (GameManager.instance.currentCardList[item] == null)
        //        {
        //            GameManager.instance.currentCardList.Add(item, new CardDataBase.Data(item));
        //        }
        //        else
        //        {
        //            GameManager.instance.currentCardList[item].cardCount++;
        //        }
        //    }
        //}
        //if (rewardRelicBox.Count != 0)
        //{
        //    foreach (var item in rewardRelicBox)
        //    {
        //        if (GameManager.instance.currentRelicList[item] == null)
        //        {
        //            GameManager.instance.currentRelicList.Add(item, new RelicData.Data(item));
        //        }
        //        else
        //        {
        //            //GameManager.instance.currentRelicList[item].overlapValueA += accrueGoldDGP;
        //            //GameManager.instance.currentRelicList[item].overlapValueB += accrueSoulDGP;
        //        }
        //    }
        //}
        //if (rewardCardBox.Count != 0)
        //{
        //    foreach (var item in rewardHeroBox)
        //    {
        //        if (GameManager.instance.currentHeroList[item] == null)
        //        {
        //            GameManager.instance.currentHeroList.Add(item, new CharacterDatabase.Data(item));
        //        }
        //        else
        //        {
        //            GameManager.instance.currentHeroList[item].overlapValueA += accrueGoldDGP;
        //            GameManager.instance.currentHeroList[item].overlapValueB += accrueSoulDGP;
        //        }
        //    }
        //}
        //GameManager.instance.data.souls += accrueSoulDGP;
        //GameManager.instance.data.golds += accrueGoldDGP;
    }
    #endregion
    #region 게임 에러 기록
    /// <summary>
    /// 던전내에서 발생한 문제를 추적 관리하기위한 리스트 목록
    /// <br><b>구조</b> : 에러가 발생한 지점에서 텍스트를 입력받아서
    /// 리스트로 에디터상에서도 확인할수 있게 해둠.</br> 
    /// <br></br>[errorList] : 스트링으로 넘겨받은 에러문자들을 누적하는곳
    /// </summary>
    /// <param name="str"></param>
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
