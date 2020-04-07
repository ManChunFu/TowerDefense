using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools
{
    public class ComponentPool<T> : IDisposable, IPool<T> where T : Component
    {
        private bool _isDisposed;
        private readonly uint _expandBy;
        private readonly Stack<T> _objects;
        private readonly List<T> _created;
        private readonly T _prefab; // a component prefab
        private readonly Transform _parent;

        public ComponentPool(uint initSize, T prefab, uint expandBy = 1, Transform parent = null)
        {
            _expandBy = expandBy;
            _prefab = prefab;
            _parent = parent;
            _objects = new Stack<T>();
            _created = new List<T>();
            Expand((uint)Mathf.Max(1, initSize));
        }

        private void Expand(uint expandBy)
        {
            for (int i = 0; i < expandBy; i++)
            {
                T instance = Object.Instantiate<T>(_prefab, _parent);
                instance.gameObject.AddComponent<EmitOnDisable>().OnDisableGameObject += UnRent;
                _objects.Push(instance);
                _created.Add(instance);
            }
        }

        public T Rent(bool returnActive)
        {
            if (_objects.Count == 0)
            {
                Expand(_expandBy);
            }
            T instance = _objects.Pop();
            return instance;
        }

        private void UnRent(GameObject gameObject)
        {
            if (_isDisposed == false)
                _objects.Push(gameObject.GetComponent<T>());
        }

        private void Clean()
        {
            foreach(T component in _created)
            {
                if (component != null)
                {
                    component.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent;
                    Object.Destroy(component.gameObject);
                }
            }
            _objects.Clear();
            _created.Clear();
        }

        public void Dispose()
        {
            _isDisposed = true;
            Clean();
        }
    }
}

