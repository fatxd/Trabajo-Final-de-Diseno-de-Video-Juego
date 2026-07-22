using UnityEngine;

public class Resorte : MonoBehaviour
{
    [SerializeField] private float fuerzaImpulso = 20f;

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoBoing; // <--- Clip de sonido para el rebote

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            // El jugador debe estar cayendo sobre el resorte
            if (rb != null && rb.linearVelocity.y <= 0f)
            {
                // Impulsa al jugador hacia arriba
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaImpulso);

                // Dispara la animación de compresión y disparo del sprite
                if (animator != null)
                {
                    animator.SetTrigger("Comprimir");
                }

                // Reproducir sonido del resorte centrado en la cámara
                if (sonidoBoing != null && Camera.main != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoBoing, Camera.main.transform.position);
                }
            }
        }
    }
}
