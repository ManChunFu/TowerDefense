using System;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListner : MonoBehaviour
{
    [SerializeField] private Text m_TextField = null;
    [SerializeField] private Text m_TextField1 = null;

    private Player m_Player;

    private CompositeDisposable m_Disposables = new CompositeDisposable();

    private void Awake() //Construct myself 1 / 2
    {

    }

    private void OnEnable()
    {
        if (m_Player != null) // Is this the first OnEnable call?
        {
            RenewSubscription();
        }
    }

    private void Start() //Construct myself 2 / 2
    {
        m_Player = FindObjectOfType<Player>();
        RenewSubscription();
        // this one doesn't unsubscribe
        m_Player.Health.Where(health => health > 0).Subscribe(_ => gameObject.SetActive(true));
    }

    private void RenewSubscription()
    {
        m_Player.Health.Subscribe(UpdateTextField).AddTo(m_Disposables);
        m_Player.Health.Where(playerHealth => playerHealth <= 0).Subscribe(Deactivate).AddTo(m_Disposables);
        m_Player.Name.Subscribe(UpdateAnotherTexField).AddTo(m_Disposables);
    }

    private void OnDisable()
    {
        m_Disposables.Dispose();
    }

    private void Deactivate(int value)
    {
        gameObject.SetActive(false);
    }

    private void UpdateTextField(int playerHealth)
    {
        m_TextField.text = playerHealth.ToString();
    }

    private void UpdateAnotherTexField(string value)
    {
        m_TextField1.text = value;
    }

}