using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceCostPresenter : MonoBehaviour
{
    public TextMeshProUGUI costText;

    public void SetCost(float cost)
    {
        costText.text = cost.ToString();
    }
}
