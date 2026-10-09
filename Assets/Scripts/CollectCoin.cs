using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("Coin Collected");
            Destroy(gameObject);
        }
    }
}
