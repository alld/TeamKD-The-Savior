using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDatabase : MonoBehaviour
{
    public static DungeonDatabase instance;
    public class InfoDungeon
    {
        public List<MonsterDatabase.InfoMonster> dungeonMonsterBox = new List<MonsterDatabase.InfoMonster>();

        
        public InfoDungeon(int num)
        {
            switch (num)
            {
                case 0:
                    dungeonMonsterBox.Add(new MonsterDatabase.InfoMonster(1));
                    break;
                case 1:
                    dungeonMonsterBox.Add(new MonsterDatabase.InfoMonster(1));
                    break;
                case 2:
                    dungeonMonsterBox.Add(new MonsterDatabase.InfoMonster(1));
                    break;
                case 3:
                    dungeonMonsterBox.Add(new MonsterDatabase.InfoMonster(1));
                    break;
                case 4:
                    dungeonMonsterBox.Add(new MonsterDatabase.InfoMonster(1));
                    break;
                case 5:
                    dungeonMonsterBox.Add(new MonsterDatabase.InfoMonster(1));
                    break;
                default:
                    break;
            }
        }
    }
    void Awake()
    {
        instance = this;
    }
}
