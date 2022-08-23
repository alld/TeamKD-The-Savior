using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameExit : MonoBehaviour
{
    public Button ExitButton;

    private void Start()
    {
        ExitButton.onClick.AddListener(() => OnClick_ExitGameBtn());    
    }

    private void OnClick_ExitGameBtn()
    {
        Application.Quit();
    }
}
