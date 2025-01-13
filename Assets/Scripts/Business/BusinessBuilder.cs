using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BusinessBuilder : MonoBehaviour
{
    public Color AllowColor;
    public Color BlockColor;
    public ParticleSystem buildEffect;

    private BusinessData _currentBusinessData;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Events.OnBusinessSelected += OnBusinessSelected;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnBusinessSelected -= OnBusinessSelected;
    }

    void Update()
    {
        //Reposition the gameobject to mouse coordinates. 
        //Round the coordinates to make it snap to a grid.

        Vector3 mousePos = transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(
            Mathf.Round(mousePos.x - 0.5f) + 0.5f, 
            Mathf.Round(mousePos.y - 0.5f) + 0.5f, 
            0);
        transform.position = mousePos;

        //Verify that building area is free of other towers. 
        //By using a static overlap method from Physics2D class we can make this work without collider and a 2d rigidbody.

        bool free = IsFree(transform.position);

        //Tint the sprite to green or red accordingly.
        
        if (free)
            TintSprite(AllowColor);
        else
            TintSprite(BlockColor);
        
        //Call the build method when the player presses left mouse button.
        
        if (Input.GetMouseButtonDown(0))
            Build();
        if (Input.GetMouseButtonDown(1))
            gameObject.SetActive(false);
    }

    bool IsFree(Vector3 pos)
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pos, 0.45f);

        if (Events.RequestMoney() < _currentBusinessData.cost) return false;

        bool found = _currentBusinessData.canBuildAnywhere;
        foreach (Collider2D overlap in overlaps)
        {
            if (!found)
            {
                SpawnedResource spawnedResource = overlap.gameObject.GetComponent<SpawnedResource>();
                if (spawnedResource != null)
                {
                    foreach (var resourceType in _currentBusinessData.canBeBuiltOn)
                    {
                        if (spawnedResource.resource.resourceType == resourceType)
                        {
                            found = true;
                        }
                    }
                }
            }
            if (!overlap.isTrigger)
                return false;
        }
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            //Ignore clicks that are inside a UI overlay.
            return false;
        }
        
        return found;
    }

    void TintSprite(Color col)
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer rend in renderers)
        {
            rend.color = col;
        }
    }

    private void OnBusinessSelected(BusinessData data)
    {
        _currentBusinessData = data;
        _spriteRenderer.sprite = data.icon;
        Vector3 mousePos = transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(
            Mathf.Round(mousePos.x - 0.5f) + 0.5f, 
            Mathf.Round(mousePos.y - 0.5f) + 0.5f, 
            0);
        transform.position = mousePos;
        gameObject.SetActive(true);
    }

    void Build()
    {
        if (!IsFree(transform.position)) return;
        //TODO correct cost
        Events.SetMoney(Events.RequestMoney() - _currentBusinessData.cost);
        Business business = Instantiate(_currentBusinessData.businessPrefab, transform.position, Quaternion.identity, null);
        
        business.businessData = _currentBusinessData;
        Instantiate(buildEffect, business.transform).Play();
        gameObject.SetActive(false);
        Events.BuildBusiness(business);
    }
}
