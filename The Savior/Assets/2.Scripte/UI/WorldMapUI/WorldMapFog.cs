using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapFog : MonoBehaviour
{
    [Header("맵을 가리는 안개")]
    public GameObject[] fogs;
    [SerializeField]
    private GameObject curFog;

    [Space(10.0f)]
    [Header("진행에 따라 던전 활성화")]
    public GameObject[] dungeons;

    void Start()
    {
        curFog = fogs[GameManager.instance.data.dungeonClear];
        curFog.SetActive(true);

        for(int i = 0; i <= GameManager.instance.data.dungeonClear; i++)
        {
            OpenDungeon(i);
        }
    }

    public void OpenDungeon(int n)
    {
        dungeons[n].SetActive(true);
    }
}
