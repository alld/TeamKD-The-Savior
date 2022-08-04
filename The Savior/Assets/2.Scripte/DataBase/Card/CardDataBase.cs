using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDataBase : MonoBehaviour
{
    /// <summary>
    /// 카드의 기본 원형값을 가지고있는 데이터 베이스 
    /// <br>InfoCard를 통해 인스턴스를 만들어서 사용해야함. </br>
    /// <br>InfoCard(int) : int == 데이터베이스상 캐릭터 고유넘버 </br>
    /// </summary>
    public static CardDataBase instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

    public class InfoCard
    {
        #region 카드 기본 설정값
        /// <summary>
        /// 카드 데이터가 어떤데이터인지 구분하기위한 넘버
        /// </summary>
        public int number;
        /// <summary>
        /// 카드의 그림 이미지
        /// </summary>
        public Image Icon;
        /// <summary>
        /// 카드의 이름
        /// </summary>
        public string name;
        /// <summary>
        /// [saveData]카드 소지 갯수
        /// </summary>
        public int cardCount;
        /// <summary>
        /// 카드의 속성
        /// </summary>
        public int propertie;
        /// <summary>
        /// 카드의 효과 대분류
        /// </summary>
        public int effectSortA;
        /// <summary>
        /// 카드의 효과 소분류
        /// </summary>
        public int effectSortB;
        /// <summary>
        /// 카드의 설명 1
        /// </summary>
        public int cardString1;
        /// <summary>
        /// 카드의 설명 2
        /// </summary>
        public int cardString2;
        /// <summary>
        /// 카드 효과 int형 A
        /// </summary>
        public int effectValue_intA;
        /// <summary>
        /// 카드 효과 int형 B
        /// </summary>
        public int effectValue_intB;
        /// <summary>
        /// 카드 효과 float형 A
        /// </summary>
        public int effectValue_floatA;
        /// <summary>
        /// 카드 효과 float형 B
        /// </summary>
        public int effectValue_floatB;
        /// <summary>
        /// 카드 효과 float형 C
        /// </summary>
        public int effectValue_floatC;
        /// <summary>
        /// 카드 효과 float형 D
        /// </summary>
        public int effectValue_floatD;
        /// <summary>
        /// 카드 효과 bool형
        /// </summary>
        public int effectValue_bool;
        public enum Conditions { 일반, 초반, 중반, 후반 };
        /// <summary>
        /// 카드 사용조건 분류
        /// </summary>
        Conditions condition;
        /// <summary>
        /// 카드의 판매가격 A형(골드)
        /// </summary>
        public int sellValueA;
        /// <summary>
        /// 카드의 판매가격 B형(소울)
        /// </summary>
        public int sellValueB;
        /// <summary>
        /// 카드의 재활용 수치 
        /// </summary>
        public float recycle;
        /// <summary>
        /// 카드의 효과가 몇번 중첩되어있는지에 관한 수치
        /// </summary>
        public int dataCount;
        #endregion

        /// <summary>
        /// 카드의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
        /// <br>(int = num) : 카드 고유의 넘버링값</br>
        /// </summary>
        /// <param name="num"></param>
        public InfoCard(int num)
        {
            switch (num)
            {
                case 1: // 머선머선 카드 아직 미정
                    number = num;
                    Icon = null;
                    name = "";
                    cardCount = 0;
                    propertie = 0;
                    effectSortA = 0;
                    effectSortB = 0;
                    cardString1 = 0;
                    cardString2 = 0;
                    effectValue_intA = 0;
                    effectValue_intB = 0;
                    effectValue_floatA = 0;
                    effectValue_floatB = 0;
                    effectValue_floatC = 0;
                    effectValue_floatD = 0;
                    effectValue_bool = 0;
                    condition = Conditions.일반;
                    sellValueA = 0;
                    sellValueB = 0;
                    recycle = 0;
                    dataCount = 0;
                    break;
                default:
                    break;
            }
        }
    }
    private void Awake()
    {
        instance = this;
    }
}

