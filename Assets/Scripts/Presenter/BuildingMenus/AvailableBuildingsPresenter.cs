using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableBuildingsPresenter : MonoBehaviour
{
    public List<BusinessData> displayedBusinesses;

    public BusinessCardPresenter prefab;
    public BusinessController businessController;
    public BuildingsMenuInfoPresenter buildingsMenuInfoPresenter;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var business in displayedBusinesses)
        {
            BusinessCardPresenter presenter = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
            presenter.businessData = business;
            presenter.businessController = businessController;
            presenter.buildingsMenuInfo = buildingsMenuInfoPresenter;
        }
    }
    
}
