using UnityEngine;
using UnityEngine.UI;

public class PlayerColorSelector : MonoBehaviour
{
    [Header("Botones")]
    [SerializeField] private Button[] colorButtons;

    [Header("Indicador")]
    [SerializeField] private GameObject selectionFrame;

    private int selectedColorIndex = 0;

    public const string ColorKey = "PlayerColorIndex";

    private void Start()
    {
        selectedColorIndex = PlayerPrefs.GetInt(ColorKey, 0);

        for (int i = 0; i < colorButtons.Length; i++)
        {
            if (colorButtons[i] == null)
                continue;

            int index = i;
            colorButtons[i].onClick.AddListener(() => SelectColor(index));
        }

        UpdateSelectionFrame();
    }

    private void SelectColor(int index)
    {
        if (index < 0 || index >= colorButtons.Length)
            return;

        selectedColorIndex = index;

        PlayerPrefs.SetInt(ColorKey, selectedColorIndex);
        PlayerPrefs.Save();

        UpdateSelectionFrame();
    }

    private void UpdateSelectionFrame()
    {
        if (selectionFrame == null)
            return;

        if (selectedColorIndex < 0 || selectedColorIndex >= colorButtons.Length)
            selectedColorIndex = 0;

        if (colorButtons[selectedColorIndex] == null)
            return;

        selectionFrame.transform.SetParent(
            colorButtons[selectedColorIndex].transform,
            false
        );

        RectTransform frameRect = selectionFrame.GetComponent<RectTransform>();

        if (frameRect == null)
            return;

        frameRect.anchorMin = Vector2.zero;
        frameRect.anchorMax = Vector2.one;
        frameRect.offsetMin = new Vector2(-8f, -8f);
        frameRect.offsetMax = new Vector2(8f, 8f);

        selectionFrame.transform.SetAsLastSibling();
    }
}