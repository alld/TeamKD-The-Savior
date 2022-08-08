using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDataTable : MonoBehaviour
{
    public static RelicDataTable instance = null;
    public TextAsset relicJSON;

    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
