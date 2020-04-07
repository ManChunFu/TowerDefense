using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject m_PausePanel = null;

    private void Start()
    {
        if (m_PausePanel == null)
            throw new MissingReferenceException("Missing refrence of Pause panel");
    }

    public void Pause()
    {
        m_PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        m_PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}
