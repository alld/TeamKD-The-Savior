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

    private void Awake()
    {
        InitContactDungeon();
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
