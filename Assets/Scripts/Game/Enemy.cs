using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float health = 1f;
    [SerializeField] private int scoreReward = 10;
    [SerializeField] private GameObject explosionEffectPrefab;

    private Rigidbody2D rb;
    private float currentHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = health;
        rb.velocity = Vector2.down * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1f);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("GameBoundary"))
        {
            // Enemy reached the bottom - player dies
            GameOver();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.AddScore(scoreReward);
        
        // Spawn explosion effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        AudioManager.Instance.PlaySFX("explosion");
        Destroy(gameObject);
    }

    private void GameOver()
    {
        // Show ragebait popup when player dies
        RagebaitPopupSystem.Instance.ShowDeathMessage();
        
        GameManager.Instance.GameOver();
        AudioManager.Instance.PlaySFX("gameover");
    }
}
