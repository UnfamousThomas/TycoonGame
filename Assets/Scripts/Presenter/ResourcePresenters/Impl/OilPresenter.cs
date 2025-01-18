using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPresenter : ResourcePresenter
{
    public override void Awake()
    {
        Events.OnSetOil += OnSetOil;
    }

    public override void OnDestroy()
    {
        Events.OnSetOil -= OnSetOil;
    }

    public void OnSetOil(float value)
    {
        text.text = FormatValue(value);
    }
}