using NaughtyAttributes;
using UnityEngine;

public class CreditsUI : UIWindow
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

    public void ReturnToMainMenu()
    {
        UIManager.Instance.HideWindow("creditsui");
        UIManager.Instance.ShowWindow("mainmenuui");
    }

}
