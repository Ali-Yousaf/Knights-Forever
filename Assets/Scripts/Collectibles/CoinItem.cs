using UnityEngine;

public class CoinPickup : PickupItem
{
    [SerializeField, Min(1)] private int value = 1;

    protected override bool OnCollect(GameObject collector)
    {
        Debug.Log("coin collected" + value);
        return true;
    }
}