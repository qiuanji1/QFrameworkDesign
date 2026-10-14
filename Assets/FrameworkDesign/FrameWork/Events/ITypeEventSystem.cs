using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITypeEventSystem
{
    void Send<T>() where T : new();
    void Send<T>(T obj);
    IUnRegister  Register<T>(Action<T> OnEvent);
    void UnRegister<T>(Action<T> OnEvent);
}
public interface IUnRegister
{
    void UnRegister();
}
public struct TypeEventSystemUnRegister<T> : IUnRegister
{
    public ITypeEventSystem TypeEventSystem { get; set; }
    public Action<T> OnEvent { get; set; }

    

    public void UnRegister()
    {
        TypeEventSystem.UnRegister<T>(OnEvent);
        TypeEventSystem = null;
        OnEvent=null;
    }
}
public class UnRegisterOnDestroyTrigger : MonoBehaviour
{
    private HashSet<IUnRegister> mUnRegisters = new HashSet<IUnRegister>();
    public void AddUnRegister(IUnRegister unRegister)
    {
        mUnRegisters.Add(unRegister);
    }
    private void OnDestroy()
    {
        foreach (var unRegister in mUnRegisters)
        {
            unRegister.UnRegister();
        }
        mUnRegisters.Clear();
    }
}
    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            trigger.AddUnRegister(unRegister);
        }
    }
public class TypeEventSystem : ITypeEventSystem
{
    interface IRegistrations
    {

    }

    class Registrations<T> : IRegistrations
    {
        /// <summary>
        /// 因为委托本身就可以一对多注册
        /// </summary>
        public Action<T> OnEvent = obj => { };
    }

    private Dictionary<Type, IRegistrations> mEventRegistrations = new Dictionary<Type, IRegistrations>();

    public void Send<T>() where T : new()
    {
        var e = new T();
        Send<T>(e);
    }

    public void Send<T>(T e)
    {
        var type = typeof(T);
        IRegistrations eventRegistrations;

        if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
        {
            (eventRegistrations as Registrations<T>)?.OnEvent.Invoke(e);
        }

    }

    public IUnRegister Register<T>(Action<T> onEvent)
    {
        var type = typeof(T);
        IRegistrations eventRegistrations;

        if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
        {

        }
        else
        {
            eventRegistrations = new Registrations<T>();
            mEventRegistrations.Add(type, eventRegistrations);
        }

        (eventRegistrations as Registrations<T>).OnEvent += onEvent;

        return new TypeEventSystemUnRegister<T>()
        {
            OnEvent = onEvent,
            TypeEventSystem = this
        };
    }

    public void UnRegister<T>(Action<T> onEvent)
    {
        var type = typeof(T);
        IRegistrations eventRegistrations;

        if (mEventRegistrations.TryGetValue(type, out eventRegistrations))
        {
            (eventRegistrations as Registrations<T>).OnEvent -= onEvent;
        }
    }
}

