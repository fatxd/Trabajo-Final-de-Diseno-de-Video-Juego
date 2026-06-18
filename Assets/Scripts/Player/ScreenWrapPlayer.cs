using UnityEngine;

public class ScreenWrapPlayer : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform visualCopy;

    [Header("Ajustes")]
    [SerializeField] private float extraMargin = 0.5f;

    private float leftLimit;
    private float rightLimit;
    private float screenWidth;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        CalculateLimits();

        if (visualCopy != null)
            visualCopy.gameObject.SetActive(false);
    }

    private void Update()
    {
        CalculateLimits();
        HandleWrap();
        HandleVisualCopy();
    }

    private void CalculateLimits()
    {
        float distanceToCamera = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 leftWorld = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, distanceToCamera));
        Vector3 rightWorld = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, distanceToCamera));

        leftLimit = leftWorld.x;
        rightLimit = rightWorld.x;
        screenWidth = rightLimit - leftLimit;
    }

    private void HandleWrap()
    {
        Vector3 position = transform.position;

        if (position.x > rightLimit + extraMargin)
        {
            position.x -= screenWidth;
            transform.position = position;
        }
        else if (position.x < leftLimit - extraMargin)
        {
            position.x += screenWidth;
            transform.position = position;
        }
    }

    private void HandleVisualCopy()
    {
        if (visualCopy == null)
            return;

        Vector3 copyPosition = transform.position;
        bool showCopy = false;

        if (transform.position.x > rightLimit - extraMargin)
        {
            copyPosition.x -= screenWidth;
            showCopy = true;
        }
        else if (transform.position.x < leftLimit + extraMargin)
        {
            copyPosition.x += screenWidth;
            showCopy = true;
        }

        visualCopy.gameObject.SetActive(showCopy);
        visualCopy.position = copyPosition;
        visualCopy.rotation = transform.rotation;
        visualCopy.localScale = transform.localScale;
    }
}