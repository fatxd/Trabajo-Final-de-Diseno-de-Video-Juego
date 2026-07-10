using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private SpriteNumberDisplay bestScoreDisplay;

    [Header("Escenas")]
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    private bool isWin;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isWin)
            return;

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void ShowWin()
    {
        if (isWin)
            return;

        isWin = true;

        if (winPanel != null)
            winPanel.SetActive(true);

        if (bestScoreDisplay != null && ScoreManager.Instance != null)
        {
            bestScoreDisplay.SetNumber(ScoreManager.Instance.BestScore);
        }

        Time.timeScale = 0f;
    }
}