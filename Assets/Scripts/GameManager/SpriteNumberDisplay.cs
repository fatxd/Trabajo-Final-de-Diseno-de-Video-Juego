using UnityEngine;
using UnityEngine.UI;

public class SpriteNumberDisplay : MonoBehaviour
{
    [Header("Sprites de números")]
    [SerializeField] private Sprite[] digitSprites; // 0 al 9

    [Header("Configuración")]
    [SerializeField] private int maxDigits = 5;
    [SerializeField] private bool hideLeadingZeros = true;

    [Header("Tamaño")]
    [SerializeField] private Vector2 digitSize = new Vector2(40f, 50f);

    private Image[] digitImages;

    private void Awake()
    {
        CreateDigitImages();
    }

    private void CreateDigitImages()
    {
        digitImages = new Image[maxDigits];

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < maxDigits; i++)
        {
            GameObject digitObject = new GameObject("Digit_" + i);
            digitObject.transform.SetParent(transform, false);

            Image image = digitObject.AddComponent<Image>();
            image.preserveAspect = true;

            RectTransform rect = digitObject.GetComponent<RectTransform>();
            rect.sizeDelta = digitSize;

            digitImages[i] = image;
        }
    }

    public void SetNumber(int number)
    {
        if (digitSprites == null || digitSprites.Length < 10)
            return;

        number = Mathf.Max(0, number);

        string value = number.ToString();

        if (value.Length > maxDigits)
            value = value.Substring(value.Length - maxDigits);

        int startIndex = maxDigits - value.Length;

        for (int i = 0; i < maxDigits; i++)
        {
            Image image = digitImages[i];

            if (image == null)
                continue;

            if (hideLeadingZeros && i < startIndex)
            {
                image.gameObject.SetActive(false);
                continue;
            }

            int valueIndex = i - startIndex;

            if (valueIndex < 0 || valueIndex >= value.Length)
            {
                image.gameObject.SetActive(false);
                continue;
            }

            int digit = value[valueIndex] - '0';

            image.gameObject.SetActive(true);
            image.enabled = true;
            image.sprite = digitSprites[digit];
        }
    }
}