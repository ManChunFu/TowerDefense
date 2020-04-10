using System;
using Tools;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ObservableProperty<int> Health = new ObservableProperty<int>();

    #region Event oriented programming
    //private int m_Health;
    //public event Action<int> OnPlayerHealthChanged;
    //public int Health
    //{
    //    get => m_Health;
    //    set
    //    {
    //        if (m_Health != value)
    //        {
    //            m_Health = value;
    //            OnPlayerHealthChanged?.Invoke(m_Health);
    //        }
    //    }
    //}
    #endregion Event oriented programming

    private string m_Name;
    public event Action<string> OnNameChanged;
    public string Name
    {
        get => m_Name;
        set
        {
            if (m_Name != value)
            {
                m_Name = value;
                OnNameChanged?.Invoke(m_Name);
            }
        }
    }

    [ContextMenu("Increase Health")]
    public void Increase()
    {
        Health.Value += 1;
    }
}
