using System;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListner : MonoBehaviour
{
    [SerializeField] private Text m_TextField = null;
    [SerializeField] private Text m_TextField1 = null;

    private Player m_Player;
    private IDisposable m_Subscription;
    private IDisposable m_NameSubscription;

    private void Awake() //Construct myself 1 / 2
    {

    }

    private void OnEnable()
    {
        if (m_Player != null) // Is this the first OnEnable call?
        {
            m_Subscription = m_Player.Health.Subscribe(UpdateTextField);
        }
    }

    private void Start() //Construct myself 2 / 2
    {
        m_Player = FindObjectOfType<Player>();
        m_Subscription = m_Player.Health.Subscribe(UpdateTextField);
        m_NameSubscription = m_Player.Name.Subscribe(UpdateAnotherTexField);
    }

    private void OnDisable()
    {
        m_Subscription?.Dispose();
        m_NameSubscription?.Dispose();
    }

    private void UpdateTextField(int playerHealth)
    {
        if (playerHealth < 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            m_TextField.text = playerHealth.ToString();
        }
    }

    private void UpdateAnotherTexField(string value)
    {
        m_TextField1.text = value;
    }

}