using UnityEngine;

public class PlayerVisualAnimator : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;


    [Header("Sprites Amarillo")]
    [SerializeField] private Sprite[] yellowSprites;

    [Header("Sprites Azul")]
    [SerializeField] private Sprite[] blueSprites;

    [Header("Sprites Blanco")]
    [SerializeField] private Sprite[] whiteSprites;

    [Header("Sprites Rojo")]
    [SerializeField] private Sprite[] redSprites;

    [Header("Ajustes")]
    [SerializeField] private float stretchedVelocity = 1.5f;
    [SerializeField] private float squashDuration = 0.08f;

    private Sprite[] currentSprites;
    private float squashTimer;

    private const string ColorKey = "PlayerColorIndex";

    private void Start()
    {
        int selectedColorIndex = PlayerPrefs.GetInt(ColorKey, 0);

        switch (selectedColorIndex)
        {
            case 0:
                currentSprites = yellowSprites;
                break;

            case 1:
                currentSprites = blueSprites;
                break;

            case 2:
                currentSprites = whiteSprites;
                break;

            case 3:
                currentSprites = redSprites;
                break;

            default:
                currentSprites = yellowSprites;
                break;
        }

        SetSprite(0);
    }

    private void Update()
    {
        if (spriteRenderer == null || rb == null || currentSprites == null)
            return;

        if (currentSprites.Length < 3)
            return;

        if (squashTimer > 0f)
        {
            squashTimer -= Time.deltaTime;
            SetSprite(1);
            return;
        }

        if (rb.linearVelocity.y > stretchedVelocity)
        {
            SetSprite(2);
        }
        else
        {
            SetSprite(0);
        }
    }

    public void PlaySquash()
    {
        squashTimer = squashDuration;
    }

    private void SetSprite(int index)
    {
        if (index < 0 || index >= currentSprites.Length)
            return;

        if (currentSprites[index] != null)
            spriteRenderer.sprite = currentSprites[index];
    }
}
