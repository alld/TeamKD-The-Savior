using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDataManager : Singleton<NewDataManager>
{
    private void Awake()
    {
        Regist(this);
    }
}
