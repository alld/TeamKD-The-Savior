using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testFire : MonoBehaviour
{
    public Sprite[] fire;
    WaitForSeconds delay02 = new WaitForSeconds(0.1f);
    private Sprite CurrntFire;
    public int temp;
    public Image fireObject;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FireEffect());
    }


    IEnumerator FireEffect()
    {
        while (true)
        {
            //if(CurrntFire != null)Destroy(CurrntFire);
            temp = ++temp % 15;
            fireObject.sprite = fire[temp];
            //CurrntFire = Instantiate(fire[temp]);
            yield return delay02;
        }
    }
}
