using System;
using System.Collections.Generic;
using UnityEngine;

public class IronController : MonoBehaviour
{

    private float _iron = 0;
    public float initialIron = 0;
    private void Awake()
    {
        Events.OnSetIron += setIron;
        Events.OnRequestIron += getIron;
    }

    private void Start()
    {
        if(!SaveSystem.saveExists()) {
            Events.SetIron(initialIron);
        }
    }

    private void OnDestroy()
    {
        Events.OnSetIron -= setIron;
        Events.OnRequestIron -= getIron;
    }
    

    private void setIron(float iron)
    {
        _iron = iron;
    }

    private float getIron()
    {
        return _iron;
    }
    
}
