using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Destroyable))]
    public class DestroyCooldownBar : MonoBehaviour
    {
        [SerializeField] private Transform cooldownBar;
        [SerializeField] private CanvasGroup cooldownBarCanvasGroup;
        [SerializeField] private float fadeoutTime;
        private float _currFadeoutTime;
        private Destroyable _destroyable;

        
        private void Awake()
        {
            _destroyable = GetComponent<Destroyable>();
            cooldownBarCanvasGroup.alpha = 0f;
        }

        private void OnEnable()
        {
            _destroyable.Repaired += DestroyableOnRepaired;
            _destroyable.DestroyCooldownFinished += DestroyableOnDestroyCooldownFinished;
        }
        
        private void OnDisable()
        {
            _destroyable.Repaired -= DestroyableOnRepaired;
            _destroyable.DestroyCooldownFinished -= DestroyableOnDestroyCooldownFinished;
        }

        private void Update()
        {
            // update bar
            if (_destroyable.CurrDestroyCooldown > 0f)
                UpdateDestructionBar();
            
            // fadeout
            if (_currFadeoutTime > 0f)
            {
                _currFadeoutTime -= Time.deltaTime;
                cooldownBarCanvasGroup.alpha = Mathf.Clamp01(_currFadeoutTime / fadeoutTime);
            }
        }

        private void DestroyableOnDestroyCooldownFinished()
        {
            UpdateDestructionBar();
            _currFadeoutTime = fadeoutTime;
        }
        
        private void DestroyableOnRepaired(Destroyable destroyable)
        {
            cooldownBarCanvasGroup.alpha = 1f;
        }

        private void UpdateDestructionBar()
        {
            float progress = 1 - (_destroyable.DestroyCooldown - _destroyable.CurrDestroyCooldown) / _destroyable.DestroyCooldown;
            cooldownBar.localScale = new Vector3(progress, 1f, 1f);
        }
    }
}