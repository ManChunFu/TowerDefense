using UnityEngine;
using UnityEngine.UI;

public class GameDataListener : MonoBehaviour
{
    [SerializeField] private Text m_PlayerHealth;
    [SerializeField] private Text m_StandardEnemyKills;
    [SerializeField] private Text m_BigEnemyKills;

    private int m_CurrentStandardEnemyKills = 0;
    private int m_CurrentBigEnemyKills = 0;
    public void UpdatePlayerHealth(int health)
    {
        m_PlayerHealth.text = health.ToString();
    }

    public void UpdateStandardEnemyKills(int kill)
    {
        m_CurrentStandardEnemyKills += kill;
        
        m_StandardEnemyKills.text = m_CurrentStandardEnemyKills.ToString();
    }

    public void UpdateBigEnemyKills(int kill)
    {
        m_CurrentBigEnemyKills += kill;

        m_BigEnemyKills.text = m_CurrentBigEnemyKills.ToString();
    }
}
