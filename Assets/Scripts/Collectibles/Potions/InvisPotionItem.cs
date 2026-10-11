using System.Collections;
using UnityEngine;

public class InvisPotionItem : PickupItem
{
    [SerializeField] private float invisDuration = 5f;
    [SerializeField, Range(0f, 1f)] private float transparency = 0.4f;

    private Coroutine invisCoroutine;

    protected override bool OnCollect(GameObject collector)
    {
        if (invisCoroutine != null)
            StopCoroutine(invisCoroutine);

        invisCoroutine = StartCoroutine(InvisibilityRoutine());

        return true;
    }

    private IEnumerator InvisibilityRoutine()
    {
        SpriteRenderer[] spriteRenderers = PlayerController.Instance.GetComponentsInChildren<SpriteRenderer>();
        Color[] originalColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
            Color color = originalColors[i];
            color.a = transparency;
            spriteRenderers[i].color = color;
        }

        yield return new WaitForSeconds(invisDuration);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] != null)
                spriteRenderers[i].color = originalColors[i];
        }

        invisCoroutine = null;
    }
}