using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Callback();
public delegate void Callback<T>(T arg1);
public delegate void Callback<T, U>(T arg1, U arg2);
public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
public delegate void Callback<T, U, V, X>(T arg1, U arg2, V arg3, X arg4);

/// <summary>
/// 消息中心
/// </summary>
public class MsgMgr : Singleton<MsgMgr>
{
    //字典管理
    Dictionary<string, Delegate> msgDic = new Dictionary<string, Delegate>();
    /// <summary>
    /// 添加无参事件
    /// </summary>
    /// <param name="msgName"></param>
    /// <param name="handler"></param>
    public void AddListener(string msgName, Callback handler)
    {
        if (AddListenerJudge(msgName, handler))
            msgDic[msgName] = (Callback)msgDic[msgName] + handler;
        else
            Debug.Log("添加 失败");
    }

    public void AddListener<T>(string msgName, Callback<T> handler)
    {
        if (AddListenerJudge(msgName, handler))
            msgDic[msgName] = (Callback<T>)msgDic[msgName] + handler;
        else
            Debug.Log("添加 失败");
    }

    public void AddListener<T, U>(string msgName, Callback<T, U> handler)
    {
        if (AddListenerJudge(msgName, handler))
            msgDic[msgName] = (Callback<T, U>)msgDic[msgName] + handler;
        else
            Debug.Log("添加 失败");
    }

    public void AddListener<T, U, V>(string msgName, Callback<T, U, V> handler)
    {
        if (AddListenerJudge(msgName, handler))
            msgDic[msgName] = (Callback<T, U, V>)msgDic[msgName] + handler;
        else
            Debug.Log("添加 失败");
    }

    bool AddListenerJudge(string msgName, Delegate handler)
    {
        bool b = true;
        if (!msgDic.ContainsKey(msgName))
        {
            msgDic.Add(msgName, null);
        }
        else if (msgDic[msgName] != null && handler.GetType() != msgDic[msgName].GetType())
        {
            Debug.Log("添加的 回调方法 不一样");
            b = false;
        }
        return b;
    }





    public void RemoveListener(string msgName, Callback handler)
    {
        if (RemoveListenerJudge(msgName, handler))
        {
            try
            {
                msgDic[msgName] = (Callback)msgDic[msgName] - handler;
            }
            catch (Exception)
            {
                Debug.Log("没有添加这个消息回调");
                Debug.Log("移除 事件 失败");
            }
        }
        else
            Debug.Log("移除 事件 失败");

        if (msgDic[msgName] == null)
            msgDic.Remove(msgName);
    }
    public void RemoveListener<T>(string msgName, Callback<T> handler)
    {
        if (RemoveListenerJudge(msgName, handler))
        {
            try
            {
                msgDic[msgName] = (Callback<T>)msgDic[msgName] - handler;
            }
            catch (Exception)
            {
                Debug.Log("没有添加这个消息回调");
                Debug.Log("移除 事件 失败");
            }
        }
        else
        {
            Debug.Log("移除 事件 失败");
        }
        if (msgDic[msgName] == null)
        {
            msgDic.Remove(msgName);
        }
    }

    public void RemoveListener<T, U>(string msgName, Callback<T, U> handler)
    {
        if (RemoveListenerJudge(msgName, handler))
        {
            try
            {
                msgDic[msgName] = (Callback<T, U>)msgDic[msgName] - handler;
            }
            catch (Exception)
            {
                Debug.Log("没有添加这个消息回调");
                Debug.Log("移除 事件 失败");
            }
        }
        else
        {
            Debug.Log("移除 事件 失败");
        }
        if (msgDic[msgName] == null)
        {
            msgDic.Remove(msgName);
        }
    }
    public void RemoveListener<T, U, V>(string msgName, Callback<T, U, V> handler)
    {
        if (RemoveListenerJudge(msgName, handler))
        {
            try
            {
                msgDic[msgName] = (Callback<T, U, V>)msgDic[msgName] - handler;
            }
            catch (Exception)
            {
                Debug.Log("没有添加这个消息回调");
                Debug.Log("移除 事件 失败");
            }
        }
        else
        {
            Debug.Log("移除 事件 失败");
        }
        if (msgDic[msgName] == null)
        {
            msgDic.Remove(msgName);
        }
    }

    bool RemoveListenerJudge(string msgName, Delegate handler)
    {
        bool b = true;
        if (msgDic.ContainsKey(msgName))
        {
            if (handler.GetType() == msgDic[msgName].GetType())
            {
                if (msgDic[msgName] == null)
                {
                    Debug.Log("监听器为空");
                    b = false;
                }
            }
            else
            {
                Debug.Log("试图删除不存在 的类型");
                b = false;
            }
        }
        else
        {
            Debug.Log("不存在这个消息");
            b = false;
        }
        return b;
    }
    public void Broadcast(string msgName)
    {
        Delegate d;
        if (msgDic.TryGetValue(msgName, out d))
        {
            Callback callback = d as Callback;
            if (callback != null)
            {
                callback();
            }
            else
            {
                Debug.Log("你他妈消息名错了");
            }
        }
    }
    public void Broadcast<T>(string msgName, T arg1)
    {
        Delegate d;
        if (msgDic.TryGetValue(msgName, out d))
        {
            Callback<T> callback = d as Callback<T>;
            if (callback != null)
            {
                callback(arg1);
            }
            else
            {
                Debug.Log("你他妈消息名错了");
            }
        }
    }
    public void Broadcast<T, U>(string msgName, T arg1, U arg2)
    {
        Delegate d;
        if (msgDic.TryGetValue(msgName, out d))
        {
            Callback<T, U> callback = d as Callback<T, U>;
            if (callback != null)
            {
                callback(arg1, arg2);
            }
            else
            {
                Debug.Log("你他妈消息名错了");
            }
        }
    }
    public void Broadcast<T, U, V>(string msgName, T arg1, U arg2, V arg3)
    {
        Delegate d;
        if (msgDic.TryGetValue(msgName, out d))
        {
            Callback<T, U, V> callback = d as Callback<T, U, V>;
            if (callback != null)
            {
                callback(arg1, arg2, arg3);
            }
            else
            {
                Debug.Log("你他妈消息名错了");
            }
        }
    }
}
