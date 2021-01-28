using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ui管理类
/// </summary>
public class UIManager : Singleton<UIManager>
{
    //canvas所有UI的父级
    public Canvas canvas;
    public Camera uiCamera;
    public UIManager()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            uiCamera = canvas.transform.Find("uiCamera").GetComponent<Camera>();
            GameObject.DontDestroyOnLoad(canvas);
        }
    }

    //添加到字典  方便管理
    Dictionary<PanelName, UIBase> uiDic = new Dictionary<PanelName, UIBase>();

    //打开ui面板
    public UIBase OpenUI(PanelName name)
    {
        UIBase temp = LoadUIBase(name);
        temp.Show();
        return temp;
    }

    //加载ui面板
    private UIBase LoadUIBase(PanelName name)
    {
        UIBase temp;
        if (!uiDic.TryGetValue(name, out temp))
        {
            GameObject res = ResManager.GetT.Load<GameObject>(ResPath.panel, name.ToString());
            if (res != null)
            {
                GameObject game = GameObject.Instantiate(res, canvas.transform, false);
                temp = game.GetComponent<UIBase>();
                if (temp != null)
                {
                    uiDic.Add(name, temp);
                    uiDic[name].Init();
                }
            }
        }

        return temp;

    }

    //关闭UI面板
    public void CloseUI(PanelName name)
    {
        UIBase temp;
        if (uiDic.TryGetValue(name, out temp))
        {
            temp.Hide();
        }
    }
}
