using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text bestScoreText;

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

        if (bestScoreText != null && ScoreManager.Instance != null)
        {
            bestScoreText.text = "BEST SCORE: " + ScoreManager.Instance.BestScore;
        }

        Time.timeScale = 0f;
    }
}
