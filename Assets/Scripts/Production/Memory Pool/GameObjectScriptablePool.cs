using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "Scriptable", menuName ="Scriptable/GameObjectScriptablePool", order = 3)]
    public class GameObjectScriptablePool : ScriptableObject, IPool<GameObject>
    {
        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private uint _initSize = 1;
        [SerializeField] private uint _expandBy = 1;
        [SerializeField] private bool _hasParent = true;
        [SerializeField] private string _parentName = "";
        private GameObjectPool _internalPool;

        public GameObject Rent(bool returnActive)
        {
            if (_internalPool == null)
            {
                Transform parent = null;
                if (_hasParent)
                    parent = new GameObject(_parentName).transform;
                _internalPool = new GameObjectPool(_initSize, _prefab, _expandBy, parent);
            }
            return _internalPool.Rent(returnActive);
        }

        public void OnDestroy()
        {
            _internalPool.Dispose();
        }
    }
}
