using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonCharacter : MonoBehaviour
{

    // 캐릭터의 수
    public int input = 5;
    // 난수
    private int rnd;
    public bool ishave = false;
    int id;
    // 소환될 캐릭터의 오브젝트
    private GameObject character;
    private Image charImg;
    // 소환된 캐릭터가 배치될 포지션
    public Transform summonCharTr; // CharacterTransform
    private Transform charInventoryTr;

    // 캐릭터 최대치 소환 경고
    public GameObject warningImg;
    public Button warningButton;
    public Button closeWarning;

    private PlayToolBar tools;

    private void Start()
    {
        warningButton.onClick.AddListener(() => OnClick_WarningSummonBtn());
        closeWarning.onClick.AddListener(() => OnClick_WarningSummonBtn());
        tools = GameObject.Find("PUIManager").GetComponent<PlayToolBar>();
    }

    /// <summary>
    /// 캐릭터 소환 버튼을 누르면 호출되는 함수.
    /// 정해진 범위 내에서 난수를 만든 뒤 해당 번호에 맞는 캐릭터를 소환한다.
    /// 돈이 부족하면 팝업창을 활성화
    /// </summary>
    /// <returns></returns>
    public void SummonRandom(int price)
    {
        if (GameManager.instance.data.golds < price)
        {
            Debug.Log(GameManager.instance.data.golds);
            Debug.Log("골드가 부족합니다.");
            return;
        }
        id = DuplicateSummon();

        if( id == 0)
        {
            return;
        }

        GameManager.instance.data.golds -= price;
        tools.Gold();

        character = Resources.Load<GameObject>("Unit/Character" + id.ToString());
        character = Instantiate(character, summonCharTr);

        // 캐릭터 소유 유무 저장
        GameManager.instance.GameSave();

        // 획득한 캐릭터의 데이터 저장.
        StartCoroutine(GameManager.instance.SaveCharExp(character.GetComponent<UnitInfo>().unitNumber));
    }


    /// <summary>
    /// 캐릭터 중복 확인.
    /// 이미 있는 캐릭터는 소환 되지 않음.
    /// </summary>
    /// <returns></returns>
    public int DuplicateSummon()
    {
        // 캐릭터 생성
        rnd = Random.Range(1, input);
        
        for(int i = 0; i < GameManager.instance.maxCharacterCount; i++)     // 최대 캐릭터 수만큼 반복해서 0이 있을 경우.
        {
            if(GameManager.instance.data.haveCharacter[i] == 0)
            {
                while (GameManager.instance.data.haveCharacter.Contains(rnd))       // 없는 캐릭터가 나올 때 까지 반복, rnd에서 0은 나오지 않는다.
                {
                    rnd = Random.Range(1, (GameManager.instance.maxCharacterCount + 1));
                }
                GameManager.instance.data.haveCharacter[i] = rnd;
                return rnd;
            }
        }
        warningImg.SetActive(true);
        ishave = true;
        return 0;
    }

    public void InitSummon()
    {
        if (summonCharTr.childCount > 0)
        {
            Destroy(summonCharTr.GetChild(0).gameObject);
        }
    }

    private void OnClick_WarningSummonBtn()
    {
        warningImg.SetActive(false);
    }

}
