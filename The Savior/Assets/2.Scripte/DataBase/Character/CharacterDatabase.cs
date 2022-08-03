using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterDatabase : MonoBehaviour
{
    /// <summary>
    /// 캐릭터의 기본 원형값을 가지고있는 데이터 베이스 
    /// <br>Ch_Info를 통해 인스턴스를 만들어서 사용해야함. </br>
    /// <br>CH_Info(int) : int == 데이터베이스상 캐릭터 고유넘버 </br>
    /// </summary>
    public static CharacterDatabase instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

    public class CH_Info
    {
        #region 캐릭터 기본 설정값
        /// <summary>
        /// 캐릭터 기본 아이콘
        /// </summary>
        public Image iconCH;
        /// <summary>
        /// 캐릭터 기본 이름
        /// </summary>
        public string nameCH;
        /// <summary>
        /// 캐릭터 기본 체력
        /// </summary>
        public float hPCH;
        /// <summary>
        /// 캐릭터 기본 데미지
        /// </summary>
        public float damageCH;
        /// <summary>
        /// 프리팹에 있는 캐릭터 오브젝트, 모델
        /// </summary>
        public GameObject objectCH;
        /// <summary>
        /// 캐릭터 기본 공격속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeedCH;
        /// <summary>
        /// 캐릭터 기본 이동속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeedCH;
        /// <summary>
        /// 캐릭터 기본 방어력
        /// </summary>
        public float defenseCH;
        /// <summary>
        /// 캐릭터 기본 공격타입
        /// <br>1. 근거리</br>
        /// <br>2. 원거리</br>
        /// <br>3. 탱커</br>
        /// </summary>
        public int attackTypeCH;
        /// <summary>
        /// 캐릭터 기본 공격 범위
        /// </summary>
        public float attackRangeCH;
        /// <summary>
        /// 캐릭터 기본 속성
        /// <br>0. 무 속성</br>
        /// <br>1. 불 속성</br>
        /// <br>2. 물 속성</br>
        /// <br>3. 풀 속성</br>
        /// </summary>
        public int propertieCH;
        /// <summary>
        /// 캐릭터 인식 범위 : 공격 대상을 인식하는 범위
        /// </summary>
        public float priRangeCH;
        /// <summary>
        /// 캐릭터 우선도 : 공격 인식 우선수치
        /// </summary>
        public int prioritiesCH;
        /// <summary>
        /// 캐릭터 기본 자리 우선도 : 자리 배치
        /// </summary>
        public int positionPerCH;
        /// <summary>
        /// [saveData] 캐릭터 클래스
        /// </summary>
        public int classCH;
        /// <summary>
        /// [saveData] 캐릭터 기본 등급 
        /// <br>1~3 단계로 구성</br>
        /// </summary>
        public int levelCH;
        /// <summary>
        /// [saveData] 캐릭터 소지 소울 
        /// </summary>
        public int soulCH;
        /// <summary>
        /// 캐릭터 파티 참여 여부 
        /// <para>세이브 여부 검토 </para>
        /// </summary>
        public bool partySetCH;
        /// <summary>
        /// 캐릭터 해금 여부 
        /// <br> 업적데이트 기준으로 설정할것이라, 해금여부 저장할필요없음.</br>
        /// </summary>
        public bool islockCH;
        /// <summary>
        /// 캐릭터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicskillCH;
        /// <summary>
        /// 캐릭터가 가진 궁극기 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int speialskillCH;
        #endregion




        /// <summary>
        /// 캐릭터의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
        /// <br>(int = num) : 캐릭터 고유의 넘버링값</br>
        /// </summary>
        /// <param name="num"></param>
        public CH_Info(int num)
        {
            switch (num)
            {
                case 1: // 머선머선 캐릭터 아직 미정
                    iconCH = null; // 이미지 설정 검토
                    nameCH = "머선머선 캐릭터"; // 던전정보와 마찬가지로 텍스트 구동방식 검토
                    hPCH = 100;
                    damageCH = 10.0f;
                    objectCH = Resources.Load<GameObject>("Unit/TestUnit");
                    attackSpeedCH = 1.0f;
                    moveSpeedCH = 1.0f;
                    defenseCH = 10.0f;
                    attackTypeCH = 1;
                    attackRangeCH = 10.0f;
                    propertieCH = 0;
                    priRangeCH = 10.0f;
                    prioritiesCH = 20;
                    positionPerCH = 30;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 2: // snrn
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 3:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 4:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 5:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                case 6:
                    iconCH = null;
                    nameCH = null;
                    hPCH = 0;
                    damageCH = 0;
                    objectCH = null;
                    attackSpeedCH = 0;
                    moveSpeedCH = 0;
                    defenseCH = 0;
                    attackTypeCH = 0;
                    attackRangeCH = 0;
                    propertieCH = 0;
                    priRangeCH = 0;
                    prioritiesCH = 0;
                    positionPerCH = 0;
                    classCH = 0;
                    levelCH = 1;
                    soulCH = 0;
                    partySetCH = false;
                    islockCH = false;
                    basicskillCH = 0;
                    speialskillCH = 0;
                    break;
                default:
                    break;
            }
}
    }

    private void Awake()
    {
        instance = this;
        //foreach (var item in ImagefulSet)
        //{
        //    Imageful.Add(item);
        //}
    }
}
