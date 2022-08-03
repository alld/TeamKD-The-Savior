using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonInfo : MonoBehaviour
{
    public static DungeonInfo instance = null;
    //public Sprite[] ImagefulSet;
    //public static List<Sprite> Imageful = null;

    public class DG_Info
    {
        public int DG_number;
        public Sprite DG_Icon;
        public string DG_name;
        public string DG_content;
        public Image DG_reward1;
        public Image DG_reward2;
        public Image DG_reward3;

        public DG_Info(int num)
        {
            switch (num)
            {
                case 1:
                    DG_number = num;
                    //DG_Icon = Imageful[num];
                    DG_name = "Æ©Åä¸®¾ó";
                    DG_content = "ºó¹®ÀÚ¿­";
                    DG_reward1 = null;
                    DG_reward2 = null;
                    DG_reward3 = null;
                    break;
                case 2:
                    DG_number = num;
                    //DG_Icon = Imageful[num];
                    DG_name = "1´øÀü";
                    DG_content = "ºó¹®ÀÚ¿­";
                    DG_reward1 = null;
                    DG_reward2 = null;
                    DG_reward3 = null;
                    break;
                case 3:
                    DG_number = num;
                    //DG_Icon = Imageful[num];
                    DG_name = "2´øÀü";
                    DG_content = "ºó¹®ÀÚ¿­";
                    DG_reward1 = null;
                    DG_reward2 = null;
                    DG_reward3 = null;
                    break;
                case 4:
                    DG_number = num;
                    //DG_Icon = Imageful[num];
                    DG_name = "ºó¹®ÀÚ¿­";
                    DG_content = "ºó¹®ÀÚ¿­";
                    DG_reward1 = null;
                    DG_reward2 = null;
                    DG_reward3 = null;
                    break;
                case 5:
                    DG_number = num; 
                    //DG_Icon = Imageful[num];
                    DG_name = "ºó¹®ÀÚ¿­";
                    DG_content = "ºó¹®ÀÚ¿­";
                    DG_reward1 = null;
                    DG_reward2 = null;
                    DG_reward3 = null;
                    break;
                case 6:
                    DG_number = num;
                    //DG_Icon = Imageful[num];
                    DG_name = "ºó¹®ÀÚ¿­";
                    DG_content = "ºó¹®ÀÚ¿­";
                    DG_reward1 = null;
                    DG_reward2 = null;
                    DG_reward3 = null;
                    break;
                default:
                    break;
            }
        }
    }
    
    public DG_Info[] DG_info = 
    { 
        new DG_Info(1),
        new DG_Info(2),
        new DG_Info(3),
        new DG_Info(4),
        new DG_Info(5),
        new DG_Info(6)
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