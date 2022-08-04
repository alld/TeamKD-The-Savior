using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonInfo : MonoBehaviour
{
    public static DungeonInfo instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

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
                    nameDG = "Æ©Åä¸®¾ó";
                    contentDG = "ºó¹®ÀÚ¿­";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 2:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "1´øÀü";
                    contentDG = "ºó¹®ÀÚ¿­";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 3:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "2´øÀü";
                    contentDG = "ºó¹®ÀÚ¿­";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 4:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "ºó¹®ÀÚ¿­";
                    contentDG = "ºó¹®ÀÚ¿­";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 5:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "ºó¹®ÀÚ¿­";
                    contentDG = "ºó¹®ÀÚ¿­";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 6:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "ºó¹®ÀÚ¿­";
                    contentDG = "ºó¹®ÀÚ¿­";
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
    private void Awake()
    {
        instance = this;
        //foreach (var item in ImagefulSet)
        //{
        //    Imageful.Add(item);
        //}
    }
}