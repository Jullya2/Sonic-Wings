using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float life = 3f;
    // Variável para o dano (CRÍTICO: Defina no Inspector, ex: 1)
    public int damage = 1; 

    // NOVO: Referência ao Prefab que contém o script FloatingText
    public GameObject floatingTextPrefab; // <<< Adicione este campo no Inspector

    void Start()
    {
        Destroy(gameObject, life);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Tenta obter o script BossBehavior (O Avião Grande)
            BossBehavior boss = other.GetComponent<BossBehavior>();

            if (boss != null)
            {
                // Se for o Boss, chama o método TakeDamage
                boss.TakeDamage(damage);
                // O BossBehavior se encarrega de chamar GameManager.LevelComplete() quando morrer.
                Debug.Log("Bala atingiu o Boss e causou dano!");
            }
            else
            {
                // Se não for o Boss, tenta obter o script Asteroid
                Asteroid a = other.GetComponent<Asteroid>();

                if (a != null)
                {
                    // A bala causa a morte e pontuação no Asteroide
                    
                    // Assumindo que você usa GameManager ou GameController
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.AddScore(10);
                        
                        // NOVIDADE: Instancia o Feedback Visual (NICE! +10)
                        if (floatingTextPrefab != null)
                        {
                            GameObject floatingText = Instantiate(
                                floatingTextPrefab, 
                                other.transform.position, 
                                Quaternion.identity
                            );
                            
                            // Tenta obter o componente FloatingText para definir a mensagem
                            FloatingText ft = floatingText.GetComponent<FloatingText>();
                            if (ft != null)
                            {
                                ft.SetText("NICE! +10"); // Mensagem que será exibida
                            }
                        }
                    }

                    a.Die();
                    Debug.Log("Bala destruiu um Asteroide.");
                }
            }

            // A bala sempre se destrói após atingir um inimigo
            Destroy(gameObject);
        }
    }
}