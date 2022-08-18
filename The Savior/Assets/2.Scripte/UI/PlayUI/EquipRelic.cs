using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipRelic : MonoBehaviour
{
    [Header("������ ��ġ")]
    public Transform relicInventoryTr;
    public Transform equipTr;

    private Image equipRelic;
    private Image selectImg;

    private Relic relic;

    IEnumerator Start()
    {
        relic = GameObject.Find("PUIManager").GetComponent<Relic>();
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < GameManager.instance.data.equipRelic.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.data.haveRelic.Count; j++)
            {
                if (relicInventoryTr.GetChild(j).GetChild(0).GetComponent<ViewRelic>().number == GameManager.instance.data.equipRelic[i])
                {

                    equipRelic = Resources.Load<Image>("Relic/Relic_" + GameManager.instance.data.equipRelic[i].ToString());
                    equipRelic = Instantiate(equipRelic, equipTr.GetChild(i).GetChild(0).GetComponent<Transform>());
                    relic.relicNum[i] = GameManager.instance.data.equipRelic[i];


                    Destroy(equipRelic.GetComponent<ViewRelic>());

                    selectImg = Resources.Load<Image>("Relic/SelectRelic");
                    selectImg = Instantiate(selectImg, relicInventoryTr.GetChild(j));
                    relic.selectTr[i] = selectImg.transform.parent;

                    InitRect(equipRelic);
                    break;
                }
            }
        }
    }

    private void InitRect(Image img)
    {
        img.rectTransform.offsetMax = Vector2.zero;
        img.rectTransform.offsetMin = Vector2.zero;
    }
}

/*
 * �������� ���� �����͸� �����´�.
 * �ش� ������ ���� ��Ű��, �κ��丮���� ���� ǥ�ø� �Ѵ�.
 */