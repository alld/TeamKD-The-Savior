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
    private int stop;

    public int Stop
    {
        get { return stop; }
        set { stop = value; }
    }

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
            Debug.Log("��尡 �����մϴ�.");
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

        // ĳ���� ���� ���� ����
        GameManager.instance.data.haveCharacter.Add(character.GetComponent<UnitInfo>().unitNumber);
        GameManager.instance.GameSave();
    }


    /// <summary>
    /// ĳ���� �ߺ� Ȯ��.
    /// �̹� �ִ� ĳ���ʹ� ��ȯ ���� ����.
    /// </summary>
    /// <returns></returns>
    public int DuplicateSummon()
    {
        int max = 0;
        // ĳ���� ����
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
