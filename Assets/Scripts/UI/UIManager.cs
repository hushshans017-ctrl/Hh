using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Text scoreText;
    [SerializeField] private Text weaponNameText;
    [SerializeField] private Text ammoText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Image reloadBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(RetryGame);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePause);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void UpdateWeaponDisplay(string gunName, int currentAmmo, int maxAmmo)
    {
        if (weaponNameText != null)
            weaponNameText.text = "Weapon: " + gunName;
        
        if (ammoText != null)
            ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
    }

    public void UpdateReloadBar(float reloadProgress)
    {
        if (reloadBar != null)
        {
            reloadBar.fillAmount = reloadProgress;
        }
    }

    public void ShowGameOverScreen(int finalScore)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (gameOverScoreText != null)
                gameOverScoreText.text = "Final Score: " + finalScore;
        }
    }

    private void TogglePause()
    {
        if (pausePanel != null && pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            GameManager.Instance.ResumeGame();
        }
        else
        {
            if (pausePanel != null)
                pausePanel.SetActive(true);
            GameManager.Instance.PauseGame();
        }
    }

    private void RetryGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
