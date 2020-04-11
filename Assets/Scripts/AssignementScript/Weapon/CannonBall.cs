using UnityEngine;
using Tools;

public class CannonBall : BulletBase
{
    [SerializeField] private GameObjectScriptablePool m_ExposionScriptablePool = null;
    [SerializeField] private float m_DamageRadius = 5.0f;
    [SerializeField] private int m_Damage = 10;

    protected override void Awake()
    {
        base.Awake();
        if (m_ExposionScriptablePool == null)
        {
            throw new MissingReferenceException("Missing reference of ExposionScriptableObject pool.");
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        AreaDamage(other.transform.position);
        gameObject.SetActive(false);
    }

    private void AreaDamage(Vector3 position)
    {
        GameObject exposion = m_ExposionScriptablePool.Rent(false);
        exposion.transform.position = position;
        exposion.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_DamageRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Health health = nearbyObject.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(m_Damage);
            }
        }
    }
}
