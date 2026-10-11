using UnityEngine;

public class ShieldPotionItem : PickupItem
{
    [SerializeField] private float shieldDuration = 5f;

    protected override bool OnCollect(GameObject collector)
    {
        PlayerPotionManager.Instance.ActivateShield(shieldDuration);
        return true;
    }
}