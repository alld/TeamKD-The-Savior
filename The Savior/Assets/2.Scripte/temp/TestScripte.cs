using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripte : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(Starttest());

    }

    public IEnumerator Starttest()
    {
        //StartCoroutine(Test(1, 0, 10));

        while (true)
        {
            Debug.Log("»Æ¿Œ");
            yield return new WaitForSeconds(0.5f);
        }
        //StartCoroutine(Test(2, 0, 15));
    }

}