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
    [SerializeField] private MapData[] m_MapData = default;
    [SerializeField] private Vector3 m_MapstartPoint = default;
    public int CellSize = 2;

    private Dictionary<char, GameObject> m_MapTileDictionary = new Dictionary<char, GameObject>();
    public  Dictionary<char, bool> m_MapWalkableDictionary = new Dictionary<char, bool>();

    private Vector3 m_StartPoint = default;
    private Vector3 m_EndPoint = default;
    public Vector3 StartPoint => m_StartPoint;
    public Vector3 EndPoint => m_EndPoint;

    public Maps Maps;

    public void BuildMap(Transform transform)
    {
        if (m_MapData != null)
        {
            m_MapTileDictionary = m_MapData.ToDictionary(d => d.Key, d => d.ObjectType);
            m_MapWalkableDictionary = m_MapData.ToDictionary(d => d.Key, d => d.Walkable);
        }

        MapReader mapReader = new MapReader();
        Maps = mapReader.ReadMap(SelectedMap);

        foreach (MapCell item in Maps.GridCells)
        {
            float z = item.YPos2D * CellSize;
            float x = item.XPos2D * CellSize;
            Vector3 newPos = m_MapstartPoint + new Vector3(x, 0, z);
            Instantiate(m_MapTileDictionary[item.ObjectType], newPos, Quaternion.identity, transform);

            if (item.ObjectType == '8')
                m_StartPoint = newPos;
            if (item.ObjectType == '9')
                m_EndPoint = newPos;
        }
    }

}
