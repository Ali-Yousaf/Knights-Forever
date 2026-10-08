using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    private PlayerController controller;
    private SpriteRenderer sprite;
    private Color baseColor;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        controller = GetComponent<PlayerController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        baseColor = sprite.color;
    }

    void Start()
    {
        if (healthBar == null)
        {
            Debug.LogError("Assign Health Bar");
            return;
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthBar.SetHealth(currentHealth);

        sprite.DOKill();
        sprite.color = Color.red;
        sprite.DOColor(baseColor, 0.2f);

        if (currentHealth <= 0)
            controller.Die();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            TakeDamage(20);
    }
}