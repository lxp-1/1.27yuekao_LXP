using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ui基类
/// </summary>
public class UIBase : MonoBehaviour
{
    //虚方法
    public virtual void Init() { }
    public virtual void Show() { gameObject.SetActive(true); }
    public virtual void Hide() { gameObject.SetActive(false); }
}
