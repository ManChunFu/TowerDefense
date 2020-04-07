using UnityEngine;

public class MapSelection : MonoBehaviour
{
    [SerializeField] private MapScriptable m_MapScriptable = null;

    private void Awake()
    {
        if (m_MapScriptable == null)
            throw new MissingReferenceException("Missing reference of MapScriptable object");
    }

    private void Start()
    {
        if (m_MapScriptable != null)
            m_MapScriptable.SelectedMap = (MapTypes)1;
    }
    public void SelectMap(int value)
    {
        m_MapScriptable.SelectedMap = (MapTypes)value;
    }
}
