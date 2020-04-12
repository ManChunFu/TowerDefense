using UnityEngine;

public class SnowBall : BulletBase
{
    [SerializeField] private int m_Damage = 10;
    [SerializeField] private float m_SlowDownTime = 3.0f;
    [SerializeField] private int m_SlowDownSpeed = 1;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        FreezeDamage(other.transform);
        gameObject.SetActive(false);
    }
    private void FreezeDamage(Transform target)
    {
        Movement movement = target.GetComponent<Movement>();
        if (movement != null)
        {
            movement.SlowDownImpact(m_SlowDownSpeed, m_SlowDownTime);
        }

        HealthObserverable  healthObserverable = target.GetComponent<HealthObserverable>();
        if (healthObserverable != null)
        {
            healthObserverable.TakeDamage(m_Damage);
        }
    }
}
