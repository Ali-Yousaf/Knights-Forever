using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    void Awake()
    {
        if(Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

        if(healthBar == null)
            Debug.Log("Assign Health Bar");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        
        if(currentHealth <= 0)
            PlayerController.Instance.Die();
            
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(20);
        }
    }
}
