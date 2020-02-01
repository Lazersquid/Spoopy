using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Repairer : MonoBehaviour
    {
        [SerializeField] private KeyCode repairKey;
        [SerializeField] private float repairRadius;
        [SerializeField] private LayerMask repairLayerMask;
        
        [Space]
        [SerializeField] private int maxEnergy;
        [SerializeField] private int startingEnergy;
        public int CurrentEnergy { get; private set; }

        [SerializeField] private UnityEvent OnNotEnoughEnergyToRepair;
        public event Action NotEnoughEnergyToRepair;

        [SerializeField] private UnityEvent OnEnergyChanged;
        public event Action<int> EnergyChanged;
        

        private void Start()
        {
            CurrentEnergy = startingEnergy;
        }

        private void Update()
        {
            if (Input.GetKeyDown(repairKey))
            {
                var hits = Physics2D.OverlapCircleAll(transform.position, repairRadius, repairLayerMask);
                for (int i = 0; i < hits.Length; i++)
                {
                    var destroyable = hits[i].GetComponent<Destroyable>();
                    if (destroyable != null && destroyable.IsDestroyed)
                    {
                        if (destroyable.RequiredEnergyToRepair > CurrentEnergy)
                        {
                            OnNotEnoughEnergyToRepair.Invoke();
                            NotEnoughEnergyToRepair?.Invoke();
                        }
                        else
                        {
                            ChangeEnergy(-destroyable.RequiredEnergyToRepair);
                            destroyable.Repair();
                        }
                        break;
                    }
                }
            }
        }

        public void ChangeEnergy(int energyDelta)
        {
            CurrentEnergy = Mathf.Clamp(CurrentEnergy + energyDelta, 0, maxEnergy);
            OnEnergyChanged.Invoke();
            EnergyChanged?.Invoke(CurrentEnergy);
        }
    }
}