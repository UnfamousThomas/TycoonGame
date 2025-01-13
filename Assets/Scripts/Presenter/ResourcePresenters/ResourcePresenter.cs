using TMPro;
using UnityEngine;

public abstract class ResourcePresenter : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    public abstract void Awake();
    public abstract void OnDestroy();
}