using Tools;
using UnityEngine;

public class WeaponExample : MonoBehaviour
{
    [SerializeField] private BulletExample m_BulletComponentPrefab = null;
    [SerializeField] private GameObject m_BulletPrefab = null;
    [SerializeField] private GameObjectScriptablePool m_ScriptablePool = null;

    private GameObjectPool m_BulletPool;
    private ComponentPool<BulletExample> _bulletComponentPool;
    private void Awake()
    {
        m_BulletPool = new GameObjectPool(10, m_BulletPrefab, 5, new GameObject("Bullet Parent").transform);
        _bulletComponentPool = new ComponentPool<BulletExample>(1, m_BulletComponentPrefab, 1, new GameObject("Bullet Component Parent").transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = m_ScriptablePool.Rent(true);
            BulletExample bulletComponent = bullet.GetComponent<BulletExample>();
            bullet.transform.position = transform.position;
            bulletComponent.Reset();
            bulletComponent.Push();
           

            //BulletExample bulletComponent = _bulletComponentPool.Rent(false);
            //bulletComponent.transform.position = transform.position;
            //bulletComponent.Reset();
            //bulletComponent.GetComponent<Renderer>().material.color = Random.ColorHSV(0.8f, 1.0f);
            //bulletComponent.gameObject.SetActive(true);
            //bulletComponent.Push();

            //GameObject bullet = _bulletPool.Rent(false);
            //BulletExample bulletComponent = bullet.GetComponent<BulletExample>();
            //bullet.transform.position = transform.position;
            //bulletComponent.Reset();
            //bullet.GetComponent<Renderer>().material.color = Random.ColorHSV(0.8f, 1.0f);
            //bullet.SetActive(true);
            //bulletComponent.Push();
        }
    }

    private void OnDestroy()
    {
        //_bulletPool.Dispose();
        //_bulletComponentPool.Dispose();
    }
}
