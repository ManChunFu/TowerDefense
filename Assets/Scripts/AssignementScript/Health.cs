using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_HealthPoints;
    [SerializeField] private EnemyTypesScriptable m_enemyTypesScriptable;

    public event Action<int> OnHealthChanged;
    public int HealthPoints
    {
        get => m_HealthPoints;
        set
        {
            if (m_HealthPoints != value)
            {
                m_HealthPoints = value;
                OnHealthChanged?.Invoke(m_HealthPoints);
            }
        }
    }

    
    public event Action<bool> OnDead;

    private bool m_IsDead = false;
    public bool IsDead
    {
        get => m_IsDead;
        set
        {
            m_IsDead = value;
            if (value)
            {                
                OnDead?.Invoke(true);
            }
        }
    }
    private void OnEnable()
    {
        m_HealthPoints = m_enemyTypesScriptable.Health;
        IsDead = false;
    }
    public void TakeDamage(int damage)
    {
        HealthPoints = Mathf.Max(HealthPoints - damage, 0);
        if (HealthPoints == 0)
            Die();
    }

    private void Die()
    {
        if (IsDead)
            return;

        IsDead = true;
    }
}
