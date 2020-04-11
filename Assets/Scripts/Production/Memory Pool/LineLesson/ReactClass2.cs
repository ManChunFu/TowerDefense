using System;
using System.Collections.Generic;

public class CompositeDisposable : IDisposable
{
    private List<IDisposable> m_Disposables = new List<IDisposable>();

    public void Add(IDisposable disposable)
    {
        m_Disposables.Add(disposable);
    }
    public void Dispose()
    {
        foreach (IDisposable disposable in m_Disposables)
        {
            disposable.Dispose();
        }
        m_Disposables.Clear();
    }
}


public static class DisposableExtensions
{
    public static void AddTo(this IDisposable disposable, CompositeDisposable composite)
    {
        composite.Add(disposable);
    }
}
public static class ObservableOperators
{
    public static IObservable<T> Skip<T>(this IObservable<T> observable, int skip)
    {
        return new SkipOperator<T>(observable, skip);
    }

    public static IObservable<T> Where<T>(this IObservable<T> observable, Predicate<T> filter)
    {
        return new WhereOperator<T>(filter, observable);
    }

    //MAP --- Select
    public static IObservable<K> Map<T,K>(this IObservable<T> observable, Func<T,K> mapper)
    {
        return new MapOperator<T, K>(mapper, observable);
    }
}

public class MapOperator<T, K> : IObservable<K>
{
    private readonly Func<T, K> m_Mapper;
    private readonly IObservable<T> m_OriginalObservable;

    public MapOperator(Func<T, K> mapper, IObservable<T> originalObservable)
    {
        m_Mapper = mapper;
        m_OriginalObservable = originalObservable;
    }

    public IDisposable Subscribe(IObserver<K> observer)
    {
        return m_OriginalObservable.Subscribe(new MapOperatorObserver(this, observer));
    }

    private class MapOperatorObserver : IObserver<T>
    {
        private readonly MapOperator<T, K> m_Parent;
        private readonly IObserver<K> m_DestinationObserver;

        public MapOperatorObserver(MapOperator<T, K> parent, IObserver<K> destinationObserver)
        {
            m_Parent = parent;
            m_DestinationObserver = destinationObserver;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            K result = m_Parent.m_Mapper(value);
            m_DestinationObserver.OnNext(result);
        }
    }
}

public class WhereOperator<T> : IObservable<T>
{
    private Predicate<T> m_Filter;
    private IObservable<T> m_OriginalObservable;

    public WhereOperator(Predicate<T> filter, IObservable<T> originalObservable)
    {
        m_Filter = filter;
        m_OriginalObservable = originalObservable;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        WhereOperatorObserver whereOperatorObserver = new WhereOperatorObserver(this, observer);
        return m_OriginalObservable.Subscribe(whereOperatorObserver);
    }

    private class WhereOperatorObserver : IObserver<T>
    {
        private readonly WhereOperator<T> m_ParentOperator;
        private readonly IObserver<T> m_Observer;
        public WhereOperatorObserver(WhereOperator<T> parentOperator, IObserver<T> observer)
        {
            m_ParentOperator = parentOperator;
            m_Observer = observer;
        }
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            if (m_ParentOperator.m_Filter(value))
            {
                m_Observer.OnNext(value);
            }
        }
    }
}

public class SkipOperator<T> : IObservable<T>
{
    private readonly int m_Skip;
    private readonly IObservable<T> m_OriginalObservable;

    public SkipOperator(IObservable<T> originalObservable, int skip)
    {
        m_Skip = skip;
        m_OriginalObservable = originalObservable;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        SkipObserver skipObserver = new SkipObserver(this, observer, m_Skip);
        return m_OriginalObservable.Subscribe(skipObserver);
    }

    // intermediate 
    private class SkipObserver : IObserver<T>
    {
        private SkipOperator<T> m_Parent;
        private readonly IObserver<T> m_OriginalObserver;
        private readonly int m_Skip;
        private int m_CurrentSkips = 0;

        public SkipObserver(SkipOperator<T> parent, IObserver<T> originalObserver, int skip)
        {
            m_Parent = parent;
            m_OriginalObserver = originalObserver;
            m_Skip = skip;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            if (m_CurrentSkips >= m_Skip)
            {
                m_OriginalObserver.OnNext(value);
            }
            m_CurrentSkips++;
        }
    }
}
