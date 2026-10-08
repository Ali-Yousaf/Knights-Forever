using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float fillTime = 0.3f;
    [SerializeField] private Color flashColor = Color.white;

    private Image fill;
    private Color baseColor;
    private int lastHealth;

    void Awake()
    {
        fill = slider.fillRect.GetComponent<Image>();
        baseColor = fill.color;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        lastHealth = health;
    }

    public void SetHealth(int health)
    {
        slider.DOKill();
        slider.DOValue(health, fillTime).SetEase(Ease.OutCubic);

        if (health < lastHealth)
        {
            transform.DOKill(true);
            transform.DOPunchScale(Vector3.one * 0.08f, 0.25f, 8, 0.5f);

            fill.DOKill();
            fill.color = flashColor;
            fill.DOColor(baseColor, 0.25f);
        }

        lastHealth = health;
    }
}