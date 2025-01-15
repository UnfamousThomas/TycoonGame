using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SpaceTycoon/Resource/Resource")]
public class Resource : ScriptableObject
{
    public ResourceType resourceType;
    public Sprite icon;
    public List<ResourceSpritePair> ResourceSprites;
    public Sprite GetSprite(ResourceLevel level)
    {
        foreach (var pair in ResourceSprites)
        {
            if (pair.level == level)
            {
                return pair.sprite;
            }
        }
        return null; // or a default sprite
    }
    
}