using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform movepoint;
    Vector3 Movetemp, Move_Yzero;
    CharacterController unitControl;
    UnitAI unit;
    GameObject unitbox;
    // Start is called before the first frame update
    void Start()
    {
        unitControl = GetComponent<CharacterController>();
        StartCoroutine(timer());
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Instantiate(this.gameObject);
            //unitbox = Instantiate(Resources.Load<GameObject>("Unit/TestBox"));
            //unit = unitbox.GetComponent<UnitAI>();
            //unit.Start();

        }
    }

    IEnumerator timer()
    {
        while (true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Move_Yzero), 600 * Time.deltaTime);
            Movetemp = (movepoint.position - transform.position).normalized * 20;
            Move_Yzero = Movetemp - (Vector3.up * Movetemp.y);
            //transform.TransformDirection(Move_Yzero);
            //unitControl.Move((movepoint.position - (Vector3.up * 2f)) * Time.deltaTime);


            unitControl.SimpleMove(Move_Yzero);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
