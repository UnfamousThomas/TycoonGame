using System;

public static class Events
{
    public static event Action<int> OnSetMoney;
    public static void SetMoney(int value) => OnSetMoney?.Invoke(value);

    public static event Func<int> OnRequestMoney;
    public static int RequestMoney() => OnRequestMoney?.Invoke() ?? 0;

    public static event Action OnLevelCompleted;
    public static void LevelCompleted() => OnLevelCompleted?.Invoke();
}
