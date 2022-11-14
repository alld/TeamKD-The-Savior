using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Don'tDestroyOnLoad ���� �ʰ�, �� ������ ���, ���� �Ͽ� ����ϴ� �̱��� �Դϴ�.
 * ����� �� ����ϰ�, ������� ���� �� ������ �ϵ��� �մϴ�.
 */
public class Singleton_OneScene<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T instance
    {
        get
        {
            return null;
        }
    }
    protected void Init()
    {

    }

    protected void Regist()
    {

    }

    protected void UnRegist()
    {

    }

}
