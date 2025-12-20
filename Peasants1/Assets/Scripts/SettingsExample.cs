using UnityEngine;

/// <summary>
/// Example script demonstrating how to use the SettingsManager to read and update settings.
/// This can be attached to UI elements like sliders, dropdowns, etc.
/// </summary>
public class SettingsExample : MonoBehaviour
{
    [Header("UI References (Example)")]
    [SerializeField] private UnityEngine.UI.Slider masterVolumeSlider;
    [SerializeField] private UnityEngine.UI.Slider musicVolumeSlider;
    [SerializeField] private UnityEngine.UI.Dropdown qualityDropdown;

    private void Start()
    {
        // Load current settings and populate UI
        LoadSettingsToUI();
    }

    /// <summary>
    /// Example: Load settings and update UI elements
    /// </summary>
    private void LoadSettingsToUI()
    {
        GameSettings settings = SettingsManager.Instance.CurrentSettings;

        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = settings.masterVolume;
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = settings.musicVolume;
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        if (qualityDropdown != null)
        {
            qualityDropdown.value = settings.qualityLevel;
            qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        }
    }

    /// <summary>
    /// Example: Update a single setting value
    /// </summary>
    public void OnMasterVolumeChanged(float value)
    {
        GameSettings settings = SettingsManager.Instance.CurrentSettings;
        settings.masterVolume = value;
        SettingsManager.Instance.UpdateSettings(settings);
    }

    public void OnMusicVolumeChanged(float value)
    {
        GameSettings settings = SettingsManager.Instance.CurrentSettings;
        settings.musicVolume = value;
        SettingsManager.Instance.UpdateSettings(settings);
    }

    public void OnQualityChanged(int qualityIndex)
    {
        GameSettings settings = SettingsManager.Instance.CurrentSettings;
        settings.qualityLevel = qualityIndex;
        SettingsManager.Instance.UpdateSettings(settings);
    }

    /// <summary>
    /// Example: Reset all settings to defaults
    /// </summary>
    public void ResetSettings()
    {
        SettingsManager.Instance.ResetToDefaults();
        LoadSettingsToUI(); // Refresh UI with default values
    }

    /// <summary>
    /// Example: Get current settings without modifying them
    /// </summary>
    public void DisplayCurrentSettings()
    {
        GameSettings settings = SettingsManager.Instance.CurrentSettings;
        Debug.Log($"Master Volume: {settings.masterVolume}");
        Debug.Log($"Music Volume: {settings.musicVolume}");
        Debug.Log($"Quality Level: {settings.qualityLevel}");
        Debug.Log($"Resolution: {settings.resolutionWidth}x{settings.resolutionHeight}");
        Debug.Log($"Fullscreen: {settings.fullscreen}");
    }
}
