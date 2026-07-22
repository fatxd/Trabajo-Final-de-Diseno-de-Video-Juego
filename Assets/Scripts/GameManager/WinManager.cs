using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private SpriteNumberDisplay bestScoreDisplay;

    [Header("Audio")]
    [SerializeField] private AudioClip winSound; // <--- Sonido de Victoria

    [Header("Escenas")]
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    private bool isWin = false;
    private float winTime = 0f;
    private const float COOLDOWN_TIME = 5f;

    private void Awake()
    {
        Instance = this;
    }

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
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isWin) return;

        if (Time.unscaledTime - winTime < COOLDOWN_TIME) return;

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

    public void ShowWin()
    {
        if (isWin) return;

        isWin = true;
        winTime = Time.unscaledTime;

        // Reproducir sonido de victoria en la posición de la cámara
        if (winSound != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position);
        }

        if (winPanel != null)
            winPanel.SetActive(true);

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