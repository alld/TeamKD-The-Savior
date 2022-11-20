using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : UI_Base
{
    enum Buttons
    {
        PauseButton,
        InventoryButton,
    }

    List<Button> buttons = new List<Button>();

    void Start()
    {
        Init();   
    }

    protected override void Init()
    {
        BindObjects();
        SetObjects();

        buttons[(int)Buttons.PauseButton].onClick.AddListener(delegate { OnClick_PauseButton(); });
        buttons[(int)Buttons.InventoryButton].onClick.AddListener(delegate { OnClick_InventoryButton(); });
    }

    private void BindObjects()
    {
        Bind<Button>(typeof(Buttons));
    }

    private void SetObjects()
    {
        buttons = Get<Button>(typeof(Buttons));
    }

    private void OnClick_PauseButton() { UIManager.instance.Show("Pause"); }
    private void OnClick_InventoryButton() { UIManager.instance.Show("Inventory"); }
}
