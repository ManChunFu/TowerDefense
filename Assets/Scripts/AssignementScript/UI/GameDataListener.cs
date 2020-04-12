using UnityEngine;
using UnityEngine.UI;

public class GameDataListener : MonoBehaviour
{
    [SerializeField] private Text m_PlayerHealth;
    [SerializeField] private Text m_StandardEnemyKills;
    [SerializeField] private Text m_BigEnemyKills;


    public void UpdatePlayerHealth(int health)
    {
        m_PlayerHealth.text = health.ToString();
    }

    public void UpdateStandardEnemyKills(int kill)
    {
        m_StandardEnemyKills.text = kill.ToString();
    }

    public void UpdateBigEnemyKills(int kill)
    {
        m_BigEnemyKills.text = kill.ToString();
    }
}
