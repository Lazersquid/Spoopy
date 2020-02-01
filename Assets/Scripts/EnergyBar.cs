using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnergyBar : MonoBehaviour
    {
        [SerializeField] private Transform energyBar;
        [SerializeField] private Repairer repairer;

        private void OnValidate()
        {
            if (energyBar == null || repairer == null)
            {
                Debug.LogError($"Energybar {this} is not setup!");
            }
        }

        private void OnEnable()
        {
            repairer.EnergyChanged += UpdateBar;
            UpdateBar(repairer.CurrentEnergy);
        }

        private void OnDisable()
        {
            repairer.EnergyChanged -= UpdateBar;
        }

        private void UpdateBar(int newEnergyValue)
        {
            float progress = 1 - (float)(repairer.MaxEnergy - repairer.CurrentEnergy) / repairer.MaxEnergy;
            energyBar.localScale = new Vector3(progress, 1f, 1f);
        }
    }
}