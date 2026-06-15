using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Transform player;
    public TMP_Text scoreText;
    public TMP_Text bestText;

    private int score;
    private int bestScore;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestText.text = "Best: " + bestScore;
    }

    void Update()
    {
        score = Mathf.Max(score, Mathf.FloorToInt(player.position.y));

        scoreText.text = "Score: " + score;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestText.text = "Best: " + bestScore;
        }
    }
}