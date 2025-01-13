using System;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{

    private float _rocks = 0;
    public float initialRocks = 0;
    private void Awake()
    {
        Events.OnSetRocks += setRocks;
        Events.OnRequestRocks += getRocks;
    }

    private void Start()
    {
        if(!SaveSystem.saveExists()) {
            Events.SetRocks(initialRocks);
        }
    }

    private void OnDestroy()
    {
        Events.OnSetRocks -= setRocks;
        Events.OnRequestRocks -= getRocks;
    }
    

    private void setRocks(float rocks)
    {
        _rocks = rocks;
    }

    private float getRocks()
    {
        return _rocks;
    }
    
}
