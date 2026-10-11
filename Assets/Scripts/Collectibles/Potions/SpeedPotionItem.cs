using UnityEngine;

public class SpeedPotionItem : PickupItem
{
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float speedBoostDuration = 5f;

    protected override bool OnCollect(GameObject collector)
    {
        PlayerPotionManager.Instance.ActivateSpeedBoost(speedMultiplier, speedBoostDuration);
        return true;
    }
}