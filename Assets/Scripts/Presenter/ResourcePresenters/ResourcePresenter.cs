using TMPro;
using UnityEngine;

public abstract class ResourcePresenter : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    public abstract void Awake();
    public abstract void OnDestroy();
    
    protected string FormatValue(float value)
    {
        if (value >= 1_000_000_000)
            return (value / 1_000_000_000f).ToString("0.#") + " bln";
        else if (value >= 1_000_000)
            return (value / 1_000_000f).ToString("0.#") + " mln";
        else if (value >= 1_000)
            return (value / 1_000f).ToString("0.#") + "k";
        else
            return value.ToString("0.#");
    }
}