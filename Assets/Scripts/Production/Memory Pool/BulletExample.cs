using UnityEngine;

public class BulletExample : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 3;
    [SerializeField] private float _maxSpeed = 15;

    [SerializeField] private Rigidbody _rigidbody = null;

    public void Push()
    {
        _rigidbody.AddForce(Vector3.up * Random.Range(_minSpeed, _maxSpeed));
        Invoke(nameof(Sleep), 1.5f); // disappear in the scene
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Sleep));
    }
}
