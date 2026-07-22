using UnityEngine;

public class JetpackItem : MonoBehaviour
{
    [Header("Configuración del Impulso")]
    [SerializeField] private float impulsoFuerza = 25f; // Fuerza del subidón inicial

    [Header("Audio")]
    [SerializeField] private AudioClip jetSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si es el jugador
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        // Aplicar el impulso inmediato al Rigidbody2D del jugador
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Reemplaza la velocidad vertical actual por la fuerza del jetpack
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, impulsoFuerza);
        }

        // Buscar el Jetpack en el objeto hijo del jugador
        JetpackEquipped jetpackHijo = other.GetComponentInChildren<JetpackEquipped>(true);
        if (jetpackHijo != null)
        {
            // Arranca el vuelo automático y la animación de la llama
            jetpackHijo.IniciarVueloAutomatico();
        }

        // Reproducir sonido en la posición de la cámara para que se escuche perfecto en 2D
        if (jetSound != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(jetSound, Camera.main.transform.position);
        }

        // Destruir el ítem del mapa de inmediato
        Destroy(gameObject);
    }
}