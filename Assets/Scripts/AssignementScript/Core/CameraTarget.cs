using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private MapScriptable m_MapScriptable = null;

    private void Awake()
    {
        if (m_MapScriptable == null)
        {
            throw new MissingReferenceException("Missing reference of MapSelection Scriptable object.");
        }
    }

    private void Start()
    {
        transform.position = new Vector3(0, 6, 0);
    }

    private void GetTarget()
    {

    }
}
