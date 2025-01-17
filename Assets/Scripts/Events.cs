using System;
using System.Collections.Generic;

public static class Events
{

    public static event Action<float> OnSetMoneyProduction;
    public static void SetMoneyProduction(float value) => OnSetMoneyProduction?.Invoke(value);

    public static event Action<BusinessData> OnBusinessSelected;
    public static void SelectBusiness(BusinessData data) => OnBusinessSelected?.Invoke(data);

    public static event Action<Business> OnBusinessBuilt;
    public static void BuildBusiness(Business business) => OnBusinessBuilt?.Invoke(business);

    public static event Action<Business> OnBusinessUpgradedFinish;
    public static void FinishUpgradeBusiness(Business business) => OnBusinessUpgradedFinish?.Invoke(business);
    
    public static event Action<Business> OnBusinessUpgradedStart;
    public static void StartUpgradeBusiness(Business business) => OnBusinessUpgradedStart?.Invoke(business);


    public static event Action<Business> OnBusinessSold;
    public static void SellBusiness(Business business) => OnBusinessSold?.Invoke(business);

    public static event Action<Business> OnBusinessClicked;
    public static void ClickBusiness(Business business) => OnBusinessClicked?.Invoke(business);
    
    public static event Action<AudioClipGroup> OnAudioClipGroupPlayed;
    public static void PlayAudioClipGroup(AudioClipGroup group) => OnAudioClipGroupPlayed?.Invoke(group);

    public static event Action<List<Business>> onLoadedBusinesses;
    public static void LoadBusinesses(List<Business> businesses) => onLoadedBusinesses?.Invoke(businesses);
    
    public static event Func<List<Business>> OnRequestBusinesses;
    public static List<Business> RequestBusinesses() => OnRequestBusinesses?.Invoke() ?? new List<Business>();

    public static event Action OnExitMenu;
    public static void ExitMenu() => OnExitMenu?.Invoke();
    
    // Tutorial
    public static event Func<bool> onTutorialFinishedRequest;
    public static bool TutorialFinishedRequest() => onTutorialFinishedRequest?.Invoke() ?? false;
    public static event Action<bool> OnSetTutorialFinished;
    public static void SetTutorialFinished(bool value) => OnSetTutorialFinished?.Invoke(value);
    
    
    
    // Resources
    //Money
    public static event Func<float> OnRequestMoney;
    public static float RequestMoney() => OnRequestMoney?.Invoke() ?? 0;

    public static event Action<float> OnSetMoney;
    public static void SetMoney(float value) => OnSetMoney?.Invoke(value);
    //Level
    
    public static event Func<float> OnRequestLevel;
    public static float RequestLevel() => OnRequestLevel?.Invoke() ?? 0;

    public static event Action<float> OnLevelChange;
    public static void SetLevel(float level) => OnLevelChange?.Invoke(level);
    //Rocks

    public static event Func<float> OnRequestRocks;
    public static float RequestRocks() => OnRequestRocks?.Invoke() ?? 0;

    public static event Action<float> OnSetRocks;
    public static void SetRocks(float rocks) => OnSetRocks?.Invoke(rocks);
    
    //Gold
    public static event Func<float> OnRequestGold;
    public static float RequestGold() => OnRequestGold?.Invoke() ?? 0;

    public static event Action<float> OnSetGold;
    public static void SetGold(float gold) => OnSetGold?.Invoke(gold);
    
    //Iron
    public static event Func<float> OnRequestIron;
    public static float RequestIron() => OnRequestIron?.Invoke() ?? 0;

    public static event Action<float> OnSetIron;
    public static void SetIron(float iron) => OnSetIron?.Invoke(iron);
    
    //Water
    public static event Func<float> OnRequestWater;
    public static float RequestWater() => OnRequestWater?.Invoke() ?? 0;

    public static event Action<float> OnSetWater;
    public static void SetWater(float water) => OnSetWater?.Invoke(water);
    
    //Oil
    public static event Func<float> OnRequestOil;
    public static float RequestOil() => OnRequestOil?.Invoke() ?? 0;

    public static event Action<float> OnSetOil;
    public static void SetOil(float oil) => OnSetOil?.Invoke(oil);
    
    //Ore Saving
    public static event Func<List<OreLocation>> OnRequestOreLocations;
    public static List<OreLocation> RequestOreLocations() => OnRequestOreLocations?.Invoke() ?? new List<OreLocation>();
}
