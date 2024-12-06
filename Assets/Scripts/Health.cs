using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    private float currentHealth;

    private Image healthBar; // Reference to the health bar UI element

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
        }
    }

    public void AssignHealthBar(Image healthBarImage)
    {
        healthBar = healthBarImage;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy Bullet"))
        {
            TakeDamage(1);
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(7); // Replace with your "game over" logic
    }
}
