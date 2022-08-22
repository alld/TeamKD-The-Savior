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
    private int stop;

    public int Stop
    {
        get { return stop; }
        set { stop = value; }
    }

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

    private void Start()
    {
        warningButton.onClick.AddListener(() => OnClick_WarningSummonBtn());
        closeWarning.onClick.AddListener(() => OnClick_WarningSummonBtn());
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
            Debug.Log("골드가 부족합니다.");
            return;
        }
        stop = DuplicateSummon();
        if (stop == 1)
        {
            return;
        }

        GameManager.instance.data.golds -= price;

        character = Resources.Load<GameObject>("Unit/Character" + rnd.ToString());
        character = Instantiate(character, summonCharTr);

        // 캐릭터 소유 유무 저장
        GameManager.instance.data.haveCharacter.Add(character.GetComponent<UnitInfo>().unitNumber);
        GameManager.instance.GameSave();
    }


    /// <summary>
    /// 캐릭터 중복 확인.
    /// 이미 있는 캐릭터는 소환 되지 않음.
    /// </summary>
    /// <returns></returns>
    public int DuplicateSummon()
    {
        int max = 0;
        // 캐릭터 생성
        rnd = Random.Range(1, input);


        for (int i = 0; i < GameManager.instance.data.haveCharacter.Count; i++)
        {
            if (GameManager.instance.data.haveCharacter[i] == rnd)
            {
                if (GameManager.instance.data.haveCharacter[i] == 0)
                {
                    DuplicateSummon();
                }
                else
                {
                    warningImg.SetActive(true);
                    max = 1;
                    break;
                }
            }
        }
        return max;
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
