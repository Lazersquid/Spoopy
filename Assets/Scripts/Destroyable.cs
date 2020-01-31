using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Destroyable : MonoBehaviour
{
    public bool IsDestroyed { get; private set; }

    [SerializeField] private UnityEvent onDestroyed;
    public event Action<Destroyable> Destroyed;
    
    [SerializeField] private UnityEvent onRepaired;
    public event Action<Destroyable> Repaired;
    
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            Debug.LogError($"Can't destroy {this}: It's already destroyed!");
            return;
        }

        IsDestroyed = false;
        Destroyed?.Invoke(this);
        onDestroyed.Invoke();
    }

    public void Repair()
    {
        if (!IsDestroyed)
        {
            Debug.LogError($"Can't repair {this}: It's already destroyed!");
            return;
        }

        IsDestroyed = true;
        Repaired?.Invoke(this);
        onRepaired.Invoke();
    }
}
