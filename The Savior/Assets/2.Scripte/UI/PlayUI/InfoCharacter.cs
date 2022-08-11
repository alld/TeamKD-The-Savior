using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using SimpleJSON;

public class InfoCharacter : MonoBehaviour
{
    // ��ư
    public Button closeInfoButton;
    public Button skillButton;
    public Button statusButton;
    public Button identityButton;

    // �ش� �̹���
    public GameObject charInfo; // : CharacterInfo Image  
    public GameObject skillImg;
    public GameObject statusImg;
    public GameObject identityImg;

    // �̹��� ��ġ
    public Transform imgTr;

    [Header("��ųâ")]
    public Transform skillTr;
    public Transform spetialTr;
    public TMP_Text skillText;
    public TMP_Text spetialText;


    [Header("ĳ���� ����")]
    // ĳ���� �̸� �ؽ�Ʈ
    public TMP_Text charName;
    // ĳ���� ���� �ؽ�Ʈ
    public TMP_Text hp;
    public TMP_Text att;
    public TMP_Text def;

    private TextAsset json;
    private string jsonData;

    private CharacterDatabase charData;

    // �������� Ȱ��ȭ �� ĳ���� �̹���
    private Image character;

    void Start()
    {
        closeInfoButton.onClick.AddListener(() => OnClick_CloseInfoBtn());
        skillButton.onClick.AddListener(() => OnClick_SkillBtn());
        statusButton.onClick.AddListener(() => OnClick_StatusBtn());
        identityButton.onClick.AddListener(() => OnClick_IdentityBtn());

        json = Resources.Load<TextAsset>("CharacterData");
        jsonData = json.text;

        charData = GameObject.Find("GameManager").GetComponent<CharacterDatabase>();
    }

    /// <summary>
    /// �κ��丮���� �ش� ĳ���͸� Ŭ���� ������â�� Ȱ��ȭ��.
    /// </summary>
    public void OnCharacterInfo(Image copyImg, int num)
    {
        var data = JSON.Parse(jsonData);
        charInfo.SetActive(true);

        // �� �Լ��� ȣ���� ĳ������ ��ȣ�� �´� �����͸� �����´�.
        charData = new CharacterDatabase(num-1, data);
        character = copyImg;
        character = Instantiate(character, imgTr);
        InitRectSize(character);

        charName.text = charData.charName;
        hp.text = "ü�� : " + charData.maxHP;
        att.text = "���ݷ� : " + charData.damage;
        def.text = "���� : " + charData.defense;


        Destroy(character.GetComponent<ViewCharacterInfo>());
    }

    /// <summary>
    /// ��Ʈ��ġ�� ����� �̹����� ����� �θ� ��ü�� ����� ����.
    /// </summary>
    /// <param name="img"></param>
    private void InitRectSize(Image img)
    {
        img.rectTransform.offsetMin = Vector2.zero;
        img.rectTransform.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// ������â�� ����
    /// �κ��丮�� �ִ� ĳ���͸� �����Ͽ� �����Ա� ������
    /// â�� ���� �� �ش� �̹����� �ı��Ѵ�.
    /// </summary>
    private void OnClick_CloseInfoBtn()
    {
        Destroy(character);
        charInfo.SetActive(false);
    }

    /// <summary>
    /// ��ųâ�� ����.
    /// </summary>
    private void OnClick_SkillBtn()
    {
        skillImg.SetActive(true);
        statusImg.SetActive(false);
        identityImg.SetActive(false);
    }

    /// <summary>
    /// �������ͽ�â�� ����.
    /// </summary>
    private void OnClick_StatusBtn()
    {
        statusImg.SetActive(true);

        skillImg.SetActive(false);
        identityImg.SetActive(false);
    }

    /// <summary>
    /// Ư��â�� ����.
    /// </summary>
    private void OnClick_IdentityBtn()
    {
        identityImg.SetActive(true);

        skillImg.SetActive(false);
        statusImg.SetActive(false);
    }
}
