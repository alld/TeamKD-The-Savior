using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DungeonOS : MonoBehaviour
{
    public static DungeonOS instance = null;
    public GameObject DungeonOSObj = null;
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

    List<MonsterDatabase> monsterBox = new List<MonsterDatabase>();
    List<int> rewardHeroBox = new List<int>();
    List<int> rewardCardBox = new List<int>();
    List<int> rewardRelicBox = new List<int>();

    List<CharacterDatabase> stageSlotPlayerBottom; 
    List<CharacterDatabase> stageSlotPlayerTop;
    List<CharacterDatabase> stageSlotPlayerMid; 

    List<MonsterDatabase> stageSlotMonsterBottom;
    List<MonsterDatabase> stageSlotMonsterTop;
    List<MonsterDatabase> stageSlotMonsterMid;


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
    /// 현재 던전이 사용중인 스테이지
    /// </summary>
    public GameObject slotStageDG;

    public GameObject playerStagePointGroup;
    public GameObject monsterStagePointGroup;
    private Transform[] playerStagePoint;
    private Transform[] monsterStagePoint;
    public int[] monsterBoxMin; // 설정해줘야함
    public int[] monsterBoxMax; // 설정해줘야함
    public int[] monsterBoxCount; // 설정해줘야함
    
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
    public List<MonsterDatabase> monsterGroup = new List<MonsterDatabase>();
    /// <summary>
    /// 플레이어 유닛 그룹
    /// </summary>
    public List<CharacterDatabase> characterGroup = new List<CharacterDatabase>(); // 오브젝트로 설정
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
    public bool isRoundPlaying
    {
        get { return isRoundPlaying; }
        set
        {
            if (value)
            {
                dele_RoundStart();
            }
            else
            {
                dele_RoundEnd();
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
    public int costDGP;
    /// <summary>
    /// 라운드 동안 지속된 시간 
    /// </summary>
    public float progressTimeDGP 
    {
        get { return progressTimeDGP; }
        set 
        { 
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
    public int timeLevelDGP
    {
        get { return timeLevelDGP; }
        set
        {
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

    public List<CardDataBase> handCard = new List<CardDataBase>();
    #endregion

    #region 던전 가중치 데이터
    // 가중치 횟수 체크 변수
    public int addCount;
    public class WeightUnit
    {
        // 아군
        /// <summary>
        /// 가중치 :: 추가 공격력
        /// </summary>
        public float Add_damage;
        /// <summary>
        /// 가중치 :: 최종 추가 공격력
        /// </summary>
        public float Add_fianlDamage = 1;
        /// <summary>
        /// 가중치 :: 피해량 감소량
        /// </summary>
        public float Add_dropDamage = 1;
        /// <summary>
        /// 가중치(추가능력) :: 보호막 수치
        /// </summary>
        public float Current_protect;
        /// <summary>
        /// 가중치(추가능력) :: 보호막 최대 수치
        /// </summary>
        public float Current_protectMax;
        /// <summary>
        /// 가중치 :: 공격 속도
        /// </summary>
        public float Add_attackSpeed;
        /// <summary>
        /// 가중치 :: 이동 속도
        /// </summary>
        public float Add_moveSpeed;
        /// <summary>
        /// 가중치 :: 방어력
        /// </summary>
        public float Add_defense;
        /// <summary>
        /// 가중치 :: 공격 범위(사거리)
        /// </summary>
        public float Add_attackRange;
        /// <summary>
        /// 가중치 :: 속성 변경 여부 (bool)
        /// </summary>
        public bool Add_attributeCheck;
        /// <summary>
        /// 가중치 :: 변경된 속성값 
        /// </summary>
        public int Add_attribute;
        /// <summary>
        /// 가중치 :: 속성 추가 데미지
        /// </summary>
        public float[] Add_attributeVlaue = {1, 1,1,1 };
        /// <summary>
        /// 가중치 :: 공격 인식 범위
        /// </summary>
        public float Add_priRange;
        /// <summary>
        /// 가중치 :: 공격 우선도
        /// </summary>
        public int Add_priorities;
        /// <summary>
        /// 가중치 :: 스킬 쿨다운
        /// </summary>
        public float Add_skilcoldown = 1;
        /// <summary>
        /// 가중치 :: 무적 유무
        /// </summary>
        public bool isinvincible;
        public List<BuffDataBase> Current_buff = new List<BuffDataBase>();
    }

    public class WeightEnemy : WeightUnit
    {
        // 적
        public int Add_rewardGold;
        public int Add_rewardSoul;
    }
    public WeightUnit weightAlly = new WeightUnit();
    public WeightUnit[] weightAllyUnit = new WeightUnit[3];
    public WeightEnemy weightEnemy = new WeightEnemy();
    public List<WeightEnemy> weightEnemyGroup = new List<WeightEnemy>();
    #endregion

    #region 전달받은 GameManager의 Data
    //캐릭터 정보 
    //public CharacterDatabase.InfoCharacter[] partyUnit = 
    //    { 
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[0].number),
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[1].number),
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[2].number),
    //        new CharacterDatabase.InfoCharacter(GameManager.instance.partySlot[3].number)
    //    };
    public List<CharacterDatabase> partyUnit = new List<CharacterDatabase>();
    public List<RelicData> equipRelic = new List<RelicData>();
    //덱정보
    public List<int> useDeckDGP = new List<int>();
    //유물 정보
    //초회 보상 유무 진행상황변수와는 별개
    bool ClearkCheck;
    #endregion
    private TextAsset jsonData;
    private string jsonText;
    private JSONNode jsonCH;

    private void Awake()
    {
        instance = this; 
        jsonData = Resources.Load<TextAsset>("CharacterData");
        jsonText = jsonData.text;
        jsonCH = JSON.Parse(jsonText);
    }
    //카드기능 제외... 카드 데이터베이스를 만들어야함. 
    void Start()
    {
        #region 캐시처리 //합칠때 다시한번 설정해줘야함..
        DungeonCtrl = DungeonController.instance;
        DungeonDatabase.InfoDungeon infoDungeon = new DungeonDatabase.InfoDungeon(dungeonNumber);
        monsterBox = infoDungeon.dungeonMonsterBox;
        playerStagePoint = playerStagePointGroup.GetComponentsInChildren<Transform>();
        monsterStagePoint = monsterStagePointGroup.GetComponentsInChildren<Transform>();
        #endregion

        GameManager.instance.dungeonOS = this;
        GameManager.instance.dungeonPlayTime = 0;
        //게임용 UI 활성화 
        DungeonCtrl.dungeonUI.SetActive(true);
        GameSetting();
    }
    #region 던전 이벤트(기능) // 주석처리 미흡
    public void OnStateCheck()
    {
        dele_stateCheck();
        if (monsterGroup.Count <= 0)
        {
            OnRoundVictory();
        }
        else if (characterGroup.Count <= 0)
        {
            OnDungeonFailed();
        }
    }

    public void OnRoundVictory()
    {
        if (roundDGP == 10)
        {
            OnDungeonAllClear();
            return;
        }
        if (roundDGP % 10 == 5) OnRest();
        StageSelectButtonSet();
    }

    void OnDungeonAllClear()
    {
        //보상 UI 처리(결과창)
    }

    void OnDungeonFailed()
    {
        //보상 UI 처리(결과창)
    }

    void NextRound(int num)
    {
        StartCoroutine(FadeIn());
        if (++roundDGP % 10 == 5) StageReset(5);
        else if (roundDGP % 10 != 0)
        {
            if (roundInfoDG[roundDGP -1] == 6)
            {
                StageReset(num);
            }
            else NextRound(roundDGP);
        }
        else StageReset(10);
        HandRefill();
    }
    // 마우스 입력 버튼 아직 안했음
    void OnStageSelect(Vector2 clickPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(clickPoint);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("STAGEPOINT"))
            {
                int temp = hit.collider.GetComponent<PointInfo>().pointNumber;
                if(temp == 0)
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
    }

    void SelectResurrection(int partySlotN)
    {
        if(resurrectable)
        {
            if(partyUnit != null & !partyUnit[partySlotN].isLive)
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
        GameObject.Find("StageSelectGroup").SetActive(true);
        // 버튼 클릭하게해서 NextRound 실행시킴 
    }
    void HandUIReset()
    {
        int temp = roundDGP % 10;
        DungeonCtrl.gameRoundbarArrow.transform.SetParent(DungeonCtrl.gameRoundbarPoint[temp].transform);
        DungeonCtrl.gameRoundbarArrow.transform.position = Vector3.zero;
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
    #endregion
    #region 던전 셋팅 // 주석기능 미흡
    /// <summary>
    /// 게임이 시작하고 던전에서 기본값들을 셋팅할때 사용
    /// </summary>
    void GameSetting()
    {
        for (int i = 0; i < 4; i++) // 추후 데이터 처리 방식 변경에따라 맞쳐서 변경
        {
            partyUnit.Add(new CharacterDatabase(GameManager.instance.partySlot[i].number, jsonCH));
            partyUnit[i].isLive = true;
        }
        foreach (var item in GameManager.instance.data.equipRelic)
        {
            equipRelic.Add(new RelicData(item));
        }

        int temp = 0;
        foreach (var item in stagePrefabGroupDG)
        {
            stageGroupDG[temp++] = Instantiate(item);
        }
        HandReset();
        PlayerUnitCreate();
        roundDGP = 1;
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
        slotStageDG?.SetActive(false);
        slotStageDG = stageGroupDG[stageNum];
        slotStageDG.SetActive(true);
        Camera.main.transform.position = slotStageDG.GetComponentInChildren<Camera>().transform.position;
        Camera.main.transform.rotation = slotStageDG.GetComponentInChildren<Camera>().transform.rotation;
        DGTimerStart();
        PlayerUnitSetting();
        MonsterCreate();
        MonsterSetting();

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
                    if (stageSlotPlayerBottom.Count < 3)
                    {
                        stageSlotPlayerBottom.Add(item);
                    }
                    else
                    {
                        CharacterDatabase moveSlot = item;
                        CharacterDatabase tempSlot = item;
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
                        if(stageSlotPlayerMid.Count < 3)
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
                        CharacterDatabase moveSlot = item;
                        CharacterDatabase tempSlot = item;
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
                            if (stageSlotPlayerBottom.Count < 3)
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
                                if (stageSlotPlayerMid[i].positionPri < moveSlot.positionPri)
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
                                    if (stageSlotPlayerTop[i].positionPri < moveSlot.positionPri)
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
                        CharacterDatabase moveSlot = item;
                        CharacterDatabase tempSlot = item;
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
                        if (stageSlotPlayerMid.Count < 3)
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
            stageSlotPlayerBottom[i].gameObject.transform.position = playerStagePoint[i + 1].position;
        }
        for (int i = 0; i < stageSlotPlayerMid.Count; i++)
        {
            stageSlotPlayerMid[i].gameObject.transform.position = playerStagePoint[i + 4].position;
        }
        for (int i = 0; i < stageSlotPlayerTop.Count; i++)
        {
            stageSlotPlayerTop[i].gameObject.transform.position = playerStagePoint[i + 7].position;
        }
    }
    /// <summary>
    /// 플레이어 유닛 오브젝트화
    /// </summary>
    void PlayerUnitCreate()
    {
        foreach (var item in partyUnit)
        {
            item.charObject = Instantiate(item.gameObject);
            characterGroup.Add(item);
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
                        MonsterDatabase moveSlot = item;
                        MonsterDatabase tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 작은수치가 우선
                        for (int i = 0; i < stageSlotMonsterBottom.Count; i++)
                        {
                            if (stageSlotMonsterBottom[i].positionPer > moveSlot.positionPer)
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
                                if (stageSlotMonsterMid[i].positionPer > moveSlot.positionPer)
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
                        MonsterDatabase moveSlot = item;
                        MonsterDatabase tempSlot = item;
                        // 수치가 낮은 경우 
                        if (item.attackType >= 30)
                        {
                            for (int i = 0; i < stageSlotMonsterMid.Count; i++)
                            {
                                if (stageSlotMonsterMid[i].positionPer > moveSlot.positionPer)
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
                                    if (stageSlotMonsterBottom[i].positionPer > moveSlot.positionPer)
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
                                if (stageSlotMonsterMid[i].positionPer < moveSlot.positionPer)
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
                                    if (stageSlotMonsterTop[i].positionPer < moveSlot.positionPer)
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
                        MonsterDatabase moveSlot = item;
                        MonsterDatabase tempSlot = item;
                        //내부 비교 밀어내기식 자리배치 // 큰수치가 우선
                        for (int i = 0; i < stageSlotMonsterTop.Count; i++)
                        {
                            if (stageSlotMonsterTop[i].positionPer < moveSlot.positionPer)
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
                                if (stageSlotMonsterMid[i].positionPer < moveSlot.positionPer)
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
        }
        for (int i = 0; i < stageSlotMonsterMid.Count; i++)
        {
            stageSlotMonsterMid[i].gameObject.transform.position = monsterStagePoint[i + 6].position;
        }
        for (int i = 0; i < stageSlotMonsterTop.Count; i++)
        {
            stageSlotMonsterTop[i].gameObject.transform.position = monsterStagePoint[i + 10].position;
        }
    }


    /// <summary>
    /// 몬스터 유닛 오브젝트화
    /// </summary>
    void MonsterCreate()
    {
        for (int i = 0; i < monsterBoxCount[roundDGP]; i++)
        {
            int tempint = Random.Range(monsterBoxMin[roundDGP], monsterBoxMax[roundDGP]);
            monsterGroup.Add(new MonsterDatabase(monsterBox[tempint].number));
            monsterGroup[i].charObject = Instantiate(monsterGroup[i].gameObject);
        }
        if (roundDGP % 10 == 5)
        {
            monsterGroup.Add(new MonsterDatabase(monsterBox[1].number));
            monsterGroup[monsterBox.Count].charObject = Instantiate(monsterBox[1].gameObject);
            monsterGroup[monsterBox.Count].gameObject.transform.position = monsterStagePoint[1].position;
            monsterGroup[monsterBox.Count].gameObject.transform.rotation = monsterStagePoint[1].rotation;
        }
        else if (roundDGP % 10 == 0)
        {
            monsterGroup.Add(new MonsterDatabase(monsterBox[0].number));
            monsterGroup[monsterBox.Count].charObject = Instantiate(monsterBox[0].gameObject);
            monsterGroup[monsterBox.Count].gameObject.transform.position = monsterStagePoint[1].position;
            monsterGroup[monsterBox.Count].gameObject.transform.rotation = monsterStagePoint[1].rotation;
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
        DeckShuffle();
        handCard.Clear();
        useDeckDGP = GameManager.instance.currentDeck[GameManager.instance.data.presset];
        HandRefill();
    }

    public void HandRefill()
    {
        for (int i = useDeckDGP.Count; i < 3; i++)
        {
            if (useDeckDGP.Count != 0)
            {
                handCard.Add(new CardDataBase(useDeckDGP[0]));
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
            handCard.Add(new CardDataBase(useDeckDGP[0]));
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
                if (costDGP <= 7) costDGP += 3;
                else costDGP = 10;
                
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
        DungeonCtrl.dungeonUI.SetActive(false);
        GameManager.instance.dungeonOS = null;
        // 데이터 전달 
        // 세이브 1회 실행

        for (int i = 0; i < 4; i++)
        {
            if (GameManager.instance.partySlot[i] != null)
            {
                GameManager.instance.partySlot[i].exp = partyUnit[i].exp;
            }
        }

        if(rewardCardBox.Count != 0)
        {
            foreach (var item in rewardCardBox)
            {
                if (GameManager.instance.currentCardList[item] == null)
                {
                    GameManager.instance.currentCardList.Add(item, new CardDataBase(item));
                }
                else
                {
                    GameManager.instance.currentCardList[item].cardCount++;
                }
            }
        }
        if (rewardRelicBox.Count != 0)
        {
            foreach (var item in rewardRelicBox)
            {
                if (GameManager.instance.currentRelicList[item] == null)
                {
                    GameManager.instance.currentRelicList.Add(item, new RelicDataBase.InfoRelic(item));
                }
                else
                {
                    GameManager.instance.currentRelicList[item].overlapValueA += accrueGoldDGP;
                    GameManager.instance.currentRelicList[item].overlapValueB += accrueSoulDGP;
                }
            }
        }
        if (rewardCardBox.Count != 0)
        {
            foreach (var item in rewardHeroBox)
            {
                if (GameManager.instance.currentHeroList[item] == null)
                {
                    GameManager.instance.currentHeroList.Add(item, new CharacterDatabase(item, jsonCH));
                }
                else
                {
                    GameManager.instance.currentHeroList[item].overlapValueA += accrueGoldDGP;
                    GameManager.instance.currentHeroList[item].overlapValueB += accrueSoulDGP;
                }
            }
        }
        GameManager.instance.data.souls += accrueSoulDGP;
        GameManager.instance.data.golds += accrueGoldDGP;
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
