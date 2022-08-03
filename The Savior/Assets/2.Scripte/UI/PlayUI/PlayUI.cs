using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    // Play씬 UI
    public GameObject topBar;
    public GameObject partyBar;
    public GameObject dungeonBar;
    public GameObject questUI;

    /// <summary>
    /// 던전 UI를 On, Off시키는함수.
    /// </summary>
    /// <param name="isDungeon">
    /// 해당 UI를 활성화 시킬것인가?
    /// </param>
    public void OnClick_DungeonBarBtn(bool isDungeon)
    {
        isDungeon = !isDungeon;
        dungeonBar.SetActive(isDungeon);
    }
    /// <summary>
    /// TopBar를 On, Off 시키는 함수.
    /// </summary>
    /// <param name="isTopBar">
    /// 해당 UI를 활성화 시킬것인가?
    /// </param>
    public void OnClick_TopBarBtn(bool isTopBar)
    {
        isTopBar = !isTopBar;
        topBar.SetActive(isTopBar);
    }

    /// <summary>
    /// PartyBar를 On, Off시키는 함수.
    /// </summary>
    /// <param name="isPartyBar">
    /// 해당 UI를 활성화 시킬것인가?
    /// </param>
    public void OnClick_PartyBarBtn(bool isPartyBar)
    {
        isPartyBar = !isPartyBar;
        partyBar.SetActive(isPartyBar);
    }
}
