using UnityEngine;

public class GameController : MonoBehaviour
{
    private float _money = 0;
    public float initialMoney = 10;
    
    private void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
        Events.OnRequestMoney += OnGetMoney;
        Events.OnLevelCompleted += OnLevelCompleted;
    }

    public void Start()
    {
        Events.SetMoney(initialMoney);
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
        Events.OnRequestMoney -= OnGetMoney;
        Events.OnLevelCompleted -= OnLevelCompleted;
    }

    private void OnSetMoney(float money)
    {
        _money = money;
    }

    private float OnGetMoney()
    {
        return _money;
    }

    private void OnLevelCompleted()
    {
        // TODO next level menu, if there are no levels left, make GameCompleted display something
    }

    private void GameCompleted()
    {
        
    }
}
