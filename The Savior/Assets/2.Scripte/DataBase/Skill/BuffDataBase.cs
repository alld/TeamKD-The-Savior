using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDataBase : MonoBehaviour
{
    public int number;
    public int ID;
    public int priorities;

    public BuffDataBase(int num)
    {
        number = 0;
        ID = Random.Range(1000, 9999);
        priorities = 0;
    }
}
