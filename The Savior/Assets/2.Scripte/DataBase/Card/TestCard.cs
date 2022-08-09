using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : MonoBehaviour
{
    public int number = 0;
    public int idx = 3;
    void Awake()
    {
        idx = number + 1;
    }
}
