using UnityEngine;

public class GeneradorJetpack : MonoBehaviour
{
    [Header("Configuración")]
    // Arrastra aquí el Prefab del resorte desde tu carpeta de Proyecto
    [SerializeField] private GameObject prefabJetpack;

    // Probabilidad de aparición (0 = nunca, 100 = en todas las plataformas)
    [Range(0, 100)]
    [SerializeField] private float probabilidadAparicion = 10f;

    [Header("Ajuste de Posición")]
    // Distancia extra en el eje Y para que el resorte no quede flotando ni hundido
    [SerializeField] private float offsetVertical = 0.10f;

    private void Start()
    {
        GenerarJetpack();
    }

    private void GenerarJetpack()
    {
        // Busca todas las plataformas activas en la escena usando el Tag
        GameObject[] plataformas = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject plataforma in plataformas)
        {
            // Genera un número aleatorio entre 0 y 100
            float suerte = Random.Range(0f, 100f);

            // Si el número es menor o igual a la probabilidad establecida, se crea el resorte
            if (suerte <= probabilidadAparicion)
            {
                // Calcula la posición superior central de la plataforma
                Vector3 posicionResorte = plataforma.transform.position;
                posicionResorte.y += offsetVertical;

                // Crea el resorte en el juego y lo hace hijo de la plataforma 
                // (Para que si la plataforma se mueve, el resorte se mueva con ella)
                GameObject nuevoResorte = Instantiate(prefabJetpack, posicionResorte, Quaternion.identity);
                nuevoResorte.transform.SetParent(plataforma.transform);
            }
        }
    }
}
