using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class TestScripte : MonoBehaviour
{

    private EventTrigger eventTrigger;
    private EventTrigger.Entry entry;

    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((Data) => { OnPointerEnter(); });
        eventTrigger?.triggers.Add(entry);

        List<float> abc = new List<float>() { 0,2,5,6,4,5 };
        foreach (var item in abc.Select((abc, def) => new { abc, def }))
        {
            Debug.Log("1:" + item);
            Debug.Log("2:" + item.abc);
            Debug.Log("3:" + item.def);
        };


        foreach (var item in abc.Select((abc, def) => new { abc, def }))
        {
            Debug.Log("1:" + item);
            Debug.Log("2:" + item.abc);
            Debug.Log("3:" + item.def);
        }

        List<int> abc2 = new List<int>() { 0,1,2,3,4,5 };
        var abc3 = from item in abc2 select item + item;

        foreach (var item in abc3)
        {
            //Debug.Log(item);
        }
        

    }


    void OnPointerEnter ()
    {
        Debug.Log("ABC");
        //Debug.Log(data);
    }
}