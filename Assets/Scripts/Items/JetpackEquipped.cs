using UnityEngine;
using System.Collections;

public class JetpackEquipped : MonoBehaviour
{
    [Header("Configuración de la velocidad")]
    [SerializeField] private float fuerzaVueloConstante = 20f;

    [SerializeField] private float duracionVuelo = 1.5f;

    [Header("Componentes")]
    [SerializeField] private Animator flameAnimator;

    private Rigidbody2D playerRb;
    private bool volandoFormaAutomatica = false;
    private Coroutine rutinaVuelo;

    private void Awake()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
        gameObject.SetActive(false); // Inicia apagado/oculto
    }

    public void IniciarVueloAutomatico()
    {
        // Si ya estaba volando por otro jetpack, reinicia el temporizador
        if (rutinaVuelo != null)
        {
            StopCoroutine(rutinaVuelo);
        }

        gameObject.SetActive(true);
        volandoFormaAutomatica = true;

        if (flameAnimator != null)
        {
            flameAnimator.SetBool("isFlaming", true); // Enciende la llama
        }

        // Inicia el contador para ver cuánto tiempo vuela solo
        rutinaVuelo = StartCoroutine(ContadorVuelo());
    }

    private void FixedUpdate()
    {
        // En juegos de física e impulsos constantes, es mejor aplicar la fuerza en FixedUpdate
        if (volandoFormaAutomatica && playerRb != null)
        {
            // Mantiene al jugador subiendo a velocidad constante sin importar la gravedad
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, fuerzaVueloConstante);
        }
    }

    private IEnumerator ContadorVuelo()
    {
        // Espera los segundos configurados de viaje
        yield return new WaitForSeconds(duracionVuelo);

        // Termina el vuelo automático
        volandoFormaAutomatica = false;

        if (flameAnimator != null)
        {
            flameAnimator.SetBool("isFlaming", false); // Apaga la llama
        }

        gameObject.SetActive(false); // Oculta el jetpack del jugador
    }
}
