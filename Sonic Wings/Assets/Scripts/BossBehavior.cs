using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // Enum e Variáveis (Você deve adaptá-las conforme sua necessidade)
    public enum BossState { IntroMove, PatternA, PatternB }

    public float movementSpeed = 3f;
    public float shootInterval = 1.0f;
    public float patternDuration = 5f;

    public int maxHealth = 100;
    private int currentHealth;

    private float shootTimer;
    private float patternTimer;
    private BossState currentState;
    private Vector3 targetScreenPosition = new Vector3(0, 4, 0); // Posição de combate

    void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.IntroMove;
        patternTimer = patternDuration;
        shootTimer = shootInterval;
        // Garanta que o Boss tenha a Tag "Enemy" no Inspector para receber dano!
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        // Adicione lógica de troca de padrão aqui, se necessário
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Chefe Destruído! Chamando Fim de Fase.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LevelComplete();
        }
        else
        {
            Debug.LogError("ERRO CRÍTICO: GameManager.Instance é nulo. A tela de vitória não pode ser ativada.");
        }

        Destroy(gameObject);
    }

    // Funções de Comportamento (Devem existir no seu script)
    void HandleMovement() { /* Implementação de MoveTowards aqui */ }
    void HandleShooting() { /* Implementação do timer de tiro aqui */ }
    // ... (outras funções de Pattern)
}