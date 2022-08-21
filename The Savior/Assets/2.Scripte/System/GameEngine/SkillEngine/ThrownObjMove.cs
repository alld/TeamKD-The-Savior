using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjMove : MonoBehaviour
{
    private WaitForSeconds delay_001 = new WaitForSeconds(0.01f);
    public Transform targetPoint;
    public float thrownSpeed = 5;

    private void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        transform.LookAt(targetPoint);
        while (true)
        {
            transform.Translate(targetPoint.position.normalized * thrownSpeed, Space.World);
            yield return delay_001;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
