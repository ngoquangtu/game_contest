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

    public GameObject winPanel;   // Panel khi thắng
    public GameObject losePanel;  // Panel khi thua

    // Game variables
    private int score = 0;
    public float timer = 60f;
    private int level = 1;
    public int winScore = 10;
    public int nextLevel;
    public static GameManager Instance { get; private set; }

    void Start()
    {
        UpdateScoreText();
        UpdateTimerText();
        UpdateLevelText();

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
        SceneManager.LoadScene(nextLevel - 1);
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
        ShowLosePanel();
    }

    private void WinGame()
    {
        ShowWinPanel();
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);

        winPanel.transform.localScale = Vector3.zero;
        winPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
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
