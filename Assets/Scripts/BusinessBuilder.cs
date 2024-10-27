using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BusinessBuilder : MonoBehaviour
{
    public Color AllowColor;
    public Color BlockColor;

    private BusinessData _currentBusinessData;

    private void Awake()
    {
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
        
        foreach (Collider2D overlap in overlaps)
        {
            if (!overlap.isTrigger)
                return false;
        }
        
        return true;
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
        gameObject.SetActive(true);
    }

    void Build()
    {
        //Verify that building area is free of other towers. (Turn this into a method)
        //Make a note to remove gold from player later when gold is implemented
        //Instantiate a tower prefab at the current position
        //Disable the Tower Builder gameobject
        
        if (!IsFree(transform.position)) return;
        
        Events.SetMoney(Events.RequestMoney() - _currentBusinessData.cost);
        
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        Business business = Instantiate(_currentBusinessData.businessPrefab, transform.position, Quaternion.identity, null);
        business.businessData = _currentBusinessData;
        gameObject.SetActive(false);
    }
}
