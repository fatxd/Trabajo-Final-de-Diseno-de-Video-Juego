using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private SpriteNumberDisplay bestScoreDisplay;

    [Header("Escenas")]
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    private bool isGameOver;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isGameOver)
            return;

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void ShowGameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (bestScoreDisplay != null && ScoreManager.Instance != null)
        {
            bestScoreDisplay.SetNumber(ScoreManager.Instance.BestScore);
        }

        Time.timeScale = 0f;
    }
}