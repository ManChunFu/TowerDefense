using Tools;
using UnityEngine;

public class CannonTower : TowerBase, IPool, IWeapon
{
    [SerializeField] private GameObjectScriptablePool m_CannonPool = null;

    private void Awake()
    {
        if (m_CannonPool == null)
        {
            throw new MissingReferenceException("Missing reference of Cannon scriptable object pool.");
        }
    }

    public void KillPool()
    {
        if (m_CannonPool != null)
        {
            m_CannonPool.DestroyMe();
        }
    }

    public override void Shoot()
    {
        if (m_CannonPool != null)
        {
            GameObject bulletType = m_CannonPool.Rent(false);
            if (bulletType != null)
            {
                BulletBase bullet = bulletType.GetComponent<BulletBase>();
                bullet.SetPosition(SpawnPoint);
                bullet.gameObject.SetActive(true);
                bullet.Shoot();
            }
        }
    }
}
