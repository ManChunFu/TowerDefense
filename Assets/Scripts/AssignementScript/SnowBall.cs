using UnityEngine;

public class SnowBall : BulletBase
{
    [SerializeField] private int m_Damage = 10;
    [SerializeField] private float m_SlowDownTime = 3.0f;
    [SerializeField] private int m_SlowDownSpeed = 1;

    private Transform m_Target = default;

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        FreezeDamage();
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
        gameObject.SetActive(false);
    }

    public override void SetTarget(Transform target)
    {
        m_Target = target;
    }


}
