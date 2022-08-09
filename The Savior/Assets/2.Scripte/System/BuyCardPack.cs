using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;
public class BuyCardPack : MonoBehaviour
{
    private TextAsset jsonData;
    private string json;
    public Transform shopCardPackTr;

    private int maxCardPack = 5;
    private List<GameObject> cardPackList = new List<GameObject>();
    private GameObject thisObject;
    private TextMeshProUGUI packName;
    private TextMeshProUGUI packContant;
    public Button[] selectPackButton;

    private void Start()
    {
        selectPackButton = new Button[maxCardPack];

        for (int i = 0; i < maxCardPack; i++)
        {
            // ���̽� �����͸� �����Ͽ� ���.
            // ���߿� �ʿ������� ����.
            //jsonData = Resources.Load<TextAsset>("CardPack");
            //json = jsonData.text;
            //var data = JSON.Parse(json);

            int idx = i + 1;
            cardPackList.Add(Resources.Load<GameObject>("PackItem"));
            packName = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[0];
            packContant = cardPackList[i].GetComponentsInChildren<TextMeshProUGUI>()[1];

            packName.text = idx.ToString() + "�� ī����";        // ī���� �̸�
            packContant.text = "ī���� ����";        // ī���� ����

            thisObject = Instantiate(cardPackList[i], shopCardPackTr);  // �����տ� �ִ� ī������ ������ ����
            thisObject.gameObject.name = "Pack_" + i;                   // ������ ������Ʈ�� �̸�
            selectPackButton[i] = thisObject.GetComponentInChildren<Button>();  // ������ ������Ʈ�� ��ư ����
            selectPackButton[i].onClick.AddListener(() => OnClick_SelectPackBtn(idx));  // ������ ������Ʈ�� Ŭ���� ȣ��Ǵ� �Լ�

        }
    }

    private void OnClick_SelectPackBtn(int idx)
    {
        Debug.Log(idx + "�� ī����");
    }

}
