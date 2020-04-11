using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private MapScriptable m_MapScriptable = null;

    private void Awake()
    {
        if (m_MapScriptable == null)
        {
            throw new MissingReferenceException("Missing reference of MapScriptable object");
        }
    }

    private void Start()
    {
        m_MapScriptable.BuildMap(transform);
    }
}

