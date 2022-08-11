using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripte : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Starttest());
    }

    public IEnumerator Starttest()
    {
        StartCoroutine(Test(1, 0, 10));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Test(2, 0, 15));
    }

    public IEnumerator Test(int A, int B, int C)
    {
        int temp = 10;
        B = 0;

        while (C > 0)
        {
            Debug.Log($"A: {A},B: {B++},C: {C--}, temp {temp--}");
            yield return new WaitForSeconds(1.0f);

        }
        
    }
}
