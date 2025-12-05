using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // Variável estática para manter a única instância do MusicManager
    public static MusicManager Instance;
    
    // Variável para o AudioSource (conecte-o no Inspector)
    private AudioSource audioSource;

    private void Awake()
    {
        // 1. Lógica do Singleton:
        // Se já existe uma instância e não é esta, destrói este objeto (o que acabou de ser criado).
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return; // Sai do Awake para não executar o código abaixo.
        }
        
        // 2. Define esta como a única instância.
        Instance = this;
        
        // 3. CRÍTICO: Impede que o objeto seja destruído ao carregar novas cenas.
        DontDestroyOnLoad(this.gameObject);

        // 4. Obtém o componente AudioSource (deve estar anexado ao mesmo objeto)
        audioSource = GetComponent<AudioSource>();

        // 5. Começa a tocar a música
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    
    // Opcional: Você pode adicionar métodos aqui para pausar, mudar o volume, ou trocar a música.
}