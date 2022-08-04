using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonInfo : MonoBehaviour
{ 
    public GameObject[] dungeonImg;
    public string[] nameDungeon = new string[6];
    public string[] contactDungeon = new string[6];
    public GameObject[] bossImg;
    public GameObject[] rewardImg1;
    public GameObject[] rewardImg2;

<<<<<<< HEAD
=======
    public class InfoDG
    {
        public int numberDG;
        public Sprite IconDG;
        public string nameDG;
        public string contentDG;
        public Image reward1DG;
        public Image reward2DG;
        public Image reward3DG;

        public InfoDG(int num)
        {
            switch (num)
            {
                case 1:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "튜토리얼";
                    contentDG = "빈문자열";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 2:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "1던전";
                    contentDG = "빈문자열";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 3:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "2던전";
                    contentDG = "빈문자열";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 4:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "빈문자열";
                    contentDG = "빈문자열";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 5:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "빈문자열";
                    contentDG = "빈문자열";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 6:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "빈문자열";
                    contentDG = "빈문자열";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                default:
                    break;
            }
        }
    }
    
    public InfoDG[] infoDG= 
    { 
        new InfoDG(1),
        new InfoDG(2),
        new InfoDG(3),
        new InfoDG(4),
        new InfoDG(5),
        new InfoDG(6)
    };
>>>>>>> main
    private void Awake()
    {
        InitDungeonName();
        InitContactDungeon();
    }

    /// <summary>
    /// 임시로 설정함
    /// </summary>
    private void InitDungeonName()
    {
        nameDungeon[0] = "튜토리얼 던전";
        nameDungeon[1] = "첫 번째 던전";
        nameDungeon[2] = "두 번째 던전";
        nameDungeon[3] = "세 번째 던전";
        nameDungeon[4] = "네 번째 던전";
        nameDungeon[5] = "마 왕 성";
    }

    /// <summary>
    /// 임시로 설정함
    /// </summary>
    private void InitContactDungeon()
    {
        contactDungeon[0] = "성과 바다를 통과하는 동굴,\n한 때는 사람들의 교통과 무역을 책임지던 장소지만\n마왕군이 동굴을 점거해 통행이 불가능하게 되었다.\n안에선 오크들의 함성소리와,\n습기 가득한 고기 썩은 악취가 풍겨온다.";
        contactDungeon[1] = "";
        contactDungeon[2] = "";
        contactDungeon[3] = "";
        contactDungeon[4] = "";
        contactDungeon[5] = "";
    }
}
