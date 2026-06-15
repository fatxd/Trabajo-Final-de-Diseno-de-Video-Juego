using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float bounceForce = 12f;

    private Rigidbody2D rb;
    private float moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection = 0;

#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2f)
                moveDirection = -1f;
            else
                moveDirection = 1f;
        }

#else

    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.position.x < Screen.width / 2f)
            moveDirection = -1f;
        else
            moveDirection = 1f;
    }

#endif
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
        if (collision.gameObject.CompareTag("Platform") &&
            rb.linearVelocity.y <= 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                bounceForce
            );
        }
    }
}