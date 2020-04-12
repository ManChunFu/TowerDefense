using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/EnemyTypes", order = 2 )]   
public class EnemyTypesScriptable : ScriptableObject
{
    public UnitType EnemyType;
    public GameObject EnemyPrefab;
    public int Speed;
}
