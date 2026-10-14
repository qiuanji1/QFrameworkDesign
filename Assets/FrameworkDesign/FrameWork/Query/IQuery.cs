using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuery<TResult> : IBelongToArchitecture,ICanGetModel,ICanGetSystem,ICanSetArchitecture
{
    TResult Do();
}
public abstract class AbstractQuery<T> : IQuery<T>
{
    public T Do()
    {
        return OnDo();
    }
    protected abstract T OnDo();
    private IArchitecture mArchitecture;

    public IArchitecture GetArchitecture()
    {
        return mArchitecture;
    }

    public void SetArchitecture(IArchitecture architecture)
    {
        mArchitecture = architecture;
    }
}

[System.Obsolete("Use AbstractQuery<T>.")]
public abstract class AbstrictQuery<T> : AbstractQuery<T>
{
}
