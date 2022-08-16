using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardEvent : MonoBehaviour
{
    #region CardTest에 있던 내용
    public int number = 0;
    public int idx = 3;
    void Awake()
    {
        idx = number + 1;
    }
    #endregion

    /// <summary>
    /// 카드 이벤트를 가지고 있는 카드를 구분하는 넘버링
    /// </summary>
    public int card_number;
    public int card_handnumber;
    public int cost;
    private Button cardButton;

    private EventTrigger eventTrigger;
    private EventTrigger.Entry eventMouseEnter;
    private EventTrigger.Entry eventMouseExit;

    private void Start()
    {
        if(GameManager.instance.dungeonOS != null)
        {
            Destroy(GetComponent<ViewCard>());
            cardButton = gameObject.AddComponent<Button>();
            cardButton.onClick.AddListener(() => OnClick_CardBtn());

            if(!TryGetComponent<EventTrigger>(out eventTrigger))
            {
                eventTrigger = gameObject.AddComponent<EventTrigger>();
            }
            eventMouseEnter.eventID = EventTriggerType.PointerEnter;
            eventMouseExit.eventID = EventTriggerType.PointerExit;
            eventMouseEnter.callback.AddListener((data) => { EventMouseEnter(); });
            eventMouseExit.callback.AddListener((data) => { EventMouseExit(); });
            eventTrigger?.triggers.Add(eventMouseExit);
            eventTrigger?.triggers.Add(eventMouseEnter);
        }
    }
    /// <summary>
    /// 던전 내에서만 작동하며, 카드가 선택됬을때 스킬이 발동되며 후처리까지 하는 기능
    /// </summary>
    public void OnClick_CardBtn()
    {
        if(GameManager.instance.dungeonOS != null) // 던전내에서 사용중인지 검사 
        {
            if(CardSkill.instance.UseCard(card_number)) // 카드가 사용되면 ture를 반환함
            {
                // 카드사용 이펙트가 있다면 이구간에서 적용
                CardEffect();
                Destroy(gameObject);
                // [작업 할것]  핸드 카드 수량 검사 // 드로우기능 작동
                DungeonOS.instance.handSlot[card_handnumber] = false;
            }
            // 사용되지않으면 아무것도 하지않음. 텍스트 출력을 검토함.  
        }
    }

    public void CardEffect()
    {
        //효과 없음 ..있다면 채우는곳...
    }

    private void EventMouseEnter()
    {
        DungeonOS.instance.HandUIReset();
        DungeonController.instance.playerExpectationsGauage.fillAmount = cost / 10;
    }

    private void EventMouseExit()
    {
        DungeonController.instance.playerExpectationsGauage.fillAmount = DungeonController.instance.playerCostGauage.fillAmount;
    }
}
