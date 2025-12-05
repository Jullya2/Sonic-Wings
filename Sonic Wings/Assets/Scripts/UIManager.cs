using UnityEngine;
using UnityEngine.SceneManagement; 

public class UIManager : MonoBehaviour
{
    // Variável para a cena principal do menu (Defina no Inspector)
    public string mainMenuSceneName = "MainMenu"; 

    // O objeto de Game Over (CRÍTICO: Arraste o seu Painel de Derrota aqui!)
    public GameObject gameOverPanel; 

    // =================================================================
    // FUNÇÕES DE BOTÃO
    // =================================================================

    // Conecte esta função ao botão "Recomeçar"
    public void RestartCurrentLevel()
    {
        // 1. CRÍTICO: Desbloqueia o tempo primeiro
        Time.timeScale = 1f;
        
        // 2. Opcional: Esconde o painel de Game Over (se ainda estiver visível)
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        // 3. Carrega a cena atual (que deve ser a fase que o jogador estava)
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    // Conecte esta função ao botão "Voltar ao Menu"
    public void LoadMainMenu()
    {
        // 1. CRÍTICO: Desbloqueia o tempo primeiro
        Time.timeScale = 1f;
        
        // 2. Carrega a cena do Menu Principal
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogError("Nome da cena do Menu Principal não está configurado no UIManager!");
        }
    }

    // NOVO: Conecte esta função ao botão "Sair do Jogo"
    public void QuitGame()
    {
        // Desbloqueia o tempo, caso o jogo esteja pausado
        Time.timeScale = 1f; 
        
        // Esta função só funciona em builds finais (não no Editor do Unity)
        Application.Quit();
        Debug.Log("Saindo do Jogo..."); 
    }
}