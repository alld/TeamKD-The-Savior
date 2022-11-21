using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Party : UI_Popup
{
    PlayerData playerData;

    enum Texts
    {
        TitleText
    }

    enum Images
    {
        ItemList
    }

    enum Buttons
    {
        CloseButton
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
        NewGameDataManager.instance.GetPlayerData();

        BindObjects();
        SetObjects();

        SetPartyData();
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
        // 언어 변환 로직
    }

    private void OnClick_CloseButton()
    {
        UIManager.instance.Hide();
    }

    // 캐릭터 인벤토리 창 오픈 시 세팅
    private void SetPartyData()
    {
        Dictionary<int, MyCharacter> characters = NewGameDataManager.instance.GetCharacterData();
        if (characters == null) return;
        Debug.Log($"내 캐릭터 : {characters.Count}");
    }
}
