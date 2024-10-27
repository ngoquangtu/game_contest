using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;

    // Game variables
    private int score = 0;
    public float timer = 60f;
    private int level = 1;
    public static GameManager Instance { get; private set; }

    void Start()
    {
        UpdateScoreText();
        UpdateTimerText();
        UpdateLevelText();
    }
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; 
    }
    void Update()
    {
        // Update Timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void NextLevel()
    {
        level++;
        timer += 10f; // Tăng thời gian khi lên cấp (tuỳ chỉnh)
        UpdateLevelText();
        UpdateTimerText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString(); 
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
    }
}
