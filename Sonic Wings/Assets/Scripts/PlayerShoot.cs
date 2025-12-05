using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Variáveis configuráveis no Inspector
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    
    // Variáveis de controle interno
    private float nextFireTime;

    void Start()
    {
        // AVISO DE ERRO DE CONFIGURAÇÃO (se o prefab estiver faltando)
        if (bulletPrefab == null)
        {
            Debug.LogError("PlayerShoot: O Prefab da Bala (bulletPrefab) não está conectado!");
        }
    }

    void Update()
    {
        // CORREÇÃO CRÍTICA: Detecta o clique do mouse ou a Barra de Espaço.
        // O tiro só funcionará se as configurações de Input estiverem corretas (veja a seção 2).
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        // Verifica o tempo de recarga (fireRate)
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Instancia a bala na posição e rotação do jogador
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}