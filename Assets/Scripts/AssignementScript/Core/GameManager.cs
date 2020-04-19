using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PausePanel = null;
    [SerializeField] private GameObject m_GameOverPanel = null;
    [SerializeField] private GameObject m_WinPanel = null;

    private void Awake()
    {
        if (m_PausePanel == null)
        {
            throw new MissingReferenceException("Missing refrence of Pause panel on Canvas");
        }

        if (m_GameOverPanel == null)
        {
            throw new MissingReferenceException("Missing refrence of GameOver panel on Canvas");
        }

        if (m_WinPanel == null)
        {
            throw new MissingReferenceException("Missing refrence of Win panel on Canvas");
        }

    }

    private void Start()
    {
        if (m_PausePanel != null)
        {
            m_PausePanel.SetActive(false);
        }

        if (m_GameOverPanel != null)
        {
            m_GameOverPanel.SetActive(false);
        }

        if (m_WinPanel != null)
        {
            m_WinPanel.SetActive(false);
        }
        
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_PausePanel.SetActive(true);
            Pause();
        }
    }

    public void Win()
    {
        m_WinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        m_GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        m_PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    #region Need to ask Ederic about it
    //Ederic: Once you kill the pool, the scriptable pool isDisposed, so you can use it anymore in the normal way. 
    //A solution to this: Don't dispose the Pool, but clear it. Dispose method is called when you won't use that Disposable system anymore. 
    //If you dispose but then you use it again the behaviour is not granted
    public void LoadMenu()
    {
        KillAllPools();
        SceneManager.LoadScene(0);
    }

    private void KillAllPools()
    {
        FindObjectOfType<EnemyManager>()?.KillPool(); // Still call Dispose pool
        FindObjectOfType<CannonBall>()?.KillPool(); // Only clear pool
        FindObjectOfType<CannonTower>()?.KillPool(); // only clear pool
        FindObjectOfType<SnowBallTower>()?.KillPool(); // only clear pool
    }
    #endregion

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}
