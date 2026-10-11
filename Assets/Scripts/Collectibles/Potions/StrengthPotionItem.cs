using System.Collections;
using UnityEngine;

public class StrengthPotionItem : PickupItem
{
    [SerializeField] private int damageMultiplier = 2;
    [SerializeField] private float strengthDuration = 5f;

    private Coroutine strengthCoroutine;

    protected override bool OnCollect(GameObject collector)
    {
        if (strengthCoroutine != null)
            StopCoroutine(strengthCoroutine);

        strengthCoroutine = StartCoroutine(StrengthDurationRoutine());

        return true;
    }

    private IEnumerator StrengthDurationRoutine()
    {
        int baseDamage = PlayerAttack.Instance.attackDamage;

        PlayerAttack.Instance.attackDamage *= damageMultiplier;

        yield return new WaitForSeconds(strengthDuration);

        PlayerAttack.Instance.attackDamage = baseDamage;
        strengthCoroutine = null;
    }
}