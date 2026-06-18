using UnityEngine;

public class Meta : MonoBehaviour
{
    public Temporizador temporizador;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (temporizador.tiempoRestante > 0)
            {
                Debug.Log("¡Ganaste!");
            }
            else
            {
                Debug.Log("Perdiste. Se acabó el tiempo.");
            }
        }
    }
}