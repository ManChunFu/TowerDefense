using System;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private MapScriptable _mapScriptable = null;

    private void Awake()
    {
        if (_mapScriptable == null)
            throw new MissingReferenceException("Missing reference of MapScriptable object");
    }

    private void Start()
    {
        _mapScriptable.BuildMap(transform);
    }
}

