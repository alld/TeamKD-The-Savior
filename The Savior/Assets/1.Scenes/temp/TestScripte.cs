using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.InputSystem;

public class TestScripte : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap playerMap;
    private InputAction clickAction;
    private InputAction mouseMoveAction;

    public GameObject box;
    Vector2 mousePoint;

    void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        playerMap = playerInput.actions.FindActionMap("Player");
        clickAction = playerMap.FindAction("Click");
        mouseMoveAction = playerMap.FindAction("Mouse");

        clickAction.performed += ctx =>
        {
            OnStageSelect();
        };

        mouseMoveAction.performed += ctx =>
        {
            //Debug.Log(ctx.ReadValue<Vector2>());
            mousePoint = ctx.ReadValue<Vector2>();
        };


    }

    void OnStageSelect()
    {
        //Debug.Log("레이캐스트 작동확인");
        Debug.Log(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int laymask = 1 << LayerMask.NameToLayer("StagePoint");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject box2;
            box2 = Instantiate(box);
            box2.transform.position = new Vector3(mousePoint.x, 0, mousePoint.y);

            Debug.Log("레이캐스트 대상확인" + hit.collider.name + "대상레이어 : " + hit.collider.gameObject.layer);
            if (hit.collider.CompareTag("STAGEPOINT"))
            {
                Debug.Log("최종");
                
            }
        }
    }


    private void Update()
    {
        //transform.rotation = Quaternion.Lerp(fire.transform.rotation, temp.rotation, Time.deltaTime * speed);
        //Quaternion.Lerp(fire.transform.rotation, temp.rotation , Time.deltaTime * 10);
    }

}