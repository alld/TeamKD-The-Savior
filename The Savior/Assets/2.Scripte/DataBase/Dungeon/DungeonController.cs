using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DungeonController : MonoBehaviour
{
    public static DungeonController instance = null; //싱글턴
    #region UI 환경변수
    public Image[] partySlotHPGauage; // 파티원 체력바
    public Image[] partySlotNomalSkillCooldown;// 일반스킬 쿨다운 표시
    public Image[] partySlotSpecialCooldown; // 궁극기 쿨다운 표시
    public Button[] partySlotActiveSkillButton; // 스킬사용버튼
    public GameObject[] partySlotDieImage; // 사망처리 UI
    public Image playerCostGauage; //실질 게이지
    public Image playerExpectationsGauage; // 기대치바
    public Image[] gameTimerBG; //타이머 배경
    public TMP_Text gameTimerText; //시계바늘
    public GameObject gameRoundbarArrow; //라운드 표시바늘
    public GameObject[] gameRoundbarPoint; //라운드 표시바 위치
    public GameObject[] gameRoundDisplayIcon; //각 라운드 아이콘
    public GameObject[] gameRoundDisplayPos; // 라운드 표시위치
    public GameObject dungeonUI; // 던전 UI
    public GameObject[] cardSlot;
    public Image fade;
    public GameObject fadeObj;
    #endregion

    private void Awake()
    {
        instance = this;
    }


}
