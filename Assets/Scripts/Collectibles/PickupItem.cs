using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PickupItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (OnCollect(other.gameObject))
            Destroy(gameObject);
    }

    // Return true if the pickup should be consumed
    protected abstract bool OnCollect(GameObject collector);
}