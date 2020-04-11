using System;
using System.Collections.Generic;

// SOLD => Single responsibility / Open & Close / Liskov substitution / Interface Segregation / Dependency inversion
// Subject is something Obsersvable
// Micro reactive framwork
namespace Tools
{
    public class Subject<T> : ISubject<T>
    {
        // Collecting of "listeners" that are notified when the event is raised.

        private int m_Index = 0;
        private readonly List<IObserver<T>> m_Observers = new List<IObserver<T>>();

        // TODO: OnCompleted and OnError should also lock to prevent multi-threading
        public void OnCompleted()
        {
            for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
            {
                m_Observers[m_Index].OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
            {
                m_Observers[m_Index].OnError(error); 
            }

        }

        public void OnNext(T value)
        {
            for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
            {
                m_Observers[m_Index].OnNext(value); 
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            m_Observers.Add(observer);
            return new Subscription(this, observer);
        }

        private class Subscription : IDisposable
        {
            private readonly Subject<T> m_Subject;
            private readonly IObserver<T> m_Observer;

            public Subscription(Subject<T> subject, IObserver<T> observer)
            {
                m_Subject = subject;
                m_Observer = observer;
            }

            public void Dispose()
            {
                int elementIndex = m_Subject.m_Observers.IndexOf(m_Observer);
                if (elementIndex < 0)
                {
                    m_Subject.m_Observers.Remove(m_Observer);

                    if (elementIndex <= m_Subject.m_Index)
                    {
                        m_Subject.m_Index--;
                    }
                }
            }
        }
    }

    public class SubjectCaller
    {
        public void SubscribeToSubject()
        {
            Subject<int> intStream = new Subject<int>();
            //IObservable<int> abstractIntStream = intStream;

            IDisposable subscription = intStream.Subscribe(null);
            subscription.Dispose();
        }
    }
}



