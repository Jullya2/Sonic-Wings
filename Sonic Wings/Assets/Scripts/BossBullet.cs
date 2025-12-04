using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 8f;
    public float life = 5f; // Tempo de vida (limpeza de cena)
    public int damage = 1; // Dano que a bala causa ao Player

    void Start()
    {
        // Destrói a bala após um tempo, caso ela passe pela tela
        Destroy(gameObject, life);
    }

    void Update()
    {
        // Move a bala para baixo (direção do inimigo)
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verifica se atingiu o jogador (Tag "Player")
        if (other.CompareTag("Player"))
        {
            // Tenta obter o script de vida do jogador
            PlayerHealth ph = other.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                // Aplica dano ao jogador
                ph.TakeDamage(damage);
            }

            // 2. Destrói a bala ao atingir o jogador
            Destroy(gameObject);
        }
    }
}