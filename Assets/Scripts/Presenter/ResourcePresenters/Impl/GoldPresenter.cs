using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPresenter : ResourcePresenter
{
    public override void Awake()
    {
        Events.OnSetGold += OnSetGold;
    }

    public override void OnDestroy()
    {
        Events.OnSetGold -= OnSetGold;
    }

    public void OnSetGold(float value)
    {
        text.text = value.ToString();
    }
}
