using UnityEngine;

public class Temporizador : MonoBehaviour
{
    [Header("Tiempo")]
    public float tiempoRestante = 180f;

    [Header("UI con sprites")]
    [SerializeField] private SpriteNumberDisplay tiempoDisplay;

    [Header("Game Over")]
    [SerializeField] private GameOverManager gameOverManager;

    private bool tiempoTerminado;

    private void Start()
    {
        UpdateTimeUI();
    }

    private void Update()
    {
        if (tiempoTerminado)
            return;

        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            tiempoTerminado = true;

            UpdateTimeUI();

            if (gameOverManager != null)
                gameOverManager.ShowGameOver();

            return;
        }

        UpdateTimeUI();
    }

    private void UpdateTimeUI()
    {
        if (tiempoDisplay != null)
            tiempoDisplay.SetNumber(Mathf.CeilToInt(tiempoRestante));
    }
}