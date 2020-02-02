using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Destroyable))]
public class SpritePrefabSwapper : MonoBehaviour
{
    [SerializeField] private Sprite repairedSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject brokenVariant;

    private Destroyable _destroyable;


    private void OnValidate()
    {
        if (repairedSprite == null || brokenVariant == null || spriteRenderer == null)
        {
            Debug.LogError($"Sprite wrapper {this} is not setup!");
            return;
        }

        spriteRenderer.sprite = repairedSprite;
        _destroyable = GetComponent<Destroyable>();
        SyncState();
    }

    private void Awake()
    {
        _destroyable = GetComponent<Destroyable>();
        spriteRenderer.sprite = repairedSprite;
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
        spriteRenderer.gameObject.SetActive(true);
        brokenVariant.SetActive(false);
    }

    private void DestroyableOnDestroyed(Destroyable destroyable)
    {
        spriteRenderer.gameObject.SetActive(false);
        brokenVariant.SetActive(true);
    }
}
