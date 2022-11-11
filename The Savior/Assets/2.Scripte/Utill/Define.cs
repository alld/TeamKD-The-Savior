using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enum 타입의 선언만 허용합니다.
 */

public class Define
{
    public enum UIEvent 
    {
        Click,
        Drag,
    }

    public enum EquipCount  // 장착 가능한 수.
    {
        Character = 4,
        Relic = 5,
    }
}
