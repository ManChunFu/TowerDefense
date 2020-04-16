using Tools;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnPoint = default;
    [SerializeField] private Transform m_LookAtPoint = default;
    [SerializeField] private float m_FireRate = 0.25f;

    private Transform m_TransformTarget = null;
    private float m_CanFire = -1f;
    public Transform SpawnPoint => m_SpawnPoint;
    private void Start()
    {
        if (m_SpawnPoint == null)
        {
            throw new MissingReferenceException("Missing reference of Cannon_Spawn_Point transform.");
        }

        if (m_LookAtPoint == null)
            throw new MissingComponentException("Missing reference of Tower_Top transform.");
    }

    // The collider gets into trigger can be child part of main object. Check the parent to get the main object's transform.
    // If collider gets into trigger is already main object, the parent is the pool which has Vector3.zero position.
    // The Tower layer only react with enemy layer
    private void OnTriggerStay(Collider other)
    {
        if (Time.time > m_CanFire)
        {
            m_CanFire = Time.time + m_FireRate;
            m_TransformTarget = (other.transform.parent.position == Vector3.zero) ? other.transform : other.transform.parent;
            m_LookAtPoint.LookAt(m_TransformTarget);
            Shoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_TransformTarget = null;
    }

    public virtual void Shoot(){}

}



