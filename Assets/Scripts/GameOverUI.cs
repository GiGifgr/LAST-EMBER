using NaughtyAttributes;
using UnityEngine;

public class GameOverUI : UIWindow
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
}
