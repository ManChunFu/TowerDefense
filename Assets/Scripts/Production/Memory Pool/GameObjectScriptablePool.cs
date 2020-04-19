using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "Scriptable", menuName ="Scriptable/GameObjectScriptablePool", order = 3)]
    public class GameObjectScriptablePool : ScriptableObject, IPool<GameObject>
    {
        [SerializeField] private GameObject m_Prefab = null;
        [SerializeField] private uint m_InitSize = 1;
        [SerializeField] private uint m_ExpandBy = 1;
        [SerializeField] private bool m_HasParent = true;
        [SerializeField] private string m_ParentName = "";
        private GameObjectPool m_InternalPool;

        public GameObject Rent(bool returnActive)
        {
            if (m_InternalPool == null)
            {
                Transform parent = null;
                if (m_HasParent)
                    parent = new GameObject(m_ParentName).transform;
                m_InternalPool = new GameObjectPool(m_InitSize, m_Prefab, m_ExpandBy, parent);
            }
            return m_InternalPool.Rent(returnActive);
        }

        public void DestroyMe()
        {
            if (m_InternalPool != null)
            {
                m_InternalPool.Dispose();
            }
        }

        public void Clear()
        {
            if (m_InternalPool != null)
            {
                m_InternalPool.Clear();
            }
        }
    }
}
