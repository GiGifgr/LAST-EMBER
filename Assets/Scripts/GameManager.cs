using System;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UIManager;

    #region Test

    [Button]
    private void ShowWindowPopup()
    {
        UIManager.ShowWindow("popupui");
    }

    [Button]
    private void ShowSettings()
    {
        UIManager.ShowWindow("settingsui");
    }

    #endregion
}
