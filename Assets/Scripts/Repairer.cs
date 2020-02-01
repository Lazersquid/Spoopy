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
        public int MaxEnergy => maxEnergy;
        
        [SerializeField] private int startingEnergy;
        public int CurrentEnergy { get; private set; }

        [SerializeField] private UnityEvent onRepairedDestroyable;
        public event Action<Destroyable> RepairedDestroyable;
        
        [SerializeField] private UnityEvent OnNotEnoughEnergyToRepair;
        public event Action NotEnoughEnergyToRepair;

        [SerializeField] private UnityEvent OnEnergyChanged;
        public event Action<int> EnergyChanged;

        [SerializeField] private UnityEvent onFoodCollected;
        public event Action FoodCollected;

        private void Start()
        {
            ChangeEnergy(startingEnergy);
        }

        private void Update()
        {
            if (Input.GetKeyDown(repairKey))
            {
                var hits = Physics2D.OverlapCircleAll(transform.position, repairRadius, repairLayerMask);
                for (int i = 0; i < hits.Length; i++)
                {
                    //TODO: Retrieve closest destroyable instead of just the first one in the array
                    var destroyable = hits[i].GetComponent<Destroyable>();
                    if (destroyable != null && destroyable.IsDestroyed)
                    {
                        if (destroyable.RequiredEnergyToRepair <= CurrentEnergy)
                            Repair(destroyable);
                        else
                        {
                            OnNotEnoughEnergyToRepair.Invoke();
                            NotEnoughEnergyToRepair?.Invoke();
                        }
                        break;
                    }
                }
            }
        }

        public void Repair(Destroyable destroyable)
        {
            ChangeEnergy(-destroyable.RequiredEnergyToRepair);
            destroyable.Repair();
            onRepairedDestroyable.Invoke();
            RepairedDestroyable?.Invoke(destroyable);
        }

        public void ConsumeFood(int grantedEnergy)
        {
            ChangeEnergy(grantedEnergy);
            onFoodCollected.Invoke();
        }
        
        public void ChangeEnergy(int energyDelta)
        {
            CurrentEnergy = Mathf.Clamp(CurrentEnergy + energyDelta, 0, maxEnergy);
            OnEnergyChanged.Invoke();
            EnergyChanged?.Invoke(CurrentEnergy);
        }
    }
}