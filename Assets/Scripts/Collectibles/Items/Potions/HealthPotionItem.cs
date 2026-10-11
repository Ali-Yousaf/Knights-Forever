using UnityEngine;

public class HealthPotionItem : PickupItem
{
    [SerializeField] private int healthAmount;

    protected override bool OnCollect(GameObject collector)
    {
        collector.GetComponent<PlayerHealth>().Heal(healthAmount);
        Debug.Log("Player Healed");
        return true;
    }
}