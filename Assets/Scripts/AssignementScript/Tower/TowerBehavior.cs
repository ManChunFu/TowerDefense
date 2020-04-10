using Tools;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool m_BulletTypePool = null;
    [SerializeField] private Transform m_SpawnPoint = default;
    [SerializeField] private Transform m_LookAtPoint = default;
    [SerializeField] private float m_FireRate = 0.25f;

    private Transform m_TransformTarget = null;
    private float m_CanFire = -1f;

    private void Start()
    {
        if (m_BulletTypePool == null)
        {
            throw new MissingReferenceException("Missing refrence of CannonScriptableObjectPool.");
        }

        if (m_SpawnPoint == null)
        {
            throw new MissingReferenceException("Missing reference of Cannon_Spawn_Point transform.");
        }

        if (m_LookAtPoint == null)
            throw new MissingComponentException("Missing reference of Tower_Top transform.");
    }
    
    // The collider gets into trigger can be child part of main object. Check the parent to get the main object's transform.
    // If collider gets into trigger is already main object, the parent is the pool which has Vector3.zero position.
    private void OnTriggerStay(Collider other)
    {
        if (Time.time > m_CanFire)
        {
            m_CanFire = Time.time + m_FireRate;
            m_TransformTarget = (other.transform.parent.position == Vector3.zero)? other.transform : other.transform.parent;
            m_LookAtPoint.LookAt(m_TransformTarget);
            Shoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_TransformTarget = null;
    }

    private void Shoot()
    {
        GameObject bulletType = m_BulletTypePool.Rent(false);
        BulletBase bullet = bulletType.GetComponent<BulletBase>();
        bullet.SetPosition(m_SpawnPoint);
        bullet.SetTarget(m_TransformTarget);
        bullet.gameObject.SetActive(true);
        bullet.Shoot();
    }
}



