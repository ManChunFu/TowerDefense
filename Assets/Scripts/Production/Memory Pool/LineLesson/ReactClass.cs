﻿using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public static class ObservableExtensions
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
    {
        IObserver<T> observer = new ActionToObserver<T>(onNext);
        return observable.Subscribe(observer);
    }

    public static IDisposable Subscribe<T>(this IObservable<T> observable, IObserver<T> observer)
    {
        return observable.Subscribe(observer);
    }
}

public class SkipFirstNotificationObserver<T> : IObserver<T>
{
    private bool m_IsFirstTime = true;
    private readonly IObserver<T> m_InnerObserver;

    public SkipFirstNotificationObserver(IObserver<T> innerObserver)
    {
        m_InnerObserver = innerObserver;
    }

    public void OnCompleted() { }
    public void OnError(Exception error) { }

    public void OnNext(T value)
    {
        if (m_IsFirstTime)
        {
            m_IsFirstTime = false;
        }
        else
        {
            m_InnerObserver.OnNext(value);
        }
    }
}

public class ActionToObserver<T> : IObserver<T>
{
    private readonly Action<T> m_Action;

    public ActionToObserver(Action<T> action)
    {
        m_Action = action;
    }

    public void OnCompleted() { }
    public void OnError(Exception error) { }

    public void OnNext(T value)
    {
        m_Action.Invoke(value);
    }
}

//[Serializable]
public class ObservableProperty<T> : IObservable<T>
{
    private bool m_HasValue = false;
    private T m_Value;
    private readonly Subject<T> m_Subject = new Subject<T>();

    public T Value
    {
        get => m_Value;
        set
        {
            m_HasValue = true;
            if (EqualityComparer<T>.Default.Equals(m_Value, value) == false || m_HasValue == false) // value == m_Value
            {
                m_HasValue = true;
                m_Value = value;
                m_Subject.OnNext(m_Value);
            }
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        IDisposable subscription = null;
        try
        {
            if (m_HasValue)
            {
                observer.OnNext(m_Value);
            }
        }
        finally
        {
            subscription = m_Subject.Subscribe(observer);
        }
        return subscription;
    }
}

public interface ISubject<T> : IObservable<T>, IObserver<T> { }
