using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Popup : UI_Base
{
    protected abstract override void Init();

    /*
     * ����.
     * == BindObjects ==
     * enum Ÿ�Կ� ���� ���̾��Ű�� �����ϴ� ������Ʈ�� �̸��� �ִ´�.
     * Bind�� ���ε� �Ϸ��� ������Ʈ�� Ÿ���� �ְ�, ���ڷ� enum�� Ÿ���� �ѱ��.
     * enum{ BGImage, TitleImage, ..., }
     * ex) Bind<Image>(Images);
     * 
     * == SetObjects ==
     * List<Image> image = new List<Image>();
     * image = Get<Image>(typeof(Images));
     * 
     */
    // ������ ������Ʈ�� ���ε��Ѵ�.
    abstract protected void BindObjects();
    // ���ε��� ������Ʈ�� ����Ʈ�� �־� �����Ѵ�.
    abstract protected void SetObjects();
}
