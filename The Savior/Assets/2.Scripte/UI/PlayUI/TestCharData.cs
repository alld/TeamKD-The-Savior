using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharData : MonoBehaviour
{
    public string charName = "TestName";

    [Range(1, 100)]
    public int hp = 100;
    [Range(1, 100)]
    public int att = 20;
    [Range(1, 100)]
    public int def = 30;
}
