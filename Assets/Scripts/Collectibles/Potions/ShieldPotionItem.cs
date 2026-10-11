using UnityEngine;

public class ShieldPotionItem : PickupItem
{
    [SerializeField] private float shieldDuration = 5f;

    protected override bool OnCollect(GameObject collector)
    {
        if (collector.GetComponent<PlayerController>() == null)
            return false;

        PlayerController.Instance.ActivateShield(shieldDuration);

        return true;
    }
}