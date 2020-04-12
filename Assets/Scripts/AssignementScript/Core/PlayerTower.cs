using System;
using UnityEngine;

[RequireComponent(typeof(HealthObserverable))]
public class PlayerTower : MonoBehaviour, IDisposable
{
    public bool IsDead { get; private set; }

    private HealthObserverable m_HealthObserverable = null;
    private IDisposable m_IDisposable = null;
    
    private void Awake()
    {
        m_HealthObserverable = GetComponent<HealthObserverable>();
        m_IDisposable = m_HealthObserverable.Health.Where(playerHealth => playerHealth <= 0).Subscribe(Die);
    }

    private void OnEnable()
    {
        IsDead = false;
        if (m_HealthObserverable != null)
        {
            m_HealthObserverable.Health.Where(playerHealth => playerHealth <= 0).Subscribe(Die);
        }
    }

    public void TakeDamage(int damage)
    {
        m_HealthObserverable.TakeDamage(damage);
    }

    private void Die(int value)
    {
        IsDead = true;
        // calll gamemanager
        // call canvas
        Dispose();
    }

    public void Dispose()
    {
        m_IDisposable.Dispose();
        gameObject.SetActive(false);
    }
}
