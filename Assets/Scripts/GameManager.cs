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

    private void Start()
    {
        ResponsiveManager.Instance.OnScreenSizeChanged.AddListener(ResizeBackground);
        ResizeBackground();
        gameplayWorld.SetActive(false);
        fireManager.enabled = false;
    }

    public void StartGame()
    {
        gameplayWorld.SetActive(true);
        fireManager.ResetFire();
        fireManager.enabled = true;
        UIManager.Instance.HideWindow("mainmenuui");
        UIManager.Instance.ShowWindow("gameplayui");
    }

    public void ReturnToMainMenu()
    {
        fireManager.enabled = false;
        gameplayWorld.SetActive(false);
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
