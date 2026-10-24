using Dino.Utility.Audio;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Dino.Utility.Audio.AudioType;


public class SettingsUI : UIWindow
{
    [SerializeField] private Toggle toggleAudio;

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

    private void OnVolumeMusicChanged(float volume)
    {
       AudioManager.Instance.UpdateAudioMixerGroupVolume(volume,AudioType.Music);
    }

    private void OnVolumeSFXChanged(float volume)
    {
        AudioManager.Instance.UpdateAudioMixerGroupVolume(volume, AudioType.SFX);
    }

    public override void Initialize()
    {
        base.Initialize();
        toggleAudio.onValueChanged.AddListener(OnToggleChange);
        OnToggleChange(toggleAudio.isOn);

    }

    public void OnToggleChange(bool value)
    {
        if (value)
        {
            OnVolumeMusicChanged(1f);
        }
        else
        {
            OnVolumeMusicChanged(0f);
        }
    }


}