using Tools;
using UnityEngine;

public class SnowBallTower : TowerBase, IPool, IWeapon
{
    [SerializeField] private GameObjectScriptablePool m_SnowBallPool = null;

    private void Awake()
    {
        if (m_SnowBallPool == null)
        {
            throw new MissingReferenceException("Missing reference of SnowBall scriptable object pool.");
        }
    }

    public void KillPool()
    {
        if (m_SnowBallPool != null)
        {
            m_SnowBallPool.Clear();
        }
    }

    public override void Shoot()
    {
        if (m_SnowBallPool != null)
        {
            GameObject bulletType = m_SnowBallPool.Rent(false);
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
