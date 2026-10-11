using System.Collections;
using UnityEngine;

public class SpeedPotionItem : PickupItem
{
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float speedBoostDuration = 5f;

    private Coroutine speedCoroutine;

    protected override bool OnCollect(GameObject collector)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        speedCoroutine = StartCoroutine(SpeedDurationRoutine());

        return true;
    }

    private IEnumerator SpeedDurationRoutine()
    {
        float baseSpeed = PlayerController.Instance.moveSpeed;

        PlayerController.Instance.moveSpeed *= speedMultiplier;

        yield return new WaitForSeconds(speedBoostDuration);

        PlayerController.Instance.moveSpeed = baseSpeed;
        speedCoroutine = null;
    }
}