using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripte : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(Starttest());


        DEF abc = new DEF();
        ABC abc2 = abc.aaa;
        Debug.Log(abc.aaa.A);

        abc.aaa.A++;

        Debug.Log(abc.aaa.A);
        Debug.Log(abc2.A);

        abc2.top();

        Debug.Log(abc.aaa.A);
        Debug.Log(abc2.A);


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


    public class ABC
    {
        public int A = 1;
        public void top()
        {
            A = 5;
        }
    }

    public class DEF
    {
        static public int B = 0;
        public ABC aaa = new ABC();
        
    }
}