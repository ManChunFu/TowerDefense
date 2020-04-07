using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel = null;

    private void Start()
    {
        if (_pausePanel == null)
            throw new MissingReferenceException("Missing refrence of Pause panel");
    }

    public void Pause()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _pausePanel.SetActive(false);
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
