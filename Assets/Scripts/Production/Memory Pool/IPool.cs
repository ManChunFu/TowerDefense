using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools
{
    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }

    public class GameObjectPool : IPool<GameObject>, IDisposable
    {
        private bool m_IsDisposed;
        private readonly uint m_ExpandBy; 
        private readonly GameObject m_Prefab;
        private readonly Transform m_Parent;
        private readonly List<GameObject> m_Created = new List<GameObject>();

        private readonly Stack<GameObject> m_Objects = new Stack<GameObject>();

        public GameObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
        {
            m_ExpandBy = (uint)Mathf.Max(1, expandBy);
            m_Prefab = prefab;
            m_Parent = parent;
            m_Prefab.SetActive(false);
            Expand((uint)Mathf.Max(1, initSize));
        }

        private void Expand(uint amout)
        {
            for (int i = 0; i < amout; i++)
            {
                m_Objects.Push(CreateNew());
            }
        }

        private GameObject CreateNew()
        {
            GameObject instance = Object.Instantiate(m_Prefab, m_Parent);
            EmitOnDisable emitOnDisable = instance.AddComponent<EmitOnDisable>();
            emitOnDisable.OnDisableGameObject += UnRent; //Listener, subscribe the event
            m_Created.Add(instance);

            return instance;
        }

        // put object back
        private void UnRent(GameObject gameObject)
        {
            if (m_IsDisposed)
                return;
            
            m_Objects.Push(gameObject);

        }

        // take object out
        public GameObject Rent(bool returnActive)
        {
            if (m_IsDisposed)
            {
                return null;
            }

            GameObject instance = m_Objects.Any() ? m_Objects.Pop() : CreateNew();

            if (instance == null)
                return null;

            instance.SetActive(returnActive);
            return instance;

        }

        // free c++
        public void Clear()
        {
            foreach (GameObject gameObject in m_Created)
            {
                if (gameObject != null)
                {
                    gameObject.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent; // unsubscribe
                    Object.Destroy(gameObject);
                }
            }
            m_Objects.Clear();
            m_Created.Clear();
        }

        public void Dispose()
        {
            m_IsDisposed = true;
            Clear();
        }
    }
}

