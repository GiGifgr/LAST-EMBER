using NaughtyAttributes;
using UnityEngine;

public class GameplayUI : UIWindow
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