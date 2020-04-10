using System;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListner : MonoBehaviour//, IObserver<int>
{
    [SerializeField] private Text m_TextField;

    private Player m_Player;
    private IDisposable m_Subscription;

    private void Awake() //Construct myself 1 / 2
    {

    }

    private void OnEnable()
    {
        if (m_Player != null) // Is this the first OnEnable call?
        {
            //m_Player.OnPlayerHealthChanged += UpdateTextField;
            m_Subscription = m_Player.Health.Subscribe(UpdateTextField);
        }
    }

    private void Start() //Construct myself 2 / 2
    {
        m_Player = FindObjectOfType<Player>();
        m_Subscription = m_Player.Health.Subscribe(UpdateTextField);
    }

    private void OnDisable()
    {
        // m_Player.OnPlayerHealthChanged -= UpdateTextField;
        m_Subscription.Dispose();
    }

    private void UpdateTextField(int playerHealth)
    {
        m_TextField.text = playerHealth.ToString();
    }

    public void OnCompleted() { }
    public void OnError(Exception error) { }
    public void OnNext(int value)
    {
        UpdateTextField(value);
    }
}