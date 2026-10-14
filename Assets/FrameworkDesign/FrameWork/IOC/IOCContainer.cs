using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOCContainer 
{
   public Dictionary<Type,object> mInstance=new Dictionary<Type,object>();
   public void Register<T>(T instance)
    {
        var key = typeof(T);
        if(mInstance.ContainsKey(key))
        {
            mInstance[key] = instance;
        }
        else
        {
            mInstance.Add(key, instance);
        }
    }
    public T Get<T>()where T:class 
    {
        var key = typeof(T);
        object retobj;
        if(mInstance.TryGetValue(key, out retobj))
        {
            return retobj as T;
        }
        else
        {
            return null;
        }

    }
}
