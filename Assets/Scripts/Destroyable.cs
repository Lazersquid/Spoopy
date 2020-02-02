using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
[DisallowMultipleComponent]
public class Destroyable : MonoBehaviour
{
    [SerializeField] private int requiredEnergyToRepair;
    public int RequiredEnergyToRepair => requiredEnergyToRepair;
    
    [SerializeField] private int awardedDestructionPoints;
    public int AwardedDestructionPoints => awardedDestructionPoints;

    public bool IsDestroyed { get; private set; }

    [SerializeField] private UnityEvent onDestroyed;
    public event Action<Destroyable> Destroyed;
    
    [SerializeField] private UnityEvent onRepaired;
    public event Action<Destroyable> Repaired;

    private DestructionBar _destructionBar;

    
    private void Awake()
    {
        _destructionBar = FindObjectOfType<DestructionBar>();
    }

    private void OnEnable()
    {
        _destructionBar.RegisterDestroyable(this);
    }
    
    private void OnDisable()
    {
        _destructionBar.UnregisterDestroyable(this);
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            Debug.LogError($"Can't destroy {this}: It's already destroyed!");
            return;
        }

        IsDestroyed = true;
        Destroyed?.Invoke(this);
        onDestroyed.Invoke();
    }

    public void Repair()
    {
        if (!IsDestroyed)
        {
            Debug.LogError($"Can't repair {this}: It's already repaired!");
            return;
        }

        IsDestroyed = false;
        Repaired?.Invoke(this);
        onRepaired.Invoke();
    }
}
