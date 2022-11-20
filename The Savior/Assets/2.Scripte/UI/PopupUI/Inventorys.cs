using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventorys : UI_Popup
{
    enum Buttons
    {
        RelicButton,
        PartyButton,
        CardDeckButton,
        CollectButton,
        CloseButton,
    }
    enum Texts
    {
        RelicText,
        PartyText,
        CardDeckText,
        CollectText,
        TitleText,
    }

    enum Images
    {
        Dimmed,
    }

    List<Button> buttons = new List<Button>();
    List<TMP_Text> texts = new List<TMP_Text>();
    List<Image> images = new List<Image>();

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        BindObjects();
        SetObjects();

        images[(int)Images.Dimmed].SetToScreenSize();

        buttons[(int)Buttons.CloseButton].onClick.AddListener(delegate { OnClick_CloseButton(); });
        buttons[(int)Buttons.RelicButton].onClick.AddListener(delegate { OnClick_RelicButton(); });
        buttons[(int)Buttons.PartyButton].onClick.AddListener(delegate { OnClick_PartyButton(); });
        buttons[(int)Buttons.CardDeckButton].onClick.AddListener(delegate { OnClick_CardDeckButton(); });
        buttons[(int)Buttons.CollectButton].onClick.AddListener(delegate { OnClick_CollectButton(); });
    }

    protected override void BindObjects()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
    }
    protected override void SetObjects()
    {
        buttons = Get<Button>(typeof(Buttons));
        texts = Get<TMP_Text>(typeof(Texts));
        images = Get<Image>(typeof(Images));
    }

    private void OnClick_CloseButton()
    {
        UIManager.instance.Hide();
    }

    private void OnClick_RelicButton()
    {
        UIManager.instance.Show("Relic");
    }
    private void OnClick_PartyButton()
    {
        UIManager.instance.Show("Party");
    }

    private void OnClick_CardDeckButton()
    {
        UIManager.instance.Show("CardDeck");
    }

    private void OnClick_CollectButton()
    {
        UIManager.instance.Show("Collect");
    }
}
