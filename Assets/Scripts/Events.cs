using System;

public static class Events
{
    public static event Action<float> OnSetMoney;
    public static void SetMoney(float value) => OnSetMoney?.Invoke(value);

    public static event Func<float> OnRequestMoney;
    public static float RequestMoney() => OnRequestMoney?.Invoke() ?? 0;

    public static event Action<float> OnLevelChange;
    public static void SetLevel(float level) => OnLevelChange?.Invoke(level);
    
    public static event Action OnGameCompleted;
    public static void GameCompleted() => OnGameCompleted?.Invoke();

    public static event Action<BusinessData> OnBusinessSelected;
    public static void SelectBusiness(BusinessData data) => OnBusinessSelected?.Invoke(data);

    public static event Action<Business> OnBusinessBuilt;
    public static void BuildBusiness(Business business) => OnBusinessBuilt?.Invoke(business);

    public static event Action<Business> OnBusinessUpgraded;
    public static void UpgradeBusiness(Business business) => OnBusinessUpgraded?.Invoke(business);

    public static event Action<Business, float> OnBusinessClicked;
    public static void ClickBusiness(Business business, float money) => OnBusinessClicked?.Invoke(business, money);
}
