using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Salto")]
    [SerializeField] private float bounceForce = 8f;

    [Header("Game Over")]
    [SerializeField] private float deathOffset = 1f;

    private Rigidbody2D rb;
    private float moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        moveDirection = 0f;

#if UNITY_EDITOR
        // Prueba con mouse en Unity
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            if (Mouse.current.position.ReadValue().x < Screen.width / 2f)
                moveDirection = -1f;
            else
                moveDirection = 1f;
        }
#else
        // Touch en móvil
        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];

            if (touch.screenPosition.x < Screen.width / 2f)
                moveDirection = -1f;
            else
                moveDirection = 1f;
        }
#endif

        float limiteInferior =
            Camera.main.transform.position.y -
            Camera.main.orthographicSize -
            deathOffset;

        if (transform.position.y < limiteInferior)
        {
            //GameManager.Instance.GameOver();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveDirection * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Choque con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("REBOTE");

            if (rb.linearVelocity.y <= 0)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    bounceForce
                );
            }
        }
    }
}