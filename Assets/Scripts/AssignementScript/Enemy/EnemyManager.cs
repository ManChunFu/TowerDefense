﻿using System.Collections;
using System.Linq;
using UnityEngine;
using Tools;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool m_StandardEnemyPool = null;
    [SerializeField] private GameObjectScriptablePool m_BigEnemeyPool = null;
    [SerializeField] private MapScriptable m_MapScriptable = null;
    [SerializeField] float m_QuequeTimeOfStandardEnemy = 1.0f;
    [SerializeField] float m_QuequeTimeofBigEnemy = 3.0f;
    [SerializeField] float m_IntervalBetweenEachWaves = 10.0f;

    private int m_SpawnWaveCount = 1;
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
    }

    private void Update()
    {
        if (!m_SpawnCompleted)
        {
            return;
        }

        if (m_MapScriptable.Maps.SpawnWaves.Any(w => w.SpawnWaveIndex == m_SpawnWaveCount))
        {
            StartCoroutine(Spawn
                (m_MapScriptable.Maps.SpawnWaves.First(w => w.SpawnWaveIndex == m_SpawnWaveCount).StandardEnemyAmout,
                m_MapScriptable.Maps.SpawnWaves.First(w => w.SpawnWaveIndex == m_SpawnWaveCount).BigEnemeyAmout));
            m_SpawnWaveCount++;
            m_SpawnCompleted = false;
        }
    }

    private IEnumerator Spawn(int smallEnemyAmout, int bigEnemyAmout)
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
        m_SpawnCompleted = true;
    }
}

