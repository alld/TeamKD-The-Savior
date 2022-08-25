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

    void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        playerMap = playerInput.actions.FindActionMap("Player");
        clickAction = playerMap.FindAction("Click");
        mouseMoveAction = playerMap.FindAction("Mouse");

        clickAction.performed += ctx =>
        {
            Debug.Log(ctx);
        };

        mouseMoveAction.performed += ctx =>
        {
            Debug.Log(ctx.ReadValue<Vector2>());
        };


    }

    private void Update()
    {
        //transform.rotation = Quaternion.Lerp(fire.transform.rotation, temp.rotation, Time.deltaTime * speed);
        //Quaternion.Lerp(fire.transform.rotation, temp.rotation , Time.deltaTime * 10);
    }

}