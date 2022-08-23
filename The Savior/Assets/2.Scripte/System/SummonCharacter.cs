using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonCharacter : MonoBehaviour
{

    // ĳ������ ��
    public int input = 5;
    // ����
    private int rnd;
    public bool ishave = false;
    int id;
    // ��ȯ�� ĳ������ ������Ʈ
    private GameObject character;
    private Image charImg;
    // ��ȯ�� ĳ���Ͱ� ��ġ�� ������
    public Transform summonCharTr; // CharacterTransform
    private Transform charInventoryTr;

    // ĳ���� �ִ�ġ ��ȯ ���
    public GameObject warningImg;
    public Button warningButton;
    public Button closeWarning;

    private void Start()
    {
        warningButton.onClick.AddListener(() => OnClick_WarningSummonBtn());
        closeWarning.onClick.AddListener(() => OnClick_WarningSummonBtn());
    }

    /// <summary>
    /// ĳ���� ��ȯ ��ư�� ������ ȣ��Ǵ� �Լ�.
    /// ������ ���� ������ ������ ���� �� �ش� ��ȣ�� �´� ĳ���͸� ��ȯ�Ѵ�.
    /// ���� �����ϸ� �˾�â�� Ȱ��ȭ
    /// </summary>
    /// <returns></returns>
    public void SummonRandom(int price)
    {
        if (GameManager.instance.data.golds < price)
        {
            Debug.Log(GameManager.instance.data.golds);
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
        id = DuplicateSummon();

        if( id == 0)
        {
            return;
        }

        GameManager.instance.data.golds -= price;

        character = Resources.Load<GameObject>("Unit/Character" + id.ToString());
        character = Instantiate(character, summonCharTr);

        // ĳ���� ���� ���� ����
        GameManager.instance.GameSave();

        // ȹ���� ĳ������ ������ ����.
        StartCoroutine(GameManager.instance.SaveCharExp(character.GetComponent<UnitInfo>().unitNumber));
    }


    /// <summary>
    /// ĳ���� �ߺ� Ȯ��.
    /// �̹� �ִ� ĳ���ʹ� ��ȯ ���� ����.
    /// </summary>
    /// <returns></returns>
    public int DuplicateSummon()
    {
        // ĳ���� ����
        rnd = Random.Range(1, input);
        
        for(int i = 0; i < GameManager.instance.maxCharacterCount; i++)     // �ִ� ĳ���� ����ŭ �ݺ��ؼ� 0�� ���� ���.
        {
            if(GameManager.instance.data.haveCharacter[i] == 0)
            {
                while (GameManager.instance.data.haveCharacter.Contains(rnd))       // ���� ĳ���Ͱ� ���� �� ���� �ݺ�, rnd���� 0�� ������ �ʴ´�.
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
