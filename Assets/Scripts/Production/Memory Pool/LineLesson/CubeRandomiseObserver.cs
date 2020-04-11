using System;
using UnityEngine;

public class CubeRandomiseObserver : MonoBehaviour
{
    private RandomCubeFinder m_Finder;

    private IDisposable m_Disposable;
    private void Start()
    {
        m_Finder = FindObjectOfType<RandomCubeFinder>();
        Subscribe();
    }

    private void Subscribe()
    {
        m_Disposable = m_Finder.OnCubeRandomised.
            Where(obj => obj.name == "Cube_0").
            Map(foundCube => foundCube.transform.position).
            Where(pos=> Vector3.Distance(Vector3.zero, pos) > 5).
            Subscribe(objPosition=> { Debug.Log(objPosition); });
    }

    private void OnDisable()
    {
        m_Disposable?.Dispose();
    }

    private void OnEnable()
    {
        if (m_Finder != null)
        {
            Subscribe();
        }
    }
}
