using System;
using UnityEngine;

[RequireComponent(typeof(HealthObserverable))]
public class PlayerTower : MonoBehaviour
{
    [SerializeField] private GameDataListener m_GameDataListener = null;
    [SerializeField] private GameManager m_GameManager = null;

    public bool IsDead { get; private set; }

    private HealthObserverable m_HealthObserverable = null;
    private CompositeDisposable m_Disposables = new CompositeDisposable();

    private void Awake()
    {
        m_HealthObserverable = GetComponent<HealthObserverable>();
    }

    private void OnEnable()
    {
        Setup();
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        IsDead = false;

        if (m_HealthObserverable != null)
        {
            m_HealthObserverable.Health.Subscribe(HealthChange).AddTo(m_Disposables);
            m_HealthObserverable.Health.Where(playerHealth => playerHealth <= 0).Subscribe(Die).AddTo(m_Disposables);
        }

        if (m_GameDataListener == null)
        {
            m_GameDataListener = FindObjectOfType<GameDataListener>();
        }

        if (m_GameDataListener != null)
        {
            m_GameDataListener.UpdatePlayerHealth(m_HealthObserverable.Health.Value);
        }

        if (m_GameManager == null)
        {
            m_GameManager = FindObjectOfType<GameManager>();
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
        
        if (m_GameManager != null)
        {
            m_GameManager.GameOver();
        }

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        m_Disposables?.Dispose();
    }

}
