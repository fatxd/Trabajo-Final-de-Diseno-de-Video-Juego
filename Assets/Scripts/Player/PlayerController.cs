using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerVisualAnimator visualAnimator;

    [Header("Salto")]
    [SerializeField] private float bounceForce = 8f;
    [SerializeField] private float bounceCooldown = 0.08f;

    [Header("Game Over")]
    [SerializeField] private float deathOffset = 1f;

    private Rigidbody2D rb;
    private float moveDirection;
    private float lastBounceTime;

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
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            if (Mouse.current.position.ReadValue().x < Screen.width / 2f)
                moveDirection = -1f;
            else
                moveDirection = 1f;
        }
#else
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
            // GameManager.Instance.GameOver();
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
        TryBounce(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryBounce(collision);
    }

    private void TryBounce(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Platform"))
            return;

        if (Time.time < lastBounceTime + bounceCooldown)
            return;

        if (rb.linearVelocity.y > 0.05f)
            return;

        if (!IsTouchingTopOfPlatform(collision))
            return;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            bounceForce
        );
        if (visualAnimator != null)
            visualAnimator.PlaySquash();

        lastBounceTime = Time.time;
    }

    private bool IsTouchingTopOfPlatform(Collision2D collision)
    {
        float playerBottom = transform.position.y - 0.25f;
        float platformTop = collision.collider.bounds.max.y;

        return playerBottom >= platformTop - 0.15f;
    }
}