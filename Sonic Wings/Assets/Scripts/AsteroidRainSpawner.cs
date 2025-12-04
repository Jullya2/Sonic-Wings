using UnityEngine;
using System.Collections;

public class AsteroidRainSpawner : MonoBehaviour
{
    // =================================================================
    // VARIÁVEIS DE CONTROLE E CÂMERA
    // =================================================================
    public GameObject asteroidPrefab;
    public float spawnInterval = 0.4f;
    public float minSize = 0.6f;
    public float maxSize = 1.4f;
    public float minSpeed = 3f;
    public float maxSpeed = 7f;

    public GameObject bossPrefab;
    public float bossTime = 30f;

    private float elapsedTime = 0f;
    private bool bossSpawned = false;

    Camera cam;
    float camWidth;
    float camHeight;


    void Start()
    {
        cam = Camera.main;
        if (cam != null)
        {
            camHeight = cam.orthographicSize * 2f;
            camWidth = camHeight * cam.aspect;
        }

        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        if (!bossSpawned)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= bossTime)
            {
                SpawnBoss();
                bossSpawned = true;
            }
        }
    }

    IEnumerator SpawnLoop()
    {
        while (!bossSpawned)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefab == null) return;

        float x = Random.Range(-camWidth / 2f, camWidth / 2f);
        Vector3 spawnPos = new Vector3(x, cam.transform.position.y + camHeight / 2f + 1f, 0f);

        GameObject a = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        Asteroid asteroid = a.GetComponent<Asteroid>();

        Vector2 dir = Vector2.down;
        float size = Random.Range(minSize, maxSize);
        float speed = Random.Range(minSpeed, maxSpeed);

        asteroid.Initialize(dir, size, speed);

        a.tag = "Enemy";
    }

    // FUNÇÃO CRÍTICA: Invoca o Boss
    void SpawnBoss()
    {
        Debug.Log("Chefe de fase invocado! Parando spawns normais.");

        StopAllCoroutines();

        if (bossPrefab != null)
        {
            // ALTERAÇÃO: Reduzindo o offset de 5f para 1f para forçar a visibilidade imediata.
            float spawnY = cam.transform.position.y + camHeight / 2f + 1f;
            Vector3 bossSpawnPos = new Vector3(0f, spawnY, 0f);

            Instantiate(bossPrefab, bossSpawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogError("ERRO CRÍTICO: Prefab do Boss não está conectado! Arraste o Prefab do Boss para o campo 'Boss Prefab' no Inspector do Spawner.");
        }
    }
}