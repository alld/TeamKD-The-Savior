using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterDatabase : MonoBehaviour
{
    /// <summary>
    /// 캐릭터의 기본 원형값을 가지고있는 데이터 베이스 
    /// <br>InfoCh를 통해 인스턴스를 만들어서 사용해야함. </br>
    /// <br>InfoCH(int) : int == 데이터베이스상 캐릭터 고유넘버 </br>
    /// </summary>
    public static CharacterDatabase instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

    public class InfoCharacter
    {
        #region 캐릭터 기본 설정값
        public int number;
        /// <summary>
        /// 캐릭터 기본 아이콘
        /// </summary>
        public Image icon;
        /// <summary>
        /// 캐릭터 기본 이름
        /// </summary>
        public string name;
        /// <summary>
        /// 캐릭터 기본 체력
        /// </summary>
        public float hP;
        /// <summary>
        /// 캐릭터 기본 데미지
        /// </summary>
        public float damage;
        /// <summary>
        /// 프리팹에 있는 캐릭터 오브젝트, 모델
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// 캐릭터 기본 공격속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float attackSpeed;
        /// <summary>
        /// 캐릭터 기본 이동속도
        /// <br>1.0f = 100%</br>
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// 캐릭터 기본 방어력
        /// </summary>
        public float defense;
        /// <summary>
        /// 캐릭터 기본 공격타입
        /// <br>1. 근거리</br>
        /// <br>2. 원거리</br>
        /// <br>3. 탱커</br>
        /// </summary>
        public int attackType;
        /// <summary>
        /// 캐릭터 기본 공격 범위
        /// </summary>
        public float attackRange;
        /// <summary>
        /// 캐릭터 기본 속성
        /// <br>0. 무 속성</br>
        /// <br>1. 불 속성</br>
        /// <br>2. 물 속성</br>
        /// <br>3. 풀 속성</br>
        /// </summary>
        public int propertie;
        /// <summary>
        /// 캐릭터 인식 범위 : 공격 대상을 인식하는 범위
        /// </summary>
        public float priRange;
        /// <summary>
        /// 캐릭터 우선도 : 공격 인식 우선수치
        /// </summary>
        public int priorities;
        /// <summary>
        /// 캐릭터 기본 자리 우선도 : 자리 배치
        /// </summary>
        public int positionPer;
        /// <summary>
        /// [saveData] 캐릭터 클래스
        /// </summary>
        public int unitClass;
        /// <summary>
        /// [saveData] 캐릭터 기본 등급 
        /// <br>1~3 단계로 구성</br>
        /// </summary>
        public int level;
        /// <summary>
        /// [saveData] 캐릭터 소지 소울 
        /// </summary>
        public int soul;
        /// <summary>
        /// 캐릭터 파티 참여 여부 
        /// <para>세이브 여부 검토 </para>
        /// </summary>
        public bool partySet;
        /// <summary>
        /// 캐릭터 해금 여부 
        /// <br> 업적데이트 기준으로 설정할것이라, 해금여부 저장할필요없음.</br>
        /// </summary>
        public bool islock;
        /// <summary>
        /// 캐릭터가 가진 기본 스킬 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int basicskill;
        /// <summary>
        /// 캐릭터가 가진 궁극기 인덱스 넘버
        /// <br> 0 은 스킬이 없는것 </br>
        /// </summary>
        public int speialskill;
        #endregion


        public InfoCharacter()
        {
            //빈 슬롯
            number = 0;
            icon = null;
            name = null;
            hP = 0;
            damage = 0;
            gameObject = null;
            attackSpeed = 0;
            moveSpeed = 0;
            defense = 0;
            attackType = 0;
            attackRange = 0;
            propertie = 0;
            priRange = 0;
            priorities = 0;
            positionPer = 0;
            unitClass = 0;
            level = 1;
            soul = 0;
            partySet = false;
            islock = false;
            basicskill = 0;
            speialskill = 0;
        }

        /// <summary>
        /// 캐릭터의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
        /// <br>(int = num) : 캐릭터 고유의 넘버링값</br>
        /// </summary>
        /// <param name="num"></param>
        public InfoCharacter(int num)
        {
            switch (num)
            {
                case 1: // 머선머선 캐릭터 아직 미정
                    number = num;
                    icon = null; // 이미지 설정 검토
                    name = "머선머선 캐릭터"; // 던전정보와 마찬가지로 텍스트 구동방식 검토
                    hP = 100;
                    damage = 10.0f;
                    gameObject = Resources.Load<GameObject>("Unit/TestUnit");
                    attackSpeed = 1.0f;
                    moveSpeed = 1.0f;
                    defense = 10.0f;
                    attackType = 1;
                    attackRange = 10.0f;
                    propertie = 0;
                    priRange = 10.0f;
                    priorities = 20;
                    positionPer = 30;
                    unitClass = 0;
                    level = 1;
                    soul = 0;
                    partySet = false;
                    islock = false;
                    basicskill = 0;
                    speialskill = 0;
                    break;
                case 2: // snrn
                    number = num;
                    icon = null;
                    name = null;
                    hP = 0;
                    damage = 0;
                    gameObject = null;
                    attackSpeed = 0;
                    moveSpeed = 0;
                    defense = 0;
                    attackType = 0;
                    attackRange = 0;
                    propertie = 0;
                    priRange = 0;
                    priorities = 0;
                    positionPer = 0;
                    unitClass = 0;
                    level = 1;
                    soul = 0;
                    partySet = false;
                    islock = false;
                    basicskill = 0;
                    speialskill = 0;
                    break;
                case 3:
                    number = num;
                    icon = null;
                    name = null;
                    hP = 0;
                    damage = 0;
                    gameObject = null;
                    attackSpeed = 0;
                    moveSpeed = 0;
                    defense = 0;
                    attackType = 0;
                    attackRange = 0;
                    propertie = 0;
                    priRange = 0;
                    priorities = 0;
                    positionPer = 0;
                    unitClass = 0;
                    level = 1;
                    soul = 0;
                    partySet = false;
                    islock = false;
                    basicskill = 0;
                    speialskill = 0;
                    break;
                case 4:
                    number = num;
                    icon = null;
                    name = null;
                    hP = 0;
                    damage = 0;
                    gameObject = null;
                    attackSpeed = 0;
                    moveSpeed = 0;
                    defense = 0;
                    attackType = 0;
                    attackRange = 0;
                    propertie = 0;
                    priRange = 0;
                    priorities = 0;
                    positionPer = 0;
                    unitClass = 0;
                    level = 1;
                    soul = 0;
                    partySet = false;
                    islock = false;
                    basicskill = 0;
                    speialskill = 0;
                    break;
                case 5:
                    number = num;
                    icon = null;
                    name = null;
                    hP = 0;
                    damage = 0;
                    gameObject = null;
                    attackSpeed = 0;
                    moveSpeed = 0;
                    defense = 0;
                    attackType = 0;
                    attackRange = 0;
                    propertie = 0;
                    priRange = 0;
                    priorities = 0;
                    positionPer = 0;
                    unitClass = 0;
                    level = 1;
                    soul = 0;
                    partySet = false;
                    islock = false;
                    basicskill = 0;
                    speialskill = 0;
                    break;
                case 6:
                    number = num;
                    icon = null;
                    name = null;
                    hP = 0;
                    damage = 0;
                    gameObject = null;
                    attackSpeed = 0;
                    moveSpeed = 0;
                    defense = 0;
                    attackType = 0;
                    attackRange = 0;
                    propertie = 0;
                    priRange = 0;
                    priorities = 0;
                    positionPer = 0;
                    unitClass = 0;
                    level = 1;
                    soul = 0;
                    partySet = false;
                    islock = false;
                    basicskill = 0;
                    speialskill = 0;
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
