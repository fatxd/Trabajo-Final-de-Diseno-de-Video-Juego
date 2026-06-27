using UnityEngine;

public class Resorte : MonoBehaviour
{
    [SerializeField] private float fuerzaImpulso = 20f;
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
            }
        }
    }
}
