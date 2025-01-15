using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    private float _water = 0;
    public float initialWater = 0;
    private void Awake()
    {
        Events.OnSetWater += setWater;
        Events.OnRequestWater += getWater;
    }

    private void Start()
    {
        if(!SaveSystem.saveExists()) {
            Events.SetWater(initialWater);
        }
    }

    private void OnDestroy()
    {
        Events.OnSetWater -= setWater;
        Events.OnRequestWater -= getWater;
    }
    

    private void setWater(float water)
    {
        _water = water;
    }

    private float getWater()
    {
        return _water;
    }
    
}
