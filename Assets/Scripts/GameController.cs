using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _money = 0;
    public int initialMoney = 10;
    
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

    private void OnSetMoney(int money)
    {
        _money = money;
    }

    private int OnGetMoney()
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
