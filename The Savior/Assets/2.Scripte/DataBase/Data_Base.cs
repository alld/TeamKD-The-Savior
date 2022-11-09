using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Data_Base : Singleton<Data_Base>
{
    // ���� ���� �� ������ ���� �Լ�.
    protected abstract void Init();    
    protected abstract void Save();
    protected abstract void Load();
}
