using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [Header("Referencia")]
    [SerializeField] private Transform player;

    [Header("Seguimiento hacia arriba")]
    [SerializeField] private float upperLimit = 0.65f;
    [SerializeField] private float smoothSpeed = 8f;

    [Header("Game Over")]
    [SerializeField] private float deathLimit = -0.15f;
    [SerializeField] private string sceneToReload = "Level 1";

    private Camera cam;
    private float targetY;

    private void Start()
    {
        cam = GetComponent<Camera>();
        targetY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (player == null || cam == null)
            return;

        Vector3 playerViewportPos = cam.WorldToViewportPoint(player.position);

        if (playerViewportPos.y > upperLimit)
        {
            float difference = playerViewportPos.y - upperLimit;
            float worldDifference = difference * cam.orthographicSize * 2f;

            targetY += worldDifference;
        }

        Vector3 targetPosition = new Vector3(
            0f,
            targetY,
            -10f
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );

        if (playerViewportPos.y < deathLimit)
        {
            SceneManager.LoadScene(sceneToReload);
        }
    }
}