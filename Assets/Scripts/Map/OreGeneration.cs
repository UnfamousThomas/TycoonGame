using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class OreGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public List<ResourcePrefabPair> resourcePrefabs;
    public List<ResourceRange> ranges;
    private Bounds _tilemapWorldBounds;
    private List<OreLocation> _loadedOreLocations = new List<OreLocation>();
    
    void Start()
    {
        _tilemapWorldBounds = GetTilemapWorldBounds();
        if(!SaveSystem.saveExists()) {
            GenerateOresWithNoise();
        }
        else
        {
            LoadOresFromSave();
        }
        Events.OnRequestOreLocations += getOres;
    }

    private void OnDestroy()
    {
        Events.OnRequestOreLocations -= getOres;
    }

    private void LoadOresFromSave()
    {
        SaveData save = SaveSystem.Load();
        foreach (var ore in save.ores)
        {
            SpawnedResource resource = getResource(ore.resourceType);
            resource.resource.resourceType = ore.resourceType;
            if (resource != null)
            {
                resource.level = ore.resourceLevel;
                _loadedOreLocations.Add(ore);
                Instantiate(resource, new Vector3(ore.x + 0.5f, ore.y + 0.5f), Quaternion.identity, null);
            }
        }
    }


    void GenerateOresWithNoise()
    {
         float randomOffsetX = Random.Range(0f, 1000f);
         float randomOffsetY = Random.Range(0f, 1000f);
        for (float x = _tilemapWorldBounds.min.x; x < _tilemapWorldBounds.max.x-5; x++)
        {
            for (float y = _tilemapWorldBounds.min.y; y < _tilemapWorldBounds.max.y-5; y++)
            {
                float noiseValue = Mathf.PerlinNoise((x + randomOffsetX) * 0.1f, (y + randomOffsetY) * 0.1f);
                handleSpawning(x,y,noiseValue);
            }
        }
    }


    
    
    private void handleSpawning(float x, float y, double noise)
    {
        noise = Mathf.Clamp((float)(noise * 100), 0, 100);
        
        SpawnedResource spawnedResource = null;
        // Check which range the noise falls into and get the resource
        foreach (var range in ranges)
        {
            if (noise >= range.MinNoise && noise < range.MaxNoise)
            {
                SpawnedResource resource = getResource(range.Type);
                resource.resource.resourceType = range.Type;
                if (resource != null)
                {
                    resource.level = GetResourceLevel(noise, range.MinNoise, range.MaxNoise);
                    spawnedResource = resource;
                }
            }
        }

        if (spawnedResource != null)
        {
           Instantiate(spawnedResource, new Vector3(x+0.5f,y+0.5f), Quaternion.identity, null); 
           OreLocation location = new OreLocation();
           location.x = x;
           location.y = y;
           location.resourceType = spawnedResource.resource.resourceType;
           location.resourceLevel = spawnedResource.level;
           _loadedOreLocations.Add(location);
        }
        Debug.Log("Currently: " + _loadedOreLocations.Count + " resources.");
    }

    Bounds GetTilemapWorldBounds()
    {
        // Get the tilemap bounds in local space
        BoundsInt bounds = tilemap.cellBounds;

        // Convert the local bounds to world bounds
        Vector3 min = tilemap.CellToWorld(bounds.min);
        Vector3 max = tilemap.CellToWorld(bounds.max);

        return new Bounds((min + max) / 2, max - min);
    }

    private SpawnedResource getResource(ResourceType resourceType)
    {
        return resourcePrefabs.Find(resource => resource.type == resourceType).prefab;
    }
    
    private ResourceLevel GetResourceLevel(double noise, double minNoise, double maxNoise)
    {
        double rangeSize = maxNoise - minNoise;
        double normalizedNoise = (noise - minNoise) / rangeSize;
        int levelIndex = (int)(normalizedNoise * 5); 

        return (ResourceLevel)levelIndex;
    }

    private List<OreLocation> getOres()
    {
        return _loadedOreLocations;
    }
}
