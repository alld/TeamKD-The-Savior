using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripte : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Starttest());
        Debug.Log("확인2");
    }

    public IEnumerator Starttest()
    {
        //StartCoroutine(Test(1, 0, 10));

        while (true)
        {
            Debug.Log("확인");
            yield return new WaitForSeconds(0.5f);
        }
        //StartCoroutine(Test(2, 0, 15));
    }


}
