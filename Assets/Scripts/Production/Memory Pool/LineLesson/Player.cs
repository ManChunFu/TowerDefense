using System;
using UnityEngine;

//[Serializable]
//public class IntObservableProperty : ObservableProperty<int> { }
public class Player : MonoBehaviour
{
    //[SerializeField] private IntObservableProperty m_Health;
    [SerializeField] private int m_Health;

    public ObservableProperty<int> Health { get; } = new ObservableProperty<int>();
    public ObservableProperty<string> Name { get; } = new ObservableProperty<string>();

    private void Awake()
    {
        Health.Value = m_Health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Health.Value += 1;
            Name.Value = Guid.NewGuid().ToString();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Health.Value -= 1;
            Name.Value = Guid.NewGuid().ToString();
        }
    }
}
