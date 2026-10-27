using System;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform gameBackground;
    public Vector3 tableta;
    public Vector3 mobile;
    public void Start()
    {
        ResponsiveManager.Instance.OnScreenSizeChanged.AddListener(ResizeBackground);
        ResizeBackground();

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
