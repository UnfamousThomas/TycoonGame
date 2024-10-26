using System;

public static class Events
{
    public static event Action<float> OnSetMoney;
    public static void SetMoney(float value) => OnSetMoney?.Invoke(value);

    public static event Func<float> OnRequestMoney;
    public static float RequestMoney() => OnRequestMoney?.Invoke() ?? 0;

    public static event Action OnLevelCompleted;
    public static void LevelCompleted() => OnLevelCompleted?.Invoke();

    public static event Action<BusinessData> OnBusinessSelected;

    public static void SelectBusiness(BusinessData data) => OnBusinessSelected?.Invoke(data);
}
