﻿using System.Collections;
using UnityEngine;
public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] private float m_FireForce = 300.0f;

    private Rigidbody m_Rigidbody = null;
    private Transform m_FirePoint = default;
    private Health m_Health = null;
    private bool m_TargetIsDead = false;
    private bool m_IsActive = false;

    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (m_Health != null)
        {
            m_Health.OnDead += TargetDie;
        }
        m_IsActive = true;
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
        m_Rigidbody.AddForce(m_FirePoint.forward * m_FireForce);
        yield return new WaitForSeconds(2.0f);
        if (m_IsActive)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !m_TargetIsDead)
        {}
    }


    public void SetPosition(Transform spawnPosition)
    {
        transform.position = spawnPosition.position;
        m_FirePoint = spawnPosition;
    }

    private void TargetDie(bool isDead)
    {
        m_TargetIsDead = true;
    }

    private void OnDisable()
    {
        m_IsActive = false;
        m_Rigidbody.velocity = Vector3.zero;
        m_Health.OnDead -= TargetDie;
        StopAllCoroutines();
    }

    public virtual void SetTarget(Transform target)
    {}
}