using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Destroyable))]
public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private Sprite repairedSprite;
    [SerializeField] private Sprite destroyedSprite;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Destroyable _destroyable;


    private void OnValidate()
    {
        if (repairedSprite == null || destroyedSprite == null || spriteRenderer == null)
        {
            Debug.LogError($"Sprite wrapper {this} is not setup!");
            return;
        }

        spriteRenderer.sprite = repairedSprite;
    }

    private void Awake()
    {
        _destroyable = GetComponent<Destroyable>();
    }

    private void OnEnable()
    {
        _destroyable.Destroyed += DestroyableOnDestroyed;
        _destroyable.Repaired += DestroyableOnRepaired;
        spriteRenderer.sprite = _destroyable.IsDestroyed
            ? destroyedSprite
            : repairedSprite;
    }

    private void OnDisable()
    {
        _destroyable.Destroyed -= DestroyableOnDestroyed;
        _destroyable.Repaired -= DestroyableOnRepaired;
    }

    private void DestroyableOnRepaired(Destroyable obj)
    {
        spriteRenderer.sprite = repairedSprite;
    }

    private void DestroyableOnDestroyed(Destroyable destroyable)
    {
        spriteRenderer.sprite = destroyedSprite;
    }
}
