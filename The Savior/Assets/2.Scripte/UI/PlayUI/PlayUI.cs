using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    // Play�� UI
    public GameObject topBar;
    public GameObject partyBar;
    public GameObject dungeonBar;
    public GameObject questUI;

    /// <summary>
    /// ���� UI�� On, Off��Ű���Լ�.
    /// </summary>
    /// <param name="isDungeon">
    /// �ش� UI�� Ȱ��ȭ ��ų���ΰ�?
    /// </param>
    public void OnClick_DungeonBarBtn(bool isDungeon)
    {
        isDungeon = !isDungeon;
        dungeonBar.SetActive(isDungeon);
    }
    /// <summary>
    /// TopBar�� On, Off ��Ű�� �Լ�.
    /// </summary>
    /// <param name="isTopBar">
    /// �ش� UI�� Ȱ��ȭ ��ų���ΰ�?
    /// </param>
    public void OnClick_TopBarBtn(bool isTopBar)
    {
        isTopBar = !isTopBar;
        topBar.SetActive(isTopBar);
    }

    /// <summary>
    /// PartyBar�� On, Off��Ű�� �Լ�.
    /// </summary>
    /// <param name="isPartyBar">
    /// �ش� UI�� Ȱ��ȭ ��ų���ΰ�?
    /// </param>
    public void OnClick_PartyBarBtn(bool isPartyBar)
    {
        isPartyBar = !isPartyBar;
        partyBar.SetActive(isPartyBar);
    }
}
