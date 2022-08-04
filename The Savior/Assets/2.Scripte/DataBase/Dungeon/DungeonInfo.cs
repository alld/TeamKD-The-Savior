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
                    nameDG = "Ʃ�丮��";
                    contentDG = "���ڿ�";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 2:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "1����";
                    contentDG = "���ڿ�";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 3:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "2����";
                    contentDG = "���ڿ�";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 4:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "���ڿ�";
                    contentDG = "���ڿ�";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 5:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "���ڿ�";
                    contentDG = "���ڿ�";
                    reward1DG = null;
                    reward2DG = null;
                    reward3DG = null;
                    break;
                case 6:
                    numberDG = num;
                    //IconDG = Imageful[num];
                    nameDG = "���ڿ�";
                    contentDG = "���ڿ�";
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
    /// �ӽ÷� ������
    /// </summary>
    private void InitDungeonName()
    {
        nameDungeon[0] = "Ʃ�丮�� ����";
        nameDungeon[1] = "ù ��° ����";
        nameDungeon[2] = "�� ��° ����";
        nameDungeon[3] = "�� ��° ����";
        nameDungeon[4] = "�� ��° ����";
        nameDungeon[5] = "�� �� ��";
    }

    /// <summary>
    /// �ӽ÷� ������
    /// </summary>
    private void InitContactDungeon()
    {
        contactDungeon[0] = "���� �ٴٸ� ����ϴ� ����,\n�� ���� ������� ����� ������ å������ �������\n���ձ��� ������ ������ ������ �Ұ����ϰ� �Ǿ���.\n�ȿ��� ��ũ���� �Լ��Ҹ���,\n���� ������ ��� ���� ���밡 ǳ�ܿ´�.";
        contactDungeon[1] = "";
        contactDungeon[2] = "";
        contactDungeon[3] = "";
        contactDungeon[4] = "";
        contactDungeon[5] = "";
    }
}
