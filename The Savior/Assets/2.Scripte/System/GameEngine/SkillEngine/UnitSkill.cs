using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill : MonoBehaviour
{
    public static UnitSkill instance = null;
    void Awake()
    {
        instance = this;
    }

    
    public void OnSkill(int num)
    {
        switch (num)
        {
            case 0:
                break;
            default:
                break;

        }
    }


}
