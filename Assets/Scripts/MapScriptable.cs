using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct MapData
{
    public char Key;
    public GameObject ObjectType;
    public bool Walkable;
}

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/MapScriptable", order = 1)]
public class MapScriptable : ScriptableObject
{
    public MapTypes SelectedMap = MapTypes.map_1;
    [SerializeField] private MapData[] _mapData = default;
    [SerializeField] private Vector3 _mapstartPoint = default;
    public int CellSize = 2;

    private Dictionary<char, GameObject> _mapTileDictionary = new Dictionary<char, GameObject>();
    public Dictionary<char, bool> MapWalkableDictionary = new Dictionary<char, bool>();

    private Vector3 startPoint = default;
    private Vector3 endPoint = default;
    public Vector3 StartPoint => startPoint;
    public Vector3 EndPoint => endPoint;

    public Map Map;

    public void BuildMap(Transform transform)
    {
        if (_mapData != null)
        {
            _mapTileDictionary = _mapData.ToDictionary(d => d.Key, d => d.ObjectType);
            MapWalkableDictionary = _mapData.ToDictionary(d => d.Key, d => d.Walkable);
        }

        MapReader mapReader = new MapReader();
        Map = mapReader.ReadMap(SelectedMap);

        foreach (MapCell item in Map.GridCells)
        {
            float z = item.YPos2D * CellSize;
            float x = item.XPos2D * CellSize;
            Vector3 newPos = _mapstartPoint + new Vector3(x, 0, z);
            Instantiate(_mapTileDictionary[item.ObjectType], newPos, Quaternion.identity, transform);

            if (item.ObjectType == '8')
                startPoint = newPos;
            if (item.ObjectType == '9')
                endPoint = newPos;
        }
    }

}
