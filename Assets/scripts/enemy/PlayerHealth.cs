using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    int currentHealth;

    [Header("UI")]
    public Slider healthBar;

    [Header("Respawn Settings")]
    public Transform respawnPoint;

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.maxValue = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Player took {amount} damage. HP: {currentHealth}/{maxHealth}");

        UpdateUI();

        if (currentHealth <= 0)
            Die();
    }

    void UpdateUI()
    {
        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    void Die()
    {
        Debug.Log("Player died!");

        // Optional: add death animation, disable controls, etc.
        StartCoroutine(RespawnAfterDelay(1.5f));
    }

    System.Collections.IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset position
        if (respawnPoint != null)
            transform.position = respawnPoint.position;
        else
            Debug.LogWarning("No respawn point set!");

        // Reset health
        currentHealth = maxHealth;
        UpdateUI();
        Debug.Log("Player respawned!");
    }
}
