using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/EnemyTypes", order = 2 )]   
public class EnemyTypesScriptable : ScriptableObject
{
    public UnitType _enemyType;
    public GameObject _enemyPrefab;
    public int _health;
    public int _speed;


}
