using UnityEngine;

public class HealthObserverable : MonoBehaviour
{
    [SerializeField] private int m_Health = default;
    [SerializeField] private bool m_IsDead;

    private int m_InitialHealth = default;
    public ObservableProperty<int> Health { get; } = new ObservableProperty<int>();

    private void Awake()
    {
        Health.Value = m_InitialHealth = m_Health;
    }

    private void OnDisable()
    {
        Health.Value = m_Health = m_InitialHealth;
        m_IsDead = false;
    }


    public void TakeDamage(int damage)
    {
        if (m_IsDead)
        {
            return;
        }

        m_Health = Health.Value = Mathf.Max(Health.Value - damage, 0);

        if (Health.Value == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (m_IsDead)
            return;

        m_IsDead = true;
    }
    
}
