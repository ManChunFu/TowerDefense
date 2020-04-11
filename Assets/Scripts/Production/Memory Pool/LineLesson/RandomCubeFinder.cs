using UnityEngine;
using Tools;

public class RandomCubeFinder : MonoBehaviour
{
    [SerializeField] private string m_TargetTag = null;

    private GameObject[] m_Objects = default;
    public Subject<GameObject> OnCubeRandomised { get; } = new Subject<GameObject>();

    private void Start()
    {
        m_Objects = GameObject.FindGameObjectsWithTag(m_TargetTag);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject target = m_Objects[Random.Range(0, m_Objects.Length)];
            OnCubeRandomised.OnNext(target);
        }
    }
}
