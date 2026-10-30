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

    private bool isGameOver = false;

    private void Start()
    {
        ResponsiveManager.Instance.OnScreenSizeChanged.AddListener(ResizeBackground);
        ResizeBackground();

        Time.timeScale = 1f;
        gameplayWorld.SetActive(false);
        fireManager.enabled = false;
        spawnManager.enabled = false;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;

        gameplayWorld.SetActive(true);

        fireManager.ResetFire();
        fireManager.enabled = true;

        spawnManager.enabled = true;

        UIManager.Instance.HideWindow("mainmenuui");
        UIManager.Instance.HideWindow("gameoverui");
        UIManager.Instance.HideWindow("popupui");
        UIManager.Instance.ShowWindow("gameplayui");
    }

    public void PauseGame()
    {
        UIManager.Instance.ShowWindow("popupui");
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        UIManager.Instance.HideWindow("popupui");
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        fireManager.enabled = false;
        spawnManager.StopSpawning();

        UIManager.Instance.HideWindow("gameplayui");
        UIManager.Instance.ShowWindow("gameoverui");
    }

    public void RetryGame()
    {
        StartGame();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        fireManager.enabled = false;
        spawnManager.StopSpawning();
        gameplayWorld.SetActive(false);

        UIManager.Instance.HideWindow("popupui");
        UIManager.Instance.HideWindow("gameoverui");
        UIManager.Instance.HideWindow("gameplayui");
        UIManager.Instance.ShowWindow("mainmenuui");
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