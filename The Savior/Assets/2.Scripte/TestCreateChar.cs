using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateChar : MonoBehaviour
{
    public GameObject[] character;
    private int n;
    void Start()
    {
        n = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            n = Random.Range(0, 3);
            Instantiate(character[n], this.transform);
        }
    }
}
