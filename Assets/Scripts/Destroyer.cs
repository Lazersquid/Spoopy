using System;
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
                var hits = Physics2D.OverlapCircleAll(transform.position, destroyRadius, destroyablesLayermask);
                for (int i = 0; i < hits.Length; i++)
                {
                    var destroyable = hits[i].GetComponent<Destroyable>();
                    if (destroyable != null && !destroyable.IsDestroyed && destroyable.CurrDestroyCooldown <= 0f)
                    {
                        Destroy(destroyable);
                        break;
                    }
                }
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