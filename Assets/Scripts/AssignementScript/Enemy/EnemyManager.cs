using System.Collections;
using System.Linq;
using UnityEngine;
using Tools;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool m_StandardEnemyPool = null;
    [SerializeField] private GameObjectScriptablePool m_BigEnemeyPool = null;
    [SerializeField] private MapScriptable m_MapScriptable = null;
    [SerializeField] private GameDataListener m_GameDataListener = null;
    [SerializeField] float m_QuequeTimeOfStandardEnemy = 1.0f;
    [SerializeField] float m_QuequeTimeofBigEnemy = 3.0f;
    [SerializeField] float m_IntervalBetweenEachWaves = 10.0f;

    private int m_SpawnWaveCounter = 1;
    private int m_TotalSpawnWave = 0;
    private bool m_SpawnCompleted = true;

    private void Awake()
    {
        if (m_MapScriptable == null)
        {
            throw new MissingReferenceException("Missing reference of MapSelectionScriptable object.");
        }

        if (m_StandardEnemyPool == null)
        {
            throw new MissingReferenceException("Missing reference of StandardEnemyPool scriptable object.");
        }

        if (m_BigEnemeyPool == null)
        {
            throw new MissingReferenceException("Missing reference of BigEnemyPool scriptable object.");
        }

        if (m_GameDataListener == null)
        {
            m_GameDataListener = FindObjectOfType<GameDataListener>();
        }
    }

    private void Start()
    {
        if (m_MapScriptable != null)
        {
            m_TotalSpawnWave = m_MapScriptable.Maps.SpawnWaves.Count();
        }

        if (m_GameDataListener != null)
        {
            m_GameDataListener.PlaceTotalWaveText(m_TotalSpawnWave);
            m_GameDataListener.UpdateCurrentWave(m_SpawnWaveCounter);
        }
    }

    private void Update()
    {
        if (!m_SpawnCompleted)
        {
            return;
        }

        if (m_TotalSpawnWave >= m_SpawnWaveCounter)
        {
            StartCoroutine(Spawn
                (m_MapScriptable.Maps.SpawnWaves.First(w => w.SpawnWaveIndex == m_SpawnWaveCounter).StandardEnemyAmout,
                m_MapScriptable.Maps.SpawnWaves.First(w => w.SpawnWaveIndex == m_SpawnWaveCounter).BigEnemeyAmout));

            if (m_GameDataListener != null)
            {
                m_GameDataListener.UpdateCurrentWave(m_SpawnWaveCounter);
            }
            m_SpawnCompleted = false;
        }
    }

    private IEnumerator Spawn(int smallEnemyAmout, int bigEnemyAmout)
    {
        if (m_StandardEnemyPool != null && m_BigEnemeyPool != null)
        {
            for (int i = 0; i < smallEnemyAmout; i++)
            {
                m_StandardEnemyPool.Rent(true);
                yield return new WaitForSeconds(m_QuequeTimeOfStandardEnemy);
            }

            for (int i = 0; i < bigEnemyAmout; i++)
            {
                m_BigEnemeyPool.Rent(true);
                yield return new WaitForSeconds(m_QuequeTimeofBigEnemy);
            }

            yield return new WaitForSeconds(m_IntervalBetweenEachWaves);

            m_SpawnWaveCounter++;
            m_SpawnCompleted = true;
        }
    }

    public void KillPool()
    {
        if (m_StandardEnemyPool != null) 
        {
            m_StandardEnemyPool.DestroyMe();
        }

        if (m_BigEnemeyPool != null)
        {
            m_BigEnemeyPool.DestroyMe();
        }
    }
}

