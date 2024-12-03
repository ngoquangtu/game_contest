using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI targetScore;

    public GameObject winPanel;   // Panel khi thắng
    public GameObject losePanel;  // Panel khi thua

    // Game variables
    private int score = 0;
    public float timer = 60f;
    private int level = 1;
    public int winScore = 10;
    public int nextLevel;
    public AudioSource audioSource;
    public AudioClip loseSound;
    public static GameManager Instance { get; private set; }

    void Start()
    {
        UpdateScoreText();
        UpdateTimerText();
        UpdateLevelText();
        UpdateTargetScore();

        winPanel.SetActive(false);
        losePanel.SetActive(false);
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
    private float elapsed = 0;
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
            if (score >= winScore)
            {
                WinGame();
            }
        }
        // Tính thời gian trôi qua
            elapsed += Time.deltaTime;

            if (elapsed >= 5f)
            {
                elapsed = 0; // Reset lại bộ đếm
                Debug.Log("5 seconds passed: " + Mathf.FloorToInt(Time.time));
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
        timer += 10f;
        SceneManager.LoadScene(nextLevel);
        UpdateLevelText();
        UpdateTimerText();
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
    private void UpdateTargetScore()
    {
        targetScore.text = "Winning Score: " + winScore.ToString();
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }

    public void EndGame()
    {
        ShowLosePanel();
        if (audioSource != null && loseSound != null)
        {
            audioSource.PlayOneShot(loseSound);
        }
        Debug.Log("Game Over");
        // Invoke("PauseGame", 2.0f);

    }

    public void WinGame()
    {
        ShowWinPanel();
        Debug.Log("You Win");
        Invoke("PauseGame", 2.0f);

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);

        // Hiệu ứng Fade-In bằng DOTween
        CanvasGroup canvasGroup = winPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = winPanel.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetEase(Ease.InOutQuad);
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true);

        // Hiệu ứng Fade-In bằng DOTween
        CanvasGroup canvasGroup = losePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = losePanel.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetEase(Ease.InOutQuad);
    }
}
