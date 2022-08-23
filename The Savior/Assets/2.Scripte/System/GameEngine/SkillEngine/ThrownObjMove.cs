using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjMove : MonoBehaviour
{
    private WaitForSeconds delay_001 = new WaitForSeconds(0.01f);
    public Transform targetPoint;
    public float thrownSpeed = 5;
    private Vector3 tempMovePoint;

    private void Start()
    {
        StartCoroutine(Move());
        Destroy(this.gameObject, 3.0f);
    }

    IEnumerator Move()
    {
        transform.LookAt(targetPoint);
        tempMovePoint = targetPoint.position - transform.position;
        while (true)
        {
            transform.Translate(tempMovePoint.normalized * thrownSpeed, Space.World);
            yield return delay_001;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
