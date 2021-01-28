using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>
{
    //添加到字典  方便管理
    Dictionary<Type, BaseModel> modelDic = new Dictionary<Type, BaseModel>();

    //注册所有模型
    public void RegisterAll()
    {

    }

    //组成单个模型数据
    void Register(BaseModel model)
    {
        BaseModel temp;
        if (!modelDic.TryGetValue(model.GetType(), out temp))
        {
            modelDic.Add(model.GetType(), model);
            modelDic[model.GetType()].LoadModel();
        }

    }

    //外部获得模型数据
    public T GetTModel<T>() where T : BaseModel
    {
        BaseModel temp;
        if (modelDic.TryGetValue(typeof(T), out temp))
        {
            return temp as T;
        }
        else
        {
            return null;
        }
    }
}
