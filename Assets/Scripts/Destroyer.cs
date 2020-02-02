using System;
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
                if (closest != null)
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