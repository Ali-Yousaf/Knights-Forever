using System.Collections;
using UnityEngine;

public class PlayerPotionManager : MonoBehaviour
{
    public static PlayerPotionManager Instance;

    private PlayerController playerController;
    private PlayerAttack playerAttack;

    private Coroutine speedCoroutine;
    private Coroutine strengthCoroutine;
    private Coroutine invisCoroutine;

    private SpriteRenderer[] spriteRenderers;
    private Color[] originalColors;

    [SerializeField] private GameObject shieldObject;

    private Coroutine shieldCoroutine;

    private float originalSpeed;
    private int originalDamage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        shieldObject.SetActive(false);

        playerController = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttack>();

        originalSpeed = playerController.moveSpeed;
        originalDamage = playerAttack.attackDamage;

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        originalColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
            originalColors[i] = spriteRenderers[i].color;
    }

    public void ActivateSpeedBoost(float multiplier, float duration)
    {
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
            playerController.moveSpeed = originalSpeed;
        }

        speedCoroutine = StartCoroutine(SpeedRoutine(multiplier, duration));
    }

    private IEnumerator SpeedRoutine(float multiplier, float duration)
    {
        playerController.moveSpeed = originalSpeed * multiplier;

        yield return StartCoroutine(PrintTimer("Speed Potion", duration));

        playerController.moveSpeed = originalSpeed;
        speedCoroutine = null;
    }

    public void ActivateStrength(float multiplier, float duration)
    {
        if (strengthCoroutine != null)
        {
            StopCoroutine(strengthCoroutine);
            playerAttack.attackDamage = originalDamage;
        }

        strengthCoroutine = StartCoroutine(StrengthRoutine(multiplier, duration));
    }

    private IEnumerator StrengthRoutine(float multiplier, float duration)
    {
        playerAttack.attackDamage = Mathf.RoundToInt(originalDamage * multiplier);

        yield return StartCoroutine(PrintTimer("Strength Potion", duration));

        playerAttack.attackDamage = originalDamage;
        strengthCoroutine = null;
    }

    public void ActivateInvisibility(float transparency, float duration)
    {
        if (invisCoroutine != null)
            StopCoroutine(invisCoroutine);

        invisCoroutine = StartCoroutine(InvisibilityRoutine(transparency, duration));
    }

    private IEnumerator InvisibilityRoutine(float transparency, float duration)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] == null)
                continue;

            Color color = originalColors[i];
            color.a = transparency;
            spriteRenderers[i].color = color;
        }

        yield return StartCoroutine(PrintTimer("Invisibility Potion", duration));

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] != null)
                spriteRenderers[i].color = originalColors[i];
        }

        invisCoroutine = null;
    }

    private IEnumerator PrintTimer(string effectName, float duration)
    {
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            Debug.Log($"[{effectName}] Time Remaining: {remainingTime:F0}s");

            yield return new WaitForSeconds(1f);

            remainingTime -= 1f;
        }

        Debug.Log($"[{effectName}] Effect Ended!");
    }

    public void ActivateShield(float duration)
    {
        if (shieldCoroutine != null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        shieldObject.SetActive(true);

        yield return StartCoroutine(PrintTimer("Shield Potion", duration));

        shieldObject.SetActive(false);
        shieldCoroutine = null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}