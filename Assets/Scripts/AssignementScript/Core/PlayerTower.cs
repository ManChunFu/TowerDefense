using System;
using UnityEngine;

[RequireComponent(typeof(HealthObserverable))]
public class PlayerTower : MonoBehaviour
{
    [SerializeField] private GameDataListener m_GameDataListener = null;
    public bool IsDead { get; private set; }

    private HealthObserverable m_HealthObserverable = null;
    private CompositeDisposable m_Disposables = new CompositeDisposable();

    private void Awake()
    {
        m_HealthObserverable = GetComponent<HealthObserverable>();
    }

    private void OnEnable()
    {
        IsDead = false;
        if (m_HealthObserverable != null)
        {
            m_HealthObserverable.Health.Subscribe(HealthChange).AddTo(m_Disposables);
            m_HealthObserverable.Health.Where(playerHealth => playerHealth <= 0).Subscribe(Die).AddTo(m_Disposables);
        }
    }

    private void Start()
    {
        if (m_GameDataListener == null)
        {
            m_GameDataListener = FindObjectOfType<GameDataListener>();
        }

        if (m_GameDataListener != null)
        {
            m_GameDataListener.UpdatePlayerHealth(m_HealthObserverable.Health.Value);
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }

        if (m_HealthObserverable != null)
        {
            m_HealthObserverable.TakeDamage(damage);
        }
    }

    private void HealthChange(int value)
    {
        if (IsDead)
        {
            return;
        }

        if (m_GameDataListener != null)
        {
            m_GameDataListener.UpdatePlayerHealth(value);
        }
    }

    private void Die(int value)
    {
        IsDead = true;
        // calll gamemanager

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        m_Disposables?.Dispose();
    }

}
