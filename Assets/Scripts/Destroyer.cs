﻿using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private KeyCode destroyKey;
        [SerializeField] private float destroyRadius;        
        [SerializeField] private LayerMask destroyablesLayermask;

        [SerializeField] private UnityEvent onDestroyedDestroyable;
        public event Action<Destroyable> DestroyedDestroyable;
        
        
        private void Update()
        {
            if (Input.GetKeyDown(destroyKey))
            {
                var closest = Utility.GetClosestDestroyableInRange(transform.position, destroyRadius, destroyablesLayermask, 
                    destroyable => !destroyable.IsDestroyed && destroyable.CurrDestroyCooldown <= 0f);

                // Retrieve closest but don't filter destroyables that are on cooldown
                // To make the cooldown bar shake
                if (closest == null)
                {
                    closest = Utility.GetClosestDestroyableInRange(transform.position, destroyRadius, destroyablesLayermask, 
                        destroyable => !destroyable.IsDestroyed);
                    
                    if(closest != null)
                        closest.OnTriedToDestroyThisWhileOnCooldown();
                }
                else if (closest != null)
                    Destroy(closest);
            }
        }
        
        public void Destroy(Destroyable destroyable)
        {
            destroyable.Destroy();
            onDestroyedDestroyable.Invoke();
            DestroyedDestroyable?.Invoke(destroyable);
        }
    }
}