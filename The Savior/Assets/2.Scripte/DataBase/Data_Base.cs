using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Data_Base : Singleton<Data_Base>
{
    // 게임 시작 시 데이터 세팅 함수.
    protected abstract void Init();    
    protected abstract void Save();
    protected abstract void Load();
}
