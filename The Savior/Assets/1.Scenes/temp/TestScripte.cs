using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class TestScripte : MonoBehaviour
{
    public Image fire;
    public Transform temp;
    public delegate void testdele();
    testdele Testdele;

    [Range(0, 100f)]
    public float speed = 10f; 


    void Start()
    {

        if (Testdele != null)
        {
            Testdele();
        }
    }

    private void Update()
    {
        //transform.rotation = Quaternion.Lerp(fire.transform.rotation, temp.rotation, Time.deltaTime * speed);
        //Quaternion.Lerp(fire.transform.rotation, temp.rotation , Time.deltaTime * 10);
    }

}