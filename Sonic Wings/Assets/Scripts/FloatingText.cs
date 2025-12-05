using UnityEngine;
using TMPro; // Necessário para TextMeshPro

public class FloatingText : MonoBehaviour
{
    // Configure esta variável no Prefab (Arraste o componente TextMeshProUGUI aqui!)
    public TextMeshProUGUI textMesh; 
    
    // Velocidade e duração do movimento
    public float moveSpeed = 1.5f;
    public float lifeTime = 0.5f;

    void Start()
    {
        // Certifica que o objeto será destruído após a vida útil
        Destroy(gameObject, lifeTime);
        
        // NOVIDADE: Garante que o componente textMesh é referenciado e ativado
        // Isso é útil se o componente não foi arrastado no Prefab
        if (textMesh == null) 
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
        if (textMesh != null)
        {
            textMesh.enabled = true; // Garante que o texto está visível
        }
    }

    void Update()
    {
        // Move o texto para cima
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
    
    // CORREÇÃO CRÍTICA: O método deve ser PUBLIC para ser chamado pelo Bullet.cs
    public void SetText(string message) 
    {
        if (textMesh != null)
        {
            textMesh.text = message;
        }
    }
}