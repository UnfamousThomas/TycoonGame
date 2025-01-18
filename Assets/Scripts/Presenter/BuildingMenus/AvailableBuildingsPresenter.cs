using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableBuildingsPresenter : MonoBehaviour
{
    public List<BusinessData> displayedBusinesses;

    public BusinessCardPresenter prefab;
    public BusinessController businessController;
    public BuildingsMenuInfoPresenter buildingsMenuInfoPresenter;

    private List<BusinessCardPresenter> _presenters = new List<BusinessCardPresenter>();
    
    // Start is called before the first frame update
    // If HQ is not the first in list, this whole system WILL collapse!
    void Start()
    {
        foreach (var business in displayedBusinesses)
        {
            BusinessCardPresenter presenter = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
            presenter.businessData = business;
            presenter.businessController = businessController;
            presenter.buildingsMenuInfo = buildingsMenuInfoPresenter;
            if (!businessController.isBusinessBuilt(displayedBusinesses[0]) && !"Headquarters".Equals(business.businessName))
                presenter._button.interactable = false;
            _presenters.Add(presenter);
        }
    }

    private void OnEnable()
    {
        if (!businessController.isBusinessBuilt(displayedBusinesses[0]))
            return;
        foreach (var presenter in _presenters)
        {
            presenter._button.interactable = true;
        }
    }
}
