using UnityEngine;
using UnityEngine.UI;
using System.Collections; // <<< NOVIDADE: Necessário para usar a Coroutine (IEnumerator)

public class BossBehavior : MonoBehaviour
{
    // =================================================================
    // ENUM E VARIÁVEIS PÚBLICAS
    // =================================================================
    public enum BossState { IntroMove, LateralMove, PatternB }
    
    // Configurações de Movimento
    public float movementSpeed = 3f;
    public Vector3 targetScreenPosition = new Vector3(0, 4, 0); 
    public float lateralSpeed = 2f; 
    public float lateralDistance = 3f; 
    
    // Configurações de Tiro e Dano
    public GameObject bossBulletPrefab;
    public float shootInterval = 1.0f;
    public int maxHealth = 100;

    // REFERÊNCIA DA UI
    public Slider healthBar; 
    
    // VARIÁVEIS DE TREMOR (SHAKE)
    public float shakeDuration = 0.1f; // Duração total do tremor
    public float shakeMagnitude = 0.1f; // Intensidade do movimento
    
    // =================================================================
    // VARIÁVEIS PRIVADAS
    // =================================================================
    private int currentHealth;
    private float shootTimer; 
    private BossState currentState; 
    private Vector3 startPosition; 
    private int direction = 1; 

    
    void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.IntroMove; 
        shootTimer = shootInterval; 
        startPosition = targetScreenPosition; 
        
        // Inicializa a barra de vida
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            healthBar.gameObject.SetActive(false); 
        }
    }

    void Update()
    {
        HandleMovement(); 
        HandleShooting(); 
    }
    
    // =================================================================
    // LÓGICA DE MOVIMENTO
    // =================================================================

    void HandleMovement()
    {
        if (currentState == BossState.IntroMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetScreenPosition, 
                movementSpeed * Time.deltaTime
            );

            if (transform.position == targetScreenPosition)
            {
                currentState = BossState.LateralMove; 
                
                if (healthBar != null)
                {
                    healthBar.gameObject.SetActive(true);
                }
            }
        }
        
        else if (currentState == BossState.LateralMove)
        {
            Vector3 target = startPosition + new Vector3(lateralDistance * direction, 0, 0);

            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                lateralSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                direction *= -1; 
            }
        }
    }
    
    // =================================================================
    // LÓGICA DE TIRO E DANO
    // =================================================================

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss atingido! Vida restante: " + currentHealth);
        
        // NOVIDADE: Inicia a rotina de tremor quando leva dano
        StartCoroutine(Shake());
        
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    // COROUTINE PARA O EFEITO DE TREMOR
    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Gera um deslocamento aleatório
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Move o Boss para a posição tremida
            transform.position = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null; // Espera o próximo frame
        }

        // Garante que o Boss volte à posição original no final do tremor
        transform.position = originalPos;
    }

    void Die()
    {
        Debug.Log("Chefe Destruído! Chamando Fim de Fase.");
        
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LevelComplete();
        } else {
            Debug.LogError("GameManager.Instance é nulo.");
        }
        Destroy(gameObject);
    }
    
    void HandleShooting()
    {
        if (currentState == BossState.LateralMove || currentState == BossState.PatternB) 
        {
            shootTimer -= Time.deltaTime;
            
            if (shootTimer <= 0)
            {
                ShootPatternA(); 
                shootTimer = shootInterval; 
            }
        }
    }
    
    void ShootPatternA()
    {
        if (bossBulletPrefab != null)
        {
            Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);
        } 
    }
}