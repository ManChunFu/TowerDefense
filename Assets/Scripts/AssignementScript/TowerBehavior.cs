using Tools;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool m_CannonPool = null;
    [SerializeField] private GameObjectScriptablePool m_SnowBallPool = null;
    [SerializeField] private TowerType m_TowerType = default;
    [SerializeField] private Transform m_SpawnPoint = default;
    [SerializeField] private Transform m_LookAtPoint = default;
    [SerializeField] private float m_FireRate = 0.25f;

    private Transform m_Target = null;
    private float m_CanFire = -1f;

    private void Start()
    {
        if (m_CannonPool == null)
        {
            throw new MissingReferenceException("Missing refrence of CannonScriptableObjectPool.");
        }

        if (m_SnowBallPool == null)
        {
            throw new MissingReferenceException("Missing reference of SnowScriptableObjectPool.");
        }

        if (m_SpawnPoint == null)
        {
            throw new MissingReferenceException("Missing reference of Cannon_Spawn_Point transform.");
        }

        if (m_LookAtPoint == null)
            throw new MissingComponentException("Missing reference of Tower_Top transform.");
    }
    private Vector3 compare = default;
    private float y = 2f;
    private void OnTriggerStay(Collider other)
    {
        if (Time.time > m_CanFire)
        {
            m_CanFire = Time.time + m_FireRate;
            m_Target = (other.transform.parent.position == compare)? other.transform : other.transform.parent;
            //m_LookAtPoint.LookAt(new Vector3(m_Target.position.x, 1, m_Target.position.z));
            m_LookAtPoint.LookAt(m_Target);
            Shoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_Target = null;
    }


    private void Shoot()
    {
        GameObject cannon ;
        if (m_TowerType == TowerType.CannonTower)
        {
            cannon = m_CannonPool.Rent(false);
        }
        else
        {
            cannon = m_SnowBallPool.Rent(false);
        }
        Bullet bullet = cannon.GetComponent<Bullet>();
        bullet.SetPosition(m_SpawnPoint);
        bullet.SetTarget(m_Target);
        bullet.gameObject.SetActive(true);
        bullet.Shoot();
    }
}



