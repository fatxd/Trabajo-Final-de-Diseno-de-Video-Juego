using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Refresh();
    }

    void Update()
    {
        // Esto sirve para que se actualice si el usuario gira el celular mientras juega
        if (Screen.safeArea != lastSafeArea)
        {
            Refresh();
        }
    }

    void Refresh()
    {
        Rect safeArea = Screen.safeArea;

        // Guardamos el estado actual
        lastSafeArea = safeArea;

        // Convertimos las coordenadas de píxeles a espacio de pantalla local
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // Aplicamos los anclajes para recortar la zona de juego
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}