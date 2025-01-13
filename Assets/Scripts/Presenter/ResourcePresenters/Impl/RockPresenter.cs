using System;

public class RockPresenter : ResourcePresenter
{
    public override void Awake()
    {
        Events.OnSetRocks += OnSetRocks;
    }

    public override void OnDestroy()
    {
        Events.OnSetRocks -= OnSetRocks;
    }

    public void OnSetRocks(float value)
    {
        text.text = FormatValue(value);
    }
}
