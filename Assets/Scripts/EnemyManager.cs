using System.Collections;
using System.Linq;
using UnityEngine;
using Tools;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool _mStandardEnemyPool = null;
    [SerializeField] private GameObjectScriptablePool _mBigEnemeyPool = null;
    [SerializeField] private MapScriptable _mapScriptable = null;
    [SerializeField] private bool _autoSpawn = true;
    [SerializeField] float _spawnWaveInterval = 10f;

    private int _spawnWaveCount = 1;
    private bool _spawnCompleted = true;
    private void Awake()
    {
        if (_mapScriptable == null)
            throw new MissingReferenceException("Missing reference of MapSelectionScriptable object.");

        if (_mStandardEnemyPool == null)
            throw new MissingReferenceException("Missing reference of StandardEnemyPool scriptable object.");

        if (_mBigEnemeyPool == null)
            throw new MissingReferenceException("Missing reference of BigEnemyPool scriptable object.");
    }

    private void Update()
    {
        if (!_spawnCompleted)
            return;

        if (_mapScriptable.Map.SpawnWaves.Any(w => w.SpawnWaveIndex == _spawnWaveCount))
        {
            StartCoroutine(Spawn
                (_mapScriptable.Map.SpawnWaves.First(w => w.SpawnWaveIndex == _spawnWaveCount).SmallEnemyAmout,
                _mapScriptable.Map.SpawnWaves.First(w => w.SpawnWaveIndex == _spawnWaveCount).BigEnemeyAmout));
            _spawnWaveCount++;
            _spawnCompleted = false;
        }
    }

    private IEnumerator Spawn(int smallEnemyAmout, int bigEnemyAmout)
    {
        for (int i = 0; i < smallEnemyAmout; i++)
        {
            _mStandardEnemyPool.Rent(true);
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < bigEnemyAmout; i++)
        {
            _mBigEnemeyPool.Rent(true);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(_spawnWaveInterval);
        _spawnCompleted = true;
    }
}

