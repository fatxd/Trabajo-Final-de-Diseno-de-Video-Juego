using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header("Efectos (Opcional)")]
    [SerializeField] private AudioClip winSound;
    private AudioSource audioSource;

    private bool hasTriggered = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar que sea el Player y que no se haya activado antes
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;

            // Sonido de victoria si existe
            if (winSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(winSound);
            }

            // Llamar al WinManager para mostrar la pantalla
            if (WinManager.Instance != null)
            {
                WinManager.Instance.ShowWin();
            }
            else
            {
                Debug.LogError("¡No se encontró el WinManager en la escena!");
            }
        }
    }
}