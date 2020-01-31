using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class DestroyableTester : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var destroyables = FindObjectsOfType<Destroyable>();
                foreach (var destroyable in destroyables)
                {
                    destroyable.Destroy();
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var destroyables = FindObjectsOfType<Destroyable>();
                foreach (var destroyable in destroyables)
                {
                    destroyable.Repair();
                }
            }
        }
    }
}