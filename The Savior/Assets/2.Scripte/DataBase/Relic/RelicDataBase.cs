using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
    /// <summary>
    /// 유물의 기본 원형값을 가지고있는 데이터 베이스 
    /// <br>InfoRelic를 통해 인스턴스를 만들어서 사용해야함. </br>
    /// <br>InfoRelic(int) : int == 데이터베이스상 캐릭터 고유넘버 </br>
    /// </summary>
public class RelicDataBase : MonoBehaviour
{

    private TextAsset jsonData;
    private string json;

    public int n;


    private void Awake()
    {
        InfoRelic info = new InfoRelic(n);

        jsonData = Resources.Load<TextAsset>("RelicData");
        json = jsonData.text;
        var data = JSON.Parse(json);

        switch (GameManager.instance.data.Language)
        {
            case 0:
                info.name = data[n]["Name_Kr"];
                break;
            case 1:
                info.name = data[n]["Name_Eng"];
                break;
            default:
                break;
        }
    }


    public class InfoRelic
    {
        #region 유물 기본 설정값
        /// <summary>
        /// 유물 데이터가 어떤데이터인지 구분하기위한 넘버
        /// </summary>
        public int number;
        /// <summary>
        /// 유물의 그림 이미지
        /// </summary>
        public Image Icon;
        /// <summary>
        /// 유물의 이름
        /// </summary>
        public string name;
        /// <summary>
        /// [saveData]유물 보유 여부
        /// </summary>
        public bool relicCount;
        /// <summary>
        /// 유물의 속성
        /// </summary>
        public int propertie;
        /// <summary>
        /// 유물의 효과 대분류
        /// </summary>
        public int effectSortA;
        /// <summary>
        /// 유물의 효과 소분류
        /// </summary>
        public int effectSortB;
        /// <summary>
        /// 유물의 설명 1
        /// </summary>
        public int relicString1;
        /// <summary>
        /// 유물의 설명 2
        /// </summary>
        public int relicString2;
        /// <summary>
        /// 유물 효과 int형 A
        /// </summary>
        public int effectValue_intA;
        /// <summary>
        /// 유물 효과 int형 B
        /// </summary>
        public int effectValue_intB;
        /// <summary>
        /// 유물 효과 float형 A
        /// </summary>
        public int effectValue_floatA;
        /// <summary>
        /// 유물 효과 float형 B
        /// </summary>
        public int effectValue_floatB;
        /// <summary>
        /// 유물 효과 float형 C
        /// </summary>
        public int effectValue_floatC;
        /// <summary>
        /// 유물 효과 float형 D
        /// </summary>
        public int effectValue_floatD;
        /// <summary>
        /// 유물 효과 bool형
        /// </summary>
        public int effectValue_bool;
        public enum Conditions { 일반, 초반, 중반, 후반 };
        /// <summary>
        /// 유물 사용조건 분류
        /// </summary>
        Conditions condition;
        /// <summary>
        /// 유물의 중복보상 A형(골드)
        /// </summary>
        public int overlapValueA;
        /// <summary>
        /// 유물의 중복보상 B형(소울)
        /// </summary>
        public int overlapValueB;
        /// <summary>
        /// 유물의 재활용 수치 
        /// </summary>
        public float recycle;
        /// <summary>
        /// 유물의 효과가 몇번 중첩되어있는지에 관한 수치
        /// </summary>
        public int dataCount;
        #endregion
        public InfoRelic()
        {

        }
        /// <summary>
        /// 유물의 기본값을 가져오기위해서는 매개변수에 (int)를 넣어줘야함.
        /// <br>(int = num) : 유물 고유의 넘버링값</br>
        /// </summary>
        /// <param name="num"></param>
        public InfoRelic(int num)
        {
            
            number = num;
            Icon = null;
            name = "";
            relicCount = false;
            propertie = 0;
            effectSortA = 0;
            effectSortB = 0;
            relicString1 = 0;
            relicString2 = 0;
            effectValue_intA = 0;
            effectValue_intB = 0;
            effectValue_floatA = 0;
            effectValue_floatB = 0;
            effectValue_floatC = 0;
            effectValue_floatD = 0;
            effectValue_bool = 0;
            condition = Conditions.일반;
            overlapValueA = 0;
            overlapValueB = 0;
            recycle = 0;
            dataCount = 0;
        }

    }

}