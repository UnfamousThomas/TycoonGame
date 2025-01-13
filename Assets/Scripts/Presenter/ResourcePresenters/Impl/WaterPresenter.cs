using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPresenter : ResourcePresenter
{
    public override void Awake()
    {
        Events.OnSetWater += OnSetWater;
    }

    public override void OnDestroy()
    {
        Events.OnSetWater -= OnSetWater;
    }

    public void OnSetWater(float value)
    {
        text.text = FormatValue(value);
    }
}
