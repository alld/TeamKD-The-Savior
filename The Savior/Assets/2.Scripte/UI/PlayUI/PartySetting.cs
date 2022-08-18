using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PartySetting : MonoBehaviour, IPointerClickHandler
{
    private PartySettingManager manager;
    public int idx = 0;

    public void OnPointerClick(PointerEventData eventData)
    {


        if (GameManager.instance.isDungeon) // 던전에 들어왔을 때.
        {
            Debug.Log("궁극기 사용.");
            return;
        }
        manager.OnPartySettingMode(idx);
    }

    private void Start()
    {
        manager = GameObject.Find("PUIManager").GetComponent<PartySettingManager>();
    }
}
