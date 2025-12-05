using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeUnlocker : MonoBehaviour
{
    // Este método é chamado ANTES de qualquer função de carregamento de cena.
    public void UnlockTime()
    {
        // Garante que o tempo seja restaurado para 1f.
        Time.timeScale = 1f;
        Debug.Log("Tempo desbloqueado (Time.timeScale = 1f)");
    }
}