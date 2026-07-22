using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private SpriteNumberDisplay bestScoreDisplay;

    [Header("Audio")]
    [SerializeField] private AudioClip gameOverSound; // <--- Sonido de Game Over

    [Header("Escenas")]
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    private bool isGameOver = false;
    private float gameOverTime = 0f;
    private const float COOLDOWN_TIME = 5f;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isGameOver) return;

        if (Time.unscaledTime - gameOverTime < COOLDOWN_TIME) return;

        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                GoToMainMenu();
                return;
            }
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            GoToMainMenu();
            return;
        }

        if (Keyboard.current != null && (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame))
        {
            GoToMainMenu();
            return;
        }
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverTime = Time.unscaledTime;

        // Reproducir sonido de derrota en la posición de la cámara
        if (gameOverSound != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (bestScoreDisplay != null && ScoreManager.Instance != null)
        {
            bestScoreDisplay.SetNumber(ScoreManager.Instance.BestScore);
        }

        Time.timeScale = 0f;
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}