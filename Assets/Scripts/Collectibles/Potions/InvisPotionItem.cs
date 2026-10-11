using UnityEngine;

public class InvisPotionItem : PickupItem
{
    [SerializeField, Range(0f, 1f)] private float transparency = 0.4f;
    [SerializeField] private float invisDuration = 5f;

    protected override bool OnCollect(GameObject collector)
    {
        PlayerPotionManager.Instance.ActivateInvisibility(transparency, invisDuration);
        return true;
    }
}