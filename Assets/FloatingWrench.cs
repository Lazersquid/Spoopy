using System;
using UnityEngine;

[RequireComponent(typeof(Destroyable))]
public class FloatingWrench : MonoBehaviour
{
    [SerializeField] private GameObject _wrench;

    private Destroyable _destroyable;
    

    private void Awake()
    {
        _destroyable = GetComponent<Destroyable>();
    }

    private void OnEnable()
    {
        _destroyable.Destroyed += DestroyableOnDestroyed;
        _destroyable.Repaired += DestroyableOnRepaired;
        SyncToDestroyable();
    }

    private void OnDisable()
    {
        _destroyable.Destroyed -= DestroyableOnDestroyed;
        _destroyable.Repaired -= DestroyableOnRepaired;
    }

    private void SyncToDestroyable()
    {
        if(_destroyable.IsDestroyed)
            DestroyableOnDestroyed(_destroyable);
        else
            DestroyableOnRepaired(_destroyable);

    }

    private void DestroyableOnRepaired(Destroyable destroyable)
    {
        _wrench.SetActive(false);
    }
    
    private void DestroyableOnDestroyed(Destroyable destroyable)
    {
        _wrench.SetActive(true);
    }
}