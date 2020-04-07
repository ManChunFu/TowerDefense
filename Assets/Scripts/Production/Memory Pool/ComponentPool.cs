using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools
{
    public class ComponentPool<T> : IDisposable, IPool<T> where T : Component
    {
        private bool m_IsDisposed;
        private readonly uint _expandBy;
        private readonly Stack<T> m_Objects;
        private readonly List<T> m_Created;
        private readonly T m_Prefab; // a component prefab
        private readonly Transform m_Parent;

        public ComponentPool(uint initSize, T prefab, uint expandBy = 1, Transform parent = null)
        {
            _expandBy = expandBy;
            m_Prefab = prefab;
            m_Parent = parent;
            m_Objects = new Stack<T>();
            m_Created = new List<T>();
            Expand((uint)Mathf.Max(1, initSize));
        }

        private void Expand(uint expandBy)
        {
            for (int i = 0; i < expandBy; i++)
            {
                T instance = Object.Instantiate<T>(m_Prefab, m_Parent);
                instance.gameObject.AddComponent<EmitOnDisable>().OnDisableGameObject += UnRent;
                m_Objects.Push(instance);
                m_Created.Add(instance);
            }
        }

        public T Rent(bool returnActive)
        {
            if (m_Objects.Count == 0)
            {
                Expand(_expandBy);
            }
            T instance = m_Objects.Pop();
            return instance;
        }

        private void UnRent(GameObject gameObject)
        {
            if (m_IsDisposed == false)
                m_Objects.Push(gameObject.GetComponent<T>());
        }

        private void Clean()
        {
            foreach(T component in m_Created)
            {
                if (component != null)
                {
                    component.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent;
                    Object.Destroy(component.gameObject);
                }
            }
            m_Objects.Clear();
            m_Created.Clear();
        }

        public void Dispose()
        {
            m_IsDisposed = true;
            Clean();
        }
    }
}

