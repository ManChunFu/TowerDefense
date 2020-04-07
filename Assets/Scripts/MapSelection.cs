using UnityEngine;

public class MapSelection : MonoBehaviour
{
    [SerializeField] private MapScriptable _mapScriptable = null;

    private void Awake()
    {
        if (_mapScriptable == null)
            throw new MissingReferenceException("Missing reference of MapScriptable object");
    }

    private void Start()
    {
        if (_mapScriptable != null)
            _mapScriptable.SelectedMap = (MapTypes)1;
    }
    public void SelectMap(int value)
    {
        _mapScriptable.SelectedMap = (MapTypes)value;
    }
}
