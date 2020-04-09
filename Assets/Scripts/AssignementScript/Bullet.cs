﻿using System.Collections;
using UnityEngine;

public enum BulletType
{
    Cannon = 1,
    SnowBall = 2
}
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType m_BulletType = default;
    [SerializeField] private float m_Speed = 10.0f;
    [SerializeField] private int m_Damage = 10;
    [SerializeField] private float m_SlowDownTime = 3.0f;
    [SerializeField] private int m_SlowDownSpeed = 1;

    private Rigidbody m_Rigidbody = null;
    private Transform m_Target = default;
    private Transform m_FirePoint = default;
    private Health m_Health = null;
    private bool m_TargetIsDead = false;
    private float m_Radius = 5.0f;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (m_Health != null)
        {
            m_Health.OnDead += TargetDie;
        }
    }

    private void Start()
    {
        m_Health = FindObjectOfType<Health>();
        m_Health.OnDead += TargetDie;
    }

    public void Shoot()
    {
        StartCoroutine(BulletMoves());
    }

    public IEnumerator BulletMoves()
    {
        m_Rigidbody.AddForce(m_FirePoint.forward * 300);
        yield return new WaitForSeconds(2.0f);
       // gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !m_TargetIsDead)
        {
            if (m_BulletType == BulletType.Cannon)
            {
                AreaDamage();
            }
            else
            {
                FreezeDamage();
            }
            gameObject.SetActive(false);
        }
    }

    private void AreaDamage()
    {
        // show effects

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_Radius);
        foreach (Collider nearbyObject in colliders)
        {
            Health health = nearbyObject.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(m_Damage);
            }
        }
    }

    private void FreezeDamage()
    {
        Movement movement = m_Target.GetComponent<Movement>();
        if (movement != null)
        {
            movement.SlowDownImpact(m_SlowDownSpeed, m_SlowDownTime);
        }

        Health health = m_Target.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(m_Damage);
        }
    }

    public void SetPosition(Transform spawnPosition)
    {
        //transform.position = new Vector3(spawnPosition.position.x, 1, spawnPosition.position.z);
        transform.position = spawnPosition.position;
        m_FirePoint = spawnPosition;
    }

    public void SetTarget(Transform target)
    {
        m_Target = target;
    }

    private void TargetDie(bool isDead)
    {
        m_TargetIsDead = true;
    }

    private void OnDisable()
    {
        m_Health.OnDead -= TargetDie;
    }

}

