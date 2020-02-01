using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private KeyCode destroyKey;
        [SerializeField] private float destroyRadius;        
        [SerializeField] private LayerMask destroyablesLayermask;

        private void Update()
        {
            if (Input.GetKeyDown(destroyKey))
            {
                var hits = Physics2D.OverlapCircleAll(transform.position, destroyRadius, destroyablesLayermask);
                for (int i = 0; i < hits.Length; i++)
                {
                    var destroyable = hits[i].GetComponent<Destroyable>();
                    if (destroyable != null && !destroyable.IsDestroyed)
                    {
                        destroyable.Destroy();
                        break;
                    }
                }
            }
        }
    }
}