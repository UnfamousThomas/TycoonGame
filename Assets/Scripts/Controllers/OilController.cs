using System;
using System.Collections.Generic;
using UnityEngine;

public class OilController : MonoBehaviour
{

    private float _oil = 0;
    public float initialOil = 0;
    private void Awake()
    {
        Events.OnSetOil += setOil;
        Events.OnRequestOil += getOil;
    }

    private void Start()
    {
        if(!SaveSystem.saveExists()) {
            Events.SetOil(initialOil);
        }
    }

    private void OnDestroy()
    {
        Events.OnSetOil -= setOil;
        Events.OnRequestOil -= getOil;
    }
    

    private void setOil(float oil)
    {
        _oil = oil;
    }

    private float getOil()
    {
        return _oil;
    }
    
}
