using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Singleton<T> where T: Singleton<T>
{
   private static T instance;
    public static T Instance
    {
        get 
        { 
            if(instance == null)
            {
                // 1. 获取类型 T（即子类的类型信息）
                var type = typeof(T);

                // 2. 获取该类型的所有实例和私有构造函数
                var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

                // 3. 查找一个无参数的构造函数
                var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                // 4. 如果找不到，抛出一个异常
                if (ctor == null)
                {
                    throw new Exception("Non Public Constructor Not Found in " + type.Name);
                }
                // 5. 如果找到了，就调用这个无参构造函数创建实例
                instance = ctor.Invoke(null) as T;
            }
            return instance;
        }
    }
}
