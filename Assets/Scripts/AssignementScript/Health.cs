using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_HealthPoints;

    private bool m_IsDead = false;
    private Animator m_Animator = null;

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

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        m_HealthPoints = Mathf.Max(m_HealthPoints - damage, 0);
        if (m_HealthPoints == 0)
            Die();
    }

    private void Die()
    {
        if (m_IsDead)
            return;

        m_IsDead = true;
        if (m_Animator != null)
        {
            m_Animator.SetTrigger("Killed");
        }
    }

    public bool IsDead()
    {
        return m_IsDead;
    }    
}
