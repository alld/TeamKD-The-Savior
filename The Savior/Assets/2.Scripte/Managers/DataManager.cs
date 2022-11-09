using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Data_Base
{
    private void Awake()
    {
        // ½Ì±ÛÅæ Àû¿ë
        Regist(this);
    }
    protected override void Init()
    {
        throw new System.NotImplementedException();
    }

    protected override void Load()
    {
        throw new System.NotImplementedException();
    }

    protected override void Save()
    {
        throw new System.NotImplementedException();
    }
}
