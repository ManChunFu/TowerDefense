using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int m_InitHealth = 15;

    public ObservableProperty<int> Health { get; } = new ObservableProperty<int>();
    public ObservableProperty<string> Name { get; } = new ObservableProperty<string>();

    private void Awake()
    {
        Health.Value = m_InitHealth;
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
