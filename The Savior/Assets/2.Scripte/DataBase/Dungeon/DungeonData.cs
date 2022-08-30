using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData : MonoBehaviour
{

    public List<MonsterDatabase.Data> dungeonMonsterBox = new List<MonsterDatabase.Data>();

    public List<int> monsterBoxMin = new List<int>();
    public List<int> monsterBoxMax = new List<int>();
    public List<int> monsterBoxCount = new List<int>();

    public int[] stageDataInfo = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 10, 1, 1, 1, 1, 2, 1, 1, 1, 1, 10 };
    public int[] stageDataIndex = { 0, 0, 0, 0, 1, 0, 0, 0, 1, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 3 };

    public void DataSetting(int num)
    {
        switch (num)
        {
            case 0:
                for (int i = 0; i < 20; i++)
                {
                    monsterBoxMin.Add(Random.Range(2, 6));
                    monsterBoxMax.Add(Random.Range(monsterBoxMin[i], 6));
                    monsterBoxCount.Add(Random.Range(3, 6));
                }
                dungeonMonsterBox.Add(new MonsterDatabase.Data(6));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(5));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(1));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(2));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(3));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(4));

                break;
            case 1:
                for (int i = 0; i < 10; i++)
                {
                    monsterBoxMin.Add(Random.Range(2, 4));
                    monsterBoxMax.Add(Random.Range(monsterBoxMin[i], 4));
                    monsterBoxCount.Add(Random.Range(1, 4));
                }
                dungeonMonsterBox.Add(new MonsterDatabase.Data(1));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(2));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(3));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(4));
                break;
            case 2:
                for (int i = 0; i < 10; i++)
                {
                    monsterBoxMin.Add(Random.Range(2, 4));
                    monsterBoxMax.Add(Random.Range(monsterBoxMin[i], 4));
                    monsterBoxCount.Add(Random.Range(1, 4));
                }
                dungeonMonsterBox.Add(new MonsterDatabase.Data(1));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(2));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(3));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(4));
                break;
            case 3:
                for (int i = 0; i < 10; i++)
                {
                    monsterBoxMin.Add(Random.Range(2, 4));
                    monsterBoxMax.Add(Random.Range(monsterBoxMin[i], 4));
                    monsterBoxCount.Add(Random.Range(1, 4));
                }
                dungeonMonsterBox.Add(new MonsterDatabase.Data(1));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(2));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(3));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(4));
                break;
            case 4:
                for (int i = 0; i < 10; i++)
                {
                    monsterBoxMin.Add(Random.Range(2, 5));
                    monsterBoxMax.Add(Random.Range(monsterBoxMin[i], 5));
                    monsterBoxCount.Add(Random.Range(1, 5));
                }
                dungeonMonsterBox.Add(new MonsterDatabase.Data(1));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(2));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(3));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(4));
                break;
            case 5:
                for (int i = 0; i < 10; i++)
                {
                    monsterBoxMin.Add(Random.Range(2, 4));
                    monsterBoxMax.Add(Random.Range(monsterBoxMin[i], 4));
                    monsterBoxCount.Add(Random.Range(1, 4));
                }
                dungeonMonsterBox.Add(new MonsterDatabase.Data(1));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(2));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(3));
                dungeonMonsterBox.Add(new MonsterDatabase.Data(4));
                break;
            default:
                break;
        }
    }
}
