public class IronPresenter : ResourcePresenter
{
    public override void Awake()
    {
        Events.OnSetIron += OnSetIron;
    }

    public override void OnDestroy()
    {
        Events.OnSetIron -= OnSetIron;
    }

    public void OnSetIron(float value)
    {
        text.text = value.ToString();
    }
}
