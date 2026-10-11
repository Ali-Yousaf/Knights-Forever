using UnityEngine;

public class StrengthPotionItem : PickupItem
{
    [SerializeField] private float damageMultiplier = 2f;
    [SerializeField] private float strengthDuration = 5f;

    protected override bool OnCollect(GameObject collector)
    {
        PlayerPotionManager.Instance.ActivateStrength(damageMultiplier, strengthDuration);
        return true;
    }
}