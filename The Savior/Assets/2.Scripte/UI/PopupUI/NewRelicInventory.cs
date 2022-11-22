using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NewRelicInventory : UI_Popup
{
    PlayerData playerData;

    enum Buttons
    {
        CloseButton,
    }
    enum Texts
    {
        TitleText,
    }
    enum Images
    {
        ItemList,
    }

    List<Button> buttons = new List<Button>();
    List<TMP_Text> texts = new List<TMP_Text>();
    List<Image> images = new List<Image>();

    void Start()
    {
        Init();   
    }

    protected override void Init()
    {
        NewGameDataManager.instance.ObserberLanguage(LanguageChangeTexts);
        playerData = NewGameDataManager.instance.GetPlayerData();

        BindObjects();
        SetObjects();

        SetRelicData();

        buttons[(int)Buttons.CloseButton].onClick.AddListener(delegate { OnClick_CloseButton(); });
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

    private void LanguageChangeTexts()
    {
        // 언어 변환 로직.
    }

    private void OnClick_CloseButton()
    {
        UIManager.instance.Hide();
    }

    // 유물 창 오픈 시 유물 데이터 세팅.
    private void SetRelicData() 
    {
        Dictionary<int, MyRelic> relics = NewGameDataManager.instance.GetRelicData();
        
    }
}
