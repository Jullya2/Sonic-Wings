using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;

    public Slider healthSlider;

    public AudioSource audioSource;
    public AudioClip damageSound;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null) healthSlider.value = 1f;
    }

    public void TakeDamage(int amount)
    {
        // CORREÇÃO PARA TESTE DE DANO:
        // Se essa linha aparecer no console, o problema é puramente visual (o Slider)
        // Se NÃO aparecer, o problema é na Tag/Collider do jogador/inimigo.
        Debug.Log("JOGADOR RECEBEU DANO! Reduzindo a vida em: " + amount + ".");

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        ScreenShake.Instance?.Shake(0.2f, 0.15f);

        if (healthSlider != null)
        {
            // Atualiza o valor do Slider para refletir a vida atual.
            healthSlider.value = (float)currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();

            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        if (healthSlider != null)
            healthSlider.value = (float)currentHealth / maxHealth;
    }
}