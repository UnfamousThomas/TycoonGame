using System;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour
{

    private float _gold = 0;
    public float initialGold = 0;
    private void Awake()
    {
        Events.OnSetGold += setGold;
        Events.OnRequestGold += getGold;
    }

    private void Start()
    {
        if(!SaveSystem.saveExists()) {
            Events.SetGold(initialGold);
        }
    }

    private void OnDestroy()
    {
        Events.OnSetGold -= setGold;
        Events.OnRequestGold -= getGold;
    }
    

    private void setGold(float gold)
    {
        _gold = gold;
    }

    private float getGold()
    {
        return _gold;
    }
    
}
