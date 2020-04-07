using Tools;
using UnityEngine;

public class WeaponExample : MonoBehaviour
{
    [SerializeField] private BulletExample _bulletComponentPrefab = null;
    [SerializeField] private GameObject _bulletPrefab = null;
    [SerializeField] private GameObjectScriptablePool _scriptablePool = null;

    private GameObjectPool _bulletPool;
    private ComponentPool<BulletExample> _bulletComponentPool;
    private void Awake()
    {
        _bulletPool = new GameObjectPool(10, _bulletPrefab, 5, new GameObject("Bullet Parent").transform);
        _bulletComponentPool = new ComponentPool<BulletExample>(1, _bulletComponentPrefab, 1, new GameObject("Bullet Component Parent").transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = _scriptablePool.Rent(true);
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
