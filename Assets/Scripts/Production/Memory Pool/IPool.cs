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
        private bool _isDisposed;
        private readonly uint _expandBy; 
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly List<GameObject> _created = new List<GameObject>();

        private readonly Stack<GameObject> _objects = new Stack<GameObject>();

        public GameObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
        {
            _expandBy = (uint)Mathf.Max(1, expandBy);
            _prefab = prefab;
            _parent = parent;
            _prefab.SetActive(false);
            Expand((uint)Mathf.Max(1, initSize));
        }

        private void Expand(uint amout)
        {
            for (int i = 0; i < amout; i++)
            {
                _objects.Push(CreateNew());
            }
        }

        private GameObject CreateNew()
        {
            GameObject instance = Object.Instantiate(_prefab, _parent);
            EmitOnDisable emitOnDisable = instance.AddComponent<EmitOnDisable>();
            emitOnDisable.OnDisableGameObject += UnRent; //Listener, subscribe the event
            _created.Add(instance);

            return instance;
        }

        // put object back
        private void UnRent(GameObject gameObject)
        {
            if (_isDisposed)
                return;
            
            _objects.Push(gameObject);

        }

        // take object out
        public GameObject Rent(bool returnActive)
        {
            if (_isDisposed)
                return null;

            GameObject instance = _objects.Any() ? _objects.Pop() : CreateNew();

            instance.SetActive(returnActive);
            return instance;

        }

        // free c++
        public void Clear()
        {
            foreach (GameObject gameObject in _created)
            {
                if (gameObject != null)
                {
                    gameObject.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent; // unsubscribe
                    Object.Destroy(gameObject);
                }
            }
            _objects.Clear();
            _created.Clear();
        }

        public void Dispose()
        {
            _isDisposed = true;
            Clear();
        }
    }
}

