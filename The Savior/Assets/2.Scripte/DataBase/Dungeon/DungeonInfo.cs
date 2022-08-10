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
