using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewCardDeck : UI_Popup
{
    private const int MAX_PRESET_COUNT = 5;
    PlayerData playerData;

    enum Buttons
    {
        PresetButtons_1,
        PresetButtons_2,
        PresetButtons_3,
        PresetButtons_4,
        PresetButtons_5,
        CloseButton,
    }
    enum Texts
    {
        TitleText,
        PresetNameText,
        CardNameText,
        CardTypeText,
        CardContentText,
    }
    enum Images
    {
        ItemList,
    }
    enum Transforms
    {
        MyCardContent,
    }

    List<Button> buttons = new List<Button>();
    List<TMP_Text> texts = new List<TMP_Text>();
    List<Image> images = new List<Image>();
    List<Transform> transforms = new List<Transform>();

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

        SetCardData();
        OnClick_PresetChange(playerData.currentCardPreset);  // 카드 세팅 창 오픈시 마지막에 열었던 프리셋이 열림.

        buttons[(int)Buttons.CloseButton].onClick.AddListener(delegate { OnClick_CloseButton(); });

        for (int i = 0; i < MAX_PRESET_COUNT; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(delegate { OnClick_PresetChange(index); });
        }

    }

    protected override void BindObjects()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Transform>(typeof(Transforms));
    }


    protected override void SetObjects()
    {
        buttons = Get<Button>(typeof(Buttons));
        texts = Get<TMP_Text>(typeof(Texts));
        images = Get<Image>(typeof(Images));
        transforms = Get<Transform>(typeof(Transforms));
    }

    private void LanguageChangeTexts()
    {
        // 언어 변환 로직
    }

    private void OnClick_CloseButton()
    {
        UIManager.instance.Hide();
    }

    private void OnClick_PresetChange(int idx)
    {
        MyPreset preset = playerData.presetData[idx];
        playerData.currentCardPreset = idx;

        Debug.Log($"내 카드 프리셋 : {preset.id.Length}");

        NewGameDataManager.instance.SetPlayerData(playerData);
    }

    private void SetCardData()
    {
        Dictionary<int, MyCard> cards = NewGameDataManager.instance.GetCardData();
        if (cards == null) return;
        Debug.Log($"내 카드 : {cards.Count}");
    }
}
