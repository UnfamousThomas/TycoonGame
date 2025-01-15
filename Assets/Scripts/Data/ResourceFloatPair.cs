using UnityEngine;

[System.Serializable]
public class ResourceFloatPair
{
    public ResourceFloatPair(ResourceType type, float value)
    {
        this.type = type;
        this.value = value;
    }

    public ResourceType type;
    public float value;
}