using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructionBar : MonoBehaviour
{
    [SerializeField] private int maxDestructionPoints;
    [SerializeField] private Transform destructionBar;
    public int CurrDestructionPoints { get; private set; }
    
    private List<Destroyable> _destroyables = new List<Destroyable>();
    

    private void Start()
    {
        RecalculateDestructionValue();
    }

    public void RegisterDestroyable(Destroyable destroyable)
    {
        if (_destroyables.Contains(destroyable))
        {
            Debug.LogError($"Can't register destroyable {destroyable} it was already registered!");
            return;
        }
        
        _destroyables.Add(destroyable);
        destroyable.Destroyed += DestroyableOnDestroyed;
        destroyable.Repaired += DestroyableOnRepaired;
        
        // add points of new destroyable if its already destroyed
        if(destroyable.IsDestroyed)
            DestroyableOnDestroyed(destroyable);
    }

    public void UnregisterDestroyable(Destroyable destroyable)
    {
        if (!_destroyables.Contains(destroyable))
        {
            Debug.LogError($"Can't unregister destroyable {destroyable} was not registered!");
            return;
        }
        
        _destroyables.Remove(destroyable);
        destroyable.Destroyed -= DestroyableOnDestroyed;
        destroyable.Repaired -= DestroyableOnRepaired;
        
        // remove points of destroyable ift its currently destroyed
        if(destroyable.IsDestroyed)
            DestroyableOnRepaired(destroyable);
    }
    
    private void DestroyableOnRepaired(Destroyable destroyable)
    {
        CurrDestructionPoints -= destroyable.AwardedDestructionPoints;
        RecalculateDestructionValue();
    }
    
    private void DestroyableOnDestroyed(Destroyable destroyable)
    {
        CurrDestructionPoints += destroyable.AwardedDestructionPoints;
        RecalculateDestructionValue();
    }

    private void RecalculateDestructionValue()
    {
        var sum = 0;
        for (int i = 0; i < _destroyables.Count; i++)
        {
            if (_destroyables[i].IsDestroyed)
                sum += _destroyables[i].AwardedDestructionPoints;
        }
        CurrDestructionPoints = Mathf.Clamp(sum, 0, maxDestructionPoints);
        
        UpdateDestructionbar();

        if (CurrDestructionPoints >= maxDestructionPoints)
            SceneManager.LoadScene("GhostWon");
    }
    
    private void UpdateDestructionbar()
    {
        float progress = 1 - (float)(maxDestructionPoints - CurrDestructionPoints) / maxDestructionPoints;
        destructionBar.localScale = new Vector3(progress, 1f, 1f);
    }
}
