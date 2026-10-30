using NaughtyAttributes;
using UnityEngine;

public class MainMenuUI : UIWindow
{
    [Button("Test Show")]
    private void TestShow()
    {
        Show();
    }

    [Button("Test Hide")]
    private void TestHide()
    {
        Hide();
    }
    public void OpenSettings()
    {
        UIManager.Instance.HideWindow("mainmenuui");
        UIManager.Instance.ShowWindow("settingsui");
    }

    public void OpenCredits()
    {
        UIManager.Instance.HideWindow("mainmenuui");
        UIManager.Instance.ShowWindow("creditsui");
    }
}
