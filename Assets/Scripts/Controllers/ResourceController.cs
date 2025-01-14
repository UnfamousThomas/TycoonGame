using System.Collections.Generic;

public class ResourceController
{
    public static Dictionary<ResourceType, float> GetResources()
    {
        Dictionary<ResourceType, float> resources = new Dictionary<ResourceType, float>();
        
        float money = Events.RequestMoney();
        float oil = Events.RequestOil();
        float water = Events.RequestWater();
        float iron = Events.RequestIron();
        float gold = Events.RequestGold();
        float rocks = Events.RequestRocks();
        
        resources.Add(ResourceType.MONEY, money);
        resources.Add(ResourceType.OIL, oil);
        resources.Add(ResourceType.WATER, water);
        resources.Add(ResourceType.IRON, iron);
        resources.Add(ResourceType.GOLD, gold);
        resources.Add(ResourceType.ROCK, rocks);

        return resources;
    }
}