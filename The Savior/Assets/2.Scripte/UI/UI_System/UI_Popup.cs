using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Popup : UI_Base
{
    protected override void Init()
    {
        Debug.Log("Create Popup !");
    }


    /*
     * 사용법.
     * == BindObjects ==
     * enum 타입에 실제 하이어라키에 존재하는 오브젝트의 이름을 넣는다.
     * Bind에 바인딩 하려는 오브젝트의 타입을 넣고, 인자로 enum의 타입을 넘긴다.
     * enum{ BGImage, TitleImage, ..., }
     * ex) Bind<Image>(Images);
     * 
     * == SetObjects ==
     * List<Image> image = new List<Image>();
     * image = Get<Image>(typeof(Images));
     * 
     */
    // 참조할 오브젝트를 바인드한다.
    abstract protected void BindObjects();
    // 바인드한 오브젝트를 리스트에 넣어 관리한다.
    abstract protected void SetObjects();
}
