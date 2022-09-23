using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public float cooldown;

    public SkillDatabase(int num)
    {
        cooldown = 20.0f;
    }
}
