using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedResource : MonoBehaviour
{
    public Resource resource;
    public ResourceLevel level;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        Sprite sprite = resource.GetSprite(level);
        if (sprite == null)
        {
            Debug.LogError("Sprite for " + level + " is null for resource " + resource.resourceType);
            return;
        }
        
        _spriteRenderer.sprite = sprite;
    }
}
