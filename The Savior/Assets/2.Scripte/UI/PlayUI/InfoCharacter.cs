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



    // 버튼
    public Button closeInfoButton;
    public Button skillButton;
    public Button statusButton;
    public Button identityButton;

    // 인포 창 이미지
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

    // 캐릭터의 스킬 이미지
    private Image skillImage;
    private Image ultimateImage;

    [Header("캐릭터 정보")]
    // 캐릭터 이름 텍스트
    public TMP_Text charName;
    // 캐릭터 스탯 텍스트
    public TMP_Text hp;         // 체력           hp
    public TMP_Text att;        // 공격력         attackPower
    public TMP_Text def;        // 방어력         defense
    public TMP_Text AS;         // 공격 속도      attackSpeed
    public TMP_Text AR;         // 공격 범위      attackRange
    public TMP_Text MS;         // 이동 속도      moveSpeed
    private int attackType;
    private string attackTypeToString;


    // 상세정보에 활성화 된 캐릭터 이미지
    private Image character;

    void Start()
    {
        closeInfoButton.onClick.AddListener(() => OnClick_CloseInfoBtn());
        skillButton.onClick.AddListener(() => OnClick_SkillBtn());
        statusButton.onClick.AddListener(() => OnClick_StatusBtn());
        identityButton.onClick.AddListener(() => OnClick_IdentityBtn());

        textAsset_infoData = Resources.Load<TextAsset>("CharacterDB/CharacterInfoData");
        textAsset_name = Resources.Load<TextAsset>("CharacterDB/CharacterNameData");
        textAsset_skill = Resources.Load<TextAsset>("CharacterDB/SkillTextData");
        textAsset_ultimate = Resources.Load<TextAsset>("CharacterDB/UltimateTextData");
        textInfoData = JArray.Parse(textAsset_infoData.text);
        textNameData = JArray.Parse(textAsset_name.text);
        textSkillData = JArray.Parse(textAsset_skill.text);
        textUltimateData = JArray.Parse(textAsset_ultimate.text);

        OnClick_SkillBtn();

    }

    /// <summary>
    /// 인벤토리에서 해당 캐릭터를 클릭시 상세정보창이 활성화됨.
    /// </summary>
    public void OnCharacterInfo(Image copyImg, int num)
    {
        charInfo.SetActive(true);

        // 이 함수를 호출한 캐릭터의 번호에 맞는 데이터를 가져온다.
        //charData = new CharacterDatabase(num-1); // 이전
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
                        attackTypeToString = "탱커";
                        break;
                    case 1:
                        attackTypeToString = "근거리 딜러";
                        break;
                    case 2:
                        attackTypeToString = "원거리 딜러";
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
                hp.text = "체력 : " + textInfoData[num - 1]["Hp_Total"].ToString();
                att.text = "공격력 : " + textInfoData[num - 1]["Chr_Power"].ToString();
                def.text = "방어력 : " + textInfoData[num - 1]["Chr_DF"].ToString();
                AS.text = "공격 속도 : " + textInfoData[num - 1]["Chr_AtkSpeed"].ToString();
                AR.text = "공격 범위 : " + textInfoData[num - 1]["Chr_AtkRange"].ToString();
                MS.text = "이동 속도 : " + textInfoData[num - 1]["Chr_MS"].ToString();

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
