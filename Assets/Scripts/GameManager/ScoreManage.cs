using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Referencias")]
    public Transform player;
    public TMP_Text scoreText;
    public TMP_Text bestText;

    private int score;
    private int bestScore;

    public int Score => score;
    public int BestScore => bestScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        UpdateScoreUI();
        UpdateBestUI();
    }

    private void Update()
    {
        if (player == null)
            return;

        score = Mathf.Max(score, Mathf.FloorToInt(player.position.y));

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
        UpdateBestUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private void UpdateBestUI()
    {
        if (bestText != null)
            bestText.text = "Best: " + bestScore;
    }
}