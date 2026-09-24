using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIWindow> _uiWindows;
    void Start()
    {
        
    }

    public void ShowWindow(string windowName)
    {
        foreach (var window in _uiWindows)
        {
            if (window.Id == windowName)
            {
                Debug.Log($"Showing window: {windowName}");
                window.Show();
                break;
            }
            else
            {
                Debug.LogError("Window not found: " + windowName);
            }
        }
    }

    public void HideWindow(string windowName)
    {
        foreach (var window in _uiWindows)
        {
            if (window.Id == windowName)
            {
                Debug.Log($"Hiding window: {windowName}");
                window.Hide();
                break;
            }
            else
            {
                Debug.LogError("Window not found: " + windowName);
            }
        }
    }
}
