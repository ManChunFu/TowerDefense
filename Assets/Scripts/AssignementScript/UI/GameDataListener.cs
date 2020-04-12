using UnityEngine;
using UnityEngine.UI;

public class GameDataListener : MonoBehaviour
{
    [SerializeField] private Text m_PlayerHealth = null;
    [SerializeField] private Text m_StandardEnemyKills = null;
    [SerializeField] private Text m_BigEnemyKills = null;
    [SerializeField] private Text m_TotalWaves = null;
    [SerializeField] private Text m_CurrentWaves = null;

    private int m_CurrentStandardEnemyKills = 0;
    private int m_CurrentBigEnemyKills = 0;

    private void Awake()
    {
        if (m_PlayerHealth == null)
        {
            throw new MissingReferenceException("Did you forget to assign the PlayerHealth_Text on Canvas?");
        }

        if (m_StandardEnemyKills == null)
        {
            throw new MissingReferenceException("Did you forget to assign the StandardEnemy_Kill_Text on Canvas?");
        }

        if (m_BigEnemyKills == null)
        {
            throw new MissingReferenceException("Did you forget to assign the BigEnemy_Kill_Text on Canvas?");
        }

        if (m_TotalWaves == null)
        {
            throw new MissingReferenceException("Did you forget to assign the TotalWave_Text on Canvas?");
        }

        if (m_CurrentWaves == null)
        {
            throw new MissingReferenceException("Did you forget to assign the CurrentWave_Text on Canvas?");
        }
    }
    public void UpdatePlayerHealth(int health)
    {
        m_PlayerHealth.text = health.ToString();
    }

    public void UpdateStandardEnemyKills(int kill)
    {
        m_CurrentStandardEnemyKills += kill;
        
        m_StandardEnemyKills.text = m_CurrentStandardEnemyKills.ToString().PadLeft(2, '0');
    }

    public void UpdateBigEnemyKills(int kill)
    {
        m_CurrentBigEnemyKills += kill;

        m_BigEnemyKills.text = m_CurrentBigEnemyKills.ToString().PadLeft(2, '0');
    }

    public void PlaceTotalWaveText(int waves)
    {
        m_TotalWaves.text = waves.ToString();
    }

    public void UpdateCurrentWave(int currentWave)
    {
        m_CurrentWaves.text = currentWave.ToString();
    }
}
