using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PausePanel = null;
    [SerializeField] private GameObject m_GameOverPanel = null;

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

    public void LoadMenu()
    {
        KillAllPools();
        SceneManager.LoadScene(0);
    }

    private void KillAllPools()
    {
        FindObjectOfType<EnemyManager>()?.KillPool();
        FindObjectOfType<CannonBall>()?.KillPool();
        FindObjectOfType<TowerBase>()?.KillPool();
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
