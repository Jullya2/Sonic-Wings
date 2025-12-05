using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using TMPro; // Necessário para o placar de TextMeshPro

public class GameManager : MonoBehaviour // Mude para GameController se for o nome final
{
    // Variável Singleton 'Instance'
    // Se você renomeou a classe para GameController, use 'public static GameController Instance'
    public static GameManager Instance { get; private set; }

    // VARIÁVEIS DE CONTROLE E UI (Configure no Inspector)
    public int currentScore = 0;
    public TextMeshProUGUI scoreText; 
    
    // Variáveis de Controle de Cena
    public string nextLevelSceneName = "Vitoria"; // Conforme sua imagem
    public string currentSceneName; 

    // Variáveis de UI (CRÍTICO: Conecte no Inspector)
    public GameObject victoryScreenUI;
    public GameObject gameOverScreenUI;


    private void Awake()
    {
        // Lógica do Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        // Garante que o jogo COMEÇA rodando
        Time.timeScale = 1f;

        // Inicializa o nome da cena atual
        currentSceneName = SceneManager.GetActiveScene().name; 
        
        // Inicializa o placar
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + currentScore.ToString();
        }

        // Desativa UI
        if (victoryScreenUI != null) victoryScreenUI.SetActive(false);
        if (gameOverScreenUI != null) gameOverScreenUI.SetActive(false);
    }

    // =================================================================
    // PONTUAÇÃO E ESTADO DO JOGO
    // =================================================================
    
    public void AddScore(int points)
    {
        currentScore += points;
        
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + currentScore.ToString();
        }
    }

    public void LevelComplete()
    {
        Time.timeScale = 0f; // Pausa
        
        if (victoryScreenUI != null)
        {
            victoryScreenUI.SetActive(true);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // Pausa
        
        if (gameOverScreenUI != null)
        {
            gameOverScreenUI.SetActive(true);
        }
    }

    // =================================================================
    // FUNÇÕES DE BOTÃO (SOLUÇÃO PARA O PROBLEMA DO TEMPO)
    // =================================================================
    
    // Conecte esta função ao botão "Recomeçar"
    public void RestartLevel()
    {
        // CRÍTICO: Desbloqueia o tempo para permitir o carregamento da cena
        Time.timeScale = 1f; 
        
        // OPCIONAL: Desativa o painel para garantir que não haja bloqueio
        if (gameOverScreenUI != null) gameOverScreenUI.SetActive(false);
        
        SceneManager.LoadScene(currentSceneName);
    }

    // Conecte esta função ao botão "Voltar ao Menu"
    public void LoadMenu(string menuSceneName = "MainMenu") 
    {
        // CRÍTICO: Desbloqueia o tempo para permitir o carregamento da cena
        Time.timeScale = 1f; 
        
        SceneManager.LoadScene(menuSceneName);
    }
}