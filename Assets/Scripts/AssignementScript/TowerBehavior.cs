using System.Collections;
using Tools;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool m_CannonPool = null;
    [SerializeField] private GameObjectScriptablePool m_SnowBallPool = null;
    [SerializeField] private TowerType m_TowerType = default;
    [SerializeField] private Transform m_SpawnPoint = default;
    [SerializeField] private float m_ShootRange = 10.0f;

    private IWeapon m_Weapon;
    private Transform m_Target = null;

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

        if (m_TowerType == TowerType.CannonTower)
        {
            m_Weapon = new CannonTower();
        }
        else
        {
            m_Weapon = new SnowTower();
        }

        if (m_SpawnPoint == null)
        {
            throw new MissingReferenceException("Missing reference of Cannon_Spawn_Point transform.");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            m_Target = other.transform;
            StartCoroutine(Shoot());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (m_TowerType == TowerType.CannonTower)
        {
            GameObject cannon = m_CannonPool.Rent(false);
            Bullet bullet = cannon.GetComponent<Bullet>();
            bullet.SetPosition(m_SpawnPoint.position);
            bullet.SetTarget(m_Target);
            bullet.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
        }
    }

}



