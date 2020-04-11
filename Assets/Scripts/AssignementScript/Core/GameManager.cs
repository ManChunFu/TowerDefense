using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PauseMenu m_PauseMenu = null;

    private void Start()
    {
        if (m_PauseMenu != null)
        {
            return;
        }

        m_PauseMenu = FindObjectOfType<Canvas>()?.GetComponent<PauseMenu>();

        if (m_PauseMenu == null)
        {
            
            throw new NullReferenceException ("Null refrence of PauseMenu script.");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_PauseMenu.Pause();
        }
    }
}
