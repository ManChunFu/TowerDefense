using UnityEngine;

public class CannonBall : BulletBase
{
    [SerializeField] private float m_DamageRadius = 5.0f;
    [SerializeField] private int m_Damage = 10;

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        AreaDamage();
    }

    private void AreaDamage()
    {
        // show effects

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
