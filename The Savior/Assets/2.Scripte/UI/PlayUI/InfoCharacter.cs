using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Newtonsoft.Json.Linq;

public class InfoCharacter : MonoBehaviour
{
    TextAsset textAsset_infoData;
    TextAsset textAsset_name;
    TextAsset textAsset_skill;
    TextAsset textAsset_ultimate;
    JArray textInfoData;
    JArray textNameData;
    JArray textSkillData;
    JArray textUltimateData;


    StringBuilder skillBuilder = new StringBuilder();
    StringBuilder ultimateBuilder = new StringBuilder();

    // ��ư
    [Header("��ư")]
    public Button closeInfoButton;
    public Button skillButton;
    public Button statusButton;
    public Button identityButton;
    public Button levelUpButton;

    [Space(10.0f)]
    
    // ���� â �̹���
    [Header("�̹���")]
    public GameObject charInfo; // : CharacterInfo Image  
    public GameObject skillImg;
    public GameObject statusImg;
    public GameObject identityImg;

    // �̹��� ��ġ
    public Transform imgTr;

    [Space(10.0f)]

    [Header("��ųâ")]
    public Transform skillTr;
    public Transform spetialTr;
    public TMP_Text skillText;
    public TMP_Text spetialText;

    // ĳ������ ��ų �̹���
    private Image skillImage;
    private Image ultimateImage;

    [Space(10.0f)]

    [Header("ĳ���� ����")]
    // ĳ���� �̸� �ؽ�Ʈ
    public TMP_Text charName;
    // ĳ���� ���� �ؽ�Ʈ
    public TMP_Text hp;         // ü��           hp
    public TMP_Text att;        // ���ݷ�         attackPower
    public TMP_Text def;        // ����         defense
    public TMP_Text AS;         // ���� �ӵ�      attackSpeed
    public TMP_Text AR;         // ���� ����      attackRange
    public TMP_Text MS;         // �̵� �ӵ�      moveSpeed
    private int attackType;
    private string attackTypeToString;

    [Space(10.0f)]
    [Header("���� ĳ���� ��ȣ")]
    public int currentCharacterNumber = 0;
    public TMP_Text level;
    public Image expBar;

    [Header("������ ������ �˾� ��ġ")]
    public Transform levelUpTr;
    private GameObject levelUpScreen;

    // �������� Ȱ��ȭ �� ĳ���� �̹���
    private Image character;

    void Start()
    {
        textAsset_infoData = Resources.Load<TextAsset>("CharacterDB/CharacterInfoData");
        textAsset_name = Resources.Load<TextAsset>("CharacterDB/CharacterNameData");
        textAsset_skill = Resources.Load<TextAsset>("CharacterDB/SkillTextData");
        textAsset_ultimate = Resources.Load<TextAsset>("CharacterDB/UltimateTextData");
        levelUpScreen = Resources.Load<GameObject>("LevelUpScreen");
        textInfoData = JArray.Parse(textAsset_infoData.text);
        textNameData = JArray.Parse(textAsset_name.text);
        textSkillData = JArray.Parse(textAsset_skill.text);
        textUltimateData = JArray.Parse(textAsset_ultimate.text);

        closeInfoButton.onClick.AddListener(() => OnClick_CloseInfoBtn());
        skillButton.onClick.AddListener(() => OnClick_SkillBtn());
        statusButton.onClick.AddListener(() => OnClick_StatusBtn());
        identityButton.onClick.AddListener(() => OnClick_IdentityBtn());
        levelUpButton.onClick.AddListener(() => OnClick_CreateLevelUpScreenBtn());

        OnClick_SkillBtn();
    }

