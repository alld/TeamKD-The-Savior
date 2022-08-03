using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOS : MonoBehaviour
{

    #region 환경 변수
    [Header("환경 변수")]
    //private PlayUIManager PUIManager;
    private GameObject DGUI;
    private GameObject DGtimerArrow;
    private GameObject[] DGtimerlevel;
    #endregion

    #region 던전 기본 데이터
    //던전 관련된 변수 : DG

    /// <summary>
    /// 던전이 가지고있는 모든 스테이지 그룹
    /// </summary>
    [Header("던전정보")]
    public GameObject[] DG_stageGroup;
    /// <summary>
    /// 현재 던전이 사용중인 스테이지
    /// </summary>
    public GameObject DG_slotStage;
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
    public int[] DG_roundInfo;
    /// <summary>
    /// 현재 라운드에 생존해있는 몬스터 그룹
    /// </summary>
    public List<int> DG_monsterGroup;
    /// <summary>
    /// 게임 분기 확인 스테이지가 순서대로 들어있기때문에, 게임분기 컷팅시키는 변수
    /// </summary>
    public int DGGame_checkCount;
    
    //던전안에서 플레이어관련된 변수 : DGP
    /// <summary>
    /// 현재 라운드 
    /// </summary>
    public int DGP_round;
    /// <summary>
    /// 현재 까지 얻은 누적골드
    /// </summary>
    public int DGP_accrueGold;
    /// <summary>
    /// 현재까지 얻은 누적 소울(개별로 얻은 소울 제외)
    /// </summary>
    public int DGP_accrueSoul;
    /// <summary>
    /// 현재 보유한 코스트 (최대치 10)
    /// </summary>
    public int DGP_cost;
    /// <summary>
    /// 라운드 동안 지속된 시간 
    /// </summary>
    public float DGP_progressTime;
    /// <summary>
    /// 시간 흐름에 따른 시간단계를 표시 
    /// <br>0. 초반</br>
    /// <br>1. 중반</br>
    /// <br>2. 후반</br>
    /// </summary>
    public bool DGP_timerOn;
    public int DGP_timeLevel;
    /// <summary>
    /// 남은 카드 (덱잔량)
    /// </summary>
    public int DGP_remainingCard;
    /// <summary>
    /// 파티에 참여한 유닛 정보
    /// </summary>
    public int[] DGP_unitGroup;
    /// <summary>
    /// 개별적으로 얻은 소울 정보
    /// </summary>
    public int[] DGP_eaGetSoul;
    /// <summary>
    /// 파티 그룹의 사망 여부 판단
    /// <br>1. 살아있음</br>
    /// <br>2. 죽었음</br>
    /// <br>3. 특수상태(빈사등..)</br>
    /// </summary>
    public int[] DGP_eaIsDie;
    /// <summary>
    /// [통계데이터] 개별 누적 피격량
    /// </summary>
    public float[] DGP_eaDamaged;
    /// <summary>
    /// [통계데이터] 개별 누적 데미지량
    /// </summary>
    public float[] DGP_eaDamage;
    /// <summary>
    /// [통계데이터] 개별 누적 킬수
    /// </summary>
    public float[] DGP_eaKillCount;
    #endregion

    #region 전달받은 GameManager의 Data
    //캐릭터 정보 
    public CharacterDatabase.CH_Info[] DGP_Party = 
        { 
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0),
            new CharacterDatabase.CH_Info(0)
        };
    //덱정보
    public List<int> DGP_useDeck;
    //유물 정보
    //초회 보상 유무 진행상황변수와는 별개
    bool ClearkCheck;
    #endregion
    //카드기능 제외... 카드 데이터베이스를 만들어야함. 
    void Start()
    {
        #region 캐시처리 //합칠때 다시한번 설정해줘야함..
        //PUIManager = GameObject.Find("PUIManaer").GetComponent<PlayUIManager>();
        //DGUI = PUIManager.DungeonUI;
        //DGtimerArrow = PUIManager.DungeonTimerArrow;
        //DGtimerlevel = PUIManager.DungeonTimerColor;
        #endregion
        //게임용 UI 활성화 
        DGUI.SetActive(true);
        GameSetting();
    }


    #region 던전 셋팅
    void GameSetting()
    {
        DeckShuffle();
        ////스테이지 설정 한번 들어가야함. 
        StageReset(DGGame_checkCount);
    }
    void StageReset(int stageNum)
    {
        
    }

    void DeckShuffle()
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < DGP_useDeck.Count; i++)
        {
            int tempA = Random.Range(0, DGP_useDeck.Count);
            tempList.Add(DGP_useDeck[tempA]);
            DGP_useDeck.RemoveAt(tempA);
        }
        DGP_useDeck = tempList;
    }
    #endregion
    #region 던전 타이머 기능
    public void DGTimerStart() 
    {
        DGP_progressTime = 0;
        DGP_timerOn = true;
        DGP_timeLevel = 0;
        StartCoroutine(DGTimer());
    }

    public IEnumerator DGTimer()
    {
        while (DGP_timerOn)
        {
            DGP_progressTime += Time.deltaTime;
            if((DGP_progressTime % 2) >= 1)
            {
                DGTimerUIReset();
            }
            if(DGP_progressTime >= 20)
            {
                DGP_timeLevel++;
                if (DGP_progressTime >= 100)
                {
                    DGTimerEnd();
                }
            }
        }
        yield return null;
    }

    public void DGTimerEnd()
    {
        DGP_timerOn = false;
    }
    public void DGTimerUIReset()
    {
        
    }
    #endregion
}
