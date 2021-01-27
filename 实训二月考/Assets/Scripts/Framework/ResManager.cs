using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : Singleton<ResManager>
{
    //添加到字典  方便管理
    Dictionary<string, UnityEngine.Object> resDic = new Dictionary<string, Object>();

    //向外提供的  加载资源的方法
    public T Load<T>(ResPath path, string name) where T : Object
    {
        string str = path.ToString() + "/" + name;
        if (path == ResPath.none)
        {
            str = name;
        }
        return Load<T>(str);
    }

    //私有的加载方法
    private T Load<T>(string str) where T : Object
    {
        Object temp;
        if (!resDic.TryGetValue(str, out temp))
        {
            temp = Resources.Load<T>(str);
            resDic.Add(str, temp);
        }
        return temp as T;
    }

    //卸载资源
    public void UnLoad()
    {
        foreach (var item in resDic.Values)
        {
            Resources.UnloadAsset(item);
        }
        System.GC.Collect();
        resDic.Clear();
    }
}
