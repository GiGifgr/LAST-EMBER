using System;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Responsive Background")]
    public Transform gameBackground;
    public Vector3 tableta;
    public Vector3 mobile;

    [Header("Gameplay")]
    [SerializeField] private GameObject gameplayWorld;
    [SerializeField] private FireManager fireManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameStatsManager gameStatsManager;
    [SerializeField] private TroncoManager troncoManager;

    private bool isGameOver = false;
    private bool isPaused = false;

    private void Start()
    {
        ResponsiveManager.Instance.OnScreenSizeChanged.AddListener(ResizeBackground);
        ResizeBackground();

        Time.timeScale = 1f;

        gameplayWorld.SetActive(false);

        fireManager.enabled = false;
        spawnManager.enabled = false;
        troncoManager.enabled = false;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        isGameOver = false;
        isPaused = false;

        gameplayWorld.SetActive(true);

        fireManager.ResetFire();
        fireManager.enabled = true;

        gameStatsManager.StartGame();

        spawnManager.enabled = true;
        troncoManager.enabled = true;

        SetGameplayInteraction(true);

        UIManager.Instance.HideWindow("mainmenuui");
        UIManager.Instance.HideWindow("gameoverui");
        UIManager.Instance.HideWindow("popupui");
        UIManager.Instance.ShowWindow("gameplayui");
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            return;
        }

        isPaused = true;

        UIManager.Instance.ShowWindow("popupui");

        SetGameplayInteraction(false);

        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        if (!isPaused)
        {
            return;
        }

        Time.timeScale = 1f;

        isPaused = false;

        UIManager.Instance.HideWindow("popupui");

        SetGameplayInteraction(true);
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        isPaused = false;

        Time.timeScale = 1f;

        fireManager.enabled = false;

        spawnManager.StopSpawning();
        troncoManager.StopTroncos();

        gameStatsManager.FinishGame();

        UIManager.Instance.HideWindow("gameplayui");
        UIManager.Instance.HideWindow("popupui");
        UIManager.Instance.ShowWindow("gameoverui");
    }

    public void RetryGame()
    {
        StartGame();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        isPaused = false;
        isGameOver = false;

        gameStatsManager.StopGame();

        fireManager.enabled = false;

        spawnManager.StopSpawning();
        troncoManager.StopTroncos();

        gameplayWorld.SetActive(false);

        SetGameplayInteraction(true);

        UIManager.Instance.HideWindow("popupui");
        UIManager.Instance.HideWindow("gameoverui");
        UIManager.Instance.HideWindow("gameplayui");
        UIManager.Instance.ShowWindow("mainmenuui");
    }

    private void SetGameplayInteraction(bool value)
    {
        UIWindow gameplayUI = UIManager.Instance.GetWindow("gameplayui");

        gameplayUI.CanvasGroup.interactable = value;
        gameplayUI.CanvasGroup.blocksRaycasts = value;
    }

    public void ResizeBackground()
    {
        if (ResponsiveManager.Instance.CurrentDeviceType == DeviceType.Tablet)
        {
            Debug.Log("Estoy en tableta");
            gameBackground.localScale = tableta;
        }

        if (ResponsiveManager.Instance.CurrentDeviceType == DeviceType.Mobile)
        {
            Debug.Log("Estoy en mobile");
            gameBackground.localScale = mobile;
        }
    }
}