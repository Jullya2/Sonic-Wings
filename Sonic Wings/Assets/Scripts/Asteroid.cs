using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Variáveis de Movimento em Onda (Seno)
    public float amplitude = 2f;
    public float frequency = 1f;
    public float speedMin = 4f;
    public float speedMax = 7f;
    public float lifeTime = 10f;

    public AudioClip explodeSound;
    AudioSource audioSource;

    Rigidbody2D rb;
    int hp = 1;
    bool hasInit = false;

    // Variáveis para o movimento em onda
    private float initialX;
    private float timeOffset;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (rb == null)
        {
            Debug.LogError("Asteroid: Rigidbody2D faltando!");
            Destroy(gameObject);
            return;
        }

        rb.freezeRotation = true;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);

        if (!hasInit)
        {
            Vector2 fallback = Vector2.down;
            float s = Random.Range(speedMin, speedMax);
            rb.linearVelocity = fallback * s;

            initialX = transform.position.x;
            timeOffset = Random.Range(0f, 10f);
        }
    }

    void Update()
    {
        ApplySineMovement();
    }

    void ApplySineMovement()
    {
        float newX = initialX + amplitude * Mathf.Sin(frequency * (Time.time + timeOffset));
        rb.position = new Vector2(newX, rb.position.y);
    }

    public void Initialize(Vector2 direction, float size = 1f, float speedOverride = -1f)
    {
        hasInit = true;
        transform.localScale = Vector3.one * size;
        hp = Mathf.Max(1, Mathf.RoundToInt(size));

        float s = speedOverride > 0 ? speedOverride : Random.Range(speedMin, speedMax);

        initialX = transform.position.x;
        timeOffset = Random.Range(0f, 10f);

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * s;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(1);

            // Chamada interna corrigida
            Die();
        }

        // REMOVIDO: A lógica de dano por Bullet aqui, pois a Bullet agora lida com o inimigo.
        // Se você quiser que o Asteroid.cs lide com o dano da Bullet, você deve reverter a mudança no Bullet.cs
        // e deixar este bloco, mas garantir que a bala não chame Die()
    }

    // CORREÇÃO CRÍTICA: A função deve ser pública e estar aqui dentro da classe!
    public void Die()
    {
        if (explodeSound != null)
        {
            AudioSource.PlayClipAtPoint(explodeSound, Camera.main != null ? Camera.main.transform.position : transform.position);
        }

        ScreenShake.Instance?.Shake(0.15f, 0.1f);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        if (rb != null) rb.linearVelocity = Vector2.zero;

        Destroy(gameObject, 0.1f);
    }
}