using System;
using System.Collections.Generic;

public static class Events
{
    public static event Action<float> OnSetMoney;
    public static void SetMoney(float value) => OnSetMoney?.Invoke(value);

    public static event Func<float> OnRequestMoney;
    public static float RequestMoney() => OnRequestMoney?.Invoke() ?? 0;

    public static event Func<float> OnRequestLevel;
    public static float RequestLevel() => OnRequestLevel?.Invoke() ?? 0;

    public static event Action<float> OnLevelChange;
    public static void SetLevel(float level) => OnLevelChange?.Invoke(level);

    public static event Action<BusinessData> OnBusinessSelected;
    public static void SelectBusiness(BusinessData data) => OnBusinessSelected?.Invoke(data);

    public static event Action<Business> OnBusinessBuilt;
    public static void BuildBusiness(Business business) => OnBusinessBuilt?.Invoke(business);

    public static event Action<Business> OnBusinessUpgraded;
    public static void UpgradeBusiness(Business business) => OnBusinessUpgraded?.Invoke(business);

    public static event Action<Business> OnBusinessClicked;
    public static void ClickBusiness(Business business) => OnBusinessClicked?.Invoke(business);
    
    public static event Action<AudioClipGroup> OnAudioClipGroupPlayed;
    public static void PlayAudioClipGroup(AudioClipGroup group) => OnAudioClipGroupPlayed?.Invoke(group);

    public static event Action<List<Business>> onLoadedBusinesses;
    public static void LoadBusinesses(List<Business> businesses) => onLoadedBusinesses?.Invoke(businesses);
    
    public static event Func<List<Business>> OnRequestBusinesses;
    public static List<Business> RequestBusinesses() => OnRequestBusinesses?.Invoke() ?? new List<Business>();

}
