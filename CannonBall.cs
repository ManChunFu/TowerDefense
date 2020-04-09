using UnityEngine;

public class CannonBall : BulletBase<CannonBall>
{
    [SerializeField] private float m_DamageRadius = 5.0f;

    private void Awake()
    {
         
       // m_Rigidbody = GetComponent<Rigidbody>();
    }
    public override void Damage()
    {
    }

    public override void SetPosition(Transform spawnPosition)
    {
        base.SetPosition(spawnPosition);
    }
    public override void BulletMoves()
    {
        base.BulletMoves();
    }
    private void OnEnable()
    {
        m_Health.OnDead += TargetDies;
    }

    private void Start()
    {
        m_Health = FindObjectOfType<Health>();
        m_Health.OnDead += TargetDies;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !m_TargetIsDead)
        {
            Damage();
        }
    }





}
