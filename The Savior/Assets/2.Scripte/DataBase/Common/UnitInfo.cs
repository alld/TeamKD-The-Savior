using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    private bool PlayerUnit;
    public bool playerUnit
    {
        get { return PlayerUnit; }
        set 
        { 
            PlayerUnit = value;
            GetComponent<UnitStateData>().playerUnit = value;
        }
    }
    private int UnitNumber;
    public int unitNumber
    {
        get { return UnitNumber; }
        set
        {
            UnitNumber = value;
            GetComponent<UnitMelee>().unitNumber = value;
        }
    }
    private int PartyNumber;
    public int partyNumber
    {
        get { return PartyNumber; }
        set
        {
            PartyNumber = value;
            GetComponent<UnitMelee>().partyNumber = value;
            GetComponent<UnitStateData>().partyNumber = value;
        }
    }

}
