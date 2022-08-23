using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicInventory : MonoBehaviour
{
    [Header("유물 인벤토리 관리")]
    public Transform relicInventoryTr;

    private Image relicImg;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.instance.isSetting);

        for (int i = 0; i < GameManager.instance.data.haveRelic.Count; i++)
        {
            for (int j = 0; j < GameManager.instance.data.haveRelic.Count; j++)
            {
                if (relicInventoryTr.GetChild(j).childCount == 0)
                {
                    relicImg = Resources.Load<Image>("Relic/Relic_" + GameManager.instance.data.haveRelic[i].ToString());
                    relicImg = Instantiate(relicImg, relicInventoryTr.GetChild(j).GetComponent<Transform>());
                    InitRect(relicImg);
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
