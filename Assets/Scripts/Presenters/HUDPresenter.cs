using TMPro;
using UnityEngine;

public class HUDPresenter : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
    }

    private void OnSetMoney(int money)
    {
        moneyText.text = "Money: " + money;
    }
    
}
