using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStatsManager : MonoBehaviour
{
    [Header("Gameplay UI")]
    [SerializeField] private TMP_Text gameplayScoreText;
    [SerializeField] private TMP_Text gameplayTimeText;

    [Header("Game Over UI")]
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private TMP_Text gameOverHighScoreText;
    [SerializeField] private GameObject newHighScoreText;

    [Header("Main Menu UI")]
    [SerializeField] private TMP_Text mainMenuHighScoreText;

    private int score = 0;
    private int highScore = 0;
    private float gameTime = 0f;
    private bool isPlaying = false;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        newHighScoreText.SetActive(false);

        UpdateGameplayUI();
        UpdateHighScoreUI();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetHighScore();
        }
#endif

        if (!isPlaying)
        {
            return;
        }

        gameTime += Time.deltaTime;

        UpdateGameplayUI();
    }

    public void StartGame()
    {
        score = 0;
        gameTime = 0f;
        isPlaying = true;

        newHighScoreText.SetActive(false);

        UpdateGameplayUI();
    }

    public void StopGame()
    {
        isPlaying = false;
    }

    public void AddScore(int amount)
    {
        if (!isPlaying)
        {
            return;
        }

        score += amount;

        UpdateGameplayUI();
    }

    public void FinishGame()
    {
        isPlaying = false;

        finalScoreText.text = score.ToString("N0");
        finalTimeText.text = FormatTime(gameTime);

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            newHighScoreText.SetActive(true);
        }
        else
        {
            newHighScoreText.SetActive(false);
        }

        UpdateHighScoreUI();
    }

    private void ResetHighScore()
    {
        highScore = 0;

        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();

        newHighScoreText.SetActive(false);

        UpdateHighScoreUI();

        Debug.Log("High Score reiniciado");
    }

    private void UpdateGameplayUI()
    {
        gameplayScoreText.text = score.ToString("N0");
        gameplayTimeText.text = FormatTime(gameTime);
    }

    private void UpdateHighScoreUI()
    {
        gameOverHighScoreText.text = highScore.ToString("N0");
        mainMenuHighScoreText.text = highScore.ToString("N0");
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}