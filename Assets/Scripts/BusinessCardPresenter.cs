using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessCardPresenter : MonoBehaviour
{
    public BusinessData businessData;
    
    public TextMeshProUGUI costText;
    public Image iconImage;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null)
            _button.onClick.AddListener(Pressed);

        if (businessData != null)
        {
            costText.text = businessData.cost.ToString();
            iconImage.sprite = businessData.icon;
        }

        Events.OnSetMoney += OnSetMoney;
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
    }

    public void Pressed()
    {
        Events.SelectBusiness(businessData);
    }

    public void OnSetMoney(float value)
    {
        if (value < businessData.cost)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }
}
