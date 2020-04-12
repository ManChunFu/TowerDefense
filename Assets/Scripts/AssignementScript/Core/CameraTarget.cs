using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private MapScriptable m_MapScriptable = null;
    [SerializeField] private Vector3 m_Map1View;
    [SerializeField] private Vector3 m_Map2View;
    [SerializeField] private Vector3 m_Map3View;

    private void Awake()
    {
        if (m_MapScriptable == null)
        {
            throw new MissingReferenceException("Missing reference of MapSelection Scriptable object.");
        }
    }

    private void Start()
    {
        GetTargetView();
    }

    private void GetTargetView()
    {
        if (m_MapScriptable.SelectedMap == MapTypes.map_1)
        {
            transform.position = m_Map1View;
        }
        else if (m_MapScriptable.SelectedMap == MapTypes.map_2)
        {
            transform.position = m_Map2View;
        }
        else
        {
            transform.position = m_Map3View;
        }
    }
}
