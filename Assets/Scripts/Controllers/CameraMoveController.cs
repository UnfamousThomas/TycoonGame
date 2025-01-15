using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMoveController : MonoBehaviour
{
    
    public float dragSpeed = 2.0f; 
    public Tilemap tileMap;
    
    public float zoomSpeed = 5.0f; 
    public float minZoom = 2.0f;  
    public float maxZoom = 10.0f;
    
    private Vector3 _dragOrigin;
    private bool _isDragging = false;
    private Bounds _tilemapWorldBounds;

    private void Start()
    {
        _tilemapWorldBounds = GetTilemapWorldBounds();
    }

    void Update()
    {
        HandleDrag();
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            Camera.main.orthographicSize -= scroll * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
        ClampCameraPosition();
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition;
            _isDragging = true;
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(_dragOrigin);
            Camera.main.gameObject.transform.position -= new Vector3(difference.x, difference.y, 0) * dragSpeed;
            _dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }

    void ClampCameraPosition()
    {
        // Get the current camera size
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Calculate the min and max bounds based on the tilemap size
        float minX = _tilemapWorldBounds.min.x + cameraWidth / 2;
        float maxX = _tilemapWorldBounds.max.x - cameraWidth / 2;
        float minY = _tilemapWorldBounds.min.y + cameraHeight / 2;
        float maxY = _tilemapWorldBounds.max.y - cameraHeight / 2;

        // Clamp the camera position
        float clampedX = Mathf.Clamp(Camera.main.transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(Camera.main.transform.position.y, minY, maxY);

        Camera.main.transform.position = new Vector3(clampedX, clampedY, Camera.main.transform.position.z);
    }
    Bounds GetTilemapWorldBounds()
    {
        // Get the tilemap bounds in local space
        BoundsInt bounds = tileMap.cellBounds;

        // Convert the local bounds to world bounds
        Vector3 min = tileMap.CellToWorld(bounds.min);
        Vector3 max = tileMap.CellToWorld(bounds.max);

        return new Bounds((min + max) / 2, max - min);
    }
    
}