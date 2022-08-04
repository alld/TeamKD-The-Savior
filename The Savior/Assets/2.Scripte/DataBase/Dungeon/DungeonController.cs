using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonController : MonoBehaviour
{
    public static DungeonController instance = null; //�̱���
    #region UI ȯ�溯��
    public Image[] partySlotHPGauage; // ��Ƽ�� ü�¹�
    public Image[] partySlotNomalSkillCooldown;// �Ϲݽ�ų ��ٿ� ǥ��
    public Image[] partySlotSpecialCooldown; // �ñر� ��ٿ� ǥ��
    public Button[] partySlotActiveSkillButton; // ��ų����ư
    public GameObject[] partySlotDieImage; // ���ó�� UI
    public Image playerCostGauage; //���� ������
    public Image playerExpectationsGauage; // ���ġ��
    public GameObject[] gameTimerBG; //Ÿ�̸� ���
    public GameObject gameTimerArrow; //�ð�ٴ�
    public GameObject gameRoundbarArrow; //���� ǥ�ùٴ�
    public GameObject[] gameRoundbarPoint; //���� ǥ�ù� ��ġ
    public GameObject[] gameRoundDisplayIcon; //�� ���� ������
    public GameObject[] gameRoundDisplayPos; // ���� ǥ����ġ
    public GameObject dungeonUI; // ���� UI
    public GameObject[] cardSlot;
    #endregion

    private void Awake()
    {
        instance = this;
    }


}
