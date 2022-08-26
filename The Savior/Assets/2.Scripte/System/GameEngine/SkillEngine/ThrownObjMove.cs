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
        //RaycastHit hit;
        //LayerMask mask = (1 << 6) + (1 << 7);
        transform.LookAt(targetPoint);
        tempMovePoint = targetPoint.position - transform.position;
        while (true)
        {
            transform.Translate(tempMovePoint.normalized * thrownSpeed, Space.World);
            //if (Physics.Raycast(transform.position, targetPoint.position, out hit, 2.0f, mask))
            //{
            //    if (hit.collider.GetComponent<UnitMelee>().ThrownTriggerCheck(transform)) Destroy(gameObject);
            //}
            ////else if ((Vector3.Distance(transform.position, targetPoint.position) < 1.0f))
            ////{
            ////    Debug.Log("에러위치확인2");
            ////    targetPoint.GetComponent<UnitMelee>().ThrownTriggerCheck(transform);
            ////    Destroy(gameObject);
            ////}
            yield return delay_001;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
