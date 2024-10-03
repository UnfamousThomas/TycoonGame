using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPresenter : MonoBehaviour
{

    public ItemData itemData;
    private Button _itemButton;

    public void init(ItemData itemData)
    {
        this.itemData = itemData;
        _itemButton.image.sprite = itemData.sprite;
    }
    private void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
        _itemButton = GetComponent<Button>();
        init(itemData);
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
    }

    private void OnSetMoney(int money)
    {
        _itemButton.interactable = itemData.cost <= money;
    }
    
    
}
