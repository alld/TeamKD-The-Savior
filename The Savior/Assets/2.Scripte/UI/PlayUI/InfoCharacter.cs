using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using SimpleJSON;

public class InfoCharacter : MonoBehaviour
{
    // 버튼
    public Button closeInfoButton;
    public Button skillButton;
    public Button statusButton;
    public Button identityButton;

    // 해당 이미지
    public GameObject charInfo; // : CharacterInfo Image  
    public GameObject skillImg;
    public GameObject statusImg;
    public GameObject identityImg;

    // 이미지 위치
    public Transform imgTr;

    [Header("스킬창")]
    public Transform skillTr;
    public Transform spetialTr;
    public TMP_Text skillText;
    public TMP_Text spetialText;


    [Header("캐릭터 정보")]
    // 캐릭터 이름 텍스트
    public TMP_Text charName;
    // 캐릭터 스탯 텍스트
    public TMP_Text hp;
    public TMP_Text att;
    public TMP_Text def;

    private TextAsset json;
    private string jsonData;


    // 상세정보에 활성화 된 캐릭터 이미지
    private Image character;

    void Start()
    {
        closeInfoButton.onClick.AddListener(() => OnClick_CloseInfoBtn());
        skillButton.onClick.AddListener(() => OnClick_SkillBtn());
        statusButton.onClick.AddListener(() => OnClick_StatusBtn());
        identityButton.onClick.AddListener(() => OnClick_IdentityBtn());

        json = Resources.Load<TextAsset>("CharacterData");
        jsonData = json.text;

        
    }

    /// <summary>
    /// 인벤토리에서 해당 캐릭터를 클릭시 상세정보창이 활성화됨.
    /// </summary>
    public void OnCharacterInfo(Image copyImg, int num)
    {
        var data = JSON.Parse(jsonData);
        charInfo.SetActive(true);

        // 이 함수를 호출한 캐릭터의 번호에 맞는 데이터를 가져온다.
        CharacterDatabase charData = new CharacterDatabase(num-1, data);
        character = copyImg;
        character = Instantiate(character, imgTr);
        InitRectSize(character);

        charName.text = charData.name;
        hp.text = "체력 : " + charData.maxHP;
        att.text = "공격력 : " + charData.damage;
        def.text = "방어력 : " + charData.defense;


        Destroy(character.GetComponent<ViewCharacterInfo>());
    }

    /// <summary>
    /// 스트레치가 적용된 이미지의 사이즈를 부모 객체의 사이즈에 맞춤.
    /// </summary>
    /// <param name="img"></param>
    private void InitRectSize(Image img)
    {
        img.rectTransform.offsetMin = Vector2.zero;
        img.rectTransform.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// 상세정보창을 닫음
    /// 인벤토리에 있는 캐릭터를 복사하여 가져왔기 때문에
    /// 창을 닫을 때 해당 이미지를 파괴한다.
    /// </summary>
    private void OnClick_CloseInfoBtn()
    {
        Destroy(character);
        charInfo.SetActive(false);
    }

    /// <summary>
    /// 스킬창을 연다.
    /// </summary>
    private void OnClick_SkillBtn()
    {
        skillImg.SetActive(true);
        statusImg.SetActive(false);
        identityImg.SetActive(false);
    }

    /// <summary>
    /// 스테이터스창을 연다.
    /// </summary>
    private void OnClick_StatusBtn()
    {
        statusImg.SetActive(true);

        skillImg.SetActive(false);
        identityImg.SetActive(false);
    }

    /// <summary>
    /// 특성창을 연다.
    /// </summary>
    private void OnClick_IdentityBtn()
    {
        identityImg.SetActive(true);

        skillImg.SetActive(false);
        statusImg.SetActive(false);
    }
}
