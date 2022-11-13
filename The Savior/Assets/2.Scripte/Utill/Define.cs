using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enum Ÿ���� ���� ����մϴ�.
 */

public class Define
{
    public enum UIEvent 
    {
        Click,
        Drag,
    }

    public enum EquipCount  // ���� ������ ��.
    {
        Character = 4,
        Relic = 5,
    }

    public enum PresetName
    {
        Count = 5,
    }

    public enum LastGameViewPoint
    {
        Main,
        World,
        Dungeon,
    }
}
