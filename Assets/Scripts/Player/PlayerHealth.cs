using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    private PlayerController playerController;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        playerController = GetComponent<PlayerController>();
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

        if (currentHealth <= 0)
            playerController.Die();

        else
            playerController.Hurt();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            TakeDamage(20);
    }
}