using System;
using UnityEngine;

public class Mover : MonoBehaviour, IObserver<int>
{
    private Player m_Player = null;
    private void Start()
    {
        m_Player = FindObjectOfType<Player>();
        m_Player.Health.Skip(1).Subscribe(this);
    }
    public void OnCompleted()
    {
        
    }

    public void OnError(Exception error)
    {
        
    }

    public void OnNext(int value)
    {
        transform.position += UnityEngine.Random.onUnitSphere;
    }
}
