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

    private Movement m_Movement;
    private float m_Radius = 5.0f;
    private Transform m_Target = default;

    private void Update()
    {
        transform.LookAt(m_Target);
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            m_Movement = other.gameObject.GetComponent<Movement>();
            if (m_Movement.m_IsDead)
                return;

            Debug.Log("Hurt!");
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
        foreach(Collider nearbyObject in colliders)
        {
            Animator animator = nearbyObject.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Damaged");
            }

            Health health = nearbyObject.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(m_Damage);
            }
        }
    }

    private void FreezeDamage()
    {
        // freez the enemy
    }

    public void SetPosition(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }
    
    public void SetTarget(Transform target)
    {
        m_Target = target;
    }
   
}
