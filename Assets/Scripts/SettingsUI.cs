using Dino.Utility.Audio;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Dino.Utility.Audio.AudioType;

public class SettingsUI : UIWindow
{
    [SerializeField] private Toggle toggleAudio;

    private const string AudioEnabledKey = "AudioEnabled";
    private const float AudioOnVolume = 0.8f;
    private const float AudioOffVolume = 0f;

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

    public override void Initialize()
    {
        base.Initialize();

        bool audioEnabled = PlayerPrefs.GetInt(AudioEnabledKey, 1) == 1;

        toggleAudio.SetIsOnWithoutNotify(audioEnabled);

        ApplyAudioSetting(audioEnabled);

        toggleAudio.onValueChanged.AddListener(OnToggleChange);
    }

    public void OnToggleChange(bool value)
    {
        ApplyAudioSetting(value);

        if (value)
        {
            PlayerPrefs.SetInt(AudioEnabledKey, 1);
        }
        else
        {
            PlayerPrefs.SetInt(AudioEnabledKey, 0);
        }

        PlayerPrefs.Save();
    }

    private void ApplyAudioSetting(bool value)
    {
        float volume;

        if (value)
        {
            volume = AudioOnVolume;
        }
        else
        {
            volume = AudioOffVolume;
        }

        AudioManager.Instance.UpdateAudioMixerGroupVolume(volume, AudioType.Music);
        AudioManager.Instance.UpdateAudioMixerGroupVolume(volume, AudioType.SFX);
    }

    public void ReturnToMainMenu()
    {
        UIManager.Instance.HideWindow("settingsui");
        UIManager.Instance.ShowWindow("mainmenuui");
    }
}