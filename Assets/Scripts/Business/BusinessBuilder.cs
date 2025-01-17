using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BusinessBuilder : MonoBehaviour
{
    public Color AllowColor;
    public Color BlockColor;
    public ParticleSystem buildEffect;
    public ScalingAnimation openAnimation;
    public ScalingAnimation closeAnimation;

    private BusinessData _currentBusinessData;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Events.OnBusinessSelected += OnBusinessSelected;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        openAnimation.enabled = true;
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
        
        if (Input.GetMouseButtonDown(0) && free)
            Build();
        if (Input.GetMouseButtonDown(1) || Input.GetButton("ExitMenu"))
            Exit();
    }

    private void Exit()
    {
        closeAnimation.enabled = true;
    }
    
    public void CloseFinished()
    {
        gameObject.SetActive(false);
    }

    bool IsFree(Vector3 pos)
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pos, 0.45f);

        if (!CanBeBuilt()) return false;

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

            if (overlap.gameObject.GetComponent<Business>() != null)
            {
                found = false;
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
        SubstractResources(_currentBusinessData.cost);
        Business business = Instantiate(_currentBusinessData.businessPrefab, transform.position, Quaternion.identity, null);
        
        business.businessData = _currentBusinessData;
        Instantiate(buildEffect, business.transform).Play();
        gameObject.SetActive(false);
        Events.BuildBusiness(business);
    }
    
    private bool CanBeBuilt()
    {
        Dictionary<ResourceType, float> availableResources = ResourceController.GetResources();

        for (var i = 0; i < _currentBusinessData.cost.Count; i++)
        {
            ResourceFloatPair currentPair = _currentBusinessData.cost[i];
            if (availableResources[currentPair.type] < currentPair.value)
                return false;
        }

        return true;
    }

    public static bool CanBeBuilt(BusinessData businessData)
    {
        Dictionary<ResourceType, float> availableResources = ResourceController.GetResources();

        for (var i = 0; i < businessData.cost.Count; i++)
        {
            ResourceFloatPair currentPair = businessData.cost[i];
            if (availableResources[currentPair.type] < currentPair.value)
                return false;
        }

        return true;
    }

    public static void SubstractResources(List<ResourceFloatPair> cost)
    {
        foreach (ResourceFloatPair pair in cost)
        {
            ResourceType type = pair.type;
            float value = pair.value;
            switch (type)
            {
                case ResourceType.OIL:
                    Events.SetOil(Events.RequestOil() - value);
                    break;
                case ResourceType.GOLD:
                    Events.SetGold(Events.RequestGold() - value);
                    break;
                case ResourceType.WATER:
                    Events.SetWater(Events.RequestWater() - value);
                    break;
                case ResourceType.IRON:
                    Events.SetIron(Events.RequestIron() - value);
                    break;
                case ResourceType.ROCK:
                    Events.SetRocks(Events.RequestRocks() - value);
                    break;
                case ResourceType.MONEY:
                    Events.SetMoney(Events.RequestMoney() - value);
                    break;
            }
        }
    }
}
