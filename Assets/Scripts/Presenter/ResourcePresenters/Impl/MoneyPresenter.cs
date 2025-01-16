public class MoneyPresenter : ResourcePresenter
{
    public override void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
    }

    public override void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
    }

    public void OnSetMoney(float value)
    {
       text.text = FormatValue(value) + " $";
    }
}
