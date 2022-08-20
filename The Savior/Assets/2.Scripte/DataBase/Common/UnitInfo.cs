using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public bool playerUnit;
    public bool changePlayerUnit
    {
        get { return playerUnit; }
        set 
        {
            playerUnit = value;
            if (GameManager.instance.dungeonOS != null)
            {
                GetComponent<UnitStateData>().playerUnit = value;
            }
        }
    }
    public int unitNumber;
    public int changeUnitNumber
    {
        get { return unitNumber; }
        set
        {
            unitNumber = value;
            if (GameManager.instance.dungeonOS != null)
            {
                GetComponent<UnitStateData>().number = value;
                GetComponent<UnitAI>().unitNumber = value;
                GetComponent<UnitMelee>().unitNumber = value;
            }
        }
    }
    public int partyNumber;
    public int changePartyNumber
    {
        get { return partyNumber; }
        set
        {
            partyNumber = value;
            if (GameManager.instance.dungeonOS != null)
            {
                GetComponent<UnitMelee>().partyNumber = value;
                GetComponent<UnitStateData>().partyNumber = value;
                GetComponent<UnitAI>().partyNumber = value;
            }
        }
    }

}
