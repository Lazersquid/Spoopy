using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Destroyable))]
    public class PrefabSwapper : MonoBehaviour
    {
        [SerializeField] private GameObject repairedVariant;
        [SerializeField] private GameObject brokenVariant;

        private Destroyable _destroyable;


        private void OnValidate()
        {
            if (repairedVariant == null || brokenVariant == null)
            {
                Debug.LogError($"Prefab swapper {this} is not setup!");
                return;
            }

            _destroyable = GetComponent<Destroyable>();
            SyncState();
        }

        private void Awake()
        {
            _destroyable = GetComponent<Destroyable>();
        }

        private void OnEnable()
        {
            _destroyable.Destroyed += DestroyableOnDestroyed;
            _destroyable.Repaired += DestroyableOnRepaired;
            SyncState();
        }

        private void OnDisable()
        {
            _destroyable.Destroyed -= DestroyableOnDestroyed;
            _destroyable.Repaired -= DestroyableOnRepaired;
        }

        private void SyncState()
        {
            if (_destroyable.IsDestroyed)
                DestroyableOnDestroyed(_destroyable);
            else
                DestroyableOnRepaired(_destroyable);
        }

        private void DestroyableOnRepaired(Destroyable destroyable)
        {
            repairedVariant.SetActive(true);
            brokenVariant.SetActive(false);
        }

        private void DestroyableOnDestroyed(Destroyable destroyable)
        {
            repairedVariant.SetActive(false);
            brokenVariant.SetActive(true);
        }

    }
}