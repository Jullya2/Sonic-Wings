using UnityEngine;
// using TMPro; // Descomente se estiver usando TextMeshPro
// using UnityEngine.UI; // Descomente se estiver usando Text padrão

public class GameManager : MonoBehaviour
{
    // CORREÇÃO: Variável Singleton 'Instance'
    public static GameManager Instance { get; private set; }

    // VARIÁVEIS DE CONTROLE E UI (Configure no Inspector)
    public int currentScore = 0;
    // public TextMeshProUGUI scoreText; 

    public GameObject victoryScreenUI;
    public GameObject gameOverScreenUI;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Time.timeScale = 1f;

        if (victoryScreenUI != null) victoryScreenUI.SetActive(false);
        if (gameOverScreenUI != null) gameOverScreenUI.SetActive(false);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log("Pontuação Atual: " + currentScore);
        // Lógica de UI para o placar aqui
    }

    public void LevelComplete()
    {
        Debug.Log(">>> LevelComplete INICIADO: Pausando o jogo e ativando UI.");
        Time.timeScale = 0f;

        if (victoryScreenUI != null)
        {
            victoryScreenUI.SetActive(true);
        }
        else
        {
            Debug.LogError("ERRO: Painel Victory Screen UI não está conectado no Inspector do GameManager!");
        }
    }

    public void GameOver()
    {
        Debug.Log("JOGADOR DESTRUIDO! Fim de Jogo.");
        Time.timeScale = 0f;

        if (gameOverScreenUI != null)
        {
            gameOverScreenUI.SetActive(true);
        }
        else
        {
            Debug.LogError("ERRO: Painel Game Over UI não está conectado no Inspector do GameManager!");
        }
    }
}