using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySettingManager : MonoBehaviour
{
    public Transform[] partyTr;             // 캐릭터 편성창의 위치.
    public GameObject characterInventory;   // 캐릭터 인벤토리

    // 게임 시작시에 이미 배치되어있는 캐릭터들의 데이터를 가져와 배치 될 이미지
    private Image character;

    // 편성창을 눌러 캐릭터 인벤토리를 활성화 시켰는가?
    public bool isPartySettingMode = false;

    // 현재 선택한 파티 편성창의 번호. 0 ~ 3
    private int idx;

    private void Start()
    {
        for (int i = 0; i < GameManager.instance.data.equipCharacter.Length; i++)
        {
            if (GameManager.instance.data.equipCharacter[i] != 0)
            {
                character = Resources.Load<Image>("Unit/Image/Character_" + GameManager.instance.data.equipCharacter[i].ToString());
                character = Instantiate(character, partyTr[i]);
                InitRect(character);
                Destroy(character.GetComponent<ViewCharacterInfo>());
            }
        }
    }


    // 파티 편성창을 누를 때 호출되는 함수, 해당 편성창의 인덱스를 받고 파티 세팅 모드로 인벤토리를 오픈한다.
    public void OnPartySettingMode(int index)
    {
        characterInventory.SetActive(true);
        isPartySettingMode = true;
        idx = index;
    }

    // 파티 세팅 모드로 인벤토리를 오픈 했을 때 캐릭터를 클릭하면 호출되는 함수.
    public void OnPartySetting(Image img)
    {
        Image copy = Instantiate(img);

        // 이미 배치된 캐릭터가 같은 캐릭터일 경우 
        // 배치된 캐릭터를 파괴하고, 저장된 데이터를 0으로 바꾼다.
        if (GameManager.instance.data.equipCharacter[idx] == copy.GetComponent<ViewCharacterInfo>().num)
        {
            GameManager.instance.data.equipCharacter[idx] = 0;
            Destroy(copy.gameObject);
            Destroy(partyTr[idx].GetChild(0).gameObject);
            characterInventory.SetActive(false);
            return;
        }


        // 이미 해당 캐릭터가 장착되어 있다면 작착되어 있는 캐릭터를 파괴하고 데이터에 저장된 값을 0으로 바꾼다.
        for (int i = 0; i < GameManager.instance.data.equipCharacter.Length; i++)
        {

            if (GameManager.instance.data.equipCharacter[i] == copy.GetComponent<ViewCharacterInfo>().num)
            {
                GameManager.instance.data.equipCharacter[i] = 0;
                Destroy(partyTr[i].GetChild(0).gameObject);
                break;
            }
        }

        
        // 현재 장착된 캐릭터가 없다면...
        // 해당 인덱스에 캐릭터를 배치하고 데이터를 저장한다.
        if (partyTr[idx].childCount == 0)
        {
            copy.transform.SetParent(partyTr[idx]);
            InitRect(copy);
            GameManager.instance.data.equipCharacter[idx] = copy.GetComponent<ViewCharacterInfo>().num;
            Destroy(copy.GetComponent<ViewCharacterInfo>());
        }
        // 현재 장착된 캐릭터가 있다면...
        // 해당 인덱스에 배치된 캐릭터를 파괴하고 해당 위치에 캐릭터를 저장한다.
        else if (partyTr[idx].childCount > 0)
        {
            Destroy(partyTr[idx].GetChild(0).gameObject);
            copy.transform.SetParent(partyTr[idx]);
            InitRect(copy);
            GameManager.instance.data.equipCharacter[idx] = copy.GetComponent<ViewCharacterInfo>().num;
            Destroy(copy.GetComponent<ViewCharacterInfo>());
        }
        characterInventory.SetActive(false);
        //GameManager.instance.GameSave();
    }
    //
    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = Vector2.zero;
        img.rectTransform.offsetMin = Vector2.zero;
    }
}