    /// <summary>
    /// �κ��丮���� �ش� ĳ���͸� Ŭ���� ������â�� Ȱ��ȭ��.
    /// </summary>
    public void OnCharacterInfo(Image copyImg, int num)
    {
        charInfo.SetActive(true);
        currentCharacterNumber = num;
        // �� �Լ��� ȣ���� ĳ������ ��ȣ�� �´� �����͸� �����´�.
        //charData = new CharacterDatabase(num-1); // ����
        character = copyImg;
        character = Instantiate(character, imgTr);
        InitRectSize(character);

        skillImage = Resources.Load<Image>("Unit/Skill/Skill_" + num);
        ultimateImage = Resources.Load<Image>("Unit/Skill/Ultimate_" + num);
        skillImage = Instantiate(skillImage, skillTr);
        ultimateImage = Instantiate(ultimateImage, spetialTr);


        attackType = textInfoData[num - 1]["Attack_Type"].ToObject<int>();

        switch (GameManager.instance.data.Language)
        {
            case 0:
                switch (attackType)
                {
                    case 0:
                        attackTypeToString = "��Ŀ";
                        break;
                    case 1:
                        attackTypeToString = "�ٰŸ� ����";
                        break;
                    case 2:
                        attackTypeToString = "���Ÿ� ����";
                        break;
                    default:
                        break;
                }


                skillBuilder.Clear();
                ultimateBuilder.Clear();

                skillBuilder.Append(textSkillData[num-1]["Name"]);
                skillBuilder.Append("\n");
                skillBuilder.Append("\n");
                skillBuilder.Append(textSkillData[num-1]["Content_1"]);
                skillBuilder.Append("\n");
                skillBuilder.Append(textSkillData[num - 1]["Content_2"]);
                    
                skillText.text = skillBuilder.ToString();               

                ultimateBuilder.Append(textUltimateData[num-1]["Name"]);
                ultimateBuilder.Append("\n");
                ultimateBuilder.Append("\n");
                ultimateBuilder.Append(textUltimateData[num - 1]["Content_1"]);
                ultimateBuilder.Append("\n");
                ultimateBuilder.Append(textUltimateData[num - 1]["Content_2"]);

                spetialText.text = ultimateBuilder.ToString();

                charName.text = textNameData[num - 1]["Name_Kr"].ToString() + " / " + attackTypeToString;
                hp.text = "ü�� : " + textInfoData[num - 1]["Hp_Total"].ToString();
                att.text = "���ݷ� : " + textInfoData[num - 1]["Chr_Power"].ToString();
                def.text = "���� : " + textInfoData[num - 1]["Chr_DF"].ToString();
                AS.text = "���� �ӵ� : " + textInfoData[num - 1]["Chr_AtkSpeed"].ToString();
                AR.text = "���� ���� : " + textInfoData[num - 1]["Chr_AtkRange"].ToString();
                MS.text = "�̵� �ӵ� : " + textInfoData[num - 1]["Chr_MS"].ToString();

                break;
            case 1:
                switch (attackType)
                {
                    case 0:
                        attackTypeToString = "Tanker";
                        break;
                    case 1:
                        attackTypeToString = "Short-Range Dealer";
                        break;
                    case 2:
                        attackTypeToString = "Long-Range Dealer";
                        break;
                    default:
                        break;
                }


                skillBuilder.Clear();
                ultimateBuilder.Clear();

                skillBuilder.Append(textSkillData[num - 1]["Name_ENG"]);
                skillBuilder.Append("\n");
                skillBuilder.Append("\n");
                skillBuilder.Append(textSkillData[num - 1]["Content_1_ENG"]);
                skillBuilder.Append("\n");
                skillBuilder.Append(textSkillData[num - 1]["Content_2_ENG"]);

                skillText.text = skillBuilder.ToString();

                ultimateBuilder.Append(textUltimateData[num - 1]["Name_ENG"]);
                ultimateBuilder.Append("\n");
                ultimateBuilder.Append("\n");
                ultimateBuilder.Append(textUltimateData[num - 1]["Content_1_ENG"]);
                ultimateBuilder.Append("\n");
                ultimateBuilder.Append(textUltimateData[num - 1]["Content_2_ENG"]);

                spetialText.text = ultimateBuilder.ToString();

                charName.text = textNameData[num - 1]["Name_Eng"].ToString() + " / " + attackTypeToString;
                hp.text = "HP : " + textInfoData[num - 1]["Hp_Total"].ToString();
                att.text = "AttackPower : " + textInfoData[num - 1]["Chr_Power"].ToString();
                def.text = "Defense : " + textInfoData[num - 1]["Chr_DF"].ToString();
                AS.text = "AttackSpeed : " + textInfoData[num - 1]["Chr_AtkSpeed"].ToString();
                AR.text = "AttackRange : " + textInfoData[num - 1]["Chr_AtkRange"].ToString();
                MS.text = "MoveSpeed : " + textInfoData[num - 1]["Chr_MS"].ToString();


                break;
            default:
                break;
        }

        LevelSystem();
        Destroy(character.GetComponent<ViewCharacterInfo>());
    }

    private void LevelSystem()
    {
        expBar.fillAmount = GameManager.instance.charExp[currentCharacterNumber-1].exp * 0.01f; ;
        level.text = GameManager.instance.charExp[currentCharacterNumber-1].level.ToString();
    }

    private void OnClick_CreateLevelUpScreenBtn()
    {
        levelUpScreen = Instantiate(levelUpScreen, levelUpTr);
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

    private void OnClick_LevelUpScreenBtn()
    {

    }
}
