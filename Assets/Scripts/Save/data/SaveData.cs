using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class SaveData
{
    public List<BusinessSaveData> businessSaveData;
    public List<OreLocation> ores;
    public float level;
    public float money;
}