using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rotater : MonoBehaviour
{
    private Player m_Player;
    private IDisposable m_Subscription;

    private void OnEnable()
    {
        if (m_Player != null)
        {
            SubscribeToPlayerHealth();
        }
    }

    private void Start()
    {
        m_Player = FindObjectOfType<Player>();
        SubscribeToPlayerHealth();
    }
    private void SubscribeToPlayerHealth()
    {
        m_Subscription = m_Player.Health.Skip(1).Subscribe(intValue =>
        {
            transform.rotation = Random.rotation;
        });
    }

    private void OnDisable()
    {
        m_Subscription?.Dispose();
    }
}
