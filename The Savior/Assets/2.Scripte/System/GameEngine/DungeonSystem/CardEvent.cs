using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : MonoBehaviour
{
    #region CardTest�� �ִ� ����
    public int number = 0;
    public int idx = 3;
    void Awake()
    {
        idx = number + 1;
    }
    #endregion

    /// <summary>
    /// ī�� �̺�Ʈ�� ������ �ִ� ī�带 �����ϴ� �ѹ���
    /// </summary>
    public int card_number;
    
    /// <summary>
    /// ���� �������� �۵��ϸ�, ī�尡 ���É����� ��ų�� �ߵ��Ǹ� ��ó������ �ϴ� ���
    /// </summary>
    public void OnClick_CardBtn()
    {
        if(GameManager.instance.dungeonOS != null) // ���������� ��������� �˻� 
        {
            if(CardSkill.instance.UseCard(card_number)) // ī�尡 ���Ǹ� ture�� ��ȯ��
            {
                // ī���� ����Ʈ�� �ִٸ� �̱������� ����
                CardEffect();
                Destroy(gameObject);
                // [�۾� �Ұ�]  �ڵ� ī�� ���� �˻� // ��ο��� �۵�
            }
            // ������������ �ƹ��͵� ��������. �ؽ�Ʈ ����� ������.  
        }
    }

    public void CardEffect()
    {
        //ȿ�� ���� ..�ִٸ� ä��°�...
    }
    


}