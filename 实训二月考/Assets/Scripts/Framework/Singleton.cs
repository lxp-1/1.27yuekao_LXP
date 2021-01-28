using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : new()
{
    private static T _GetT;
    public static T GetT
    {
        get
        {
            if (_GetT == null)
            {
                _GetT = new T();
            }
            return _GetT;
        }
    }
}
