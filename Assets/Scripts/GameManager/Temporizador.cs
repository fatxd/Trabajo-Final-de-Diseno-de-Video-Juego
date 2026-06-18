using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Temporizador : MonoBehaviour
{
    private TextMeshProUGUI tiempoTexto;

    public float tiempoRestante = 180f;

    void Start()
    {
        tiempoTexto = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante < 0)
            tiempoRestante = 0;

        tiempoTexto.text = $"Tiempo: {Mathf.CeilToInt(tiempoRestante)}";
    }
}