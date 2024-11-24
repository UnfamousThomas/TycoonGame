using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    private float level = 0;
    
    private void Awake()
    {
        Events.OnLevelChange += onLevelChange;
        Events.OnRequestLevel += getLevel;
    }
    
    private void OnDestroy()
    {
        Events.OnLevelChange -= onLevelChange;
        Events.OnRequestLevel -= getLevel;
    }
    

    private void onLevelChange(float level)
    {
        this.level = level;
    }

    private float getLevel()
    {
        return level;
    }
    
}
