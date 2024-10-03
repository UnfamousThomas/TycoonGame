using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int scoreIncrease = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Events.SetMoney(Events.RequestMoney() + scoreIncrease);
        Destroy(gameObject);
    }
}
